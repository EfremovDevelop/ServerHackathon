using ServerHackathon.Core.DtoModels;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Services
{
    public interface IEventCommentService
    {
        Task AddComment(EventCommentDto commentDto);
        Task<List<EventCommentDto>> GetEventComments(Guid eventId);
    }
}