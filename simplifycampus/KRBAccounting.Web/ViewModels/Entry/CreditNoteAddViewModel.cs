using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class CreditNoteAddViewModel : BaseViewModel
    {
        public CreditNoteMaster CreditNoteMaster { get; set; }
        public IEnumerable<UDFEntry> UdfEntries { get; set; }
     [DataType(DataType.Date)]
        public string Date { get; set; }
        [Required(ErrorMessage = " ")]
        public string RefDate { get; set; }
        [Required(ErrorMessage = " ")]
        public string PartyName { get; set; }
        public string AgentName { get; set; }
        public string SubLedger { get; set; }
        public string CurrentBalance { get; set; }
        public string GlCode { get; set; }
        public string GlDetailCurrBal { get; set; }
        public string TotalAmt { get; set; }
        public string TotalAmtRs { get; set; }
        public string DisplayDate { get; set; }

        public EntryControlPL EntryControl { get; set; }
    }
}