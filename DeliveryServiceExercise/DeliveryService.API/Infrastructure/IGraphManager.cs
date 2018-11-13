using System.Threading.Tasks;

namespace DeliveryService.API.Infrastructure
{
    public interface IGraphManager
    {
        void SyncDatabase();
    }
}
