using System.Net;
using System.Net.Mail;
using Cypherly.EmailService.Application.Settings;
using Microsoft.Extensions.Options;

namespace Cypherly.EmailService.Application.Email.Smtp;

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

    public Task Send(MailMessage email)
    {
        throw new NotImplementedException();
    }
}