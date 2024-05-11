using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Repositories;

public interface IEventStatusRepository
{
    Task<List<EventStatus>> GetStatuses();
}