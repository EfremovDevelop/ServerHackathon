namespace ServerHackathon.DomainModel;

public class Event
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string? Thumbnail { get; set; }

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    public int PlaceId { get; set; }

    public virtual Place Place { get; set; }

    public int StatusId { get; set; }

    public virtual EventStatus Status { get; set; }

    public ICollection<User> Users { get; set; } = [];
}
