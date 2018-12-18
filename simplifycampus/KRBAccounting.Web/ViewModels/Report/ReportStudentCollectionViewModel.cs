using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportStudentCollectionViewModel
    {
        public int Count { get; set; }
        public SelectList CountList { get; set; }


    }
}