using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Master
{
    public class AccountGroupViewModel : BaseViewModel
    {
        public AccountGroup AccountGroup { get; set; }

        public SelectList Type { get; set; }

        public SelectList SubType { get; set; }
    }
}