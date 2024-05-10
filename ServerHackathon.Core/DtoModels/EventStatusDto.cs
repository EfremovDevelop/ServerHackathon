using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.DtoModels;

public class EventStatusDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public EventStatusDto() { }
    public EventStatusDto(EventStatus eventStatus)
    {
        Id = eventStatus.Id;
        Name = eventStatus.Name;
    }
}
