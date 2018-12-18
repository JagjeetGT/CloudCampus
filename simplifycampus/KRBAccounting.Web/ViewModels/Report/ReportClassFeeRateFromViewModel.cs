using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportClassFeeRateFromViewModel
    {
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> BoaderList { get; set; }
        public List<SelectListItem> ShiftList { get; set; }
        public int ClassId { get; set; }
        public int ShiftId { get; set; }
        public int BoaderId { get; set; }
    }
}