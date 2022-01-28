using System.Text.Json.Serialization;

namespace Conduit.Shared.Events.Models.Likes.Favorite;

public class FavoriteArticleEventModel
{
    [JsonPropertyName("ai")]
    public Guid ArticleId { get; set; }

    [JsonPropertyName("ui")]
    public Guid UserId { get; set; }
}
