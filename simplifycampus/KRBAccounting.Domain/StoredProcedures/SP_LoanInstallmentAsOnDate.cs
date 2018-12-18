using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_LoanInstallmentAsOnDate
    {
        public int AccountId { get; set; }
        public string AccountHolderName { get; set; }
        public string LoanName { get; set; }
        public DateTime InstallmentDates { get; set; }
        public decimal InstallmentAmt { get; set; }
        public decimal InterestAmt { get; set; }
        public decimal Balance { get; set; }
        public bool PaymentFlag { get; set; }
        public string Remarks { get; set; }
    }
}
