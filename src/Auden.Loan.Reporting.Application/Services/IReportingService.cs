using Auden.Loan.Reporting.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Auden.Loan.Reporting.Application.Services
{
    public interface IReportingService
    {
        Task<IList<LoansReport>> GetLoansReport(string dataType);
    }
}