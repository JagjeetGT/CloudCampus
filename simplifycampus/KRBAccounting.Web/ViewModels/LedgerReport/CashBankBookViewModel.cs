using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class CashBankBookViewModel :BaseViewModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int LedgerId { get; set; }
        public bool Remarks { get; set; }
        public bool Subledger { get; set; }
        public bool DateShow { get; set; }        
    }       
}   