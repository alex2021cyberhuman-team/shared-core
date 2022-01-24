using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Conduit.Shared.Events.Models.Profiles.CreateFollowing;

public class CreateFollowingEventModel
{
    [JsonPropertyName("fr")]
    public Guid FollowerId { get; set; }

    [JsonPropertyName("fd")]
    public Guid FollowedId { get; set; }
}
