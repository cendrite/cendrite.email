namespace Cypherly.Common.Messaging.Messages.PublishMessages.UserProfile;

public sealed class FriendRequestMessage(
    Guid userId,
    IReadOnlyCollection<Guid> connectionIds,
    string friendUsername,
    string friendTag,
    DateTime friendRequestDate,
    bool isSeen,
    string? friendDisplayName,
    string? friendProfilePictureUrl,
    Guid correlationId,
    Guid? causationId = null)
    : BaseMessage(correlationId, causationId)
{
    public Guid UserId { get; private init; } = userId;
    public IReadOnlyCollection<Guid> ConnectionIds { get; private init; } = connectionIds;
    public string FriendTag { get; private init; } = friendTag;

    public DateTime FriendRequestDate { get; private init; } = friendRequestDate;
    public bool IsSeen { get; private init; } = isSeen;
    public string? FriendDisplayName { get; private init; } = friendDisplayName;
    public string? FriendProfilePictureUrl { get; private init; } = friendProfilePictureUrl;
    public string FriendUsername { get; private init; } = friendUsername;
}
