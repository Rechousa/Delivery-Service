using System.Collections.Generic;
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
    public class LocationsController : DeliveryServiceBaseController
    {
        private readonly ILocationRepository _repository;

        public LocationsController(ILocationRepository locationRepository,
            IDistributedCache cache,
            IOptions<AppSettings> appSettings) : base(cache, appSettings)
        {
            _repository = locationRepository;
        }

        private async Task<bool> LocationExistsAsync(int id)
        {
            return await _repository.Exists(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetLocations()
        {
            var cacheContent = GetCachedItem<List<Location>>(ApplicationConstants.Redis_Locations_Cache_Key);
            if(cacheContent != null)
            {
                return Ok(cacheContent);
            }

            var data = await _repository.GetAll();
            CacheItem(ApplicationConstants.Redis_Locations_Cache_Key, data);

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

        [Authorize(Roles = ApplicationConstants.Role_Admin)]
        [HttpPost]
        public async Task<IActionResult> PostLocation([FromBody] LocationAddVM location)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationToBeAdded = LocationAddVM.ToLocation(location);
            await _repository.Add(locationToBeAdded);

            return CreatedAtAction("GetLocation", new { id = locationToBeAdded.Id }, locationToBeAdded);
        }

        [Authorize(Roles = ApplicationConstants.Role_Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation([FromRoute] int id, [FromBody] LocationEditVM location)
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

            var locationInDb = await _repository.Find(id);

            await _repository.Update(LocationEditVM.UpdateLocationData(locationInDb, location));

            return Ok(location);
        }

        [Authorize(Roles = ApplicationConstants.Role_Admin)]
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
