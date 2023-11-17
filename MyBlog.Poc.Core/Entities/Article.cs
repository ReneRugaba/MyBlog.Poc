namespace MyBlog.Poc.Core.Entities;

public class Article : BaseEntity
{
    public int ArticleId { get; set; }
    public int Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int AuthorId { get; set; }
    public Author Author { get; set; }

    public ICollection<Commentary> Commentaries { get; set; } = new HashSet<Commentary>();
}
