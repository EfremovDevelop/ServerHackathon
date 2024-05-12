using Microsoft.AspNetCore.Mvc;
using ServerHackathon.API.Contracts;
using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Services;

namespace ServerHackathon.API.Controllers
{
    [Route("api/place")]
    [ApiController]
    public class PlaceController : BaseController
    {
        private readonly IPlaceService _placeService;
        public PlaceController(IPlaceService placeService)
        {
            _placeService = placeService;
        }
        [HttpGet]
        public async Task<ActionResult<List<BookingSlotDto>>> GetAvailableSlots([FromBody] PlaceRequest placeRequest)
        {
            var slots = await _placeService.GetSlots(placeRequest.placeId, placeRequest.data);
            return Ok(slots);
        }
    }
}