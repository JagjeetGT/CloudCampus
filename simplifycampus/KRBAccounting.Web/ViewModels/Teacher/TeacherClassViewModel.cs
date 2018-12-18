using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Teacher
{
    public class TeacherClassViewModel
    {
        public IEnumerable<ScStudentRegistrationDetail> RegistrationDetails { get; set; }

        public SchClass Class { get; set; }
        public ScSection Section { get; set; }
       
    }
}