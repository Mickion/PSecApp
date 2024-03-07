using PSecApp.Domain.Entities;
using PSecApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace PSecApp.Infrastructure.Repositories
{
    public class DailyContractsRepository : IDailyContractsRepository
    {
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
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=PSecDb;Integrated Security=True;";

            string sproc = "Insert_DailyMTM";
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // Using Transaction as I'm processing the entire file,
                // if any one record fails to insert, roll back the entire file;
                await using (var transaction = connection.BeginTransaction())
                {
                    foreach (var data in dataList)
                    {
                        var primaryKey = 
                            await connection.ExecuteScalarAsync<int>(sproc, data, transaction, commandType: CommandType.StoredProcedure);
                        data.Id = (int)primaryKey!;
                    }

                    if(dataList.All(p => p.Id > 0))                   
                        await transaction.CommitAsync();                   
                    else                   
                        // roll the transaction back
                        await transaction.RollbackAsync();                                           

                }
            }
            


            return dataList.All(p => p.Id > 0);            
        }
    }
}
