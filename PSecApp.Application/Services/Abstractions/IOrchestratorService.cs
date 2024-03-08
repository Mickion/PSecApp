using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application.Services.Abstractions
{
    public interface IOrchestratorService
    {
        /// <summary>
        /// Download and process files
        /// </summary>
        /// <param name="forYear"></param>
        /// <returns></returns>
        Task ProcessFilesAsync(int forYear);

        /// <summary>
        /// Download and process files using other services
        /// </summary>
        /// <param name="forYear"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        Task ProcessFilesAsync(int forYear, string source, string destination);
    }
}
