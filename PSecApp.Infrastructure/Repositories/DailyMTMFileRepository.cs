using PSecApp.Domain.Entities;
using PSecApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Infrastructure.Repositories
{
    internal class DailyMTMFileRepository : IDailyMTMFileRepository
    {
        public List<DailyMTMFile> GetAllDownloadedFiles()
        {
            throw new NotImplementedException();
        }

        public List<DailyMTMFile> GetAllUnprocessedFiles()
        {
            throw new NotImplementedException();
        }

        public DailyMTMFile GetFileById(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public bool MarkFileAsDownloaded(DailyMTMFile dailyMTMFile)
        {
            throw new NotImplementedException();
        }

        public bool MarkFileAsProcessed(DailyMTMFile dailyMTMFile)
        {
            throw new NotImplementedException();
        }
    }
}
