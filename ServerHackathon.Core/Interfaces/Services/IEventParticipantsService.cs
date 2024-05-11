using ServerHackathon.Core.DtoModels;

namespace ServerHackathon.Core.Interfaces.Services;

public interface IEventParticipantsService
{
    Task<List<UserDto>> GetEventParticipants(Guid eventId);
}