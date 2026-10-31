using Cendrite.EmailService.API.Consumer;
using Cendrite.EmailService.API.Interfaces;
using Cendrite.EmailService.Test.Integration.Setup;
using Cendrite.Message.Contracts.Abstractions;
using Cendrite.Message.Contracts.Messages.Common;
using Cendrite.Message.Contracts.Messages.Email;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Resend;

namespace Cendrite.EmailService.Test.Integration.ConsumerTest;

public class SendEmailMessageConsumerTest : IntegrationTestBase
{
    private readonly SendEmailConsumer _sut;
    private readonly IResend _resend;

    public SendEmailMessageConsumerTest(IntegrationTestFactory<Program> factory) : base(factory)
    {
        var scope = factory.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<SendEmailConsumer>>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
        var producer = scope.ServiceProvider.GetRequiredService<IProducer<OperationSucceededMessage>>();
        _resend = scope.ServiceProvider.GetRequiredService<IResend>();
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
        (await Harness.Consumed.Any<SendEmailMessage>()).Should().BeTrue("the consumer must process the message before we check the email was sent");

        A.CallTo(() => _resend.EmailSendAsync(
                A<EmailMessage>.That.Matches(m =>
                    m.To.Single().Email == message.To &&
                    m.Subject == message.Subject &&
                    m.HtmlBody == message.Body),
                A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }
}
