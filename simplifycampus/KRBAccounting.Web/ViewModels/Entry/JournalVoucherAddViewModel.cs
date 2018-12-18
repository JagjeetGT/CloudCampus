using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class JournalVoucherAddViewModel : BaseViewModel
    {
        public JournalVoucher JournalVoucher { get; set; }
        public IEnumerable<UDFEntry> UdfEntries { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date")]
        public string Date { get; set; }
        public string GlCode { get; set; }
        public string GlDetailCurrBal { get; set; }

        public string DrAmt { get; set; }
        public string CrAmt { get; set; }
        public string DrCrDiffAmt { get; set; }
        public string DrCrDiffAmtRs { get; set; }
        public string DisplayDate { get; set; }
        public EntryControlPL EntryControl { get; set; }
    }
}