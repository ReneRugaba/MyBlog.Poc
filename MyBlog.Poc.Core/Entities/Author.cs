namespace MyBlog.Poc.Core.Entities
{
    public class Author : BaseEntity
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public ICollection<Article> Articles { get; set; } = new HashSet<Article>();
        public ICollection<Commentary> Commentaries { get; set; } = new HashSet<Commentary>();
    }
}
