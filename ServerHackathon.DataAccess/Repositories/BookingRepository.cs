using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
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

        public async Task<bool> CheckBookingExists(int placeId, DateTime checkIn, DateTime checkOut)
        {
            return await _context.Booking.AnyAsync(e => e.PlaceId == placeId && (checkIn >= e.CheckIn && checkOut > checkIn || checkIn> e.CheckIn && checkOut>checkIn && checkOut <=e.CheckIn));
        }

        public async Task<bool> CheckBookingPlaceExists(int placeId, DateTime checkIn, DateTime checkOut)
        {
            var place = await _context.Event
			.FirstOrDefaultAsync(p => p.PlaceId == placeId && p.Date > checkIn && p.Date < checkOut);

            if (place == null)
                return false;
            else
                return true;
        }

        public async Task<Guid> Create(Booking booking)
        {
            await _context.Booking.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking.Id;
        }

        public async Task<List<Booking>> GetAllBookingFromDay(int placeId, DateTime day)
        {
            IQueryable<Booking> query = _context.Booking;
		    query = query.Where(b => b.PlaceId >= placeId && b.CheckIn.Date == day.Date);

            List<Booking> events = await query
                .Include(p => p.Place)
                .ToListAsync();
		    return events;
        }
    }
}