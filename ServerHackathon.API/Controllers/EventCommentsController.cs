using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerHackathon.API.Contracts.EventComment;
using ServerHackathon.Application.Services;
using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Services;

namespace ServerHackathon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCommentsController : BaseController
    {
        public readonly IEventCommentService _eventCommentService;
        public readonly UsersService _usersService;

        public EventCommentsController(IEventCommentService eventCommentService, UsersService usersService)
        {
            _eventCommentService = eventCommentService;
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<EventCommentResponse>> GetEventComments(Guid eventId)
        {
            var eventComments = await _eventCommentService.GetEventComments(eventId);
            
            var response = eventComments
                .Select(c => new EventCommentResponse(
                    c.Text, c.CreatedAt, c.User.Login)).ToList();
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<EventCommentResponse>> AddEventComment([FromBody] EventCommentRequest request)
        {
            var userId = GetUserId();

            if (userId == Guid.Empty)
                return Unauthorized();
            bool checkUser = await _usersService.CheckUserById(userId);
            if (checkUser == false)
                return Unauthorized();

            var comment = new EventCommentDto
            {
                User = new UserDto() { Id = userId },
                EventId = request.eventId,
                Text = request.Text
            };

            await _eventCommentService.AddComment(comment);

            // Получение комментариев снова после добавления
            var eventComments = await _eventCommentService.GetEventComments(comment.EventId);

            var response = eventComments
                .Select(c => new EventCommentResponse(
                    c.Text, c.CreatedAt, c.User.Login)).ToList();

            return Ok(response);
        }
    }
}
