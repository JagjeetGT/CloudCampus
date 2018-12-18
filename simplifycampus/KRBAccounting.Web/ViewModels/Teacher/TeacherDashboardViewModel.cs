using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Teacher
{
    public class TeacherDashboardViewModel
    {
        public IEnumerable<ScClassTeacherMapping> ClassTeacherMappings { get; set; }
        public IEnumerable<TP_SubjectList> SubjectLists { get; set; }
        public IEnumerable<TP_ClassList> ClassLists { get; set; }
        public List<SelectListItem> TeacherListItems { get; set; }
        public ScEmployeeInfo EmployeeInfo { get; set; }
        public int TotalMessage { get; set; }

    }

    public class TeacherClassList
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
    }

    public class TP_SubjectList

    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }

    public class TP_ClassList
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int SubjectId { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
}

}