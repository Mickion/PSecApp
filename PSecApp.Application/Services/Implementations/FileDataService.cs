using PSecApp.Application.Models;
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
    /// <summary>
    /// Commits file content & nothing else (SRP)
    /// </summary>
    public class FileDataService : IFileDataService
    {

        private readonly IDailyContractsRepository _dailyContractsRepository;

        public FileDataService(IDailyContractsRepository dailyContractsRepository)
        {
            _dailyContractsRepository = dailyContractsRepository;
        }
        public Task<List<DailyMTM>> GetAllFileData()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveFileDataAsync(List<DailyMTM> dataList)
        {
            return await _dailyContractsRepository.InsertFileDataAsyc(dataList);
        }
    }
}
