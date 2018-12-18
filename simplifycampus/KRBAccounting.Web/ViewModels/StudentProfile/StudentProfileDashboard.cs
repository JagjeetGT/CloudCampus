using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.StudentProfile
{
    public class StudentProfileDashboard
    {
        public string Class { get; set; }
        public string Section { get; set; }
        public string ClassTeacher { get; set; }
        public string TeacherUserName { get; set; }
        public ScStudentinfo Studentinfo { get; set; }
        public ScStudentRegistrationDetail StudentRegistrationDetail { get; set; }
        public int LibraryCardUsed { get; set; }
        public IEnumerable<string> CardName { get; set; }
        public IEnumerable<StCardDetails> CardDetails { get; set; }
        public decimal TotalDueAmount { get; set; }
        public IEnumerable<BillInfo> BillInfos { get; set; }
        public SystemControl SystemControl { get; set; }
        public SelectList BooKlist { get; set; }
        public int TotalMessage { get; set; }
        
    }

    public class StCardDetails
    {
        public string CardNo { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public string BookName { get; set; }
        public string Edition { get; set; }
        public string Publisher { get; set; }
        public decimal Fine { get; set; }
    }
    public class BillInfo
    {
        public int MonthId { get; set; }
        public decimal Amount { get; set; }
    }
}