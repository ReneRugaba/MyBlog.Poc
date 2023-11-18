using Microsoft.EntityFrameworkCore;
using MyBlog.Poc.Core.Entities;

namespace MyBlog.Poc;

public static class DbInitializer
{
    public async static Task InitialDatabaseAsync(BlogContext blogContext)
    {
        await blogContext.Database.EnsureCreatedAsync();

        if (await blogContext.Articles.AnyAsync())
        {
            return;
        }

        var auhors = new HashSet<Author>()
        {
            new Author()
            {
                FirstName = "FirstName 1",
                LastName ="LastName 1"
            },new Author()
            {
                FirstName = "FirstName 2",
                LastName ="LastName 2"
            },new Author()
            {
                FirstName = "FirstName 3",
                LastName ="LastName 3"
            },new Author()
            {
                FirstName = "FirstName 4",
                LastName ="LastName 4"
            },new Author()
            {
                FirstName = "FirstName 5",
                LastName ="LastName 5"
            }
        };
        await blogContext.AddRangeAsync(auhors);
        await blogContext.SaveChangesAsync();

        var article = new HashSet<Article>()
        {
            new Article()
            {
                AuthorId = auhors.ToList()[0].AuthorId,
                Title="Article 1",
                Description="this is my description 1",
                Content="This is a content 1",
                CreatedAt=DateTime.Now,
                UpdatedAt=DateTime.Now,
            },new Article()
            {
                AuthorId = auhors.ToList()[1].AuthorId,
                Title="Article 2",
                Description="this is my description 2",
                Content="This is a content 2",
                CreatedAt=DateTime.Now,
                UpdatedAt=DateTime.Now,
            },new Article()
            {
                AuthorId = auhors.ToList()[2].AuthorId,
                Title="Article 3",
                Description="this is my description 3",
                Content="This is a content 3",
                CreatedAt=DateTime.Now,
                UpdatedAt=DateTime.Now,
            },new Article()
            {
                AuthorId = auhors.ToList()[3].AuthorId,
                Title="Article 4",
                Description="this is my description 4",
                Content="This is a content 4",
                CreatedAt=DateTime.Now,
                UpdatedAt=DateTime.Now,
            },
        };

        await blogContext.AddRangeAsync(article);
        await blogContext.SaveChangesAsync();

        var commentaries = new HashSet<Commentary>()
        {
            new Commentary()
            {
                Content="his is my first commentary 1",
                AuthorId = auhors.ToList()[0].AuthorId,
                ArticleId = 3,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },new Commentary()
            {
                Content="his is my first commentary 2",
                AuthorId = auhors.ToList()[1].AuthorId,
                ArticleId = 3,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },new Commentary()
            {
                Content="his is my first commentary 3",
                AuthorId = auhors.ToList()[3].AuthorId,
                ArticleId = 3,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
        };
        await blogContext.AddRangeAsync(commentaries);
        await blogContext.SaveChangesAsync();
    }

}
