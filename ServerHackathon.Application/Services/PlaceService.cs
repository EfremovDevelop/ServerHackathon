using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.Core.Interfaces.Services;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Application.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IPlaceRepository _placeRepository;
        public PlaceService(IEventsRepository eventsRepository, IBookingRepository bookingRepository, IPlaceRepository placeRepository)
        {
            _eventsRepository = eventsRepository;
            _bookingRepository = bookingRepository;
            _placeRepository = placeRepository;
        }
        public async Task<List<BookingSlotDto>> GetSlots(int placeId, DateTime day)
        {
            //Проверка, основаня логика поиска =(
            var place = await _placeRepository.GetPlace(placeId);
            if(place == null)
            {
                return null;
            }
            // Получаем все ивенты и букинги для placeId
            var listEvents = await _eventsRepository.GetAllEventsFromDay(placeId, day);
            var listBookings = await _bookingRepository.GetAllBookingFromDay(placeId, day);
            List<BookingSlotDto> busySlots = new List<BookingSlotDto>();
            
            // формируем список из DateTime
            listEvents.ForEach(e=> busySlots.Add(new BookingSlotDto{from = e.Date, to = e.Date.AddMinutes(place.minuteStep)}));
            listBookings.ForEach(e=> busySlots.Add(new BookingSlotDto{from = e.CheckIn, to = e.CheckOut}));
            if(place.WorkFrom ==null || place.WorkTo ==null){
                return null;
            }
            var timeFrom = (DateTime)place.WorkFrom;
            var timeTo = (DateTime)place.WorkFrom;

            var workTo = (DateTime)place.WorkTo;
            var slots = new List<BookingSlotDto>();
            do
            {
                timeTo = timeTo.AddMinutes(place.minuteStep);
                if(!busySlots.Any(e=> timeFrom.TimeOfDay < e.to.TimeOfDay && e.from.TimeOfDay < timeTo.TimeOfDay))
                {
                    slots.Add(new BookingSlotDto{from = timeFrom, to = timeTo});
                }
                timeFrom = timeFrom.AddMinutes(place.minuteStep);
                if(timeFrom.TimeOfDay == workTo.TimeOfDay) //TODO: Сделать проверку на невозможность выхода за режим работы
                break;
            } while (timeFrom.TimeOfDay<=workTo.TimeOfDay);
            return slots;
        }
    }
}