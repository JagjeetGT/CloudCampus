using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Inventory
{
    public class InventoryMaterialIssueFormViewModel : BaseViewModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}