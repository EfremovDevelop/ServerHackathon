using ServerHackathon.Core.DtoModels;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Repositories;

public interface IEventParticipantRepository
{
    Task<int> AddParticipant(Guid userId, Guid eventId);
    Task<List<UserDto>> GetEventPartisipants(Guid eventId);
}