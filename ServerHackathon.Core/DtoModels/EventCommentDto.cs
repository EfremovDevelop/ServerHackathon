using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.DtoModels;

public class EventCommentDto
{
    public int Id { get; set; }

    public string Text { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid EventId { get; set; }

    public virtual UserDto User { get; set; }

    public EventCommentDto() { }

    public EventCommentDto(EventComment eventComment)
    {
        Id = eventComment.Id;
        Text = eventComment.Text;
        CreatedAt = eventComment.CreatedAt;
        EventId = eventComment.EventId;
        User = new UserDto {Id = eventComment.UserId, Login = eventComment.User.Login };
    }
}
