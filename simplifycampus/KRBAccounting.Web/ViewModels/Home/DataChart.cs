using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Home
{
    public class DataChart: BaseChart<TotalChartDataItem>
    {
        public DataChart()
        {
        }
        public List<TotalChartDataItem> Total { get; set; }
    }
}