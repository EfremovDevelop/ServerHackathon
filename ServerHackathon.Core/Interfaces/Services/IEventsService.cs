using ServerHackathon.Core.DtoModels;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Services;

public interface IEventsService
{
    Task<Guid> Create(EventDto newEvent, Guid userId);
    Task<List<EventDto>> GetEvents(int? universityId = null, DateTime? startDate = null);
    Task<Guid> Update(EventDto updateEvent, Guid userId);
}
