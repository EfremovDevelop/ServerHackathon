using Microsoft.EntityFrameworkCore;
using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.DomainModel;

namespace ServerHackathon.DataAccess.Repositories;

public class EventParticipantRepository : IEventParticipantRepository
{
    private readonly DataContext _context;

    public EventParticipantRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> AddParticipant(Guid userId, Guid eventId)
    {
        var participant = new EventParticipant
        {
            EventId = eventId,
            UserId = userId,
        };
        await _context.EventParticipant.AddAsync(participant);
        await _context.SaveChangesAsync();
        return participant.Id;
    }

    public async Task<List<UserDto>> GetEventPartisipants(Guid eventId)
    {
        var participants = await _context.Event
        .Where(e => e.Id == eventId)
        .SelectMany(e => e.Users)
        .Include(g => g.Gender)
        .Include(u => u.University)
        .Select(u => new UserDto(u))
        .ToListAsync();

        return participants;
    }

    public async Task DeleteParticipant(Guid userId, Guid eventId)
    {
        await _context.EventParticipant
            .Where(e => e.UserId == userId && e.EventId == eventId)
            .ExecuteDeleteAsync();
    }

    public async Task<bool> CheckRegisterParticipant(Guid userId, Guid eventId)
    {
        var participant =  await _context.EventParticipant
            .FirstOrDefaultAsync(p => p.UserId == userId && p.EventId == eventId);

        return participant != null;
    }
}
