using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Teacher
{
    public class TeacherStudentListViewModel
    {
        public IEnumerable<ScStudentRegistrationDetail> StudentRegistrationDetails { get; set; }
        public IEnumerable<IGrouping<int, ScStudentRegistrationDetail>> GroupByList { get; set; }
    }
}