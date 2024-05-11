using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.Core.Interfaces.Services;

namespace ServerHackathon.Application.Services;

public class EventParticipantsService : IEventParticipantsService
{
    private readonly IEventParticipantRepository _eventParticipantRepository;
    public EventParticipantsService(IEventParticipantRepository eventParticipantRepository)
    {
        _eventParticipantRepository = eventParticipantRepository;
    }

    public async Task<List<UserDto>> GetEventParticipants(Guid eventId)
    {
        return await _eventParticipantRepository.GetEventPartisipants(eventId);
    }
}
