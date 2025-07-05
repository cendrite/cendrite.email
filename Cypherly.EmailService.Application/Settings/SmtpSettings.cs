namespace Cypherly.EmailService.Application.Settings;

public sealed class SmtpSettings
{
    public required string Host { get; set; }
    public int Port { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public bool UseSsl { get; set; } = true;
}