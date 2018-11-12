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
    public class RoutesController : DeliveryServiceBaseController
    {
        private readonly IRouteRepository _repository;

        public RoutesController(IRouteRepository routeRepository,
            IDistributedCache cache,
            IOptions<AppSettings> appSettings) : base(cache, appSettings)
        {
            _repository = routeRepository;
        }

        private async Task<bool> RouteExistsAsync(int locationA, int locationB)
        {
            return await _repository.Exists(locationA, locationB);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoutes()
        {
            var cacheContent = GetCachedItem<List<RouteVM>>(ApplicationConstants.Redis_Routes_Cache_Key);
            if (cacheContent != null)
            {
                return Ok(cacheContent);
            }

            var data = await _repository.GetAll();
            var vm = data.Select(t => RouteVM.FromRoute(t)).ToList();

            CacheItem(ApplicationConstants.Redis_Routes_Cache_Key, vm);

            return Ok(vm);
        }

        [HttpGet("{locationA}/{locationB}", Name = "GetRoute")]
        public async Task<IActionResult> GetRoute([FromRoute] int locationA, [FromRoute] int locationB)
        {
            if(await RouteExistsAsync(locationA, locationB))
            {
                var data = await _repository.Find(locationA, locationB);
                var vm = RouteVM.FromRoute(data);
                return Ok(vm);
            }

            return NotFound();
        }

        [Authorize(Roles = ApplicationConstants.Role_Admin)]
        [HttpPost]
        public async Task<IActionResult> PostRoute([FromBody] RouteVM route)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.Add(RouteVM.ToRoute(route));

            return CreatedAtAction("GetRoute", new { locationA = route.LocationA, locationB = route.LocationB }, route);
        }

        [Authorize(Roles = ApplicationConstants.Role_Admin)]
        [HttpPut("{locationA}/{locationB}")]
        public async Task<IActionResult> PutRoute([FromRoute] int locationA, [FromRoute] int locationB, [FromBody] RouteVM route)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!((route.LocationA == locationA && route.LocationB == locationB) || (route.LocationA == locationB && route.LocationB == locationA)))
            {
                return BadRequest();
            }

            if(!await RouteExistsAsync(locationA, locationB))
            {
                return NotFound();
            }

            var routeInDb = await _repository.Find(locationA, locationB);

            await _repository.Update(RouteVM.UpdateRouteData(routeInDb, route));

            return Ok(route);
        }

        [Authorize(Roles = ApplicationConstants.Role_Admin)]
        [HttpDelete("{locationA}/{locationB}")]
        public async Task<IActionResult> DeleteRoute([FromRoute] int locationA, [FromRoute] int locationB)
        {
            if (!await RouteExistsAsync(locationA, locationB))
            {
                return NotFound();
            }

            await _repository.Remove(locationA, locationB);

            return Ok();
        }
    }
}
