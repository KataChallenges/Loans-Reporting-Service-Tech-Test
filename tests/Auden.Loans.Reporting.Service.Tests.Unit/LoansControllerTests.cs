using Auden.Loan.Reporting.Api.Controllers;
using Auden.Loan.Reporting.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Auden.Loans.Reporting.Service.Tests.Unit
{
    public class LoansControllerTests : BaseControllerTests
    {
        [Theory]
        [InlineData("textfile")]
        [InlineData("database")]
        public async Task GetRecords_Return_OK_With_Valid_Filter_Type(string type)
        {
            var mockLogger = GetMockLogger<LoansController>();
            var mockReportingService = new Mock<IReportingService>();
            mockReportingService.Setup(x => x.GetLoansReport(type)).ReturnsAsync(loansData);
            var controller = new LoansController(mockLogger, mockReportingService.Object);

            var result = await controller.GetRecords(type) as OkObjectResult;

            Assert.NotNull(result.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }


        [Theory]
        [InlineData("123")]
        [InlineData("dummy")]
        [InlineData(null)]
        public async Task GetRecords_Return_BadRequest__With_Invalid_Filter_Type(string type)
        {
            var mockLogger = GetMockLogger<LoansController>();
            var mockReportingService = new Mock<IReportingService>();
            var controller = new LoansController(mockLogger, mockReportingService.Object);



            var result = await controller.GetRecords(type) as BadRequestResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
