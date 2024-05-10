using Microsoft.AspNetCore.Mvc;
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
            return Ok(universities);
        }
    }
}
