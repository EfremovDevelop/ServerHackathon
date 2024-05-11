using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerHackathon.API.Contracts.Users
{
    public record EventRequest(string Name, DateTime Date, int placeId, int statusId, IFormFile? thumbnail);
    public record EventUpdateRequest(string? Name, DateTime? Date, IFormFile? thumbnail, Guid eventId);
}