using Auden.Loan.Reporting.Domain.Models;
using Auden.Loan.Reporting.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IList<LoansReport>> GetLoansReport(string dataType)
        {
            if (dataType != "textfile" && dataType != "database")
            {
                throw new Exception();
            }

            var results = dataType == "database" ? await _dataRepository.GetDataFromSql() : await _dataRepository.GetDataFromFile();

            return results;
        }
    }
}