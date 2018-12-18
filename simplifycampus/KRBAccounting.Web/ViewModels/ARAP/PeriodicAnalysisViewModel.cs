using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.ARAP
{
    public class PeriodicAnalysisViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_AnalysisMonthly> AnalysisMonthly { get; set; }
        public IEnumerable<SP_AnalysisYearly> AnalysisYearly { get; set; }
        public int Type { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Divisor { get; set; }

    }
}