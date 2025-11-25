using Microsoft.Extensions.DependencyInjection;
using TenshiShop.Application.ApiCommands.Auth;
using TenshiShop.Application.Validation.Auth;

namespace TenshiShop.Core.DependencyInjection;

public static class InjectValidators
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegisterCommand>, RegisterRequestValidator>();
        
        return services;
    }
}