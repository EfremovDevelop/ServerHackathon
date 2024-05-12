using ServerHackathon.Core.DtoModels;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Repositories;

public interface IEventParticipantRepository
{
    Task<int> AddParticipant(Guid userId, Guid eventId);
    Task<List<UserDto>> GetEventPartisipants(Guid eventId);
    Task DeleteParticipant(Guid userId, Guid eventId);
    Task<bool> CheckRegisterParticipant(Guid userId, Guid eventId);
}