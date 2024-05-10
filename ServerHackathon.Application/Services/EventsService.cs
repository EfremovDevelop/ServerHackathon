using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Enums;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.Core.Interfaces.Services;
using ServerHackathon.DomainModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServerHackathon.Application.Services;

public class EventsService : IEventsService
{
    private readonly IEventsRepository _eventsRepository;

    public EventsService(IEventsRepository eventsRepository)
    {
        _eventsRepository = eventsRepository;
    }

    public async Task<Guid> Create(EventDto newEvent)
    {
        bool isEventExists = await _eventsRepository.CheckEventExists(newEvent.Place.Id, newEvent.Date);

        if (isEventExists)
        {
            throw new InvalidOperationException("Мероприятие для указанного места и даты уже существует.");
        }

        bool isEventPlaceExist = await _eventsRepository.CheckEventPlaceExists(PlaceTypeEnum.Event.ToString());

        if (isEventPlaceExist)
        {
            throw new InvalidOperationException("Мероприятия нельзя проводить в данном месте");
        }

        var eventEntity = new Event
        {
            Id = Guid.NewGuid(),
            Name = newEvent.Name,
            Thumbnail = newEvent.Thumbnail,
            Description = newEvent.Description,
            Date = newEvent.Date,
            PlaceId = newEvent.Place.Id,
            StatusId = newEvent.Status.Id
        };

        return await _eventsRepository.Create(eventEntity);
    }

    public async Task<List<EventDto>> GetEvents(int? universityId = null, DateTime? startDate = null)
    {
        var events = await _eventsRepository.GetEvents(universityId, startDate);

        return events.Select(e => new EventDto(e)).ToList();
    }
}
