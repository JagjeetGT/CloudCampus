using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportTeacherScheduleReportViewModel : ReportBaseViewModel
    {

        public IEnumerable<List<ScClassSchedule>> ScheduleGroupByClassSection { get; set; }
        public IEnumerable<ScClassSchedule> Schedules { get; set; }

        
    }
}