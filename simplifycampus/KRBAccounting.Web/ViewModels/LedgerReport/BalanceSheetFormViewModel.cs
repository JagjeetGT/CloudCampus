using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class BalanceSheetFormViewModel : BaseViewModel
    {

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string AsOnDate { get; set; }
        public SelectList ReportTypeList { get; set; }
        public SelectList ReportFormatList { get; set; }
        [Required(ErrorMessage = " ")]
        public int ReportType { get; set; }
        public int ReportFormat { get; set; }
        public string ReportShow { get; set; }
        public SelectList ReportShowList { get; set; }
        public bool AccountGroup { get; set; }
        public bool AccountSubGroup { get; set; }
        public bool Ledger { get; set; }
        public bool SubLedger { get; set; }
        public bool DateShow { get; set; }
    }
}