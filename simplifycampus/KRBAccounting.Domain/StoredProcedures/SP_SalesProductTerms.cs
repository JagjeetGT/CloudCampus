using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_SalesProductTerms
    {
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public decimal TermAmount { get; set; }
    }
}
