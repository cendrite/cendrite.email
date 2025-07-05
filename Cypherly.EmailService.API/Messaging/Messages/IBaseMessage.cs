namespace Cypherly.EmailService.API.Messaging.Messages;

public interface IBaseMessage
{
    /// <summary>
    /// The time that the message was created.
    /// </summary>
    DateTime OccurredOn { get; }

    /// <summary>
    /// The unique identifier for this message.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Should be the same for all messages that are part of the same logical operation.
    /// Set by the originating message.
    /// </summary>
    public Guid CorrelationId { get; }

    /// <summary>
    /// Should match the ID of the message that caused this message to be created.
    /// Will be null for the first message in a chain.
    /// </summary>
    public Guid? CausationId { get; }
}