using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Home
{
    public class CashAnalysisChart
    {
        public DataChart CashInHand { get; set; }
        public DataChart CashInBank { get; set; }
    }
}