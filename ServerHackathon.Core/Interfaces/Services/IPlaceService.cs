using ServerHackathon.Core.DtoModels;

namespace ServerHackathon.Core.Interfaces.Services
{
    public interface IPlaceService
    {
        Task<List<BookingSlotDto>> GetSlots(int placeId, DateTime day);
    }
}