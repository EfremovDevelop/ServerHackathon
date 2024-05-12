using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerHackathon.API.Contracts.Users;
using ServerHackathon.Application.Services;
using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Services;
using ServerHackathon.DomainModel;

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

            if (string.IsNullOrWhiteSpace(_env.WebRootPath))
            {
                _env.WebRootPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "wwwroot");
            }
            Console.WriteLine("env: WebRootPath = "+_env.WebRootPath);
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
                string path = _env.WebRootPath + "/uploads/events/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);

                }
                using (FileStream fileStream = System.IO.File.Create(path + eventRequest.thumbnail.FileName))
                {
                    eventRequest.thumbnail.CopyTo(fileStream);
                    fileStream.Flush();
                    path = "/static/uploads/events/" + eventRequest.thumbnail.FileName;
                    var baseUri = $"{Request.Scheme}://{Request.Host}";
                    // thumbnail = baseUri+path.Replace("\\", "/");
                    thumbnail = baseUri+path;
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

        [HttpPut("update")]
        [Authorize]
        public async Task<IResult> Update([FromForm] EventUpdateRequest eventUpdateRequest)
        {
            //Thumbnail Validation
            string thumbnail = "";
            if (eventUpdateRequest.thumbnail != null && eventUpdateRequest.thumbnail.Length > 0)
            {
                string path = _env.WebRootPath + "/uploads/events/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);

                }
                using (FileStream fileStream = System.IO.File.Create(path + eventUpdateRequest.thumbnail.FileName))
                {
                    eventUpdateRequest.thumbnail.CopyTo(fileStream);
                    fileStream.Flush();
                    path = "/static/uploads/events/" + eventUpdateRequest.thumbnail.FileName;
                    var baseUri = $"{Request.Scheme}://{Request.Host}";
                    // thumbnail = baseUri+path.Replace("\\", "/");
                }
            }

            Guid userId = GetUserId();
            if (userId == Guid.Empty)
            {
                return Results.NotFound();
            }

            var eventDto = new EventDto();
            eventDto.Id = eventUpdateRequest.eventId;
            eventDto.Thumbnail = thumbnail;
            if(eventUpdateRequest.Name != null)
                eventDto.Name = eventUpdateRequest.Name;
            if(eventUpdateRequest.Date != null)
            {
                DateTime date = DateTime.SpecifyKind((DateTime)eventUpdateRequest.Date, DateTimeKind.Utc);
                eventDto.Date = date;
            }

            var id = await _eventService.Update(eventDto, userId);
            return Results.Ok();
        }


    }
}