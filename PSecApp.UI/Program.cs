using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSecApp.Application.Services.Abstractions;
using PSecApp.UI;

// get settings used in the app
// db connection, where to get files from etc.

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

// register all the services that are used
// by this application with their implementation

var services = new ServiceCollection();
services.ConfigureInfrastructureServices(configuration);
services.ConfigureApplicationServices(configuration);

using var serviceProvider = services.BuildServiceProvider();

try
{
    // get entry service
    var app = serviceProvider.GetService<IOrchestratorService>() 
        ?? throw new ArgumentNullException("serviceProvider failed to return");

    await app.ProcessFilesAsync(2024);
}
catch(Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(ex.ToString());
}

Console.ReadLine();