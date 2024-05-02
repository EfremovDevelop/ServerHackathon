namespace ServerHackathon.Core.Interfaces.Auth
{
    public interface IPasswordHash
    {
        string Generate(string password);
        bool Verify(string password, string passwordHash);
    }
}
