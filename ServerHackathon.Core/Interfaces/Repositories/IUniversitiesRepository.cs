using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Repositories;

public interface IUniversitiesRepository
{
    Task<List<University>> GetUniversities();
}