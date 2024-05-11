using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.Core.Interfaces.Services;

namespace ServerHackathon.Application.Services;

public class EventStatusService : IEventStatusService
{
    private readonly IEventStatusRepository _eventStatusRepository;

    public EventStatusService(IEventStatusRepository eventStatusRepository)
    {
        _eventStatusRepository = eventStatusRepository;
    }

    public async Task<List<EventStatusDto>> GetStatuses()
    {
        var eventStatuses = await _eventStatusRepository.GetStatuses();

        return eventStatuses.Select(e => new EventStatusDto(e)).ToList();
    }
}
