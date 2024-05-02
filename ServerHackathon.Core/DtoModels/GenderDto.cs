using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.DtoModels;

public class GenderDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public GenderDto() { }
    public GenderDto(Gender gender)
    {
        Id = gender.Id;
        Name = gender.Name;
    }
}
