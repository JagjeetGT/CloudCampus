﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportStudentFeeRateReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<List<ScStudentFeeRateMaster>> StudentFeeRateGrouping { get; set; }
        public IEnumerable<ScStudentFeeRateMaster> StudentFeeRates { get; set; } 
    }
}