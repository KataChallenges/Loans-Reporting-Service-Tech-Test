using Auden.Loan.Reporting.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auden.Loan.Reporting.Infrastructure.Repositories
{
    public interface IDataRepository
    {
        Task<IList<LoansReport>> GetDataFromSql();
        Task<IList<LoansReport>> GetDataFromFile();
    }
}
