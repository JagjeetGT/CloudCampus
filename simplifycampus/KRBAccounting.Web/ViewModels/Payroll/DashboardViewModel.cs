using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class DashboardViewModel
    {
        public IEnumerable<EmployeePostViewModel> EmployeePosts { get; set; }
        public IEnumerable<MonthlySalaryViewModel> MonthlySalaries { get; set; }
        public List<decimal> SalaryPaid { get; set; }
        public List<decimal> SalaryGeneration { get; set; }
    }
}