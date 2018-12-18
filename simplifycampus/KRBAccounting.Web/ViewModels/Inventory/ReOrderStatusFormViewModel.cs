using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Inventory
{
    public class ReOrderStatusFormViewModel : BaseViewModel
    {
        //[DataType(DataType.Date)]
        public string AsOnDate { get; set; }
        public bool SelectAll { get; set; }
    }
}