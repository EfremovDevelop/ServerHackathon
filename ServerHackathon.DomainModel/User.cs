namespace ServerHackathon.DomainModel;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string? Phone { get; set; }

    public int Points { get; set; } = 0;

    public string? Email { get; set; }

    public string Login {  get; set; }

    public string Password { get; set; }

    public int GenderId { get; set; }

    public virtual Gender Gender { get; set; }

    public string? ProfileImageUrl { get; set; }
}