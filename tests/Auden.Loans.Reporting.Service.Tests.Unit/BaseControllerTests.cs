using Auden.Loan.Reporting.Domain.Models;
using Auden.Loan.Reporting.Infrastructure.Database;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Auden.Loans.Reporting.Service.Tests.Unit
{
    public class BaseControllerTests
    {
        public IList<LoansReport> loansData = new List<LoansReport>()
        {
            new LoansReport(){
                Amount = "£100",
                TotalRecords = 22
                }
        };

        public string loansStringData = "[{\"amount\": \"£101\",\"totalRecords\": 30 }, {\"amount\": \"£102\",\"totalRecords\": 33},{\"amount\": \"£XXX\",\"totalRecords\": 1 }]";
        public static IOptions<DataSourceSettings> BuildSettings()
        {
            var settings = new DataSourceSettings
            {
                DatabaseConnection = "Data Source=data.db;",
                FileLocation = "\\data.txt"
            };

            return Options.Create(settings);
        }


        public ILogger<T> GetMockLogger<T>()
        {
            return new Mock<ILogger<T>>().Object;
        }



        public async Task SetupInMemoryDatabase(string connectionString)
        {
            using var connection = new SqliteConnection(connectionString);

            var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'main';");
            var tableName = table.FirstOrDefault();
            if (!string.IsNullOrEmpty(tableName) && tableName.ToLower() == "loans")
            {
                return;
            }

            connection.Execute("CREATE TABLE loans (id TEXT PRIMARY KEY, customerId TEXT, amount INT, createdDate DATE)");
        }


        public HttpClient MockHttpClient(HttpStatusCode statusCode)
        {
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = statusCode,
                    Content = new StringContent(loansStringData)
                })
                .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://auden.loans.api.proxy/")
            };

            return httpClient;
        }
    }
}
