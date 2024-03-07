using PSecApp.Domain.Entities;
using PSecApp.Domain.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace PSecApp.Infrastructure.Repositories
{
    public class AuditFileRepository : IAuditFileRepository
    {
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
    }
}
