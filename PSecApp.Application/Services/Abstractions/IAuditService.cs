using PSecApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application.Services.Abstractions
{
    public interface IAuditService
    {
        /// <summary>
        /// Update file audit trail
        /// </summary>
        /// <param name="audit"></param>
        /// <returns></returns>
        Task<DailyMTMFilesAudit> AuditFileAsync(DailyMTMFilesAudit audit);

        /// <summary>
        /// Update file audit trail
        /// </summary>
        /// <param name="audit"></param>
        /// <returns></returns>
        Task<DailyMTMFilesAudit> GetFileAuditByFileNameAsync(string fileName);
    }
}
