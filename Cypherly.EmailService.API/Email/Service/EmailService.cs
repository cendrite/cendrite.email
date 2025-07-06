using System.Net.Mail;
using Cypherly.EmailService.API.Settings;
using Cypherly.EmailService.Application.Email.Smtp;
using Microsoft.Extensions.Options;

namespace Cypherly.EmailService.API.Email.Service;

public sealed class EmailService(
    ISmtpClient smtpClient,
    IOptions<EmailSettings> emailSettings,
    ILogger<EmailService> logger)
    : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var mailMessage = new MailMessage(emailSettings.Value.FromAddress, to, subject, body)
        {
            IsBodyHtml = true
        };

        logger.LogInformation("Sending email to {To} with subject {Subject}", to, subject);
        await smtpClient.Send(mailMessage);
    }
}