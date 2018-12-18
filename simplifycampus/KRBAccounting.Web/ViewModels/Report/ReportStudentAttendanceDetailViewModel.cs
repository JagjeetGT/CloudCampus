using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportStudentAttendanceDetailViewModel : BaseViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int PresentDay { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
    }
}