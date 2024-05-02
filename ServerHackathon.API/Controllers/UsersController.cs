using Microsoft.AspNetCore.Mvc;
using ServerHackathon.API.Contracts.Users;
using ServerHackathon.Application.Services;
using ServerHackathon.Core.DtoModels;

namespace ServerHackathon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("register")]
        public async Task<IResult> Register([FromBody] UsersRequest usersRequest)
        {
            var userDto = new UserDto();
            userDto.Name = usersRequest.Name;
            userDto.Surname = usersRequest.Surname;
            userDto.Email = usersRequest.Email;
            userDto.Password = usersRequest.Password;
            userDto.GenderId = usersRequest.GenderId;
            userDto.Phone = usersRequest.Phone;

            await _usersService.Register(userDto);
            return Results.Ok();
        }

        [HttpGet]
        public async Task<ActionResult<UsersResponse>> GetUserByEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email address is required.");
            }

            var user = await _usersService.GetUserByEmail(email);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var response = new UsersResponse(user.Id, user.Name,
                user.Surname, user.GenderId, user.Phone, user.Email);

            return Ok(response);
        }
    }
}
