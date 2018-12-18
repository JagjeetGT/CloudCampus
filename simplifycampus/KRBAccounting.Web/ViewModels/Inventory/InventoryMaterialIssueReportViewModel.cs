using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Inventory
{
    public class InventoryMaterialIssueReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScMaterialIssueMaster> MaterialIssueMasters { get; set; }
    }
}