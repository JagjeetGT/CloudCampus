using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_LoanMaturation
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal DisbursedAmount { get; set; }
        public int LoanPeriod { get; set; }
        public DateTime LoanStartDate { get; set; }
        public DateTime MaturedDate { get; set; }
        public string AccountHolderName { get; set; }
    }
}
