using ServerHackathon.Core.DtoModels;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Services
{
    public interface IPlaceService
    {
        Task<List<BookingAvaliableSlotsDto>> GetSlots(DateTime day);
        Task<int> CreatePlace(PlaceDto place);
        Task AddTypeToPlace(int placeId, int typeId);
        Task<PlaceDto?> GetPlace(int placeId);
        Task UpdatePlace(Place place);
    }
}