using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Attendance
{
    public class DailyAttendanceFormViewModel
    {
        public SelectList EmployeeList { get; set; }
        public SelectList DepartmentList { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public SystemControl SystemControl { get; set; }
        public bool AllDepartment { get; set; }
        public bool AllEmployee { get; set; }
      
      
    }
}