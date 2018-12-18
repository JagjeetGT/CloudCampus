using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.StudentProfile
{
    public class TeacherListViewModel
    {
        public int SubjectType { get; set; }
        public int SubjectId { get; set; }
        public string TeacherUserName { get; set; }
        public string SubName { get; set; }
        public string TeacherName { get; set; }
        public string ClassName { get; set; }
        public int UserId { get; set; }


    }
}