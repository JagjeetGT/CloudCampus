using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Master
{
    public class BillingTermAddViewModel :BaseViewModel
    {
        public BillingTerm BillingTerm { get; set; }

        public SelectList CategoryList { get; set; }

        public SelectList GeneralLedgerList { get; set; }

        public SelectList BasisList { get; set; }

        public SelectList SignList { get; set; }
    }
}