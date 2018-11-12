using DeliveryService.API.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace DeliveryService.API.Infrastructure
{
    public class DeliveryServiceBaseController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        private readonly AppSettings _appSettings;

        public DeliveryServiceBaseController(IDistributedCache cache, IOptions<AppSettings> appSettings)
        {
            _cache = cache;
            _appSettings = appSettings.Value;
        }

        protected void CacheItem(string key, object value)
        {
            var cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(_appSettings.RedisKeyTimeToLiveInSeconds));
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
