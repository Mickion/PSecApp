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

        public Task<DailyMTMFilesAudit> AuditFile(DailyMTMFilesAudit audit)
        {
            throw new NotImplementedException();
        }
    }
}
