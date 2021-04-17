using Auden.Loan.Reporting.Domain.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Auden.Loan.Reporting.Infrastructure.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly ILogger<DataRepository> _logger;
        private List<LoansReport> reportingList = new List<LoansReport>();

        public DataRepository(ILogger<DataRepository> logger)
        {
            _logger = logger;
        }

        public Task<IList<LoansReport>> GetDataFromFile()
        {
            string[] records = File.ReadAllLines($"{Environment.CurrentDirectory}\\DataSource\\data.txt");

            foreach (string line in records)
            {
                var test = line;
            }
            return null;
        }

        public async Task<IList<LoansReport>> GetDataFromSql()
        {
            string cs = $"Data Source=DataSource\\data.db;";

            using var con = new SqliteConnection(cs);
            con.Open();

            //string stm = "SELECT SQLITE_VERSION()";
            //using var cmd = new SqliteCommand(stm, con);
            //string version = cmd.ExecuteScalar().ToString();

            string query = "Select Amount, Count(Amount) as total from loans group by Amount;";
            using var command = new SqliteCommand(query, con);
            var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                try
                {
                    var item = new LoansReport()
                    {
                        Amount = reader["amount"].ToString() != "" ? $"£{reader["amount"]}" : null,
                        TotalRecords = int.Parse(reader["total"].ToString())
                    };
                    reportingList.Add(item);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message, exception);
                }
            }

            con.Close();


            return reportingList;
        }
    }
}
