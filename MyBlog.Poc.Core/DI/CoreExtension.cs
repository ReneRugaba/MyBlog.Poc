using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Poc.Core.Entities;

namespace MyBlog.Poc.Core.DI;

public static class CoreExtension
{
    public static string connectionString { get; set; } = "Data Source=SF298;Initial Catalog=myblog;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    public static IServiceCollection ConfigureCoreServices(this IServiceCollection services)
    {
        services.AddDbContext<BlogContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        }).AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        return services;
    }
}
