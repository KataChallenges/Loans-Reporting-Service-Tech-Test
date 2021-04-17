using System;

namespace Auden.Loan.Reporting.Domain.Models
{
    public class LoanEntity
    {
        public string Id { get; set; }
        public string CUstomerId { get; set; }
        public int? Amount { get; set; }
        public DateTime Date { get; set; }
    }
}