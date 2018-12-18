using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Master
{
    [Bind(Exclude = "Category,Ledger,AmountType")]

    public class OpeningLedgerAddViewModel : BaseViewModel
    {
        public OpeningLedger OpeningLedger { get; set; }
        public SelectList Category { get; set; }
        public SelectList Ledger { get; set; }
        public SelectList AmountType { get; set; }
        public int LegderId { get; set; }

        public IEnumerable<OpeningLedger> CustomerOpenings { get; set; }
        public IEnumerable<OpeningLedger> VendorOpenings { get; set; }
        public IEnumerable<OpeningLedger> BothOpenings { get; set; }
        public IEnumerable<OpeningLedger> OtherOpenings { get; set; } 

    }
}