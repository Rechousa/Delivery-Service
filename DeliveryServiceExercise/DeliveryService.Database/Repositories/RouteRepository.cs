using DeliveryService.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryService.Database.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly DeliveryServiceDbContext _context;

        Func<Route, int, int, bool> searchPredicate = (Route route, int locationA, int locationB) => (route.LocationA == locationA && route.LocationB == locationB) || (route.LocationA == locationB && route.LocationB == locationA);

        public RouteRepository(DeliveryServiceDbContext context)
        {
            _context = context;
        }

        public async Task<Route> Add(Route route)
        {
            await _context.Routes.AddAsync(route);
            await _context.SaveChangesAsync();
            return route;
        }

        public async Task<bool> Exists(int locationA, int locationB)
        {
            return await _context.Routes.AnyAsync(t => searchPredicate(t, locationA, locationB));
        }

        public async Task<Route> Find(int locationA, int locationB)
        {
            return await _context.Routes.SingleOrDefaultAsync(t => searchPredicate(t, locationA, locationB));
        }

        public IEnumerable<Route> GetAll()
        {
            return _context.Routes;
        }

        public async Task<Route> Remove(int locationA, int locationB)
        {
            var route = await _context.Routes.SingleOrDefaultAsync(t => searchPredicate(t, locationA, locationB));
            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
            return route;
        }

        public async Task<Route> Update(Route route)
        {
            _context.Routes.Update(route);
            await _context.SaveChangesAsync();
            return route;
        }
    }
}
