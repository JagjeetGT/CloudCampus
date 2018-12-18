using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class StaffAttendanceDateViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int MonthCount { get; set; }
        public DateTime Date { get; set; }
        public int AcademicId { get; set; }
        public IEnumerable<ScEmployeeInfo> ScEmployeeInfos { get; set; }
    }
}