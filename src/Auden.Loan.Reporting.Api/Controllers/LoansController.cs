using Auden.Loan.Reporting.Application.Services;
using Auden.Loan.Reporting.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auden.Loan.Reporting.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILogger<LoansController> _logger;
        private readonly IReportingService _reportingService;

        public LoansController(ILogger<LoansController> logger, IReportingService reportingService)
        {
            _logger = logger;
            _reportingService = reportingService;
        }

        [HttpGet]
        public async Task<IList<LoanEntity>> Get()
        {
            _logger.LogInformation($"Getting loans issued grouped by Amount.");

            var results = await _reportingService.GetLoansReport();

            _logger.LogInformation($"{results.Count} of results are found.");

            return results;
        }
    }
}
