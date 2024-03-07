using PSecApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Domain.Interfaces
{
    /// <summary>
    /// Persist processed files to a database.
    /// If any files are deleted from the download folder, we can still validate 
    /// </summary>
    public interface IAuditFileRepository
    {
        /// <summary>
        /// Insert or Update an audit entry
        /// </summary>
        /// <param name="dailyMTMFilesAudit"></param>
        /// <returns></returns>
        Task<DailyMTMFilesAudit> AuditFileAsync(DailyMTMFilesAudit dailyMTMFilesAudit);
    }
}
