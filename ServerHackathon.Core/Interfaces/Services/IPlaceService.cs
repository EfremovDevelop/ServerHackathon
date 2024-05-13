using ServerHackathon.Core.DtoModels;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Services
{
    public interface IPlaceService
    {
        Task<List<BookingAvaliableSlotsDto>> GetSlots(DateTime day);
        Task CreatePlace(PlaceDto place);
    }
}