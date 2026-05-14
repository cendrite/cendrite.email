using Cypherly.EmailService.API.Consumer;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Cypherly.EmailService.Test.Integration.Setup;

public class IntegrationTestFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime where TProgram : class
{
    private readonly IContainer _mailContainer = new ContainerBuilder()
        .WithImage("mailhog/mailhog:latest")
        .WithExposedPort(8025)
        .WithExposedPort(1025)
        .WithPortBinding(8025, 8025)
        .WithPortBinding(1025, 1025)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");
        builder.ConfigureServices(services =>
        {
            var rmgDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IBusControl));

            if (rmgDescriptor is not null)
                services.Remove(rmgDescriptor);

            services.AddMassTransitTestHarness(cfg =>
            {
                cfg.AddConsumer<SendEmailConsumer>();
            });
        });
    }

    public async ValueTask InitializeAsync()
    {
        await _mailContainer.StartAsync();
    }

    public async new Task DisposeAsync()
    {
        await _mailContainer.StopAsync();
        await _mailContainer.DisposeAsync();
    }
}