using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TenshiShop.Application.Settings;
using TenshiShop.Application.Permissions;
using TenshiShop.Application.Permissions.Impl;

namespace TenshiShop.Core.DependencyInjection;

public static class InjectAuthServices
{
    public static IServiceCollection AddAuthServices(
        this IServiceCollection services,
        AuthOptions authOptions)
    {
        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });

        services.AddTransient<IActiveChecker, ActiveCkeckerByEmail>();
        
        return services;
    }
}