namespace Cypherly.EmailService.Application.Messaging.Producer;

public interface IProducer<in TMessage> where TMessage : IBaseMessage
{
    Task PublishMessageAsync(TMessage message, CancellationToken cancellationToken = default);
}