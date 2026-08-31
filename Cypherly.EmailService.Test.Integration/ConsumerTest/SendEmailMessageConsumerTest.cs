using Cypherly.EmailService.API.Consumer;
using Cypherly.EmailService.API.Interfaces;
using Cypherly.EmailService.Test.Integration.Setup;
using Cypherly.Message.Contracts.Abstractions;
using Cypherly.Message.Contracts.Messages.Common;
using Cypherly.Message.Contracts.Messages.Email;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cypherly.EmailService.Test.Integration.ConsumerTest;

public class SendEmailMessageConsumerTest : IntegrationTestBase
{
    private readonly SendEmailConsumer _sut;
    public SendEmailMessageConsumerTest(IntegrationTestFactory<Program> factory) : base(factory)
    {
        var scope = factory.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<SendEmailConsumer>>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
        var producer = scope.ServiceProvider.GetRequiredService<IProducer<OperationSuccededMessage>>();
        _sut = new(emailService, producer, logger);
    }

    [Fact]
    public async Task Given_PublishedMessage_Should_Consume_And_SendEmail()
    {
        // Arrange
        var message = new SendEmailMessage()
        {
            Body = "Test",
            To = "test@mail.dk",
            Subject = "Test",
            CorrelationId = Guid.NewGuid(),
            CausationId = Guid.Empty
        };

        // Act
        await Harness.Bus.Publish(message);

        // Assert
        (await Harness.Consumed.Any<SendEmailMessage>()).Should().BeTrue("the consumer must process the message before we check MailHog");
        var email = await MailHogHelper.GetMessagesFromMailHog();
        email.Should().NotBeNull();
        var emailItem = email!.Items.FirstOrDefault();
        emailItem!.From.Domain.Should().Be("email.dk");
        emailItem.To.FirstOrDefault()!.Domain.Should().Be("mail.dk");
    }
}
