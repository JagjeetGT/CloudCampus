using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Inventory
{
    public class StockLedgerFormViewModel : BaseViewModel
    {
       // [DataType(DataType.Date)]
        public string StartDate { get; set; }
       // [DataType(DataType.Date)]
        public string EndDate { get; set; }
        public bool SelectAll { get; set; }
        public bool Summary { get; set; }
        public int UnitId { get; set; }
        public bool WithValue { get; set; }
        public bool ZeroBalance { get; set; }
        public bool Godown { get; set; }
        public string GodownId { get; set; }
        public IEnumerable<Godown> GodownList { get; set; }
        public List<SelectListItem> UnitList { get; set; }
        public bool Batch { get; set; }
        public bool DateShow { get; set; }
    }
}