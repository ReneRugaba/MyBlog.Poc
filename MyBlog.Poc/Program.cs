

using Microsoft.Extensions.Hosting;
using MyBlog.Poc.Core.DI;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((hCtx, sericeCollection) =>
    {
        sericeCollection.ConfigureCoreServices();
    }).Build();
await host.RunAsync();