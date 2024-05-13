using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Enums;
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
        public async Task<List<BookingAvaliableSlotsDto>> GetSlots(DateTime day)
        {
            //Проверка, основаня логика поиска =(
            var listPlaces = await _placeRepository.GetByTypePlaces(PlaceTypeEnum.Сoworking);
            // Получаем все ивенты и букинги для placeId
            var slots = new List<BookingAvaliableSlotsDto>();
            foreach (var item in listPlaces)
            {
                var listEvents = new List<Event>();
                var listBookings = new List<Booking>();
                listEvents.AddRange(await _eventsRepository.GetAllEventsFromDay(item.Id,day));
                listBookings.AddRange(await _bookingRepository.GetAllBookingFromDay(item.Id,day));
                List<BookingSlotDto> busySlots = new List<BookingSlotDto>();
                listEvents.ForEach(e=> busySlots.Add(new BookingSlotDto{from = new DateTime(e.Date.Year, e.Date.Month, e.Date.Day, 00, 00, 00),
                    to = new DateTime(e.Date.Year, e.Date.Month, e.Date.Day, 23, 59, 59)}));
                listBookings.ForEach(e=> busySlots.Add(new BookingSlotDto{from = e.CheckIn, to = e.CheckOut}));

                var timeFrom = (DateTime)item.WorkFrom;
                var timeTo = (DateTime)item.WorkFrom;
                
                var workTo = (DateTime)item.WorkTo;
                var slot = new BookingAvaliableSlotsDto{place = item, bookingSlot = new List<BookingSlotDto>()};
                do
                {
                    timeTo = timeTo.AddMinutes(item.minuteStep);
                    if(timeTo.TimeOfDay > workTo.TimeOfDay)
                        timeTo = workTo;
                    if(!busySlots.Any(e=> timeFrom.TimeOfDay < e.to.TimeOfDay && e.from.TimeOfDay < timeTo.TimeOfDay))
                    {
                        slot.bookingSlot.Add(new BookingSlotDto{from = timeFrom, to = timeTo});
                    }
                    timeFrom = timeFrom.AddMinutes(item.minuteStep);
                    if(timeFrom.TimeOfDay >= workTo.TimeOfDay)
                    break;
                } while (timeFrom.TimeOfDay<=workTo.TimeOfDay);
                slots.Add(slot);
            }
            return slots;
        }

        public async Task CreatePlace(PlaceDto placeDto)
        {
            var place = new Place
            {
                Id = placeDto.Id,
                Name = placeDto.Name,
                Adress = placeDto.Adress,
                Location = placeDto.Location,
                Description = placeDto.Description,
                Capacity = placeDto.Capacity,
                WorkFrom = placeDto.WorkFrom,
                WorkTo = placeDto.WorkTo,
                minuteStep = placeDto.minuteStep,
                UniversityId = placeDto.University.Id
            };
            await _placeRepository.CreatePlace(place);
        }

        //public async Task<bool> UpdatePlace(PlaceDto placeDto)
        //{
        //    var place = await _placeRepository.GetPlace(placeDto.Id);

        //    if (place == null)
        //        return false;
        //    if (placeDto.Name != null)
        //        place.Name = placeDto.Name;
        //    if (placeDto.Description != null)
        //        place.Description = placeDto.Description;
        //    if (placeDto.Capacity != null)
        //        place.Capacity = placeDto.Capacity;
        //}
    }
}