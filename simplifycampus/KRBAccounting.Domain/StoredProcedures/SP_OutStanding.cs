using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
   public class SP_OutStanding
    {
        public int LedgerId { get; set; }
        public string VoucherNo { get; set; }
        public string Source { get; set; }
        public DateTime VoucherDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Adjusted { get; set; }
        public decimal Balance { get; set; }
        public DateTime DueDate { get; set; }
        public int DueAge { get; set; }
    }
}
