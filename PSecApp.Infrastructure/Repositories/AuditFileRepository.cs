using PSecApp.Domain.Entities;
using PSecApp.Domain.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace PSecApp.Infrastructure.Repositories
{
    public class AuditFileRepository : IAuditFileRepository
    {
        /// <summary>
        /// Persist audit record to a dabatase
        /// </summary>
        /// <param name="dailyMTMFilesAudit"></param>
        /// <returns></returns>
        public async Task<DailyMTMFilesAudit> AuditFileAsync(DailyMTMFilesAudit dailyMTMFilesAudit)
        {
            // TODO: MOVE CONNECTION STRING APP.JSON
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=PSecDb;Integrated Security=True;";

            string sproc = "Audit_DailyMTMFile";
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
         
                var primaryKey =
                    await connection.ExecuteScalarAsync<int>(sproc, dailyMTMFilesAudit, commandType: CommandType.StoredProcedure);

                dailyMTMFilesAudit.Id = (int)primaryKey!;
            }

            return dailyMTMFilesAudit;
        }

        /// <summary>
        /// Get a single record by filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DailyMTMFilesAudit> GetAuditByFileNameAsync(string filename)
        {
            // TODO: MOVE CONNECTION STRING APP.JSON
            var results = new DailyMTMFilesAudit();
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=PSecDb;Integrated Security=True;";

            string query = "SELECT TOP 1 * FROM [dbo].[DailyMTMFilesAudit] WHERE [FileName]=@FileName";

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                results = await connection.QuerySingleOrDefaultAsync<DailyMTMFilesAudit>(query, new { FileName = filename });
            }

            return (results != null) ? results : new DailyMTMFilesAudit();
        }

        public async Task<DailyMTMFilesAudit> GetAuditByIdAsync(string id)
        {
            // TODO: MOVE CONNECTION STRING APP.JSON
            var results = new DailyMTMFilesAudit();
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=PSecDb;Integrated Security=True;";

            string query = "SELECT TOP 1 * FROM [dbo].[DailyMTMFilesAudit] WHERE [Id]=@Id";

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                results = await connection.QuerySingleOrDefaultAsync<DailyMTMFilesAudit>(query, new { Id = id });
            }

            return (results != null) ? results : new DailyMTMFilesAudit();
        }
    }
}
