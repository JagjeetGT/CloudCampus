using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class DebitNoteDetailEntryViewModel
    {
        public int GlCode { get; set; }
        public decimal? Amount { get; set; }
        public string Narration { get; set; }
        public int? SubLedgerId { get; set; }

        public EntryControlPL EntryControl { get; set; }
    }
}