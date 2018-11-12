using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;

namespace DeliveryService.API.Infrastructure
{
    public class DeliveryServiceBaseController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        public DeliveryServiceBaseController(IDistributedCache cache)
        {
            _cache = cache;
        }

        protected void CacheItem(string key, object value)
        {
            var cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
            _cache.SetString(key, JsonConvert.SerializeObject(value), cacheOptions);
        }

        protected T GetCachedItem<T>(string key)
        {
            var cacheContent = _cache.GetString(key);
            if (!string.IsNullOrWhiteSpace(cacheContent))
            {
                return JsonConvert.DeserializeObject<T>(cacheContent);
            }
            return default(T);
        }
    }
}
