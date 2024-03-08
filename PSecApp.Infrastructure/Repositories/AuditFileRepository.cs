using PSecApp.Domain.Entities;
using PSecApp.Domain.Interfaces;
using System.Data;
using Dapper;

namespace PSecApp.Infrastructure.Repositories
{
    public class AuditFileRepository : IAuditFileRepository
    {
        private readonly IDbConnection _dbConnection;

        public AuditFileRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Persist audit record to a dabatase
        /// </summary>
        /// <param name="dailyMTMFilesAudit"></param>
        /// <returns></returns>
        public async Task<DailyMTMFilesAudit> AuditFileAsync(DailyMTMFilesAudit dailyMTMFilesAudit)
        {
            string sproc = "Audit_DailyMTMFile";

            var primaryKey =
                await _dbConnection.ExecuteScalarAsync<int>(sproc, dailyMTMFilesAudit, commandType: CommandType.StoredProcedure);

            dailyMTMFilesAudit.Id = (int)primaryKey!;
  
            return dailyMTMFilesAudit;
        }

        /// <summary>
        /// Get single record by filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DailyMTMFilesAudit> GetAuditByFileNameAsync(string filename)
        {
            var results = new DailyMTMFilesAudit();
         
            string query = "SELECT TOP 1 * FROM [dbo].[DailyMTMFilesAudit] WHERE [FileName]=@FileName";

            results = await _dbConnection.QuerySingleOrDefaultAsync<DailyMTMFilesAudit>(query, new { FileName = filename });

            return results ?? new DailyMTMFilesAudit();
        }

        /// <summary>
        /// Get single record by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DailyMTMFilesAudit> GetAuditByIdAsync(string id)
        {
            var results = new DailyMTMFilesAudit();

            string query = "SELECT TOP 1 * FROM [dbo].[DailyMTMFilesAudit] WHERE [Id]=@Id";

            results = await _dbConnection.QuerySingleOrDefaultAsync<DailyMTMFilesAudit>(query, new { Id = id });

            return results ?? new DailyMTMFilesAudit();
        }
    }
}
