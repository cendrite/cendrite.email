using Cypherly.EmailService.API.Messaging.Messages;

namespace Cypherly.EmailService.API.Messaging.Producer;

public interface IProducer<in TMessage> where TMessage : IBaseMessage
{
    Task PublishMessageAsync(TMessage message, CancellationToken cancellationToken = default);
}