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

        //var page = 2;

        var articleSpec = new ArticleQuerySpecification(3);
        var article = await _repository.FindByAsync(articleSpec);

        var commentarySpec = new CommentaryQuerySpecification(article.ArticleId);
        await _repository.AllAsync(commentarySpec);


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
        foreach (var com in article.Commentaries)
        {
            Console.WriteLine($"\n============[ Commentary of Acrticle {article.ArticleId} ]============");
            Console.WriteLine($"**** Created by {com.Author.FirstName} {com.Author.LastName} at {com.CreatedAt:d}");
            Console.WriteLine($"**** Content: {com.Content}");
        }

        Console.WriteLine($"\n");
        Console.WriteLine("============[ ARTICLE END]===========");
        Console.WriteLine($"\n");


        //Console.WriteLine("======================================");
        //Console.WriteLine("======================================");
        //Console.WriteLine("======================================");
        //foreach (var article in articles)
        //{
        //    Console.WriteLine($"\n");
        //    Console.WriteLine($"============[ ARTICLE {article.ArticleId} ]============");
        //    Console.WriteLine($"Article title: {article.Title}");
        //    Console.WriteLine($"Article title: {article.Content}");
        //    Console.WriteLine($"\n");
        //    if (article.Author != null)
        //    {
        //        Console.WriteLine($"============[ Author {article.AuthorId} ]============");
        //        Console.WriteLine($"**** {article.Author.LastName} {article.Author.LastName}");
        //        Console.WriteLine($"**** Created at {article.CreatedAt:d}");
        //    }

        //    Console.WriteLine($"\n");
        //    Console.WriteLine("============[ ARTICLE END]===========");
        //    Console.WriteLine($"\n");

        //}
        //Console.WriteLine("======================================");
        //Console.WriteLine("======================================");
        //Console.WriteLine("======================================");
        //Console.WriteLine($"\n");
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