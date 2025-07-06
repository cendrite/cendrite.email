namespace Cypherly.Common.Messaging.Messages.PublishMessages.UserProfile;

public class FriendshipAcceptedMessage(Guid correlationId, Guid? causationId = null) : BaseMessage(correlationId, causationId)
{
    public required string Username { get; init; }
    public required string Tag { get; init; }
    public string? DisplayName { get; init; }
    public string? ProfilePictureUrl { get; init; }
    public required IReadOnlyCollection<Guid> RouteIds { get; init; }
    public required IReadOnlyCollection<Guid> ConnectionIds { get; init; }
}