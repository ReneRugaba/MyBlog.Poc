namespace MyBlog.Poc.Core.Entities;

public class Article : BaseEntity
{
    public int ArticleId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int AuthorId { get; set; }
    public Author Author { get; set; }

    public ICollection<Commentary> Commentaries { get; set; } = new HashSet<Commentary>();
}
