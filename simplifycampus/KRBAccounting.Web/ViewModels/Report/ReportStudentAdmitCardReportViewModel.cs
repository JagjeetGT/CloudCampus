using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportStudentAdmitCardReportViewModel
    {
        public IEnumerable<ScStudentRegistrationDetail> StudentRegistrationDetails { get; set; } 
    }
}