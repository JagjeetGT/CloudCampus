using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class StudentCategoryLedgerSheetViewModel
    {
        public int CategoryId { get; set; }
        public int ExamId { get; set; }
        public List<SelectListItem> ExamList { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
    }
}