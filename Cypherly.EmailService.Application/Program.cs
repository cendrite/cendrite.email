using System.Reflection;
using Cypherly.EmailService.Application.Extensions;
using Cypherly.EmailService.Application.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .AddEnvironmentVariables()
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

try
{
    var host = Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((builder) =>
        {
            builder.AddConfiguration(configuration);
        })
        .UseSerilog()
        .ConfigureServices((_, services) =>
        {
            services.SetupOptions(configuration);
            services.AddMassTransitWithRabbitMq(Assembly.GetExecutingAssembly());
            services.AddEmailServices();
        })
        .Build();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}


public partial class Program {}