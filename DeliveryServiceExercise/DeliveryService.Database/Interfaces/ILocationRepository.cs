using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryService.Database.Interfaces
{
    public interface ILocationRepository
    {
        Task<Location> Add(Location location);

        Task<IEnumerable<Location>> GetAll();

        Task<Location> Find(int id);

        Task<Location> Update(Location location);

        Task<Location> Remove(int id);

        Task<bool> Exists(int id);
    }
}
