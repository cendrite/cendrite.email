using Cypherly.EmailService.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cypherly.EmailService.Application.Extensions;

public static class OptionsExtensions
{
    public static void ConfigureOptions(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOption<RabbitMqSettings>(configuration, "RabbitMq");
    }

    private static void ConfigureOption<T>(this IServiceCollection services, IConfiguration configuration, string sectionName) where T : class
    {
        var section = configuration.GetSection(sectionName);

        if (section.Exists())
        {
            services.Configure<T>(section);
        }
        else
        {
            throw new ArgumentException($"Configuration section '{sectionName}' not found.");
        }
    }
}