namespace ServerHackathon.API.Contracts.Users
{
    public record UsersRequest(string Name, string Surname, int GenderId,
        string Phone, string Email, string Password);
}
