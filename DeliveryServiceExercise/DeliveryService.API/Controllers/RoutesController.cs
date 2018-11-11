using System.Linq;
using System.Threading.Tasks;
using DeliveryService.Database;
using DeliveryService.Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteRepository _repository;

        public RoutesController(IRouteRepository routeRepository)
        {
            _repository = routeRepository;
        }

        private async Task<bool> RouteExistsAsync(int locationA, int locationB)
        {
            return await _repository.Exists(locationA, locationB);
        }

        [HttpGet]
        public IActionResult GetRoute()
        {
            var data = _repository.GetAll();

            return Ok(data);
        }

        [HttpGet("{locationA}/{locationB}", Name = "GetRoute")]
        public async Task<IActionResult> GetRoute([FromRoute] int locationA, [FromRoute] int locationB)
        {
            if(await RouteExistsAsync(locationA, locationB))
            {
                var data = await _repository.Find(locationA, locationB);
                return Ok(data);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostRoute([FromBody] Route route)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.Add(route);

            return CreatedAtAction("Route", new { locationA = route.LocationA, locationB = route.LocationB }, route);
        }

        [HttpPut("{locationA}/{locationB}")]
        public async Task<IActionResult> PutRoute([FromRoute] int locationA, [FromRoute] int locationB, [FromBody] Route route)
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

            await _repository.Update(route);

            return Ok(route);
        }

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
