using Cendrite.EmailService.API.Settings;

namespace Cendrite.EmailService.API.Extensions;

public static class OptionsExtensions
{
    public static void SetupOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOption<SmtpSettings>(configuration, "Smtp");
        services.ConfigureOption<RabbitMqSettings>(configuration, "RabbitMq");
        services.ConfigureOption<EmailSettings>(configuration, "Email");
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