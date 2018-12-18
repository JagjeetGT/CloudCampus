using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class BuildingReportViewModel
    {
        public string BuildingName { get; set; }
        public string RoomName { get; set; }
        public int StudentCount { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public List<StudentBuildingCount> StatisticsStudentBuildingCounts { get; set; }
    }
}