using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Setup
{
    public class SystemControlViewModel
    {
        public SystemControl SystemControl { get; set; }
        public SelectList DateFormat { get; set; }
        public SelectList AuditTrial { get; set; }
        public SelectList UDF { get; set; }
        public SelectList AccountGroupList { get; set; }
        public List<SelectListItem> NoOfFeeReceiptPrintList { get; set; }
        public List<SelectListItem> NoOfBillPrintList { get; set; }
        public SelectList ExpiredProductList { get; set; }
        public SelectList NegativeStockList { get; set; }
    }
}