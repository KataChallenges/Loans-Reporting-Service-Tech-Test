using Auden.Loan.Reporting.Application.Services;
using Auden.Loan.Reporting.Domain.Models;
using Auden.Loan.Reporting.Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Auden.Loans.Reporting.Application.Tests.Unit
{
    public class ReportingServiceTests
    {
        public IList<LoansReport> loansData = new List<LoansReport>()
        {
            new LoansReport(){
                Amount = "£100",
                TotalRecords = 22
                }
        };

        [Theory]
        [InlineData("invalid")]
        [InlineData("0000")]
        public async Task GetLoansReport_Throws_Exception_If_Invalid_TypeAsync(string type)
        {
            var mockReposiory = new Mock<IDataRepository>();
            var sut = new ReportingService(mockReposiory.Object);

            await Assert.ThrowsAnyAsync<Exception>(
                          () => sut.GetLoansReport(type));
        }


        [Theory]
        [InlineData("database")]
        [InlineData("textfile")]
        public async Task GetLoansReport_Return_Valid_Data(string type)
        {
            var mockReposiory = new Mock<IDataRepository>();
            if (type == "database")
            {
                mockReposiory.Setup(a => a.GetDataFromSql()).ReturnsAsync(loansData);
            }
            else
            {
                mockReposiory.Setup(a => a.GetDataFromFile()).ReturnsAsync(loansData);
            }

            var sut = new ReportingService(mockReposiory.Object);
            var result = await sut.GetLoansReport(type);

            Assert.NotNull(result);

            if (type == "database")
            {
                mockReposiory.Verify(x => x.GetDataFromSql(), Times.Once);
                mockReposiory.Verify(x => x.GetDataFromFile(), Times.Never);
            }
            else
            {
                mockReposiory.Verify(x => x.GetDataFromFile(), Times.Once);
                mockReposiory.Verify(x => x.GetDataFromSql(), Times.Never);
            }
        }
    }
}
