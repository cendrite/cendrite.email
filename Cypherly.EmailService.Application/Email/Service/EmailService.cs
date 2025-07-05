using Cypherly.EmailService.Application.Email.Smtp;
using Microsoft.Extensions.Logging;

namespace Cypherly.EmailService.Application.Email.Service;

public sealed class EmailService(
    ISmtpClient smtpClient,
    ILogger<EmailService> logger
    ) : IEmailService
{
    public Task SendEmailAsync(string to, string subject, string body)
    {
        throw new NotImplementedException();
    }
}