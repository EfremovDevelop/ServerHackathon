using Microsoft.AspNetCore.Mvc;
using ServerHackathon.API.Contracts;
using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Services;
using ServerHackathon.DomainModel;

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

            int placeId = await _placeService.CreatePlace(placeDto);

            // ��������� ��������� ���� � �����
            foreach (var typeId in request.TypeIds)
            {
                await _placeService.AddTypeToPlace(placeId, typeId);
            }

            return Results.Ok();
        }

        [HttpPut]
        public async Task<IResult> UpdatePlace([FromBody] PlaceUpdateRequest request)
        {
            var place = await _placeService.GetPlace(request.Id);
            if (place == null)
                return Results.BadRequest();
            var placeModel = new Place
            {
                Id = request.Id,
                Name = request.Name != null ? request.Name : place.Name,
                Adress = request.Adress != null ? request.Adress : place.Adress,
                Location = request.Location != null ? request.Location : place.Location,
                Description = request.Description != null ? request.Description : place.Description,
                Capacity = request.Capacity != null ? request.Capacity : place.Capacity,
                WorkFrom = request.WorkFrom != null ? request.WorkFrom : place.WorkFrom,
                WorkTo = request.WorkTo != null ? request.WorkTo : place.WorkTo,
                minuteStep = (int)(request.minuteStep != null ? request.minuteStep : place.minuteStep),
                UniversityId = (int)(request.UniversityId != null ? request.UniversityId : place.University.Id)
            };
            await _placeService.UpdatePlace(placeModel);
            return Results.Ok();
        }
    }
}