namespace Cypherly.EmailService.Application.Messaging;

public interface IProducer<in TMessage> where TMessage : IBaseMessage
{
    Task PublishMessageAsync(TMessage message, CancellationToken cancellationToken = default);
}