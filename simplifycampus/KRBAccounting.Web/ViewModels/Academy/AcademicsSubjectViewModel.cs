using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class AcademicsSubjectViewModel : BaseViewModel
    {
        public virtual ScSubject ScSubject { get; set; }
        public SelectList ClassType { get; set; }
        public SelectList ResultSystem { get; set; }
        public SelectList EvaluationForPass { get; set; }
        public SelectList SubjectType { get; set; }
    }
}