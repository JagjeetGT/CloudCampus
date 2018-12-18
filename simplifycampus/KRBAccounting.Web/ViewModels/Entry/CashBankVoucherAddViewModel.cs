using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class CashBankVoucherAddViewModel : BaseViewModel
    {
        public CashBankMaster CashBankMaster { get; set; }
        public IEnumerable<UDFEntry> UdfEntries { get; set; }
       // [Remote("CheckFiscalyearDateinCashBank","Entry")]
        [DataType(DataType.Date)]
        public string VoucherDate { get; set; }
        public string ChequeDate { get; set; }
        public decimal? RecAmt { get; set; }
        public decimal? PayAmt { get; set; }
        public decimal? NetAmt { get; set; }
        public decimal? NetAmtRs { get; set; }
        public string CurrentBalance { get; set; }
        public string GlCode { get; set; }
        public string GlDetailCurrBal { get; set; }
        public string DisplayDate { get; set; }
        public EntryControlPL EntryControl { get; set; }
        public Ledger Ledger { get; set; }
    }
}