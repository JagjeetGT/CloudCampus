using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class PayementSlipViewModel : BaseViewModel
    {
        public List<SelectListItem> DepartmentList { get; set; }

        public int DepartmentId { get; set; }
        public List<int> EmployeeId { get; set; }
        public IEnumerable<ScEmployeeInfo> EmployeeList { get; set; }
        public SelectList MonthList { get; set; }
        public int Month { get; set; }
        public bool SelectAll { get; set; }


    }
}