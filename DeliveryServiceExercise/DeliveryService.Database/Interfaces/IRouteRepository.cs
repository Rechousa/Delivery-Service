using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryService.Database.Interfaces
{
    public interface IRouteRepository
    {
        Task<Route> Add(Route route);

        IEnumerable<Route> GetAll();

        Task<Route> Find(int id);

        Task<Route> Update(Route route);

        Task<Route> Remove(int id);

        Task<bool> Exists(int id);
    }
}
