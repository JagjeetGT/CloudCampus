using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_OpeningTrialBalance
    {
        public int LedgerId { get; set; }
        public int AccountGroupId { get; set; }
        public string AccountName { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public string Type { get; set; }
    }
}
