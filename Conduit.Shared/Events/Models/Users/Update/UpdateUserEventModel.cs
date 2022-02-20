using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Conduit.Shared.Events.Models.Users.Update;

public record UpdateUserEventModel
{
    [Key]
    [JsonPropertyName("i")]
    public Guid Id { get; init; }

    [Required]
    [JsonPropertyName("n")]
    public string Username { get; init; } = string.Empty;

    [Required]
    [EmailAddress]
    [JsonPropertyName("e")]
    public string Email { get; init; } = string.Empty;

    [DataType(DataType.ImageUrl)]
    [JsonPropertyName("im")]
    public string? Image { get; init; }

    [DataType(DataType.MultilineText)]
    [JsonPropertyName("b")]
    public string? Biography { get; init; }
}
