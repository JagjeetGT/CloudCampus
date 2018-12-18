using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.ARAP
{
    public class PeriodicAnalysisFormViewModel
    {
        public SelectList TypeList { get; set; }
        public SelectList YearList { get; set; }
        public SelectList MonthList { get; set; }
        public SelectList DivisorList { get; set; }

        public int Type { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Divisor { get; set; }
    }
}