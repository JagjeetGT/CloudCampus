using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportApplyStudentListFormViewModel : BaseViewModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public SelectList GenderList { get; set; } 
        public int CategoryId { get; set; }
        public int ClassId { get; set; }
        public string Gender { get; set; }
        public string MitiFrom { get; set; }
        public string MitiTo { get; set; }
        public string DisplayDateFrom { get; set; }
        public string DisplayDateTo { get; set; }
      
    }
}