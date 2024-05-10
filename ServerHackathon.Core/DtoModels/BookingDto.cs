using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.DtoModels;

public class BookingDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public UserDto User { get; set; }

    public DateTime CheckIn { get; set; }

    public DateTime CheckOut { get; set; }

    public bool Status { get; set; } = true;

    public PlaceDto Place { get; set; }

    public BookingDto() { }
    public BookingDto(Booking booking)
    {
        Id = booking.Id;
        User = new UserDto(booking.User);
        CheckIn = booking.CheckIn;
        CheckOut = booking.CheckOut;
        Status = booking.Status;
        Place = new PlaceDto(booking.Place);
    }
}
