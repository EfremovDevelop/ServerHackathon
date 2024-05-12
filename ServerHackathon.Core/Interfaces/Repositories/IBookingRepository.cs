using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        Task<Guid> Create(Booking booking);
        Task<bool> CheckBookingExists(int placeId, DateTime checkIn, DateTime checkOut);
        Task<bool> CheckBookingPlaceExists(int placeId, DateTime checkIn, DateTime checkOut);
    }
}