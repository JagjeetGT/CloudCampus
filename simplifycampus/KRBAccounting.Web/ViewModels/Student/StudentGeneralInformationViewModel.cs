using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Student
{
    public class StudentGeneralInformationViewModel : ReportBaseViewModel
    {
        public ScBillTransaction BillTransaction { get; set; }
        public decimal DueAmount { get; set; }
        public ScStudentinfo Studentinfo { get; set; }
        public ScStudentRegistrationDetail ScStudentRegistrationDetail { get; set; }
        public IEnumerable<List<ScExamMarksEntry>> ExamMarksGrouping { get; set; }
        public IEnumerable<ScExamMarksEntry> ExamMarksEntries { get; set; }
        public IEnumerable<ScDivision> Division { get; set; }

        public IEnumerable<ScAbsentApplication> ScAbsentApplications { get; set; }
        public IEnumerable<ScStudentExtraActivityDetails> StudentExtraActivitys { get; set; }
        public IEnumerable<GetStudentFeeRate> GetStudentFeeRate { get; set; }
        public IEnumerable<SP_StudentDeviceAttendance> StudentAttendances { get; set; }
        public IEnumerable<List<SP_StudentDeviceAttendance>> StduentAttendacegrouping { get; set; }
        public int WorkingDays { get; set; }
        public int StudentId { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public IEnumerable<SP_StudentLedgerSummary> StudentLedgerList { get; set; }

        public IEnumerable<SP_StudentMonthlyBill> StudentMonthlyList { get; set; }
        public IEnumerable<ScMonthlyBillStudent> BillStudents { get; set; }

        public string Section { get; set; }
        public SystemControl SystemControl { get; set; }
        public IEnumerable<SP_StudentInfoHeader> SpStudentInfoHeader { get; set; }
        
        public IEnumerable<ExtraActivityInformation> ExtraActivityInformations { get; set; }
    }
}