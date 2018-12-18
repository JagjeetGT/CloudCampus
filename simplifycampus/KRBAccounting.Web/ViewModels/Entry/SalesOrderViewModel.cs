using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class SalesOrderViewModel
    {
        public PagedList<SalesOrderMaster> AcceptedList { get; set; }
        public PagedList<SalesOrderMaster> PendingList { get; set; }
    }
}