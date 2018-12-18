using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_PartyLedgersSummary
    {
        public int GlCode { get; set; }
        public string Ledger { get; set; }
        public int SlCode { get; set; }
        public string SLName { get; set; }
        public decimal OpnDr { get; set; }
        public decimal OpnCr { get; set; }
        public decimal PeriodDr { get; set; }
        public decimal PeriodCr { get; set; }
        public decimal CloseDr { get; set; }
        public decimal CloseCr { get; set; }
        
    }
}
