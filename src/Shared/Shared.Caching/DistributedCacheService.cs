using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Shared.Caching;

public class DistributedCacheService(IDistributedCache distributedCache) : IDistributedCacheService
{
    public async Task<IEnumerable<T>> GetAllAsync<T>(string key)
    {
        var stringResult = await distributedCache.GetStringAsync(key);

        if (stringResult == null)
            return null;

        return JsonSerializer.Deserialize<IEnumerable<T>>(stringResult)!;

    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var stringResult = await distributedCache.GetStringAsync(key);

        if (stringResult == null)
            return default;

        return JsonSerializer.Deserialize<T>(stringResult)!;
    }

    public async Task RemoveAsync(string key)
    {
        await distributedCache.RemoveAsync(key);
    }

    public async Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options)
    {
        var stringResult = JsonSerializer.Serialize(value);
        await distributedCache.SetStringAsync(key, stringResult, options);
    }

    public async Task SetListAsync<T>(string key, IEnumerable<T> values, DistributedCacheEntryOptions options)
    {
        var stringResult = JsonSerializer.Serialize(values);
        await distributedCache.SetStringAsync(key, stringResult, options);
    }

    public async Task SetAsync<T>(string key, T value)
    {
        var stringResult = JsonSerializer.Serialize(value);
        await distributedCache.SetStringAsync(key, stringResult);
    }

    public async Task SetListAsync<T>(string key, IEnumerable<T> values)
    {
        var stringResult = JsonSerializer.Serialize(values);
        await distributedCache.SetStringAsync(key, stringResult);
    }
}
