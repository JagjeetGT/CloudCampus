using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_AmountDepositedDate
    {
        public int AccountId { get; set; }
        public string AccountHolderName { get; set; }
        public decimal DepositedAmount { get; set; }
        public string Remarks { get; set; }
        public DateTime DepositedOn { get; set; }
        public int FYId { get; set; }
    }
}
