using Cypherly.EmailService.API.Settings;
using Microsoft.Extensions.Options;
using Resend;

namespace Cypherly.EmailService.API.Email.Service;

public sealed class EmailService(
    IResend resender,
    IOptions<EmailSettings> emailSettings,
    ILogger<EmailService> logger)
    : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var message = new EmailMessage
        {
            From = emailSettings.Value.FromAddress,
            To = to,
            Subject = subject,
            HtmlBody = body
        };

        logger.LogInformation("Sending email to {To} with subject {Subject}", to, subject);

        await resender.EmailSendAsync(message);

    }
}
