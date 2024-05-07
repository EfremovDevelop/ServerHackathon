using ServerHackathon.Core.Interfaces.Auth;

namespace ServerHackathon.Infrastructure.Auth;

public class PasswordHash : IPasswordHash
{
    public string Generate(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool Verify(string password, string passwordHash) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
}
