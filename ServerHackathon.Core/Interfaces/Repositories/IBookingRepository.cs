using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        Task<Guid> Create(Booking booking);
    }
}