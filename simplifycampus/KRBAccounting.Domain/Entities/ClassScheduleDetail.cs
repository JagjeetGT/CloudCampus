using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class ScClassScheduleDetail{        [Key]        public int Id {get;set;}        public string Day {get;set;}        public string StartTime {get;set;}        public string EndTime {get;set;}        public int ClassScheduleId {get;set;}
        public int DisplayOrder { get; set; }

        [ForeignKey("ClassScheduleId")]
        public virtual ScClassSchedule ClassSchedule { get; set; }

        public virtual ICollection<ScTeacherSchedule> TeacherSchedules { get; set; }    }}