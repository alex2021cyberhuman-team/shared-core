namespace Conduit.Shared.Events.Models.Articles.DeleteArticle;

public class DeleteArticleEventModel
{
    public string Slug { get; set; } = string.Empty;
    
    public Guid AuthorId { get; set; } 
}
