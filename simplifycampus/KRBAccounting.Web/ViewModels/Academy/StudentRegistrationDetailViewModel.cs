using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class StudentRegistrationDetailViewModel
    {
        public ScStudentRegistrationDetail ScStudentRegistrationDetail { get; set; }
        public ScExam Exam { get; set; }
    }
}