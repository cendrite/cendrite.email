using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Cypherly.EmailService.Test.Integration.Setup;

[Collection("ApplicationTestFixture")]
public class IntegrationTestBase(IntegrationTestFactory<Program> factory) : IDisposable
{
    protected readonly ITestHarness Harness = factory.Services.GetRequiredService<ITestHarness>();

    public void Dispose()
    {
        Harness.Stop();
    }
}