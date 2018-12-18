using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.ARAP
{
    public class OutStandingViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_Ledgers> LedgerList { get; set; }
        public string AsOnDate { get; set; }
}
}