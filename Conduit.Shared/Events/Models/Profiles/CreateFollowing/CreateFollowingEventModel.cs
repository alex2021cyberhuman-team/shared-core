namespace Conduit.Shared.Events.Models.Profiles.CreateFollowing;

public class CreateFollowingEventModel
{
    public CreateFollowingEventModel()
    {
    }

    public CreateFollowingEventModel(
        Guid followerId,
        Guid followedId)
    {
        FollowerId = followerId;
        FollowedId = followedId;
    }

    public Guid FollowerId { get; set; }

    public Guid FollowedId { get; set; }
}
