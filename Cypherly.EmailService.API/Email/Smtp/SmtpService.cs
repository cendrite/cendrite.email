using System.Net;
using System.Net.Mail;
using Cypherly.EmailService.API.Settings;
using Microsoft.Extensions.Options;

namespace Cypherly.EmailService.API.Email.Smtp;

public sealed class SmtpService : ISmtpClient
{
    private readonly SmtpClient _client;

    public SmtpService(IOptions<SmtpSettings> settings)
    {
        var smtpSettings = settings.Value;
        _client = new()
        {
            Host = smtpSettings.Host,
            Port = smtpSettings.Port,
            Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password),
            EnableSsl = smtpSettings.UseSsl
        };
    }

    public async Task Send(MailMessage email)
    {
        await _client.SendMailAsync(email);
    }
}