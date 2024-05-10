using Microsoft.EntityFrameworkCore;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.DomainModel;

namespace ServerHackathon.DataAccess.Repositories;

public class EventsRepository : IEventsRepository
{
    private readonly DataContext _context;
    public EventsRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Guid> Create(Event newEvent)
    {
        await _context.AddAsync(newEvent);
        await _context.SaveChangesAsync();
        return newEvent.Id;
    }

    public async Task<List<Event>> GetEvents(int? universityId = null, DateTime? startDate = null)
    {
        IQueryable<Event> query = _context.Event;

        if (startDate == null)
        {
            startDate = DateTime.Today;
        }
        query = query.Where(e => e.Date >= startDate);

        if (universityId != null)
        {
            query = query.Where(e => e.Place.UniversityId == universityId);
        }

        List<Event> events = await query.ToListAsync();
        return events;
    }
}
