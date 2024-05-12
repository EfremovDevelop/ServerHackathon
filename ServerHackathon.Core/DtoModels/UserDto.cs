using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.DtoModels;

public class UserDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string Login {  get; set; }

    public string Password { get; set; }

    public GenderDto Gender { get; set; }

    public string? ProfileImageUrl { get; set; }

    public UniversityDto University { get; set; }

    public string? Card {  get; set; }

    public ICollection<EventDto> Events { get; set; }

    public UserDto() { }

    public UserDto(User user)
    {
        Id = user.Id;
        Name = user.Name;
        Surname = user.Surname;
        Phone = user.Phone;
        Email = user.Email;
        Login = user.Login;
        Password = user.Password;
        Gender = new GenderDto(user.Gender);
        ProfileImageUrl = user.ProfileImageUrl;
        University = new UniversityDto(user.University);
    }
}
