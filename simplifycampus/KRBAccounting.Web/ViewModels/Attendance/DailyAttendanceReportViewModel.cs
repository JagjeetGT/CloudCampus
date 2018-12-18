using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Attendance
{
    public class DailyAttendanceReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<Sp_DailyAttendance> DailyAttendances { get; set; }
        public IEnumerable<IGrouping<int, Sp_DailyAttendance>> DailyAttendancesGroup { get; set; }
    }
}