using Cypherly.EmailService.Application.Email.Service;
using Cypherly.EmailService.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Cypherly.EmailService.Application.Consumer;

public class SendEmailConsumer(
    IEmailService emailService,
    IProducer<EmailOperationSuccededMessage> operationSuccededProducer,
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

            await operationSuccededProducer.PublishMessageAsync(new EmailOperationSuccededMessage
            {
                CorrelationId = message.CorrelationId,
                CausationId = message.CausationId,
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