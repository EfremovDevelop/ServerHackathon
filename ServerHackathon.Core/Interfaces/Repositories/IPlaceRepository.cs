using ServerHackathon.Core.Enums;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Repositories
{
    public interface IPlaceRepository
    {
        Task<Place?> GetPlace(int id);
        Task CreatePlace(Place place);
        Task UpdatePlace(Place place);
        Task<List<Place>> GetByTypePlaces(PlaceTypeEnum placeTypeEnum);
    }
}