using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportClassListReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<SchClass> ClassList { get; set; }
    }

    public class ReportSectionListReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScSection> SectionList { get; set; }
    }

    public class ReportExaminationListReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScExam> ExamList { get; set; }
    }

    public class ReportFeeListReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScFeeItem> FeeList { get; set; }
    }
    public class ReportBusListReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScBus> BusList { get; set; } 
    }
    public class ReportBoaderListReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScBoader> BoaderList { get; set; }
    }
    public class ReportFeeTermListReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScFeeTerm> FeetermList { get; set; }
    }
    public class ReportBusStopListReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScBusStop> BusStopsList { get; set; }
    }
    public class ReportBusStopRouteListReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScBusRouteDetails> BusStopRouteList { get; set; }
    }
    public class ReportStudentCategoryListReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScStudentCategory> StudentCategoryList { get; set; }
    }
    public class ReportStaffgroupListReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScEmployeeCategory> StaffGroupList { get; set; }
    }
    public class ReportBuildingListReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<SchBuilding> BuildingGroupList { get; set; }
    }
    public class ReportClassRoomReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScClassRoomMapping> ClassRoomlist { get; set; }
    }
}