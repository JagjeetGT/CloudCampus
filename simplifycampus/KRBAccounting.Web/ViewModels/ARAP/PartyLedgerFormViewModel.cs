using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Web.Controllers;

namespace KRBAccounting.Web.ViewModels.ARAP
{
    public class PartyLedgerFormViewModel: BaseViewModel
    {
        // [DataType(DataType.Date)]
        public string StartDate { get; set; }
        //[DataType(DataType.Date)]
        public string EndDate { get; set; }
        public SelectList CategoryList { get; set; }
        [Required(ErrorMessage = " ")]
        public int Category { get; set; }
        public int Opening { get; set; }
        public bool AllLedger { get; set; }
        public bool SubLedger { get; set; }
        public bool AllSubLedger { get; set; }
        public bool Summary { get; set; }
        public bool ProductDetails { get; set; }
        public bool TermDetails { get; set; }
        public string Remarks { get; set; }
        public string Narration { get; set; }
        public int Datetype { get; set; }
        public SystemControl SystemControl { get; set; }
        public int GroupBy { get; set; }
        public SelectList GroupByList { get; set; }
        public SelectList OpeningList { get; set; }
        public bool DocAgent { get; set; }
       
    }

}