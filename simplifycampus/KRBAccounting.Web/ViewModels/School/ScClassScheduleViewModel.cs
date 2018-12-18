using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.School
{
    public class ScClassScheduleViewModel : BaseViewModel
    {
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public string SundayStartTime { get; set; }
        public string SundayEndTim { get; set; }
        public string MondayStartTime { get; set; }
        public string MondayEndTime { get; set; }
        public string TuesdayStartTime { get; set; }
        public string TuesdayEndTime { get; set; }
        public string WednesdayStartTime { get; set; }
        public string WednesdayEndTime { get; set; }
        public string ThursdayStartTime { get; set; }
        public string ThursdayEndTime { get; set; }
        public string FridayStartTime { get; set; }
        public string FridayEndTime { get; set; }

        public int Sno { get; set; }
        public int ScheduleDetailId { get; set; }
        public int OldClassId { get; set; }
        public int OldSectionId { get; set; }
        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }

        [ForeignKey("SectionId")]
        public virtual ScSection Section { get; set; }

        public ScClassSchedule ClassSchedule { get; set; }
    }
}