using System.Text.Json.Serialization;

namespace Conduit.Shared.Events.Models.Images;

public class UploadArticleImageEventModel
{
    [JsonPropertyName("i")]
    public Guid Id { get; set; }

    public string MediaType { get; set; }
}