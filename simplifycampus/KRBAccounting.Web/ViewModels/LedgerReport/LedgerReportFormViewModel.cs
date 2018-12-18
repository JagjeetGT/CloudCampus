using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KRBAccounting.Service.Helpers;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class LedgerReportFormViewModel : BaseViewModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool AllLedger { get; set; }
        public bool SubLedger { get; set; }
        public bool AllSubLedger { get; set; }
        public bool Remarks { get; set; }
        public bool ZeroBalance { get; set; }
        public bool Summary { get; set; }
        public bool ProductDetails { get; set; }
        public bool TermDetails { get; set; }
        public int ReportType { get; set; }
        public int Opening { get; set; }
        public int GroupBy { get; set; }
        public SelectList ReportTypeList { get; set; }
        public SelectList GroupByList { get; set; }
        public SelectList OpeningList { get; set; }
        public bool DateShow { get; set; }
        public bool DocAgent { get; set; }
    }
}