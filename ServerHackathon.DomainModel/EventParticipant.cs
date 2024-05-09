namespace ServerHackathon.DomainModel;

public class EventParticipant
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public Guid EventId { get; set; }
}
