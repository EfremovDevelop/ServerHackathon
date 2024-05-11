using ServerHackathon.Core.DtoModels;

namespace ServerHackathon.Core.Interfaces.Services
{
    public interface IBookingService
    {
        Task<Guid> Create(BookingDto newBooking, Guid userId);
        Task<List<EventDto>> GetEvents(Guid userId);
    }
}