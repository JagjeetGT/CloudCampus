using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_GetInstallmentPeriod
    {
        public int Id { get; set; }
        public int LoanAmortizationId { get; set; }
        public int AccountId { get; set; }
        public DateTime InstallmentDates { get; set; }
        public decimal InstallmentAmt { get; set; }
        public decimal PayedAmt { get; set; }

    }
}