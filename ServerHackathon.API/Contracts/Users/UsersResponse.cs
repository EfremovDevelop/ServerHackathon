using ServerHackathon.Core.DtoModels;

namespace ServerHackathon.API.Contracts.Users
{
    public record UsersResponse(Guid Id, string Name, string Surname, int GenderId,
        string Phone, string Email);
}
