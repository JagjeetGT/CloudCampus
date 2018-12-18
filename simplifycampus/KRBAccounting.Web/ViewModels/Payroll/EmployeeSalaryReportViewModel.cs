using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class EmployeeSalaryReportViewModel: ReportBaseViewModel
    {
        public IEnumerable<ScEmployeeInfo> EmployeeInfos { get; set; }
        public int MonthId { get; set; }
        public int Year { get; set; }
        public int AYear { get; set; }
    }
}