using System.Text.Json.Serialization;

namespace Conduit.Shared.Events.Models.Likes.Unfavorite;

public class UnfavoriteArticleEventModel
{
    [JsonPropertyName("ai")]
    public Guid ArticleId { get; set; }

    [JsonPropertyName("ui")]
    public Guid UserId { get; set; }
}
