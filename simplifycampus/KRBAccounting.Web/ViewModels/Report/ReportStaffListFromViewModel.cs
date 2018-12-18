using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportStaffListFromViewModel : BaseViewModel
    {
        public SelectList GenderList { get; set; }
        public List<SelectListItem> ReligionList { get; set; }
        public List<SelectListItem> StatusList { get; set; }
        public List<SelectListItem> GroupList { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public int Status { get; set; }
        public int Group { get; set; }


    }
}