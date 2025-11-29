namespace TenshiShop.Domain.Redis;

public interface IRedisSaver
{
    Task SaveString(string key, string value, TimeSpan lifeTime, CancellationToken ct);
}