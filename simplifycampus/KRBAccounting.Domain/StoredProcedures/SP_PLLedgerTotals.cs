using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_PLLedgerTotals
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string AccountName { get; set; }
        public int? ACSGrpCode { get; set; }
        public string ACSGrpName { get; set; }
        public int? SLCode { get; set; }
        public string SLName { get; set; }
        public decimal Total { get; set; }

    }
}
