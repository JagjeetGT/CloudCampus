using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class StudentMonthlyBillFromViewModel : BaseViewModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public bool Detials { get; set; }
        public bool SelectAll { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public List<IGrouping<int, ScStudentRegistrationDetail>> StudentinfoList { get; set; }
        public string MitiFrom { get; set; }
        public string MitiTo { get; set; }
        public string DisplayDateFrom { get; set; }
        public string DisplayDateTo { get; set; }

    }
}