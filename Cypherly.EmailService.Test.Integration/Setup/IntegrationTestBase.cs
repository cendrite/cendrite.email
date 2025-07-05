using Cypherly.EmailService.Test.Integration.Setup.Helpers;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Cypherly.EmailService.Test.Integration.Setup;

[Collection("ApplicationTestFixture")]
public class IntegrationTestBase : IDisposable
{
    protected readonly ITestHarness Harness;
    protected readonly MailHogHelper MailHogHelper = new MailHogHelper();

    public IntegrationTestBase(HostedIntegrationTestService factory)
    {
        Harness = factory.Services.GetRequiredService<ITestHarness>();
    }
    public void Dispose()
    {
        Harness.Stop();
        MailHogHelper.ClearMailHogMessage().Wait();
    }
}