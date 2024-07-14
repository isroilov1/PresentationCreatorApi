using Microsoft.Extensions.Caching.Distributed;
using PresentationCreatorAPI.Application.Interfaces;

namespace PresentationCreatorAPI.Application.Services;

public class RedisService(IDistributedCache cache) : IRedisService
{
    private readonly IDistributedCache _cache = cache;

    public async Task DeleteAsync(string key)
        => await _cache.RemoveAsync(key);

    public async Task<string?> GetAsync(string key)
        => await _cache.GetStringAsync(key);

    public async Task SetAsync(string key, string value)
    {
        await _cache.SetStringAsync(key, value, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3),
            SlidingExpiration = TimeSpan.FromMinutes(1)
        });
    }

}
