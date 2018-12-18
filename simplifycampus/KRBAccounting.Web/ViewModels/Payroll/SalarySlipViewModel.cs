using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class SalarySlipViewModel
    {
        public int DepartmentId { get; set; }
        public List<int> EmployeeId { get; set; }
        public SelectList EmployeeList { get; set; }
        public SelectList DepartmentList { get; set; }
        public int MonthId { get; set; }
        public int MonthMitiId { get; set; }
        public SelectList MonthList { get; set; }
 
    }
}