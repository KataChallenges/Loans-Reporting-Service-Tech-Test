using System.Threading.Tasks;
using Auden.Loan.Reporting.Domain.Models;
using System.Collections.Generic;


namespace Auden.Loan.Reporting.Application.Services
{
    public interface IReportingService
    {
        Task<IList<LoanEntity>> GetLoansReport();
    }
}