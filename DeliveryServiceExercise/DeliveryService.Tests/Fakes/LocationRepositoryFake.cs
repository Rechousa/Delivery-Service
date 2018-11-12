using DeliveryService.Database;
using DeliveryService.Database.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Tests
{
    public class LocationRepositoryFake : ILocationRepository
    {
        private readonly List<Location> _locations;

        public LocationRepositoryFake()
        {
            _locations = new List<Location>()
            {
                new Location { Id = 1, Name = "Lisboa"},
                new Location { Id = 2, Name = "Madrid"},
                new Location { Id = 3, Name = "London"},
                new Location { Id = 4, Name = "Paris"},
                new Location { Id = 5, Name = "New York"},
            };
        }

        public Task<Location> Add(Location location)
        {
            location.Id = _locations.Max(t => t.Id) + 1;
            _locations.Add(location);
            return Task.Run(() => location);
        }

        public Task<bool> Exists(int id)
        {
            return Task.Run(() => _locations.Any(t => t.Id == id));
        }

        public Task<Location> Find(int id)
        {
            return Task.Run(() => _locations.SingleOrDefault(t => t.Id == id));
        }

        public Task<IEnumerable<Location>> GetAll()
        {
            return Task.Run(() => _locations.AsEnumerable());
        }

        public Task<Location> Remove(int id)
        {
            var locationInDB = _locations.Single(t => t.Id == id);
            _locations.Remove(locationInDB);

            return Task.Run(() => locationInDB);
        }

        public Task<Location> Update(Location location)
        {
            var locationInDB = _locations.Single(t => t.Id == location.Id);
            locationInDB = location;

            return Task.Run(() => locationInDB);
        }
    }
}
