using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.School
{
    public class ScTeacherScheduleViewModel : BaseViewModel
    {
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }

        public int ClassTeacherId { get; set; }
        public string TeacherName { get; set; }

        public int Sno { get; set; }

        public int SundayTeacherId { get; set; }
        public int MondayTeacherId { get; set; }
        public int TuesdayTeacherId { get; set; }
        public int WednesdayTeacherId { get; set; }
        public int ThursdayTeacherId { get; set; }
        public int FridayTeacherId { get; set; }

        public int ClassscheduleId { get; set; }

        public int OldClassId { get; set; }
        public int OldSectionId { get; set; }

        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }
        [ForeignKey("SubjectId")]
        public virtual ScSubject Subject { get; set; }

        [ForeignKey("SectionId")]
        public virtual ScSection Section { get; set; }

        [ForeignKey("SundayTeacherId")]
        public virtual ScEmployeeInfo SundayTeacher { get; set; }

        [ForeignKey("MondayTeacherId")]
        public virtual ScEmployeeInfo MondayTeacher { get; set; }

        [ForeignKey("TuesdayTeacherId")]
        public virtual ScEmployeeInfo TuesdayTeacher { get; set; }
        [ForeignKey("WednesdayTeacherId")]
        public virtual ScEmployeeInfo WednesdayTeacher { get; set; }
        [ForeignKey("ThursdayTeacherId")]
        public virtual ScEmployeeInfo ThursdayTeacher { get; set; }
        [ForeignKey("FridayTeacherId")]
        public virtual ScEmployeeInfo FridayTeacher { get; set; }

        public IEnumerable<ScClassSchedule> ClassSchedules { get; set; }
        
    }
}