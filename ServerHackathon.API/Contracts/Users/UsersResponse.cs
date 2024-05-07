using ServerHackathon.Core.DtoModels;

namespace ServerHackathon.API.Contracts.Users
{
    public record UsersResponse(Guid? Id, string Name, string Surname, string Login, int GenderId,
        string? Phone, string? Email);
    public record UsersTokenResponse(Guid? Id, string Name, string Surname, string Login, int GenderId,
        string? Phone, string? Email, string token);
}
