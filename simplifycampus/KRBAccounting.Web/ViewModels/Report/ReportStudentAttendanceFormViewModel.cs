using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportStudentAttendanceFormViewModel : BaseViewModel
    {
        public int ClassId { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public int SectionId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
       

    }
}