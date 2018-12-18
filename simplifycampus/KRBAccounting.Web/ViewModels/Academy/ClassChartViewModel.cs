using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class ClassChartViewModel
    {
        public string ClassName { get; set; }
        public int MaleCount { get; set; }
        public int FemaleCount { get; set; }
    }
}