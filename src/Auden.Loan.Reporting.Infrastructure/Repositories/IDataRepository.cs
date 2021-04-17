using Auden.Loan.Reporting.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auden.Loan.Reporting.Infrastructure.Repositories
{
    public interface IDataRepository
    {
        Task<IList<LoanEntity>> GetDataFromSql();
        Task<IList<LoanEntity>> GetDataFromFile();
    }
}
