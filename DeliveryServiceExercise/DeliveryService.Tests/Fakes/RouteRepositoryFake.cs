using DeliveryService.Database;
using DeliveryService.Database.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Tests
{
    public class RouteRepositoryFake : IRouteRepository
    {
        private readonly List<Route> _routes;

        public RouteRepositoryFake()
        {
            _routes = new List<Route>()
            {
                new Route { Id = 1, LocationA = 1, LocationB = 2, Distance = 800, Cost = 300 },
                new Route { Id = 2, LocationA = 1, LocationB = 3, Distance = 3000, Cost = 800 },
                new Route { Id = 3, LocationA = 1, LocationB = 4, Distance = 1800, Cost = 410 },
                new Route { Id = 4, LocationA = 1, LocationB = 5, Distance = 6000, Cost = 920 },
            };
        }

        public Task<Route> Add(Route route)
        {
            route.Id = _routes.Max(t => t.Id) + 1;
            _routes.Add(route);
            return Task.Run(() => route);
        }

        public Task<bool> Exists(int id)
        {
            return Task.Run(() => _routes.Any(t => t.Id == id));
        }

        public Task<Route> Find(int id)
        {
            return Task.Run(() => _routes.SingleOrDefault(t => t.Id == id));
        }

        public IEnumerable<Route> GetAll()
        {
            return _routes;
        }

        public Task<Route> Remove(int id)
        {
            var routeInDB = _routes.Single(t => t.Id == id);
            _routes.Remove(routeInDB);

            return Task.Run(() => routeInDB);
        }

        public Task<Route> Update(Route route)
        {
            var routeInDB = _routes.Single(t => t.Id == route.Id);
            routeInDB = route;

            return Task.Run(() => routeInDB);
        }
    }
}
