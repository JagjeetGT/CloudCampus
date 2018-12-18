using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class CashBankDetailEntryViewModel
    {
        public int LedgerId { get; set; }
        public decimal? RecAmount { get; set; }
        public decimal? PayAmount { get; set; }
        [Required(ErrorMessage = " ")]
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public int SubLedgerId { get; set; }
        public EntryControlPL EntryControl { get; set; }

        public Ledger Ledger { get; set; }
    }
}