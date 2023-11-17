namespace MyBlog.Poc.Core.Entities
{
    public class Author : BaseEntity
    {
        public int AuthorId { get; set; }
        public int FirstName { get; set; }
        public int LastName { get; set; }

        public ICollection<Article> Articles { get; set; } = new HashSet<Article>();
        public ICollection<Commentary> Commentaries { get; set; } = new HashSet<Commentary>();
    }
}
