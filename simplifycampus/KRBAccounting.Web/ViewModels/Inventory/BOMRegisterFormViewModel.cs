using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Inventory
{
    public class BOMRegisterFormViewModel : BaseViewModel
    {
        public bool AllFinishedGoods { get; set; }
        public bool AllCostCenter { get; set; }
    }
}