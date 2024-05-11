using Microsoft.AspNetCore.Mvc;
using ServerHackathon.API.Contracts.Users;
using ServerHackathon.Application.Services;
using ServerHackathon.Core.DtoModels;

namespace ServerHackathon.API.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController: BaseController
    {
        private readonly EventsService _eventService;
        public EventController(EventsService eventsService)
        {
            _eventService = eventsService;
        }

        [HttpPost("create")]
        public async Task<IResult> Create([FromBody] EventRequest eventRequest)
        {
            var eventDto = new EventDto();
            eventDto.Name = eventRequest.Name;
            eventDto.Date = eventRequest.Date;
            eventDto.Place = new PlaceDto{Id = eventRequest.placeId};
            eventDto.Status = new EventStatusDto{Id = eventRequest.statusId};

            var id = await _eventService.Create(eventDto);
            if(id == null)
            {
                return Results.BadRequest("Failed to create event");
            }

            return Results.Ok();
        } 
    }
}