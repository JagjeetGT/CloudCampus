using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.ARAP
{
    public class OutStandingFromViewModel : BaseViewModel
    {
        //[DataType(DataType.Date)]
        public string AsOnDate { get; set; }
        //[DataType(DataType.Date)]
        public string StartDate { get; set; }
        //[DataType(DataType.Date)]
        public string EndDate { get; set; }
        public bool AllLedger { get; set; }
        public bool DateShow { get; set; }     
        public int Datetype { get; set; }
        public SystemControl SystemControl { get; set; }
        public int GroupBy { get; set; }
        public SelectList GroupByList { get; set; }
        public SelectList OpeningList { get; set; }
        public bool DocAgent { get; set; }
      
    }
}