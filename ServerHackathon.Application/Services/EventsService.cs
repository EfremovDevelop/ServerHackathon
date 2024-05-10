using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.Core.Interfaces.Services;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Application.Services;

public class EventsService : IEventsService
{
    private readonly IEventsRepository _eventsRepository;

    public EventsService(IEventsRepository eventsRepository)
    {
        _eventsRepository = eventsRepository;
    }

    public Task<Guid> Create(Event newEvent)
    {
        throw new NotImplementedException();
    }

    public Task<List<Event>> GetEvents(int? universityId = null, DateTime? startDate = null)
    {
        throw new NotImplementedException();
    }
}
