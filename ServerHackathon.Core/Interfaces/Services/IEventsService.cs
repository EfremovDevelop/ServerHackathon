using ServerHackathon.Core.DtoModels;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Services;

public interface IEventsService
{
    Task<Guid> Create(EventDto newEvent);
    Task<List<EventDto>> GetEvents(int? universityId = null, DateTime? startDate = null);
}
