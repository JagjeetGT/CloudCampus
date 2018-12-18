using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class AcademicsFeeItemViewModel :BaseViewModel
    {
        public ScFeeItem FeeItem { get; set; }
        public SelectList FeeTypeList { get; set; }
    }
}