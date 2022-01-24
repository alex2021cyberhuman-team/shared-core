using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Conduit.Shared.Events.Models.Articles.DeleteArticle;

public class DeleteArticleEventModel
{
    [JsonPropertyName("i")]
    public Guid Id { get; set; }

    [JsonPropertyName("ui")]
    public Guid UserId { get; set; } 
}
