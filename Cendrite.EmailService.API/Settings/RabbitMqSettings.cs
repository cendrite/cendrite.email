namespace Cendrite.EmailService.API.Settings;

public sealed class RabbitMqSettings
{
    public required string Host { get; init; } = "localhost";
    public required string Username { get; init; } = "root";
    public required string Password { get; init; } = "root";
}