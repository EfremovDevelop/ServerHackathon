using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Repositories;

public interface IEventsRepository
{
    Task<Guid> Create(Event newEvent);
    Task<List<Event>> GetEvents(int? universityId = null, DateTime? startDate = null);
    Task<List<Event>> GetAllEventsFromDay(int placeId, DateTime day);
    Task<bool> CheckEventExists(int placeId, DateTime date);
    Task<bool> CheckEventExists(int placeId, DateTime date, Guid eventId);
    Task<bool> CheckEventPlaceExists(string eventName, int placeId);

    Task<Guid> Update(Event updatedEvent);
    Task<Event?> GetEvent(Guid id);
}