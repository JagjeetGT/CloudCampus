using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class Sp_PurchaseBillTerms
    {
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public decimal TermAmount { get; set; }
    }
}
