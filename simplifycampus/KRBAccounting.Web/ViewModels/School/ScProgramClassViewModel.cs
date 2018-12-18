using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Web.ViewModels.Academy;

namespace KRBAccounting.Web.ViewModels.School
{
    public class ScProgramClassViewModel
    {
        public ScProgramMaster ProgramMaster { get; set; }
        public IEnumerable<AcademicSemesterAddViewModel> Class { get; set; }
        public int ClassCount { get; set; }
    }
} 