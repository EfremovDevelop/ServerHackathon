using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.Core.Interfaces.Services;

namespace ServerHackathon.Application.Services;

public class UniversitiesService : IUniversitiesService
{
    private readonly IUniversitiesRepository _universityRepository;

    public UniversitiesService(IUniversitiesRepository universityRepository)
    {
        _universityRepository = universityRepository;
    }

    public async Task<List<UniversityDto>> GetUniversities()
    {
        var universities = await _universityRepository.GetUniversities();
        var universityDtos = universities
            .Select(u => new UniversityDto(u)).ToList();
        return universityDtos;
    }
}
