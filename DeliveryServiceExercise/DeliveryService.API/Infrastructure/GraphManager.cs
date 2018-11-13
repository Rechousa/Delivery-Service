using DeliveryService.API.Settings;
using DeliveryService.Common;
using DeliveryService.Database.Interfaces;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DeliveryService.API.Infrastructure
{
    public class GraphManager : IGraphManager
    {
        private ILocationRepository _locationRepository;
        private IRouteRepository _routeRepository;
        private AppSettings _appSettings;

        public GraphManager(ILocationRepository locationRepository,
            IRouteRepository routeRepository,
            IOptions<AppSettings> appSettings)
        {
            _locationRepository = locationRepository;
            _routeRepository = routeRepository;
            _appSettings = appSettings.Value;
        }

        public void SyncDatabase()
        {
            using (var client = new DeliveryServiceNeo4jClient(_appSettings.GraphDatabaseEndpoint,
                _appSettings.GraphDatabaseLogin,
                _appSettings.GraphDatabasePassword))
            {
                client.DeleteAllNodes();

                foreach (var location in _locationRepository.GetAll().Result)
                {
                    client.InsertNode(location.Id, location.Name);
                }

                foreach (var route in _routeRepository.GetAll().Result)
                {
                    client.ConnectNodes(route.LocationA, route.LocationB, route.Distance, route.Cost);
                }
            }
        }
    }
}
