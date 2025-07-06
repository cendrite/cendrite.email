// ReSharper disable ConvertToPrimaryConstructor
namespace Cypherly.Common.Messaging.Messages.PublishMessages.UserProfile;

public sealed class ProfilePictureUpdatedMessage : BaseMessage
{
    public IReadOnlyCollection<Guid> ConnectionIds { get; private set; }
    public string UserTag { get; private set; }
    public string ProfilePictureUrl { get; private set; }

    public ProfilePictureUpdatedMessage(string userTag, string profilePictureUrl, IReadOnlyCollection<Guid> connectionIds, Guid correlationId, Guid? causationId = null) : base(correlationId, causationId)
    {
        UserTag = userTag;
        ProfilePictureUrl = profilePictureUrl;
        ConnectionIds = connectionIds;
    }
}