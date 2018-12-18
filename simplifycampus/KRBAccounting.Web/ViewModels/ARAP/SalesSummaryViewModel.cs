using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.ARAP
{
    public class SalesSummaryViewModel : ReportBaseViewModel
    {
        public DataTable Report { get; set; }
        public IEnumerable<SP_SalesDetail> SalesDetails { get; set; }
        public IEnumerable<SP_SalesGodownHeader> SalesGodownHeaders { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool Detail { get; set; }
        public bool Godown { get; set; }
        public bool DisplayRemarks { get; set; }
        public int Datetype { get; set; }
        public int Groupby { get; set; }
        public int BranchId { get; set; }
    }
}