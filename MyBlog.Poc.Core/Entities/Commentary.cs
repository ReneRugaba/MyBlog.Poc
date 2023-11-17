namespace MyBlog.Poc.Core.Entities;

public class Commentary : BaseEntity
{
    public int CommentaryId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int AuthorId { get; set; }
    public Author Author { get; set; }

    public int ArticleId { get; set; }
    public Article Article { get; set; }
}
