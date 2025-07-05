using Cypherly.EmailService.Application.Email.Service;
using Cypherly.EmailService.Application.Email.Smtp;

namespace Cypherly.EmailService.API.Extensions;

public static class EmailExtensions
{
    public static void AddEmailServices(this IServiceCollection services)
    {
        services.AddScoped<ISmtpClient, SmtpService>();
        services.AddScoped<IEmailService, Email.Service.EmailService>();
    }
}