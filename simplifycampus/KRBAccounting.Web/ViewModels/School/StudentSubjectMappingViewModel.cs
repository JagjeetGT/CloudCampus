using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.School
{
    public class StudentSubjectMappingViewModel
    {
        public ScStudentSubjectMapping StudentSubjectMapping { get; set; }
        public ScStudentRegistrationDetail StudentRegistrationDetail { get; set; }
        public string Section { get; set; }
        public string Rollno { get; set; }
    }


}