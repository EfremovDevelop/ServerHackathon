using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Auth

{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}