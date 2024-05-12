using ServerHackathon.Core.DtoModels;

namespace ServerHackathon.Core.Interfaces.Services;

public interface IUniversitiesService
{
    Task<List<UniversityDto>> GetUniversities();
}
