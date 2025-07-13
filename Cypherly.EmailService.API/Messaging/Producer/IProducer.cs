using Cypherly.Message.Contracts.Abstractions;

namespace Cypherly.EmailService.API.Messaging.Producer;

public interface IProducer<in TBaseMessage> where TBaseMessage : IBaseMessage
{
    Task PublishMessageAsync(TBaseMessage message, CancellationToken cancellationToken = default);
}