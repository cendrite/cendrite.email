using System.Reflection;
using Cypherly.EmailService.Application.Consumer;
using Cypherly.EmailService.Application.Extensions;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Cypherly.EmailService.Test.Integration.Setup;

public sealed class HostedIntegrationTestService : IAsyncLifetime
{
    private readonly IContainer _mailContainer = new ContainerBuilder()
        .WithImage("mailhog/mailhog:latest")
        .WithPortBinding(8025, 8025)
        .WithPortBinding(1025, 1025)
        .Build();

    private IHost _host;

    public IServiceProvider Services => _host.Services;

    public async Task InitializeAsync()
    {

        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.test.json", optional: false)
            .Build();

        _host = new HostBuilder()
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddConfiguration(config);
            })
            .ConfigureServices((context, services) =>
            {
                services.SetupOptions(context.Configuration);
                services.AddMassTransitWithRabbitMq(Assembly.GetExecutingAssembly());
                services.AddEmailServices();

                // Test configuration
                var rmgDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IBusControl));

                if (rmgDescriptor is not null)
                    services.Remove(rmgDescriptor);

                services.AddMassTransitTestHarness(cfg =>
                {
                    cfg.AddConsumer<SendEmailConsumer>();
                });
            })
            .Build();

        await _mailContainer.StartAsync();
        await _host.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _host.StopAsync();
        await _mailContainer.StopAsync();
    }
}