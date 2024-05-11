using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Services;

namespace ServerHackathon.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventParticipantsController : ControllerBase
{
    private readonly IEventParticipantsService _eventParticipantsService;

    public EventParticipantsController(IEventParticipantsService eventParticipantsService)
    {
        _eventParticipantsService = eventParticipantsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetEventParticipants(Guid eventId)
    {
        return await _eventParticipantsService.GetEventParticipants(eventId);
    }
}