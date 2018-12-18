using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Helpers;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class AcademicsConsolidatedMarksSetupsViewModel : BaseViewModel
    {
        public ScConsolidatedMarksSetup ConsolidatedMarksSetup { get; set; }
        public IEnumerable<List<ScConsolidatedMarksSetup>> ConsolidatedMarksSetupGrouping { get; set; }
        public IEnumerable<ScConsolidatedMarksSetup> ConsolidatedMarksSetups { get; set; }
        public int Classid { get; set; }
        public int ExamId { get; set; }
        public List<SelectList> asdf { get; set; }


    }


}