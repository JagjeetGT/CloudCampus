using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Inventory
{
    public class InventoryMaterialReturnReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScMaterialReturnMaster> MaterialReturnMasters { get; set; }
    }
}