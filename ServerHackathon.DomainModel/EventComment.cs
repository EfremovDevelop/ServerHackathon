namespace ServerHackathon.DomainModel;

public class EventComment
{
    public int Id { get; set; }

    public string Text { get; set; }

    public DateTime CreatedAt {  get; set; }

    public Guid UserId { get; set; }

    public Guid EventId { get; set; }

    public virtual User User { get; set; }

    public virtual Event Event { get; set; }
}