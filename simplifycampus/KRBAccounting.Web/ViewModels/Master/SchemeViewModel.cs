using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Master
{
    public class SchemeViewModel
    {
        public Scheme Scheme { get; set; }

        public SchemeProduct SchemeProduct { get; set; }
        public SchemeFreeProduct SchemeFreeProduct { get; set; }

        public IEnumerable<SchemeProduct> SchemeProducts { get; set; }
        

        public SelectList AmountType { get; set; }
        public string ParentGuid { get; set; }
 
    }
}