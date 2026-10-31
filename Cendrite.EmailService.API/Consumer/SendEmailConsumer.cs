using Cendrite.EmailService.API.Interfaces;
using Cendrite.Message.Contracts.Abstractions;
using Cendrite.Message.Contracts.Enums;
using Cendrite.Message.Contracts.Messages.Common;
using Cendrite.Message.Contracts.Messages.Email;
using MassTransit;

namespace Cendrite.EmailService.API.Consumer;

public class SendEmailConsumer(
    IEmailService emailService,
    IProducer<OperationSucceededMessage> operationSuccededProducer,
    ILogger<SendEmailConsumer> logger)
    : IConsumer<SendEmailMessage>
{
    public async Task Consume(ConsumeContext<SendEmailMessage> context)
    {
        try
        {
            var message = context.Message;

            logger.LogInformation("Sending email to {To} with subject {Subject}",
                message.To,
                message.Subject);

            await emailService.SendEmailAsync(message.To, message.Subject, message.Body);
            await operationSuccededProducer.PublishMessageAsync(new OperationSucceededMessage
            {
                CorrelationId = message.CorrelationId,
                CausationId = message.Id,
                OperationType = OperationType.SendEmail
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error in {Consumer} for message with id: {Id}",
                nameof(SendEmailConsumer),
                context.Message.Id);
            throw;
        }
    }
}
