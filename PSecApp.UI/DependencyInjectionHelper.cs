using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSecApp.Application.POCOs;
using PSecApp.Application.Services.Abstractions;
using PSecApp.Application.Services.Implementations;
using PSecApp.Domain.Entities;
using PSecApp.Domain.Interfaces;
using PSecApp.Infrastructure.Repositories;
using System.Data;
using System.Data.SqlClient;

namespace PSecApp.UI
{
    public static class DependencyInjectionHelper
    {
        public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(options => configuration.GetSection("ApplicationOptions").Bind(options));

            var tsts = configuration.GetSection("ApplicationOptions").Key;

            string? dbConnectionStr = configuration.GetSection("ApplicationOptions")["ConnectionString"];

            // inject IDbConnection to be used by infrastructure
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(dbConnectionStr));

            services.AddTransient<IAuditFileRepository, AuditFileRepository>();
            services.AddTransient<IDailyContractsRepository, DailyContractsRepository>();
        }

        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(options => configuration.GetSection("ApplicationOptions").Bind(options));

            services.AddTransient<IAuditService, AuditService>();
            services.AddTransient<IFileDataService, FileDataService>();
            services.AddTransient<IFileValidatorService, FileValidatorService>();
            services.AddTransient<IFileDownloadService, FileDownloadService>();
  
            services.AddTransient<IFileReaderService<DailyMTM, DownloadFile>, FileReaderService>();

            services.AddTransient<IFileDataService, FileDataService>();

            services.AddSingleton<IOrchestratorService, OrchestratorService>();
        }
    }
}
