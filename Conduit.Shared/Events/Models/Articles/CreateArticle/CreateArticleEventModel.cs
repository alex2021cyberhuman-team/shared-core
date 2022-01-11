namespace Conduit.Shared.Events.Models.Articles.CreateArticle;

public class CreateArticleEventModel
{
    public string Slug { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public ISet<string> TagList { get; set; } = new HashSet<string>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    
    public Guid AuthorId { get; set; }
}
