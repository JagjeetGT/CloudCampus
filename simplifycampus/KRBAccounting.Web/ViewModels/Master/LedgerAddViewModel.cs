using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Master
{
    public class LedgerAddViewModel :BaseViewModel
    {
        public Ledger Ledger { get; set; }
        public IEnumerable<UDFEntry> UdfEntries { get; set; } 
        public SelectList Category { get; set; }
        public SelectList Group { get; set; }
        public SelectList SubGroup { get; set; }
        public SelectList Area { get; set; }
        public SelectList Agent { get; set; }
        public SelectList Currency { get; set; }
    }
}