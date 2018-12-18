using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Inventory
{
    public class StockLedgerSummaryViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_StockLedgerSummary> StockLedgerSummaries { get; set; }
        public bool Batch { get; set; }
        public int WithValue { get; set; }
    }
}