
namespace Cypherly.EmailService.API.Messaging.Messages;

public sealed class EmailOperationSuccededMessage : IBaseMessage
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public Guid Id { get; } = Guid.NewGuid();
    public required Guid CorrelationId { get; init; }
    public Guid? CausationId { get; init; }
    private int OperationType { get; init; } = 1;
}