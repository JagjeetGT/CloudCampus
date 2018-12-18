using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;
using KRBAccounting.Web.ViewModels;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class BalanceSheetViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_BSLedgerWise> BsLedgerWiseList { get; set; }
        public IEnumerable<SP_BSAccountGroupWise> BsAccountGroupWiseList { get; set; }
        public IEnumerable<SP_BSPeriodic> BsPeriodicList { get; set; }
        public string ReportShow { get; set; }
        public bool AccountGroup { get; set; }
        public bool Ledger { get; set; }
        public bool Subledger { get; set; }
        public bool AccountSubGroup { get; set; }
    }
}