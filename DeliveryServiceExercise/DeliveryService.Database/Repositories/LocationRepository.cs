using DeliveryService.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryService.Database.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DeliveryServiceDbContext _context;

        public LocationRepository(DeliveryServiceDbContext context)
        {
            _context = context;
        }

        public async Task<Location> Add(Location location)
        {
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Locations.AnyAsync(c => c.Id == id);
        }

        public async Task<Location> Find(int id)
        {
            return await _context.Locations.SingleOrDefaultAsync(t => t.Id == id);
        }

        public IEnumerable<Location> GetAll()
        {
            return _context.Locations;
        }

        public async Task<Location> Remove(int id)
        {
            var location = await _context.Locations.SingleOrDefaultAsync(t => t.Id == id);
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<Location> Update(Location location)
        {
            _context.Locations.Update(location);
            await _context.SaveChangesAsync();
            return location;
        }
    }
}
