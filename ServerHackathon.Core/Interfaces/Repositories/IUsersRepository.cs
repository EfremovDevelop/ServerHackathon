using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Repositories;

public interface IUsersRepository
{
    Task<Guid> Create(User user);
    Task<User?> GetByLogin(string email);
    Task<bool> CheckById(Guid id);
}
