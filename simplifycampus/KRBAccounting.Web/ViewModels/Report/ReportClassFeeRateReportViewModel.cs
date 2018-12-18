using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportClassFeeRateReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<List<ScClassFeeRate>> ClassFeeRateGrouping { get; set; }
        public IEnumerable<ScClassFeeRate> ClassFeeRates { get; set; } 
    }
}