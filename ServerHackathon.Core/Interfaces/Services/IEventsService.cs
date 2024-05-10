using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Services;

public interface IEventsService
{
    Task<Guid> Create(Event newEvent);
    Task<List<Event>> GetEvents(int? universityId = null, DateTime? startDate = null);
}
