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

        public async Task<DailyMTMFilesAudit> AuditFileAsync(DailyMTMFilesAudit audit)
        {
            //var fileFound = await _auditFileRepository.GetAuditByFileNameAsync(audit.FileName);

            //if (fileFound != null)
            //{
            //    audit.Id = fileFound.Id; // Update existing record
            //}

            return await _auditFileRepository.AuditFileAsync(audit);
        }

        public async Task<DailyMTMFilesAudit> GetFileAuditByFileNameAsync(string fileName)
        {
            return await _auditFileRepository.GetAuditByFileNameAsync(fileName);
        }
    }
}
