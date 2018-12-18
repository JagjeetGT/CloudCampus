using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Master
{
    public class ProductSubGroupAddViewModel
    {
        public ProductSubGroup ProductSubGroup { get; set; }

        public SelectList ProductGroupList { get; set; }
    }
}