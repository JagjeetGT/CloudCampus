using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.ARAP
{
    public class SalesChallanSummaryViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_SalesChallanDetails> SalesChallan { get; set; }
        public IEnumerable<SP_SalesGodownHeader> SalesGodownHeaders { get; set; }
        public DataTable Report { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int GroupBy { get; set; }
        public bool DateShow { get; set; }
        public bool Detail { get; set; }
        public bool Godown { get; set; }
        public bool DisplayRemarks { get; set; }
        public int Datetype { get; set; }



    }
}

