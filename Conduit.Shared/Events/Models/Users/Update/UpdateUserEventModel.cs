using System.ComponentModel.DataAnnotations;

namespace Conduit.Shared.Events.Models.Users.Update;

public record UpdateUserEventModel
{
    public UpdateUserEventModel()
    {
    }

    public UpdateUserEventModel(
        Guid id,
        string username,
        string email,
        string? image = default,
        string? biography = default)
    {
        Id = id;
        Username = username;
        Email = email;
        Image = image;
        Biography = biography;
    }

    [Key]
    public Guid Id { get; init; }

    [Required]
    public string Username { get; init; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [DataType(DataType.ImageUrl)]
    public string? Image { get; init; }

    [DataType(DataType.MultilineText)]
    public string? Biography { get; init; }
}
