using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Web.ViewModels.Report;

namespace KRBAccounting.Web.ViewModels.Student
{
    public class StudentSearchViewModel
    {
        public string SrStudentName { get; set; }
        public int SrClassId { get; set; }
        public List<SchClass> SrClassList { get; set; }
        public string SrRollNo { get; set; }
        public string SrRegNo { get; set; }
        public List<ScProgramMaster> ProgramMasters { get; set; }
      
       
    }
    public class StudentSearchReportViewModel
    {
        public ScStudentRegistrationDetail ScStudentRegistrationDetail { get; set; }
        public decimal DueAmount { get; set; }
       
    }
}