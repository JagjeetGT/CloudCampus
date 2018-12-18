using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class BuildingStudentViewModel : ReportBaseViewModel
    {

        public SchBuilding SchBuilding { get; set; }

        public SchClass SchClass { get; set; }

        public   List<SelectListItem> BuildingList { get; set; }

        public List<SelectListItem> RoomList { get; set; }
        
        public List<StudentBuildingCount> StatisticsStudentBuildingCounts { get; set; }
    }
}