namespace ServerHackathon.DomainModel;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string Login {  get; set; }

    public string Password { get; set; }

    public int GenderId { get; set; }

    public virtual Gender Gender { get; set; }

    public string? ProfileImageUrl { get; set; }

    public int UniversityId { get; set; }

    public virtual University University { get; set; }

    public string? Card {  get; set; }

    public virtual ICollection<Event> Events { get; set; } = [];

    public virtual ICollection<Booking> Bookings { get; set; } = [];
}
