using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_CashFlow
    {
        public DateTime vdate { get; set; }
        public decimal OutTotal { get; set; }
        public decimal InTotal { get; set; }
    }
}
