using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class AgeWiseReportFormViewModel
    {
        public List<int> ClassId { get; set; }
        public IEnumerable<SchClass> ClassList { get; set; }
        public List<int> CategoryId { get; set; }
        public SelectList CategoryList { get; set; }
        public bool SelectAllClass { get; set; }
        public bool SelectAllCategory { get; set; }

        public IEnumerable<ScProgramMaster> ProgramMasters { get; set; }

        
    }
}