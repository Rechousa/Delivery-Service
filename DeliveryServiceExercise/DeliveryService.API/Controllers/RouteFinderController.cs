using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryService.API.Infrastructure;
using DeliveryService.API.Settings;
using DeliveryService.Common;
using DeliveryService.Database;
using DeliveryService.Database.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace DeliveryService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteFinderController : DeliveryServiceBaseController
    {
        private readonly IRouteRepository _repository;
        private AppSettings _appSettings;

        public RouteFinderController(IRouteRepository routeRepository,
            IDistributedCache cache,
            IOptions<AppSettings> appSettings) : base(cache, appSettings)
        {
            _repository = routeRepository;
            _appSettings = appSettings.Value;
        }

        [HttpGet("cheapest/{locationA}/{locationB}")]
        public IActionResult FindCheapestRoute([FromRoute] int locationA, [FromRoute] int locationB)
        {
            using (var client = new DeliveryServiceNeo4jClient(_appSettings.GraphDatabaseEndpoint,
                _appSettings.GraphDatabaseLogin,
                _appSettings.GraphDatabasePassword))
            {
                var result = client.FindCheapestRoute(locationA, locationB);
                return Ok(result);
            }
        }

        [HttpGet("shortest/{locationA}/{locationB}")]
        public IActionResult FindShortestRoute([FromRoute] int locationA, [FromRoute] int locationB)
        {
            using (var client = new DeliveryServiceNeo4jClient(_appSettings.GraphDatabaseEndpoint,
                _appSettings.GraphDatabaseLogin,
                _appSettings.GraphDatabasePassword))
            {
                var result = client.FindShortestRoute(locationA, locationB);
                return Ok(result);
            }
        }

        private void Teste()
        {
        }
    }
}
