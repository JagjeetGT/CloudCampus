using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class EmployeeStatementViewModel : BaseViewModel
    {

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string AsOnDate { get; set; }
        public int EmployeeId { get; set; }
        public SelectList ReportTypeList { get; set; }
        [Required(ErrorMessage = " ")]
        public int ReportType { get; set; }
        public SelectList TypeList { get; set; }
        public int Type { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public int DepartmentId { get; set; }
        public SelectList EmployeeList { get; set; }

    }
}