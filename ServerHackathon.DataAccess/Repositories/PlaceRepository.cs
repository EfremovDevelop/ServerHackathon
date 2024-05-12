using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.DomainModel;

namespace ServerHackathon.DataAccess.Repositories
{
    public class PlaceRepository :IPlaceRepository
    {
        private readonly DataContext _context;
        public PlaceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Place?> GetPlace(int id)
        {
            return await _context.Place.FindAsync(id);
        }
    }
}