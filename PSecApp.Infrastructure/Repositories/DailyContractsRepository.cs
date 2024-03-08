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

        //static IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString);

        /// <summary>
        /// Persists file data into a dababase
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> InsertFileDataAsyc(List<DailyMTM> dataList)
        {
            // TODO: MOVE CONNECTION STRING APP.JSON
            //string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=PSecDb;Integrated Security=True;";

            string sproc = "Insert_DailyMTM";
            //await using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    await connection.OpenAsync();

            //var transaction = _dbConnection.BeginTransaction();
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
