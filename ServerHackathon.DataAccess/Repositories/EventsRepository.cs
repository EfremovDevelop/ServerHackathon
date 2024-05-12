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
			startDate = DateTime.Today.ToUniversalTime();
		}
		query = query.Where(e => e.Date >= startDate);

		if (universityId != null)
		{
			query = query.Where(e => e.Place.UniversityId == universityId);
		}

		List<Event> events = await query
			.Include(p => p.Place)
			.ThenInclude(u => u.University)
			.Include(s => s.Status)
			.Include(e => e.Place.Types)
			.Include(u => u.Users)
			.ToListAsync();
		return events;
	}

	public async Task<bool> CheckEventExists(int placeId, DateTime date)
	{
		// Проверяем, есть ли мероприятие для указанного места и даты
		return await _context.Event.AnyAsync(e => e.PlaceId == placeId && e.Date.Date == date.Date);
	}

    public async Task<bool> CheckEventExists(int placeId, DateTime date, Guid eventId)
	{
		// Проверяем, есть ли мероприятие для указанного места и даты
		return await _context.Event.AnyAsync(e => e.PlaceId == placeId && e.Date.Date == date.Date && e.Id != eventId);
	}

	public async Task<bool> CheckEventPlaceExists(string typeName, int placeId)
	{
		var place = await _context.Place
			.Include(p => p.Types)
			.FirstOrDefaultAsync(p => p.Id == placeId);

		if (place == null)
		{
			return false;
		}
		var disallowedType = place.Types.FirstOrDefault(t => t.Name == typeName);

		return disallowedType != null;
	}
    

    public async Task<Guid> Update(Event updatedEvent)
    {
        await _context.Event
        .Where(i => i.Id == updatedEvent.Id)
        .ExecuteUpdateAsync(s => s
            .SetProperty(p => p.Name, p => updatedEvent.Name)
            .SetProperty(p => p.Thumbnail, p => updatedEvent.Thumbnail)
            .SetProperty(p => p.Description, p => updatedEvent.Description)
            .SetProperty(p=>p.Date, p=> updatedEvent.Date)
            .SetProperty(p => p.PlaceId, p=> updatedEvent.PlaceId)
            .SetProperty(p => p.StatusId, p => updatedEvent.StatusId));
        await _context.SaveChangesAsync();
        return updatedEvent.Id;
    }

    public async Task<Event?> GetEvent(Guid id)
    {
        return await _context.Event.FindAsync(id);
    }

    public async Task<List<Event>?> GetAllEventsFromDay(int placeId, DateTime day)
    {
		IQueryable<Event> query = _context.Event;
		query = query.Where(e => e.PlaceId >= placeId && e.Date.Date == day.Date );

        List<Event> events = await query
			.Include(p => p.Place)
			// .Include(p=>p.Date)
			.ToListAsync();
		return events;
    }
}
