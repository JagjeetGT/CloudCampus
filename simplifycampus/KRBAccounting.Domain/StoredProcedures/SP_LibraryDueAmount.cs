using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
   public class SP_LibraryDueAmount
    {
        public decimal? TotalDueAmount { get; set; }
        public decimal? PaidDueAmount { get; set; }
        public decimal? RemainingDue { get; set; }
    }
}
