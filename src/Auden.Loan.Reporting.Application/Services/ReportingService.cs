using Auden.Loan.Reporting.Domain.Models;
using Auden.Loan.Reporting.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auden.Loan.Reporting.Application.Services
{
    public class ReportingService : IReportingService
    {
        private readonly IDataRepository _dataRepository;

        public ReportingService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public Task<IList<LoanEntity>> GetLoansReport()
        {
            throw new System.NotImplementedException();
        }
    }
}