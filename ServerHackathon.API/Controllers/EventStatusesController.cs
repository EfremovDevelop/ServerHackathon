using Microsoft.AspNetCore.Mvc;
using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Services;

namespace ServerHackathon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventStatusesController : ControllerBase
    {
        private readonly IEventStatusService _eventStatusService;

        public EventStatusesController(IEventStatusService eventStatusService)
        {
            _eventStatusService = eventStatusService;
        }

        [HttpGet]
        public async Task<ActionResult<EventStatusDto>> GetEventStatuses()
        {
            var events = await _eventStatusService.GetStatuses();

            return Ok(events);
        }
    }
}
