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

    public async Task<int> AddParticipant(Guid userId, Guid eventId)
    {
        return await _eventParticipantRepository.AddParticipant(userId, eventId);
    }

    public async Task DeleteParticipant(Guid userId, Guid eventId)
    {
        await _eventParticipantRepository.DeleteParticipant(userId, eventId);
    }

    public async Task<bool> CheckRegisterParticipant(Guid userId, Guid eventId)
    {
        return await _eventParticipantRepository.CheckRegisterParticipant(userId, eventId);
    }
}
