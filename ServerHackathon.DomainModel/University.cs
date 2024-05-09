namespace ServerHackathon.DomainModel;

public class University
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Initials { get; set; }

    public string? Logo { get; set; }

    public string? Description { get; set; }

    public ICollection<User> Users { get; set; } = [];
}