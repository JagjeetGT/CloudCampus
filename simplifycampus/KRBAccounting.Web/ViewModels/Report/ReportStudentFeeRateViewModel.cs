using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportStudentFeeRateViewModel
    {
        public IEnumerable<ScStudentFeeRateMaster> StudentFeeRateMasters { get; set; }
    }
}