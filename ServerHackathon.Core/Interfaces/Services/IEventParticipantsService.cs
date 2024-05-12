using ServerHackathon.Core.DtoModels;

namespace ServerHackathon.Core.Interfaces.Services;

public interface IEventParticipantsService
{
    Task<List<UserDto>> GetEventParticipants(Guid eventId);
    Task<int> AddParticipant(Guid userId, Guid eventId);
    Task DeleteParticipant(Guid userId, Guid eventId);
    Task<bool> CheckRegisterParticipant(Guid userId, Guid eventId);
}