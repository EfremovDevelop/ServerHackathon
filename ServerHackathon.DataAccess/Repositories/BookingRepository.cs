using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.DomainModel;

namespace ServerHackathon.DataAccess.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _context;
        public BookingRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Guid> Create(Booking booking)
        {
            await _context.Booking.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking.Id;
        }
    }
}