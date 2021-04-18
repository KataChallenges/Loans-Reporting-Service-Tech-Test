using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Auden.Loan.Reporting.Infrastructure.Database
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly IOptions<DataSourceSettings> _sourceConfig;

        public DatabaseContext(IOptions<DataSourceSettings> sourceConfig)
        {
            _sourceConfig = sourceConfig;
        }

        public void Setup()
        {
            using var connection = new SqliteConnection(_sourceConfig.Value.DatabaseConnection);

            var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'loans';");
            var tableName = table.FirstOrDefault();
            if (!string.IsNullOrEmpty(tableName) && tableName.ToLower() == "loans")
            {
                return;
            }

            connection.Execute("CREATE TABLE loans (id TEXT PRIMARY KEY, customerId TEXT, amount INT, createdDate DATE)");
        }
    }
}
