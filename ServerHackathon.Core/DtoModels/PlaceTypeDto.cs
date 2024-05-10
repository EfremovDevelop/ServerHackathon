using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.DtoModels;

public class PlaceTypeDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public PlaceTypeDto() { }

    public PlaceTypeDto(PlaceType placeType)
    {
        Id = placeType.Id;
        Name = placeType.Name;
    }
}
