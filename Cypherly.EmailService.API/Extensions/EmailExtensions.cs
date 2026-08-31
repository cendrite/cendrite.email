using Cypherly.EmailService.API.Interfaces;
using Resend;

namespace Cypherly.EmailService.API.Extensions;

public static class EmailExtensions
{
    public static void AddEmailServices(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddScoped<IEmailService, Services.EmailService>();
        services.AddHttpClient<ResendClient>();
        services.Configure<ResendClientOptions>(o =>
        {
            o.ApiToken = cfg["RESEND_APITOKEN"] ?? throw new NullReferenceException("Missing Resend API_TOKEN");
        });

        services.AddTransient<IResend, ResendClient>();
    }
}
