using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace SalesForce.Data.Cache
{
    public class Cache : ICache
    {
        private readonly IDistributedCache _distributedCache;

        public Cache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<List<T>> GetListAsync<T>(string key)
        {
            var keyValue = $"{nameof(T)}.{key}";
            var list = new List<T>();
            var json = await _distributedCache.GetStringAsync(key);            
            if (json != null)
            {
                list = JsonSerializer.Deserialize<List<T>>(json);
            }
            return list;
        }

        public async Task SetListAsync<T>(string key, IEnumerable<T> data)
        {
            DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
            var keyValue = $"{nameof(T)}.{key}";
            var json = JsonSerializer.Serialize<IEnumerable<T>>(data);
            await _distributedCache.SetStringAsync(keyValue, json, cacheOptions);
        }
    }
}
