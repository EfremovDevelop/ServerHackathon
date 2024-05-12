using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.DtoModels;

public class EventDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string? Thumbnail { get; set; }

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    public int PlaceId { get; set; }

    public PlaceDto Place { get; set; }

    public EventStatusDto Status { get; set; }

    //public ICollection<UserDto> Users { get; set; }

    public EventDto() { }
    public EventDto(Event currEvent)
    {
        Id = currEvent.Id;
        Name = currEvent.Name;
        Thumbnail = currEvent.Thumbnail;
        Description = currEvent.Description;
        Date = currEvent.Date;
        PlaceId = currEvent.PlaceId;
        Place = new PlaceDto(currEvent.Place);
        Status = new EventStatusDto(currEvent.Status);
    }
}
