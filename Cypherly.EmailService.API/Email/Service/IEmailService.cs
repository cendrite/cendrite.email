namespace Cypherly.EmailService.Application.Email.Service;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}