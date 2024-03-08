using PSecApp.Domain.Entities;
using PSecApp.Domain.Interfaces;
using System.Data;
using Dapper;

namespace PSecApp.Infrastructure.Repositories
{
    public class DailyContractsRepository : IDailyContractsRepository
    {
        private readonly IDbConnection _dbConnection;

        public DailyContractsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        
        /// <summary>
        /// Persists file data into a dababase
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> InsertFileDataAsyc(List<DailyMTM> dataList)
        {
            string sproc = "Insert_DailyMTM";

            foreach (var data in dataList)
            {
                var primaryKey =
                   await _dbConnection.ExecuteScalarAsync<int>(sproc, data, commandType: CommandType.StoredProcedure);

                data.Id = (int)primaryKey!;
            }

            return dataList.All(p => p.Id > 0);
        }
    }
}
