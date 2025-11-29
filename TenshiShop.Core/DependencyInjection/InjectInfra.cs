using Microsoft.Extensions.DependencyInjection;
using TenshiShop.Application.ApiCommands.Auth;
using TenshiShop.Domain.Redis;
using TenshiShop.Infrastructure.Redis;

namespace TenshiShop.Core.DependencyInjection;

public static class InjectInfra
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost";
            options.InstanceName = "local";
        });

        services.AddScoped<IRedisSaver, RedisDispatcher>();
        services.AddScoped<IRedisGetter, RedisDispatcher>();

        return services;
    }
}