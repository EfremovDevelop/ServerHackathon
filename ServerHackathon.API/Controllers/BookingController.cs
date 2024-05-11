using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerHackathon.API.Contracts;
using ServerHackathon.Application.Services;
using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Services;

namespace ServerHackathon.API.Controllers
{
    [Route("/api/booking")]
    [ApiController]
    public class BookingController: BaseController
    {
       private readonly UsersService _usersService;
       private readonly BookingService _bookingService;
       public BookingController(UsersService usersService, BookingService bookingService)
       {
            _usersService = usersService;
            _bookingService = bookingService;
       }

       [Authorize]
       [HttpPost]
       public async Task<IResult> Create([FromForm] BookingRequest bookingRequest)
        {
            var userId = GetUserId();

            if (userId == Guid.Empty)
                return Results.Unauthorized();
            bool checkUser = await _usersService.CheckUserById(userId);
            if (checkUser == false)
                return Results.Unauthorized();

            DateTime checkIn = DateTime.SpecifyKind(bookingRequest.CehckIn, DateTimeKind.Utc);
            DateTime checkOut = DateTime.SpecifyKind(bookingRequest.CehckOut, DateTimeKind.Utc);
            var bookingDto = new BookingDto();
            bookingDto.CheckIn = checkIn;
            bookingDto.CheckOut = checkOut;
            bookingDto.Place = new PlaceDto{Id = bookingRequest.placeId };
            bookingDto.User = new UserDto{Id = userId};

            await _bookingService.Create(bookingDto, userId);
            return Results.Ok();
        }

    }
}