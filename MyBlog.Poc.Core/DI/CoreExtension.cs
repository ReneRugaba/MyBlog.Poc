using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Poc.Core.Entities;

namespace MyBlog.Poc.Core.DI;

public static class CoreExtension
{
    private static readonly string connectionString = "<Set your connectionString here>";
    public static IServiceCollection ConfigureCoreServices(this IServiceCollection services)
    {
        services.AddDbContext<BlogContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        }).AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        return services;
    }
}
