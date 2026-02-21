using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace SalesForce.Data.Cache
{
    public class Cache : ICache
    {
        private readonly IDistributedCache _distributedCache;
        private readonly bool _enableCache;

        public Cache(IDistributedCache distributedCache, IConfiguration configuration)
        {
            _distributedCache = distributedCache;
            _enableCache = configuration.GetValue<bool>("CacheConfig:EnableCache");
        }

        public async Task<List<T>> GetListAsync<T>(string key)
        {
            var list = new List<T>();

            if (!_enableCache) return list;

            var keyValue = $"{nameof(T)}.{key}";
            var json = await _distributedCache.GetStringAsync(keyValue);            
            if (json != null)
            {
                list = JsonSerializer.Deserialize<List<T>>(json);
            }
            return list;
        }

        public async Task SetListAsync<T>(string key, IEnumerable<T> data)
        {
            if (!_enableCache) return;

            DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
            var keyValue = $"{nameof(T)}.{key}";
            var json = JsonSerializer.Serialize<IEnumerable<T>>(data);
            await _distributedCache.SetStringAsync(keyValue, json, cacheOptions);
        }
    }
}
