using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBlog.Poc.Core;
using MyBlog.Poc.Core.Entities;

namespace MyBlog.Poc;

public class HostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IBaseRepository<BlogContext> _repository;

    public HostedService(IServiceProvider serviceProvider, IBaseRepository<BlogContext> repository)
    {
        _serviceProvider = serviceProvider;
        _repository = repository;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var service = scope.ServiceProvider;

        await CreateBdIfNotExistAdSeed(service);

        var articleSpec = new ArticleQuerySpecification()
        {
            IsPagingEnabled = true,
            Includes = new()
            {
                a => a.Author,
                a => a.Commentaries,
            },
            Take = 2,
            Skip = 1,
            IsSplitQuery = true,
        };
        var articles = await _repository.AllAsync(articleSpec);


        Console.WriteLine("======================================");
        Console.WriteLine("======================================");
        Console.WriteLine("======================================");
        foreach (var article in articles)
        {
            Console.WriteLine($"\n");
            Console.WriteLine($"============[ ARTICLE {article.ArticleId} ]============");
            Console.WriteLine($"Article title: {article.Title}");
            Console.WriteLine($"Article title: {article.Content}");
            Console.WriteLine($"\n");
            if (article.Author != null)
            {
                Console.WriteLine($"============[ Author {article.AuthorId} ]============");
                Console.WriteLine($"**** {article.Author.LastName} {article.Author.LastName}");
                Console.WriteLine($"**** Created at {article.CreatedAt:d}");
            }
            Console.WriteLine($"\n");
            if (article.Commentaries.Any())
            {
                CommentaryQuerySpecification commentaryQuerySpecification = new(article.ArticleId);
                await _repository.AllAsync(commentaryQuerySpecification);
                Console.WriteLine($"\n");
                Console.WriteLine($"============[ Article n° {article.ArticleId} commentaries ]============");
                foreach (var commentary in article.Commentaries)
                {
                    Console.WriteLine($"**** Created by {commentary.Author.FirstName} {commentary.Author.LastName} at {commentary.CreatedAt:d}");
                    Console.WriteLine($"**** Content: {commentary.Content}");
                }
            }
            Console.WriteLine($"\n");
            Console.WriteLine("============[ ARTICLE END]===========");
            Console.WriteLine($"\n");

        }
        Console.WriteLine("======================================");
        Console.WriteLine("======================================");
        Console.WriteLine("======================================");
        Console.WriteLine($"\n");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    private async Task CreateBdIfNotExistAdSeed(IServiceProvider service)
    {
        try
        {
            await DbInitializer.InitialDatabaseAsync(
                service.GetRequiredService<BlogContext>()
                );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}