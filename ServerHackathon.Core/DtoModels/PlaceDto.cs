using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.DtoModels;

public class PlaceDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Adress { get; set; }

    public string Location { get; set; }

    public string? Description { get; set; }

    public int? Capacity { get; set; }

    public bool isBlocked { get; set; } = false;

    public UniversityDto University { get; set; }

    public PlaceDto() { }
    public PlaceDto(Place place)
    {
        Id = place.Id;
        Name = place.Name;
        Adress = place.Adress;
        Location = place.Location;
        Description = place.Description;
        Capacity = place.Capacity;
        isBlocked = place.isBlocked;
        University = new UniversityDto(place.University);
    }
}
