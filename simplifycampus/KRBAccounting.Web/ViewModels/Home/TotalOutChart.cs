using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Home
{
    public class TotalOutChart: BaseChart<TotalOutChartDataItem>
    {
        public TotalOutChart()
        {
        }
        public List<TotalOutChartDataItem> TotalOut { get; set; }
    }
}