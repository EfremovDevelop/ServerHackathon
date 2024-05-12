using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerHackathon.Application.Services;
using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Services;

namespace ServerHackathon.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventParticipantsController : BaseController
{
    private readonly IEventParticipantsService _eventParticipantsService;
    private readonly UsersService _usersService;

    public EventParticipantsController(IEventParticipantsService eventParticipantsService, UsersService usersService)
    {
        _eventParticipantsService = eventParticipantsService;
        _usersService = usersService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetEventParticipants(Guid eventId)
    {
        return await _eventParticipantsService.GetEventParticipants(eventId);
    }

    [Authorize]
    [HttpPost]
    public async Task<IResult> AddEventParticipant(Guid eventId)
    {
        var userId = GetUserId();

        if (userId == Guid.Empty)
            return Results.Unauthorized();
        bool checkUser = await _usersService.CheckUserById(userId);
        if (checkUser == false)
            return Results.Unauthorized();
        if (await _eventParticipantsService.CheckRegisterParticipant(userId, eventId))
            return Results.BadRequest();

        await _eventParticipantsService.AddParticipant(userId, eventId);
        return Results.Ok();
    }

    [Authorize]
    [HttpDelete]
    public async Task<IResult> DeleteParticipant(Guid eventId)
    {
        var userId = GetUserId();

        if (userId == Guid.Empty)
            return Results.Unauthorized();
        bool checkUser = await _usersService.CheckUserById(userId);
        if (checkUser == false)
            return Results.Unauthorized();

        if (!await _eventParticipantsService.CheckRegisterParticipant(userId, eventId))
            return Results.BadRequest();

        await _eventParticipantsService.DeleteParticipant(userId, eventId);
        return Results.Ok();
    }
}