using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels.Inventory
{
    public class ProductBatchSummaryFormViewModel : BaseViewModel
    {
        public bool Mfg { get; set; }
        public string MfgStartDate { get; set; }
        public string MfgEndDate { get; set; }
        public bool Exp { get; set; }
        public string ExpStartDate { get; set; }
        public string ExpEndDate { get; set; }
        public bool AllProducts { get; set; }
        public int BranchId { get; set; }
        public string ExpiredGoods { get; set; }
        public int RemainingDays { get; set; }
        public SelectList BranchList { get; set; }
        public string ProductId { get; set; }
        public bool Expgoods { get; set; }
        public string MStartDate { get; set; }
        public string MEndDate { get; set; }
        public string EStartDate { get; set; }
        public string EEndDate { get; set; }
        


        

    }
}