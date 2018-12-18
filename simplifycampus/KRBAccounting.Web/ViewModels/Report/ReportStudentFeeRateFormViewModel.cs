using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportStudentFeeRateFormViewModel:BaseViewModel
    {
        public List<SelectListItem> ClassList { get; set; }
        public int ClassId { get; set; }
        public bool SelectAll { get; set; }
        public List<IGrouping<int, ScStudentRegistrationDetail>> StudentinfoList { get; set; }
    }
}