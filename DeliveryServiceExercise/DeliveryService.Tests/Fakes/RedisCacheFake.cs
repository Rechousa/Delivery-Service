using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeliveryService.Tests
{
    public class RedisCacheFake : IDistributedCache
    {
        private Dictionary<string, byte[]> cache = new Dictionary<string, byte[]>();


        public byte[] Get(string key)
        {
            if(cache.ContainsKey(key))
            {
                return cache[key];
            }

            return null;
        }

        public Task<byte[]> GetAsync(string key, CancellationToken token = default(CancellationToken))
        {
            return Task.Run(() =>
            {
                if (cache.ContainsKey(key))
                {
                    return cache[key];
                }

                return null;
            });
        }

        public void Refresh(string key)
        {
        }

        public Task RefreshAsync(string key, CancellationToken token = default(CancellationToken))
        {
            return Task.Run(() => { });
        }

        public void Remove(string key)
        {
            if (cache.ContainsKey(key))
            {
                cache.Remove(key);
            }
        }

        public Task RemoveAsync(string key, CancellationToken token = default(CancellationToken))
        {
            return Task.Run(() => {
                if (cache.ContainsKey(key))
                {
                    cache.Remove(key);
                }
            });
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            if (cache.ContainsKey(key))
            {
                cache[key] = value;
            }
            else
            {
                cache.Add(key, value);
            }
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken))
        {
            return Task.Run(() => {
                if (cache.ContainsKey(key))
                {
                    cache[key] = value;
                }
                else
                {
                    cache.Add(key, value);
                }
            });
        }
    }
}
