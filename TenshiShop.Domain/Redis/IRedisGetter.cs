namespace TenshiShop.Domain.Redis;

public interface IRedisGetter
{
    Task<object?> Get(string key, CancellationToken ct);
}