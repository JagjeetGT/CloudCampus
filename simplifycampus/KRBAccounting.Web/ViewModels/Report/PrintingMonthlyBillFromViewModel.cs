using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class PrintingMonthlyBillFromViewModel : BaseViewModel
    {
      
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public bool Detials { get; set; }
        public bool SelectAll { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public SelectList MonthList { get; set; }
        public int Month { get; set; }
        public List<IGrouping<int, ScStudentRegistrationDetail>> StudentinfoList { get; set; }
    }
}