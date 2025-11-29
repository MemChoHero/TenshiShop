using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TenshiShop.Domain.Mail;
using TenshiShop.Infrastructure.Mail;

namespace TenshiShop.Core.DependencyInjection;

public static class InjectMailServices
{
    public static IServiceCollection AddMailServices(
        this IServiceCollection services,
        IConfigurationSection section)
    {
        services.Configure<MailSettings>(section);
        services.AddTransient<IMailSender, MailSender>();

        return services; 
    }
}
