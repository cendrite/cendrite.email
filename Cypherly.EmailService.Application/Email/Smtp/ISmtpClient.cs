using System.Net.Mail;

namespace Cypherly.EmailService.Application.Email.Smtp;

public interface ISmtpClient
{
    Task Send(MailMessage email);
}