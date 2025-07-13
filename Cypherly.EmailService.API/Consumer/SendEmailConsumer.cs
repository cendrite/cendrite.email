using Cypherly.EmailService.API.Email.Service;
using Cypherly.EmailService.API.Messaging.Producer;
using Cypherly.Message.Contracts.Enums;
using Cypherly.Message.Contracts.Messages.Common;
using Cypherly.Message.Contracts.Messages.Email;
using MassTransit;

namespace Cypherly.EmailService.API.Consumer;

public class SendEmailConsumer(
    IEmailService emailService,
    IProducer<OperationSuccededMessage> operationSuccededProducer,
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
            await operationSuccededProducer.PublishMessageAsync(new OperationSuccededMessage
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