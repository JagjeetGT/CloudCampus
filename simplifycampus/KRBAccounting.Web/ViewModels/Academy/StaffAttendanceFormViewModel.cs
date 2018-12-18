using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class StaffAttendanceFormViewModel : BaseViewModel
    {
        public List<SelectListItem> EmployeeDepartmentList { get; set; }
       
        public int DepartmentId { get; set; }
    }
}