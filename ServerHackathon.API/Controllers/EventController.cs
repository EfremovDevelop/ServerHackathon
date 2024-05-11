using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerHackathon.API.Contracts.Users;
using ServerHackathon.Application.Services;
using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Services;

namespace ServerHackathon.API.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : BaseController
    {
        private readonly IEventsService _eventService;
        private readonly UsersService _usersService;
        public EventController(IEventsService eventsService, UsersService usersService)
        {
            _eventService = eventsService;
            _usersService = usersService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IResult> Create([FromBody] EventRequest eventRequest)
        {
            var userId = GetUserId();

            if (userId == Guid.Empty)
                return Results.Unauthorized();
            bool checkUser = await _usersService.CheckUserById(userId);
            if (checkUser == false)
                return Results.Unauthorized();

            DateTime date = DateTime.SpecifyKind(eventRequest.Date, DateTimeKind.Utc);
            var eventDto = new EventDto();
            eventDto.Name = eventRequest.Name;
            eventDto.Date = date;
            eventDto.Place = new PlaceDto { Id = eventRequest.placeId };
            eventDto.Status = new EventStatusDto { Id = eventRequest.statusId };

            await _eventService.Create(eventDto, userId);

            return Results.Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<EventDto>>> GetEvents(int? universityId = null, DateTime? startDate = null)
        {
            var events = await _eventService.GetEvents(universityId, startDate);
            return Ok(events);
        }
    }
}