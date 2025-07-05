using Cypherly.EmailService.Application.Consumer;
using Cypherly.EmailService.Application.Email.Service;
using Cypherly.EmailService.Application.Messaging;
using Cypherly.EmailService.Application.Messaging.Messages;
using Cypherly.EmailService.Application.Messaging.Producer;
using Cypherly.EmailService.Test.Integration.Setup;
using FakeItEasy;
using FluentAssertions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cypherly.EmailService.Test.Integration.ConsumerTest;

public class SendEmailMessageConsumerTest : IntegrationTestBase
{
    private readonly SendEmailConsumer _sut;
    public SendEmailMessageConsumerTest(HostedIntegrationTestService factory) : base(factory)
    {
        var scope = factory.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<SendEmailConsumer>>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
        var producer = scope.ServiceProvider.GetRequiredService<IProducer<EmailOperationSuccededMessage>>();
        _sut = new(emailService, producer, logger);
    }

    [Fact]
    public async Task Given_PublishedMessage_Should_Consume_And_SendEmail()
    {
        // Arrange
        var msg = new SendEmailMessage
        {
            Body = "This is a test email.",
            CorrelationId = Guid.NewGuid(),
            CausationId = null,
            Id = Guid.NewGuid(),
            OccurredOn = DateTime.UtcNow,
            Subject = "Test Email",
            To = "Test@user.dk",
        };

        // Act
        await Harness.Bus.Publish(msg);

        // Assert
        (await Harness.Consumed.Any<SendEmailMessage>()).Should().BeTrue("the consumer must process the message before we check MailHog");
        var email = await MailHogHelper.GetMessagesFromMailHog();
        email.Should().NotBeNull();
        var emailItem = email!.Items.FirstOrDefault();
        emailItem!.From.Domain.Should().Be("email.dk");
        emailItem.To.FirstOrDefault()!.Domain.Should().Be("user.dk");
    }
}