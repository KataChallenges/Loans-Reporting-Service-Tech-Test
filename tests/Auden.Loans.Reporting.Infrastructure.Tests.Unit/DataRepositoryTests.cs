using Auden.Loan.Reporting.Infrastructure.Repositories;
using Auden.Loans.Reporting.Service.Tests.Unit;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Auden.Loans.Reporting.Infrastructure.Tests.Unit
{
    public class DataRepositoryTests : BaseControllerTests
    {
        [Fact]
        public async Task GetDataFromFile_Returns_Correct_DataAsync()
        {
            var mockLogger = GetMockLogger<DataRepository>();
            var mockSettings = BuildSettings();
            var sut = new DataRepository(mockLogger, mockSettings);

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            var result = await sut.GetDataFromFile();

            Assert.NotNull(result);
            Assert.Equal(400, result.Count);
        }


        [Fact]
        public async Task GetDataFromSql_Returns_Correct_DataAsync()
        {
            var mockLogger = GetMockLogger<DataRepository>();
            var mockSettings = BuildSettings();
            var sut = new DataRepository(mockLogger, mockSettings);

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            var result = await sut.GetDataFromSql();

            Assert.NotNull(result);
            Assert.Equal(399, result.Count);
        }
    }
}
