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
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _repository;

        public LocationsController(ILocationRepository locationRepository)
        {
            _repository = locationRepository;
        }

        private async Task<bool> LocationExistsAsync(int id)
        {
            return await _repository.Exists(id);
        }

        [HttpGet]
        public IActionResult GetLocation()
        {
            var data = _repository.GetAll();

            return Ok(data);
        }

        [HttpGet("{id}", Name = "GetLocation")]
        public async Task<IActionResult> GetLocation([FromRoute] int id)
        {
            if (await LocationExistsAsync(id))
            {
                var data = await _repository.Find(id);
                return Ok(data);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostLocation([FromBody] Location location)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.Add(location);

            return CreatedAtAction("Location", new { id = location.Id }, location);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation([FromRoute] int id, [FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(location.Id != id)
            {
                return BadRequest();
            }

            if (!await LocationExistsAsync(id))
            {
                return NotFound();
            }

            await _repository.Update(location);

            return Ok(location);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] int id)
        {
            if (!await LocationExistsAsync(id))
            {
                return NotFound();
            }

            await _repository.Remove(id);

            return Ok();
        }
    }
}
