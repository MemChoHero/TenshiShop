using Microsoft.Extensions.DependencyInjection;
using TenshiShop.Application.ApiCommands.Auth;

namespace TenshiShop.Core.DependencyInjection;

public static class InjectInfra
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly));
        
        return services;
    }
}