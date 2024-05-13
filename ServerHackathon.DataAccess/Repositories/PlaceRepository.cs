using Microsoft.EntityFrameworkCore;
using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Enums;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.DomainModel;
using System.Linq;

namespace ServerHackathon.DataAccess.Repositories
{
    public class PlaceRepository : IPlaceRepository
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

        public async Task<int> CreatePlace(Place place)
        {
            await _context.Place.AddAsync(place);
            await _context.SaveChangesAsync();
            return place.Id;
        }

        public async Task<List<Place>> GetByTypePlaces(PlaceTypeEnum placeTypeEnum)
        {
            return await _context.Place
                .Where(place => place.Types.Any(t => t.Id == (int)placeTypeEnum))
                .ToListAsync();
        }

        public async Task UpdatePlace(Place place)
        {
            await _context.Place
                .Where(i => i.Id == place.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.Name, p => place.Name)
                    .SetProperty(p => p.Description, p => place.Description)
                    .SetProperty(p => p.Adress, p => place.Adress)
                    .SetProperty(p => p.Location, p => place.Location)
                    .SetProperty(p => p.minuteStep, p => place.minuteStep)
                    .SetProperty(p => p.UniversityId, p => place.UniversityId)
                    .SetProperty(p => p.WorkFrom, p => place.WorkFrom)
                    .SetProperty(p => p.WorkTo, p => place.WorkTo));
            await _context.SaveChangesAsync();
        }

        public async Task AddTypeToPlace(int placeId, int typeId)
        {
            var placeType = new PlaceTypeList
            {
                PlaceId = placeId,
                PlaceTypeId = typeId
            };
            await _context.PlaceTypeList.AddAsync(placeType);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Place>> GetAllPlaces()
        {
            return await _context.Place
                .AsNoTracking()
                .Include(t => t.Types)
                .Include(u => u.University)
                .ToListAsync();
        }
    }
}