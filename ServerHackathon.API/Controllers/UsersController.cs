using Microsoft.AspNetCore.Authorization;
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
            userDto.Login = usersRequest.Login;
            userDto.Password = usersRequest.Password;
            userDto.GenderId = usersRequest.GenderId;
            userDto.Phone = usersRequest.Phone;

            var id = await _usersService.Register(userDto);
            if (id == null)
            {
                return Results.BadRequest("Failed to register user.");
            }
            return Results.Ok();
        }

        [HttpGet]
        public async Task<ActionResult<UsersResponse>> GetUserByLogin([FromQuery] string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                return BadRequest("Email address is required.");
            }

            var user = await _usersService.GetUserByLogin(login);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var response = new UsersResponse(user.Id, user.Name, user.Surname, user.Login,
                user.GenderId, user.Phone, user.Email);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsersResponse>> Login(LoginUserRequest request)
        {
            var token = await _usersService.Login(request.Login, request.Password);

            var user = await _usersService.GetUserByLogin(request.Login);

            var response = new UsersTokenResponse(user.Id, user.Name, user.Surname,
                user.Login, user.GenderId, user.Phone, user.Email, token);

            return Ok(response);
        }

        [HttpGet("verify")]
        [Authorize]
        public ActionResult<string> GetSecureResource()
        {
            return Ok();
        }
    }
}
