using System.Text.Json.Serialization;

namespace Conduit.Shared.Events.Models.Images;

public class UploadArticleImageEventModel
{
    [JsonPropertyName("i")]
    public Guid Id { get; set; }

    [JsonPropertyName("ui")]
    public Guid UserId { get; set; }

    [JsonPropertyName("ud")]
    public DateTime Uploaded { get; set; }

    [JsonPropertyName("mt")]
    public string MediaType { get; set; } = string.Empty;

    [JsonPropertyName("sn")]
    public string StorageName { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}
