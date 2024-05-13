using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Repositories;

public interface IEventCommentRepository
{
    Task AddComment(EventComment comment);
    Task<List<EventComment>> GetEventComments(Guid eventId);
}