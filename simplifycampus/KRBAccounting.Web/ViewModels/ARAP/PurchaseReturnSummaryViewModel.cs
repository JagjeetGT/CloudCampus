using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.ARAP
{
    public class PurchaseReturnSummaryViewModel : ReportBaseViewModel
    {
        public DataTable Report { get; set; }

        public IEnumerable<SP_PurchaseReturnDetail> PurchaseReturnDetails { get; set; }
        public IEnumerable<SP_PurchaseReturnGodownHeader> PurchaseReturnGodownHeaders { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool DateShow { get; set; }
        public bool Godown { get; set; }
        public int Datetype { get; set; }
        public int Groupby { get; set; }

        public bool DisplayRemarks { get; set; }
    }
}