using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSecApp.Application.Services.Abstractions;
using PSecApp.UI;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var services = new ServiceCollection();
services.ConfigureInfrastructureServices(configuration);
services.ConfigureApplicationServices(configuration);

using var serviceProvider = services.BuildServiceProvider();

var app = serviceProvider.GetService<IOrchestratorService>();

await app.ProcessFilesAsync(2024);
