using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Services;

namespace ServerHackathon.Application.Services
{
    public class BookingService : IBookingService
    {
        public Task<Guid> Create(BookingDto newBooking, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<EventDto>> GetEvents(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}