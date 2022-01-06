namespace Conduit.Shared.Events.Models.Favorites;

public class FavoriteArticleEventModel
{
    public string ArticleSlug { get; set; } = string.Empty;

    public string CurrentUsername { get; set; } = string.Empty;

    public Guid CurrentUserId { get; set; }
}
