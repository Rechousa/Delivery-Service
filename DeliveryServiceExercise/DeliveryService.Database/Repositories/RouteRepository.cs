using DeliveryService.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryService.Database.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly DeliveryServiceDbContext _context;

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

        public async Task<bool> Exists(int id)
        {
            return await _context.Routes.AnyAsync(c => c.Id == id);
        }

        public async Task<Route> Find(int id)
        {
            return await _context.Routes.SingleOrDefaultAsync(t => t.Id == id);
        }

        public IEnumerable<Route> GetAll()
        {
            return _context.Routes;
        }

        public async Task<Route> Remove(int id)
        {
            var route = await _context.Routes.SingleOrDefaultAsync(t => t.Id == id);
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
