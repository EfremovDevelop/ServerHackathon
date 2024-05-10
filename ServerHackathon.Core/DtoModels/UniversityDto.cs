using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.DtoModels;

public class UniversityDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Initials { get; set; }

    public string? Logo { get; set; }

    public string? Description { get; set; }

    public UniversityDto() { }
    public UniversityDto(University university)
    {
        Id = university.Id;
        Name = university.Name;
        Initials = university.Initials;
        Logo = university.Logo;
        Description = university.Description;
    }
}
