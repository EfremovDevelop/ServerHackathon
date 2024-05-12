using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.Interfaces.Repositories
{
    public interface IPlaceRepository
    {
        Task<Place?> GetPlace(int id);
    }
}