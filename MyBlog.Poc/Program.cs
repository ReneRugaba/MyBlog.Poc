using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBlog.Poc;
using MyBlog.Poc.Core.DI;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((hCtx, sericeCollection) =>
    {
        sericeCollection
            .ConfigureCoreServices()
            .AddHostedService<HostedService>();
    }).Build();

await host.RunAsync();