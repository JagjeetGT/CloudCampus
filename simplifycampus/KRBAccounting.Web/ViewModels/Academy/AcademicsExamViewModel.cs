using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class AcademicsExamViewModel : BaseViewModel
    {
        public PagedList<ScExam> ScExam { get; set; }
    }
}