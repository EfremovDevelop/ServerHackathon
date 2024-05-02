namespace ServerHackathon.DomainModel;

public class Gender
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; } = [];
}
