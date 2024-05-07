using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.DtoModels;

public class UserDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string? Phone { get; set; }

    public int Points { get; set; }

    public string? Email { get; set; }

    public string Login {  get; set; }

    public string Password { get; set; }

    public int GenderId { get; set; }

    public string? ProfileImageUrl { get; set; }

    public UserDto() { }

    public UserDto(User user)
    {
        Id = user.Id;
        Name = user.Name;
        Surname = user.Surname;
        Phone = user.Phone;
        Points = user.Points;
        Email = user.Email;
        Login = user.Login;
        Password = user.Password;
        GenderId = user.GenderId;
        ProfileImageUrl = user.ProfileImageUrl;
    }
}
