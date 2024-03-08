using Microsoft.Extensions.DependencyInjection;
using PSecApp.Application.POCOs;
using PSecApp.Application.Services.Abstractions;
using PSecApp.Application.Services.Implementations;
using PSecApp.Domain.Entities;
using PSecApp.Domain.Interfaces;
using PSecApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application
{
    public class ApplicationDependencies
    {
        /// <summary>
        /// Dependency Injection for the Application Layer
        /// </summary>
        /// <param name="services"></param>
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddTransient<IAuditService, AuditService>();
            services.AddTransient<IFileValidatorService, FileValidatorService>();
            services.AddTransient<IFileDownloadService, FileDownloadService>();
            services.AddTransient<IFileDataService, FileDataService>();
            services.AddTransient<IFileReaderService<DailyMTM, DownloadFile>, FileReaderService>();

            services.AddTransient<IFileDataService, FileDataService>();

        }
    }
}
