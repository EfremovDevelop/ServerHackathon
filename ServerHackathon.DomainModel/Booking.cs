namespace ServerHackathon.DomainModel;

public class Booking
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; }

    public DateTime CheckIn { get; set; }

    public DateTime CheckOut { get; set; }

    public bool Status { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int PlaceId { get; set; }

    public virtual Place Place { get; set; }
}