using ServerHackathon.Core.DtoModels;

namespace ServerHackathon.Core.Interfaces.Services
{
    public interface IEventStatusService
    {
        Task<List<EventStatusDto>> GetStatuses();
    }
}