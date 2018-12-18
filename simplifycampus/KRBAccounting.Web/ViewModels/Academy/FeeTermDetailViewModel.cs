using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class FeeTermDetailViewModel : FeeTermSelectViemModel
    {
        public int? FeeTermIndex { get; set; }
        public bool IsProductWise { get; set; }
        public string ParentGuid { get; set; }
        public int DispalyOrder { get; set; }
    }
}