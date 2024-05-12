using ServerHackathon.API.Contracts.Events;
using ServerHackathon.Core.DtoModels;

namespace ServerHackathon.API.Contracts.Users
{
    public record UsersResponse(Guid Id, string Name, string Surname, string Login, int GenderId,
        string? Phone, string? Email, string? ProfileImageUrl);

    public record UsersWithEventsResponse(Guid Id, string Name, string Surname, string Login, GenderDto Gender,
        string? Phone, string? Email, string? ProfileImageUrl, ICollection<EventsResponse> Events);

    public record UsersTokenResponse(Guid Id, string Name, string Surname, string Login, GenderDto Gender,
        string? Phone, string? Email, UniversityDto University, string? ProfileImageUrl,
        string? card, string token);
}
