using System.Reflection;
using Cendrite.EmailService.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


var env = builder.Environment;

var configuration = builder.Configuration;
configuration.AddJsonFile("appsettings.json", false, true).AddEnvironmentVariables();

if (env.IsDevelopment())
{
    configuration.AddJsonFile($"appsettings.{Environments.Development}.json", true, true);
    configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
}

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddObservability(configuration);


builder.Services.SetupOptions(configuration);
builder.Services.AddMassTransitWithRabbitMq(Assembly.GetExecutingAssembly());
builder.Services.AddEmailServices(configuration);


var app = builder.Build();

app.MapPrometheusScrapingEndpoint();

app.Run();

public partial class Program { } 
