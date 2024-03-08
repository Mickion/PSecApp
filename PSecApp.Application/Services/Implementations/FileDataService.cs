using PSecApp.Application.Services.Abstractions;
using PSecApp.Domain.Entities;
using PSecApp.Domain.Interfaces;

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

        public async Task<bool> SaveFileDataAsync(List<DailyMTM> dataList)
        {
            return await _dailyContractsRepository.InsertFileDataAsyc(dataList);
        }
    }
}
