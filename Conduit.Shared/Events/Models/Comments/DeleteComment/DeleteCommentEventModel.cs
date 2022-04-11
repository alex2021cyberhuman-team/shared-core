using System.Text.Json.Serialization;

namespace Conduit.Shared.Events.Models.Comments.DeleteComment;

public class DeleteCommentEventModel
{
    [JsonPropertyName("ui")]
    public Guid UserId { get; set; }

    [JsonPropertyName("ai")]
    public Guid ArticleId { get; set; }

    [JsonPropertyName("i")]
    public Guid Id { get; set; }
}
