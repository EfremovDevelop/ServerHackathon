﻿using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Enums;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.Core.Interfaces.Services;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Application.Services;

public class EventsService : IEventsService
{
    private readonly IEventsRepository _eventsRepository;
    private readonly IEventParticipantRepository _eventParticipantRepository;

    public EventsService(IEventsRepository eventsRepository, IEventParticipantRepository eventParticipantRepository)
    {
        _eventsRepository = eventsRepository;
        _eventParticipantRepository = eventParticipantRepository;
    }

    public async Task<Guid> Create(EventDto newEvent, Guid userId)
    {
        bool isEventExists = await _eventsRepository.CheckEventExists(newEvent.Place.Id, newEvent.Date);
        // нужно добавить проверку на пересечение с бронированием коворкингов имеющих тип ивента
        if (isEventExists)
        {
            throw new InvalidOperationException("Мероприятие для указанного места и даты уже существует.");
        }

        bool isEventPlaceExist = await _eventsRepository.CheckEventPlaceExists(PlaceTypeEnum.Event.ToString(), newEvent.Place.Id);

        if (!isEventPlaceExist)
        {
            throw new InvalidOperationException("Мероприятия нельзя проводить в данном месте");
        }

        var eventEntity = new Event
        {
            Id = Guid.NewGuid(),
            Name = newEvent.Name,
            Thumbnail = "http://202.181.148.90:8080/static/uploads/events/EJLlpZJWsAIb-hh.jpg",
            Description = newEvent.Description,
            Date = newEvent.Date,
            PlaceId = newEvent.Place.Id,
            StatusId = newEvent.Status.Id
        };

        Guid eventId = await _eventsRepository.Create(eventEntity);
        if (eventId != Guid.Empty && userId != Guid.Empty)
        {
            await _eventParticipantRepository.AddParticipant(userId, eventId);
        }
        return eventId;
    }

    public async Task<List<EventDto>> GetEvents(Guid userId, int? universityId = null, DateTime? startDate = null)
    {
        var events = await _eventsRepository.GetEvents(universityId, startDate);

        var eventDtos = events
            .OrderBy(e => e.Date)
            .Select(e => new EventDto(e)
            {
                isParticipant = e.Users.Any(u => u.Id == userId)
            })
            .ToList();

        return eventDtos;
    }

    public async Task<Guid> Update(EventDto updatedEvent, Guid userId)
    {
        var eventEntity = await _eventsRepository.GetEvent(updatedEvent.Id);
        if(eventEntity == null){
            throw new InvalidOperationException("Не найдено мероприятие с данным id");
        }
        if(updatedEvent.Name != null)
                eventEntity.Name = updatedEvent.Name;
        if(updatedEvent.Date.Year != 0001) //Костыль
        {
            eventEntity.Date = updatedEvent.Date;
        }
        if(updatedEvent.Thumbnail != null && updatedEvent.Thumbnail.Length >0)
            eventEntity.Thumbnail = updatedEvent.Thumbnail;

        bool isEventExists = await _eventsRepository.CheckEventExists(eventEntity.PlaceId, eventEntity.Date, eventEntity.Id);
        // нужно добавить проверку на пересечение с бронированием коворкингов имеющих тип ивента
        if (isEventExists)
        {
            throw new InvalidOperationException("Мероприятие для указанного места и даты уже существует.");
        }

        Guid eventId = await _eventsRepository.Update(eventEntity);
        return eventId;
    }
}
