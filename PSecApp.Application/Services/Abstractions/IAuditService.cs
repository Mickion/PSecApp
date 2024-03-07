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
        /// Save file audit trail
        /// </summary>
        /// <param name="audit"></param>
        /// <returns></returns>
        Task<DailyMTMFilesAudit> AuditFile(DailyMTMFilesAudit audit);
    }
}
