using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class PrintingFeeReceiptReportViewModel 
    {
        public IEnumerable<ScFeeReceipt> ScFeeReceipts { get; set; }
        public ReportHeader Header { get; set; }
    }
}