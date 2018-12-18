using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportStaffListReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScEmployeeInfo> StaffList { get; set; }
    }
}