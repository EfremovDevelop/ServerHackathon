using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.Core.Interfaces.Services;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Application.Services;

public class EventCommentService : IEventCommentService
{
    private readonly IEventCommentRepository _commentRepository;

    public EventCommentService(IEventCommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task AddComment(EventCommentDto commentDto)
    {
        var comment = new EventComment
        {
            UserId = commentDto.User.Id,
            EventId = commentDto.EventId,
            Text = commentDto.Text,
            CreatedAt = DateTime.UtcNow
        };
        await _commentRepository.AddComment(comment);
    }

    public async Task<List<EventCommentDto>> GetEventComments(Guid eventId)
    {
        var comments = await _commentRepository.GetEventComments(eventId);

        return comments.Select(c => new EventCommentDto(c)).ToList();
    }
}