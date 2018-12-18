using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Master
{
    public class OpeningStudentViewModel:BaseViewModel
    {
        public OpeningStudent OpeningStudent { get; set; }
        public SelectList OpeningStudentCategory { get; set; }
        public IEnumerable<OpeningStudent> OpeningStudentDue { get; set; }
        public IEnumerable<OpeningStudent> OpeningStudentDeposit { get; set; }
        public SelectList AmountType { get; set; }
    }
}