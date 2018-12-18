using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_GetIsFixedAccount
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AccountHolderName { get; set; }
        public string DepositName { get; set; }
        public decimal DepositedAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int TimeSpan { get; set; }
        public string MaturityDate { get; set; }
        public string TimePeriodIn { get; set; }
    }
}
