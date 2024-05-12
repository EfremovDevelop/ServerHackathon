using ServerHackathon.Core.DtoModels;

namespace ServerHackathon.API.Contracts.Events
{
    public record EventsResponse(Guid Id, string Name, string? thumbnail, string? Description,
        DateTime date, int placeId, EventStatusDto status);
}
