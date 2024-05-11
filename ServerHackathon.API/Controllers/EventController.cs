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
        private readonly IWebHostEnvironment _env;
        public EventController(IEventsService eventsService, UsersService usersService, IWebHostEnvironment env)
        {
            _eventService = eventsService;
            _usersService = usersService;
            _env = env;
        }

        [Authorize]
        [HttpPost]
        public async Task<IResult> Create([FromForm] EventRequest eventRequest)
        {
            var userId = GetUserId();

            if (userId == Guid.Empty)
                return Results.Unauthorized();
            bool checkUser = await _usersService.CheckUserById(userId);
            if (checkUser == false)
                return Results.Unauthorized();

            //Thumbnail Validation
            string thumbnail = "";
            if (eventRequest.thumbnail != null && eventRequest.thumbnail.Length > 0)
            {
                string path = _env.WebRootPath + "\\uploads\\events\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);

                }
                using (FileStream fileStream = System.IO.File.Create(path + eventRequest.thumbnail.FileName))
                {
                    eventRequest.thumbnail.CopyTo(fileStream);
                    fileStream.Flush();
                    path = "\\static\\uploads\\events\\" + eventRequest.thumbnail.FileName;
                    var baseUri = $"{Request.Scheme}://{Request.Host}";
                    thumbnail = baseUri+path.Replace("\\", "/");
                }
            }

            DateTime date = DateTime.SpecifyKind(eventRequest.Date, DateTimeKind.Utc);
            var eventDto = new EventDto();
            eventDto.Name = eventRequest.Name;
            eventDto.Date = date;
            eventDto.Place = new PlaceDto { Id = eventRequest.placeId };
            eventDto.Status = new EventStatusDto { Id = eventRequest.statusId };
            if(thumbnail.Length >0){
                eventDto.Thumbnail = thumbnail;
            }

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