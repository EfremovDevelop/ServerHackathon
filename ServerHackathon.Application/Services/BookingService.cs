using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.Core.Interfaces.Services;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<Guid> Create(BookingDto newBooking, Guid userId)
        {
            //Проверка на занятость места для коворкинга
            bool isBookingExists = await _bookingRepository.CheckBookingExists(newBooking.Place.Id, newBooking.CheckIn, newBooking.CheckOut);
            if(isBookingExists)
            {
                throw new InvalidOperationException("Забронировать на уже заброннированную дату невозможно");
            }

            bool isBookingPlaceExists = await _bookingRepository.CheckBookingPlaceExists(newBooking.Place.Id, newBooking.CheckIn, newBooking.CheckOut);
            if(isBookingPlaceExists)
            {
                throw new InvalidOperationException("Забронировать невозможно, так на данное время запланировано мероприятие.");
            }
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                UserId = newBooking.User.Id,
                CheckIn = newBooking.CheckIn,
                CheckOut = newBooking.CheckOut,
                Status = newBooking.Status,
                CreatedAt = DateTime.UtcNow,
                PlaceId = newBooking.Place.Id,
            };
            Guid bookingId = await _bookingRepository.Create(booking);
            return bookingId;
        }

        public Task<List<EventDto>> GetEvents(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}