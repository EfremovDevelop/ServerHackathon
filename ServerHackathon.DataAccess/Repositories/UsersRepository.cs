using Microsoft.EntityFrameworkCore;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.DomainModel;

namespace ServerHackathon.DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly DataContext _context;
    public UsersRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Guid> Create(User user)
    {
        await _context.User.AddAsync(user);
        await _context.SaveChangesAsync();
        return user.Id;
    }

    public async Task<User?> GetByLogin(string login)
    {
        var user = await _context.User
            .Include(g => g.Gender)
            .Include(u => u.University)
            .Include(e => e.Events)
            .Include(b => b.Bookings)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Login == login);

        return user;
    }
}
