using PSecApp.Application.Services.Abstractions;
using PSecApp.Domain.Entities;
using PSecApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application.Services.Implementations
{
    public class AuditService : IAuditService
    {
        private readonly IAuditFileRepository _auditFileRepository;
        public AuditService(IAuditFileRepository auditFileRepository)
        {
            _auditFileRepository = auditFileRepository;
        }

        /// <summary>
        /// Save file audit details
        /// </summary>
        /// <param name="audit"></param>
        /// <returns></returns>
        public async Task<DailyMTMFilesAudit> AuditFileAsync(DailyMTMFilesAudit audit)
        {
            return await _auditFileRepository.AuditFileAsync(audit);
        }

        /// <summary>
        /// Get an Audit record by Id
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<DailyMTMFilesAudit> GetFileAuditByFileNameAsync(string fileName)
        {
            return await _auditFileRepository.GetAuditByFileNameAsync(fileName);
        }
    }
}
