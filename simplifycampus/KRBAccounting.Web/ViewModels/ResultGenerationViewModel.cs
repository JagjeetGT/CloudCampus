using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels
{
    public class ResultGenerationViewModel
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public decimal Percentage { get; set; }
        public string Division { get; set; }
        public decimal TotalMark { get; set; }
        public decimal ObtMark { get; set; }
        public bool Checkbox { get; set; }
        public string RollNo { get; set; }
        public string StudentName { get; set; }
        public string Result { get; set; }
        public List<SelectListItem> CLassList { get; set; }
        public int PromoteClassId { get; set; }
        public int AYId { get; set; }
        public SelectList AcademyList { get; set; }
    }
}