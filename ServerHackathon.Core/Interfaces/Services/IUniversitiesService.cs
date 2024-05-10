using ServerHackathon.Core.DtoModels;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Services;

public interface IUniversitiesService
{
    Task<List<UniversityDto>> GetUniversities();
}
