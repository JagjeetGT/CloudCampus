using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_LoanPaymentDetails
    {
        public DateTime PaidDate { get; set; }
        public string PaidMiti { get; set; }
        public string Description { get; set; }
        public decimal PaidPrincipal { get; set; }
        public decimal PaidInterest { get; set; }
        public decimal PaidPenalty { get; set; }
        public string Collector { get; set; }
    }
}
