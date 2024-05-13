using Microsoft.AspNetCore.Authorization;
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
            var slots = await _placeService.GetSlots(placeRequest.data);
            return Ok(slots);
        }
        [Authorize]
        [HttpPost]
        public async Task<IResult> CreatePlace([FromBody] PlaceCreateRequest request)
        {
            var placeDto = new PlaceDto
            {
                Name = request.Name,
                Adress = request.Adress,
                Location = request.Location,
                Description = request.Description,
                Capacity = request.Capacity,
                WorkFrom = request.WorkFrom,
                WorkTo = request.WorkTo,
                minuteStep = request.minuteStep,
                University = new UniversityDto { Id = request.UniversityId }
            };

            await _placeService.CreatePlace(placeDto);
            return Results.Ok();
            // �������� �����
        }

        //[Authorize]
        //[HttpPut]
        //public async Task<IResult> UpdatePlace([FromBody] PlaceUpdateRequest request)
        //{
        //    var placeDto = new PlaceDto
        //    {
        //        Id = request.Id,
        //        Name = request.Name,
        //        Adress = request.Adress,
        //        Location = request.Location,
        //        Description = request.Description,
        //        Capacity = request.Capacity,
        //        WorkFrom = request.WorkFrom,
        //        WorkTo = request.WorkTo,
        //        minuteStep = request.minuteStep,
        //        University = new UniversityDto { Id = request.UniversityId }
        //    };
        //}
    }
}