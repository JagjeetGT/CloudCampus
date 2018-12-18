using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Service.Helpers;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class DayBookFormViewModel : BaseViewModel
    {
      
        public string StartDate { get; set; }
      //[Display(Name = "dd/MM/yyyy")]
        public string EndDate { get; set; }
        public bool SubLedger { get; set; }
        public bool Remarks { get; set; }
        public bool DateShow { get; set; }
        public int Datetype { get; set; }
        public List<CheckBoxListInfo> ModuleList { get; set; }
    }
}