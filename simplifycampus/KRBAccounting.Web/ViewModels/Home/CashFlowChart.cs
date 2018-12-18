using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Home
{
    public class CashFlowChart: BaseChart<TotalChartDataItem>
    {
        public CashFlowChart()
        {
        }
        public List<TotalChartDataItem> Total { get; set; }
    }
}