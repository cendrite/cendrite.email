using Cendrite.EmailService.API.Consumer;
using FakeItEasy;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Resend;

namespace Cendrite.EmailService.Test.Integration.Setup;

public class IntegrationTestFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
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

            var resendDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IResend));

            if (resendDescriptor is not null)
                services.Remove(resendDescriptor);

            services.AddSingleton(_ => A.Fake<IResend>());
        });
    }
}
