using Microsoft.Extensions.Caching.Distributed;
using TenshiShop.Domain.Redis;

namespace TenshiShop.Infrastructure.Redis;

public class RedisDispatcher : IRedisSaver, IRedisGetter
{
    private IDistributedCache _cache;

    public RedisDispatcher(IDistributedCache cache)
    {
        _cache = cache; 
    }

    public async Task SaveString(string key, string value, TimeSpan lifeTime, CancellationToken ct)
    {
        await _cache.SetStringAsync(key, value, new DistributedCacheEntryOptions
        { 
            AbsoluteExpirationRelativeToNow = lifeTime
        }, ct);
    }

    public async Task<object?> Get(string key, CancellationToken ct)
    {
        return await _cache.GetStringAsync(key, ct);
    }
}