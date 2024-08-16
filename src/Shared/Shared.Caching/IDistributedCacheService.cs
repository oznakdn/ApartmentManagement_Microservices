using Microsoft.Extensions.Caching.Distributed;

namespace Shared.Caching;

public interface IDistributedCacheService
{
    Task<T?> GetAsync<T>(string key);
    Task<IEnumerable<T>> GetAllAsync<T>(string key);
    Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options);
    Task SetAsync<T>(string key, T value);

    Task SetAsync<T>(string key, IEnumerable<T> values, DistributedCacheEntryOptions options);
    Task SetAsync<T>(string key, IEnumerable<T> values);
    Task RemoveAsync(string key);

}
