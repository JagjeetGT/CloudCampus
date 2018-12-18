using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.ARAP
{
    public class SalesChallanSummaryFormViewModel : BaseViewModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set;   }
        public bool DateShow { get; set; }
        public SelectList GroupByList { get; set; }
        public string GodownId { get; set; }
        public bool Detail { get; set; }
        public bool Godown { get; set; }
        public bool Remarks { get; set; }
        public int GroupBy { get; set; }
        public int Datetype { get; set; }
       public IEnumerable<Godown> GodownList { get; set; }

    }
}