using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Inventory
{
    public class StockLedgerViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_StockProductOpening> ProductOpenings { get; set; }
        public IEnumerable<SP_StockLedgerProductWise> ProcuctLedgers { get; set; }
        public IEnumerable<SP_StockLedgerProducts> Products { get; set; }
        public string ProductCode { get; set; }
        public IEnumerable<SP_StocLedgerGodowns> Godowns { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ProductIds { get; set; }
        public bool? Batch { get; set; }
        public bool? Godown { get; set; }
        public int WithValue { get; set; }
        public int Datetype { get; set; }
    }
}