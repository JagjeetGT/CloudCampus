using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.ARAP
{
    public class SalesReturnSummaryViewModel : ReportBaseViewModel
    {
        public DataTable Report { get; set; }
        public IEnumerable<SP_SalesReturnDetail> SalesReturnDetails { get; set; }
        public IEnumerable<SP_SalesReturnGodownHeader> SalesReturnGodownHeaders { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool Godown { get; set; }
        public int GroupBy { get; set; }
        public bool DateShow { get; set; }
        public bool Detail { get; set; }
       
        public bool DisplayRemarks { get; set; }
        public int Datetype { get; set; }
    }
}