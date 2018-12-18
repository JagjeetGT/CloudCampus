using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Home
{
    public class AnalysisChart
    {
        public DataChart PurchaseChart { get; set; }
        public DataChart PurchaseReturnChart { get; set; }
        public DataChart SalesChart { get; set; }
        public DataChart SalesReturnChart { get; set; }

        public DateTime CurrentDate { get; set; }
    }
}