using Auden.Loan.Reporting.Application.Services;
using Auden.Loan.Reporting.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
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

        [HttpGet("{dataType}")]
        [Consumes("application/vnd.uk.co.auden.loans.database.aggregate.list-v1+json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IList<LoansReport>))]
        public async Task<IActionResult> GetRecords(string dataType)
        {
            if (dataType != null && (dataType.ToLower() == "textfile" || dataType.ToLower() == "database"))
            {
                _logger.LogInformation($"Getting loans detail grouped by Amount using {nameof(dataType)}.");
                var results = await _reportingService.GetLoansReport(dataType);
                _logger.LogInformation($"{nameof(results.Count)} number of records are found.");
                return Ok(results);
            }
            return BadRequest();
        }
    }
}
