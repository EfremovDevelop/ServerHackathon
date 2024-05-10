using Microsoft.EntityFrameworkCore;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.DomainModel;

namespace ServerHackathon.DataAccess.Repositories;

public class UniversitiesRepository : IUniversitiesRepository
{
    private readonly DataContext _context;
    public UniversitiesRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<University>> GetUniversities()
    {
        return await _context.University.ToListAsync();
    }
}
