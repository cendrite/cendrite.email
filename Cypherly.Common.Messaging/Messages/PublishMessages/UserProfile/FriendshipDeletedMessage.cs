namespace Cypherly.Common.Messaging.Messages.PublishMessages.UserProfile;

public class FriendshipDeletedMessage : BaseMessage
{
    public string UserTag { get; private set; }
    public string DeletedUserTag { get; private set; }
    public IReadOnlyCollection<Guid> ConnectionIds { get; private set; }

    public FriendshipDeletedMessage(string userTag, string deletedUserTag, IReadOnlyCollection<Guid> connectionIds, Guid correlationId, Guid? causationId = null) : base(correlationId, causationId)
    {
        UserTag = userTag;
        DeletedUserTag = deletedUserTag;
        ConnectionIds = connectionIds;
    }
}