using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryService.Database.Interfaces
{
    public interface IRouteRepository
    {
        Task<Route> Add(Route route);

        IEnumerable<Route> GetAll();

        Task<Route> Find(int locationA, int locationB);

        Task<Route> Update(Route route);

        Task<Route> Remove(int locationA, int locationB);

        Task<bool> Exists(int locationA, int locationB);
    }
}
