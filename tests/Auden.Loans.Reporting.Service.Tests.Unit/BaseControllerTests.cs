using Auden.Loan.Reporting.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

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
        public ILogger<T> GetMockLogger<T>()
        {
            return new Mock<ILogger<T>>().Object;
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
