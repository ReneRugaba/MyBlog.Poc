using Microsoft.EntityFrameworkCore;

namespace MyBlog.Poc.Core.Entities;

public class BlogContext : DbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Commentary> Commentaries { get; set; }

    public BlogContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArticleBuilder(modelBuilder);
        CommentaryBuilder(modelBuilder);
    }

    private static void CommentaryBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Commentary>(entity =>
        {
            entity.HasOne(c => c.Author).WithMany()
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }

    private static void ArticleBuilder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasOne(a => a.Author)
                .WithMany(auth => auth.Articles)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasMany(a => a.Commentaries)
                .WithOne(c => c.Article)
                .HasForeignKey(a => a.ArticleId);
        });
    }
}
