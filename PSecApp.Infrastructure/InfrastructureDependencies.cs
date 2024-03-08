using Microsoft.Extensions.DependencyInjection;
using PSecApp.Domain.Interfaces;
using PSecApp.Infrastructure.Repositories;

namespace PSecApp.Infrastructure
{
    public static class InfrastructureDependencies
    {
        /// <summary>
        /// Dependency Injection for the Infrastructure Layer
        /// </summary>
        /// <param name="services"></param>
        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddTransient<IAuditFileRepository, AuditFileRepository>();
            services.AddTransient<IDailyContractsRepository, DailyContractsRepository>();
        }
    }
}
