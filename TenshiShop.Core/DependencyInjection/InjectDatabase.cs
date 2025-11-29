using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TenshiShop.Domain.Gateways;
using TenshiShop.Persistence;
using TenshiShop.Persistence.DataAccess;

namespace TenshiShop.Core.DependencyInjection;

public static class InjectDatabase
{
    public static IServiceCollection AddDatabaseServices(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<AppDbContext>(op => op.UseNpgsql(connectionString));
        services.AddScoped<ICreateUserGateway, UserDataAccess>();
        services.AddScoped<IFindUserByEmailGateway, UserDataAccess>();
        services.AddScoped<IActivateUserGateway, UserDataAccess>();
        return services;
    }
}