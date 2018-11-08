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

        private async Task<bool> RouteExistsAsync(int id)
        {
            return await _repository.Exists(id);
        }

        [HttpGet]
        public IActionResult GetRoute()
        {
            var data = _repository.GetAll();

            return Ok(data);
        }

        [HttpGet("{id}", Name = "GetRoute")]
        public async Task<IActionResult> GetRoute([FromRoute] int id)
        {
            if(await RouteExistsAsync(id))
            {
                var data = await _repository.Find(id);
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

            return CreatedAtAction("Route", new { id = route.Id }, route);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoute([FromRoute] int id, [FromBody] Route route)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (route.Id != id)
            {
                return BadRequest();
            }

            if(!await RouteExistsAsync(id))
            {
                return NotFound();
            }

            await _repository.Update(route);

            return Ok(route);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute([FromRoute] int id)
        {
            if (!await RouteExistsAsync(id))
            {
                return NotFound();
            }

            await _repository.Remove(id);

            return Ok();
        }
    }
}
