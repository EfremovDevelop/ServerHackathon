using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerHackathon.API.Contracts.Universities;
using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Services;

namespace ServerHackathon.API.Controllers
{
    [Route("api/universities")]
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private IUniversitiesService _universitiesService;
        public UniversitiesController(IUniversitiesService universitiesService)
        {
            _universitiesService = universitiesService;
        }
        [HttpGet]
        public async Task<ActionResult<UniversityDto>> GetListUniversity()
        {
            var universities = await _universitiesService.GetUniversities();

            var response = universities.Select(u => new UniversitiesForRegisterResponse(u.Id, u.Initials));
            return Ok(response);
        }
    }
}
