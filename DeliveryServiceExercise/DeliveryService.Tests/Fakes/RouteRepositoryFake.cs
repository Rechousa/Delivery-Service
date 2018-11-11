using DeliveryService.Database;
using DeliveryService.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Tests
{
    public class RouteRepositoryFake : IRouteRepository
    {
        private readonly List<Route> _routes;

        Func<Route, int, int, bool> searchPredicate = (Route route, int locationA, int locationB) => (route.LocationA == locationA && route.LocationB == locationB) || (route.LocationA == locationB && route.LocationB == locationA);

        public RouteRepositoryFake()
        {
            _routes = new List<Route>()
            {
                new Route { LocationA = 1, LocationB = 2, Distance = 800, Cost = 300 },
                new Route { LocationA = 1, LocationB = 3, Distance = 3000, Cost = 800 },
                new Route { LocationA = 1, LocationB = 4, Distance = 1800, Cost = 410 },
                new Route { LocationA = 1, LocationB = 5, Distance = 6000, Cost = 920 },
            };
        }

        public Task<Route> Add(Route route)
        {
            _routes.Add(route);
            return Task.Run(() => route);
        }

        public Task<bool> Exists(int locationA, int locationB)
        {
            return Task.Run(() => _routes.Any(t => searchPredicate(t, locationA, locationB)));
        }

        public Task<Route> Find(int locationA, int locationB)
        {
            return Task.Run(() => _routes.SingleOrDefault(t => searchPredicate(t, locationA, locationB)));
        }

        public IEnumerable<Route> GetAll()
        {
            return _routes;
        }

        public Task<Route> Remove(int locationA, int locationB)
        {
            var routeInDB = _routes.Single(t => searchPredicate(t, locationA, locationB));
            _routes.Remove(routeInDB);

            return Task.Run(() => routeInDB);
        }

        public Task<Route> Update(Route route)
        {
            var routeInDB = _routes.Single(t => searchPredicate(t, route.LocationA, route.LocationB));
            routeInDB = route;

            return Task.Run(() => routeInDB);
        }
    }
}
