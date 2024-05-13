using Microsoft.EntityFrameworkCore;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.Core.Interfaces.Services;
using ServerHackathon.DomainModel;

namespace ServerHackathon.DataAccess.Repositories;

public class EventCommentRepository : IEventCommentRepository
{
    private readonly DataContext _context;

    public EventCommentRepository(DataContext context)
    {
        _context = context;
    }

    public async Task AddComment(EventComment comment)
    {
        await _context.EventComment.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<EventComment>> GetEventComments(Guid eventId)
    {
        return await _context.EventComment
            .AsNoTracking()
            .Include(u => u.User)
            .Where(e => e.EventId == eventId).ToListAsync();
    }
}
