using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Inventory
{
    public class ReOrderStatusViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_ReOrderStatus> ReOrderStatuses { get; set; }
    }
}