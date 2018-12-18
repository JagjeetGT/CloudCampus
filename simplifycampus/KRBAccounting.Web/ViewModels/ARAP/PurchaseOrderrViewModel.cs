using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.ARAP
{
    public class PurchaseOrderViewModel : ReportBaseViewModel
    {
        public DataTable Report { get; set; }

        public IEnumerable<SP_PurchaseOrderDetails> PurchaseDetails { get; set; }
        public IEnumerable<SP_PurchaseGodownHeader> PurchaseGodownHeaders { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public bool Godown { get; set; }
        public bool DisplayRemarks { get; set; }
        public int Datetype { get; set; }
        public int Groupby { get; set; }
      
    }
}