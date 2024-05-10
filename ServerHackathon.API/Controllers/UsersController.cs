using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerHackathon.API.Contracts.Users;
using ServerHackathon.Application.Services;
using ServerHackathon.Core.DtoModels;

namespace ServerHackathon.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    private readonly UsersService _usersService;
    public static IWebHostEnvironment  _webHostEnvironment;

    public UsersController(UsersService usersService, IWebHostEnvironment webHostEnvironment)
    {
        _usersService = usersService;
        _webHostEnvironment = webHostEnvironment;
        if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
        {
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "wwwroot");
        }
    }

    [HttpPost("register")]
    public async Task<IResult> Register([FromBody] UsersRequest usersRequest)
    {
        var userDto = new UserDto();
        userDto.Name = usersRequest.Name;
        userDto.Surname = usersRequest.Surname;
        userDto.Login = usersRequest.Login;
        userDto.Password = usersRequest.Password;
        userDto.GenderId = usersRequest.GenderId;
        userDto.UniversityId = usersRequest.UniversityId;

        var id = await _usersService.Register(userDto);
        if (id == null)
        {
            return Results.BadRequest("Failed to register user.");
        }
        return Results.Ok();
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IResult> UpdateUser([FromForm] UsersUpdateRequest userRequest)
    {
        //Avatar Validation
        string profileImageUrl = "";
        if (userRequest.formFile != null && userRequest.formFile.Length > 0)
        {
            string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }
            using (FileStream fileStream = System.IO.File.Create(path + userRequest.formFile.FileName))
            {
                userRequest.formFile.CopyTo(fileStream);
                fileStream.Flush();
                profileImageUrl = "\\static\\uploads\\" + userRequest.formFile.FileName;
            }
        }
        else
        {
            return Results.BadRequest("Not Loaded Image");
        }

        Guid userId = GetUserId();
        if (userId == Guid.Empty)
        {
            return Results.NotFound();
        }
        return Results.Ok(); //надо будет доделать обновление пользователя
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
        var baseUri = $"{Request.Scheme}://{Request.Host}";
        var path = baseUri+user.ProfileImageUrl?.Replace("\\", "/");

        var response = new UsersLoginResponse(user.Id, user.Name, user.Surname, user.Login,
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

    // GET api/<controller>/5
    [HttpGet("static")]
    public IActionResult Get(string id)
    {
        
        var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", id);
        try
        {
            var imageFileStream = System.IO.File.OpenRead(path);
            return File(imageFileStream, "image/jpeg");
            
        }
        catch (System.Exception)
        {
            return NotFound();
        }
    }
}