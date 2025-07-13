using System.Net.Mail;

namespace Cypherly.EmailService.API.Email.Smtp;

public interface ISmtpClient
{
    Task Send(MailMessage email);
}