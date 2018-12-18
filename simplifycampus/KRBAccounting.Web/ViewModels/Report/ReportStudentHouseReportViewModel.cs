using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportStudentHouseReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_StudentWiseHouse> StudentWiseHouseList { get; set; }
        public IEnumerable<IGrouping<string, SP_StudentWiseHouse>> HouseMappingsGrouping { get; set; } 
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class ReportStudentBioDataReportViewModel : ReportBaseViewModel
    {
        public ScStudentinfo StudentInfo{ get; set; }
        
    }

    public class ReportStudentIdCardaReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScStudentRegistrationDetail> StudentRegistrationDetailList { get; set; }

    }

    public class ReportStudentCertificateReportViewModel : ReportBaseViewModel
    {
        public Template Template { get; set; }
        public IEnumerable<Template> Templates { get; set; }

    }

    public class ReportStudentAdmitCardViewModel:ReportBaseViewModel
    {
        public IEnumerable<ScStudentRegistrationDetail> StudentRegistrationDetailList { get; set; }
        public ScExam Exam { get; set; }
        public string ExamDescription { get; set; }
        
    }
}