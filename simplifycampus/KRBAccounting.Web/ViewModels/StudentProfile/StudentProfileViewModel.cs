using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.StudentProfile
{
    public class StudentProfileViewModel
    {
        public ScStudentinfo Studentinfo { get; set; }
        public ScStudentRegistrationDetail RegistrationDetail { get; set; }
        public IEnumerable<StudentDocument> StudentDocument { get; set; }
    }
}