namespace Cypherly.Common.Messaging.Messages.PublishMessages.UserProfile;

public sealed class FriendRequestRejectedMessage : BaseMessage
{
    public string RejectedUserProfileTag { get; private set; }
    public IReadOnlyCollection<Guid> ConnectionIds { get; private set; }

    public FriendRequestRejectedMessage(string rejectedUserProfileTag, IReadOnlyCollection<Guid> connectionIds, Guid correlationId, Guid? causationId = null) : base(correlationId, causationId)
    {
        RejectedUserProfileTag = rejectedUserProfileTag;
        ConnectionIds = connectionIds;
    }
}