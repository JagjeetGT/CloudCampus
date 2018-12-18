using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class SalesChallanViewModel
    {
        public PagedList<SalesChallanMaster> AcceptedList { get; set; }
        public PagedList<SalesChallanMaster> PendingList { get; set; }
    }
}