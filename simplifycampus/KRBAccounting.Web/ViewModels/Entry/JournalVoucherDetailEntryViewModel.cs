using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class JournalVoucherDetailEntryViewModel
    {
        public int LedgerId { get; set; }
        public decimal? DrAmount { get; set; }
        public decimal? CrAmount { get; set; }
        public string Narration { get; set; }
        public int? SubLedgerId { get; set; }
        public EntryControlPL EntryControl { get; set; }
    }
}