using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportStudentAttendanceReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_StudentDeviceAttendance> StudentAttendances { get; set; }
        public IEnumerable<List<SP_StudentDeviceAttendance>> StduentAttendacegrouping { get; set; }
      public int WorkingDays { get; set; }
        public int StudentId { get; set; }
       
    }



  
  
    
}