namespace Conduit.Shared.Events.Models.Profiles.RemoveFollowing;

public record RemoveFollowingEventModel
{
    public RemoveFollowingEventModel()
    {
    }

    public RemoveFollowingEventModel(
        Guid followerId,
        Guid followedId)
    {
        FollowerId = followerId;
        FollowedId = followedId;
    }

    public Guid FollowerId { get; set; }

    public Guid FollowedId { get; set; }
}
