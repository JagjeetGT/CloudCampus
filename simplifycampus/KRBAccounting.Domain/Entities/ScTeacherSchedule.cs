using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScTeacherSchedule
    {
        [Key]
        public int Id { get; set; }
        public int StaffId { get; set; }
        public int ClassScheduleDetailId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        

        [ForeignKey("StaffId")]
        public virtual ScEmployeeInfo Staff { get; set; }

        [ForeignKey("ClassScheduleDetailId")]
        public virtual ScClassScheduleDetail ClassScheduleDetail { get; set; }
    }
}
