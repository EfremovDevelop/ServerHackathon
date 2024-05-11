using Microsoft.EntityFrameworkCore;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.DomainModel;

namespace ServerHackathon.DataAccess.Repositories;

public class EventStatusRepository : IEventStatusRepository
{
    private readonly DataContext _context;

    public EventStatusRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<EventStatus>> GetStatuses()
    {
        return await _context.EventStatus.ToListAsync();
    }
}
