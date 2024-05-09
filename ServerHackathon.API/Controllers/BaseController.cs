using Microsoft.AspNetCore.Mvc;
using ServerHackathon.Infrastructure.Auth;

namespace ServerHackathon.API.Controllers;

public class BaseController : ControllerBase
{
    protected Guid GetUserId()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId)?.Value;

        if (userId == null)
            return Guid.Empty;

        return Guid.Parse(userId);
    }
}
