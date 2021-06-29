using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesForce.Data.Cache
{
    public interface ICache
    {
        Task<List<T>> GetListAsync<T>(string key);

        Task SetListAsync<T>(string key, IEnumerable<T> data);
    }
}
