using Auden.Loan.Reporting.Domain.Models;
using Auden.Loan.Reporting.Infrastructure.Database;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Auden.Loan.Reporting.Infrastructure.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly ILogger<DataRepository> _logger;
        private readonly IOptions<DataSourceSettings> _sourceConfig;
        private List<LoansReport> reportingList = new List<LoansReport>();

        public DataRepository(ILogger<DataRepository> logger, IOptions<DataSourceSettings> sourceConfig)
        {
            _logger = logger;
            _sourceConfig = sourceConfig;
        }

        public async Task<IList<LoansReport>> GetDataFromFile()
        {
            string[] records = await File.ReadAllLinesAsync($"{Environment.CurrentDirectory}{_sourceConfig.Value.FileLocation}");

            foreach (string record in records)
            {
                var loanId = record.Substring(0, 19).Trim();
                var customerId = record.Substring(20, 29).Trim();
                var loanAmount = record.Substring(50, 10).Trim().TrimStart('0');
                var createdDate = record.Substring(60, 8).Trim();
                var item = new LoansReport()
                {
                    Amount = loanAmount != "" ? $"£{loanAmount}" : null,
                    TotalRecords = 0
                };
                reportingList.Add(item);
            }
            var results = reportingList.GroupBy(loan => loan.Amount)
                        .Select(loan => new LoansReport()
                        {
                            Amount = loan.Key,
                            TotalRecords = loan.Count()
                        })
                        .OrderBy(x => x.Amount);

            return results.ToList();
        }

        public async Task<IList<LoansReport>> GetDataFromSql()
        {
            try
            {
                var connection = new SqliteConnection(_sourceConfig.Value.DatabaseConnection);
                connection.Open();
                string query = "Select Amount, Count(Amount) as total from loans where amount is not null group by Amount order by Amount;";
                var command = new SqliteCommand(query, connection);
                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var item = new LoansReport()
                    {
                        Amount = reader["amount"].ToString() != "" ? $"£{reader["amount"]}" : null,
                        TotalRecords = int.Parse(reader["total"].ToString())
                    };
                    reportingList.Add(item);
                }
                connection.Close();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);
                throw new Exception("Something went wrong. Please check logs.");
            }

            return reportingList;
        }
    }
}
