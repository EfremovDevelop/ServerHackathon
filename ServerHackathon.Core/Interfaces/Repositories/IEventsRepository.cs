using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Repositories;

public interface IEventsRepository
{
    Task<Guid> Create(Event newEvent);
    Task<List<Event>> GetEvents(int? universityId = null, DateTime? startDate = null);
}