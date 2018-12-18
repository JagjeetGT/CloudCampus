using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.ARAP
{
    public class PurchaseReturnSummaryFormViewModel : BaseViewModel
    {
        //[DataType(DataType.Date)]
        public string StartDate { get; set; }
       // [DataType(DataType.Date)]
        public string EndDate { get; set; }

        public bool Detail { get; set; }
        public bool Godown { get; set; }
        public string GodownId { get; set; }
        public IEnumerable<Godown> GodownList { get; set; }
        public bool DateShow { get; set; }
        public bool Remarks { get; set; }
        public SelectList GroupByList { get; set; }
        public int GroupBy { get; set; }
      
    }
}