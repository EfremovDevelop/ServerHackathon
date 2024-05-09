namespace ServerHackathon.API.Contracts.Users
{
    public record UsersRequest(string Name, string Surname, string Login, int GenderId,
        string? Phone, string? Email, string Password, int UniversityId);

    public record UsersUpdateRequest(string? Phone, string? Email, IFormFile? formFile);
}
