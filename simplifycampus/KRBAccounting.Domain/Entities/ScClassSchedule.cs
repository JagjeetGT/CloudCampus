using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class ScClassSchedule
        public int SectionId { get; set; }
       

        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }

        [ForeignKey("SectionId")]
        public virtual ScSection Section { get; set; }

        [ForeignKey("SubjectId")]
        public virtual ScSubject Subject { get; set; }

        [NotMapped]
        public IEnumerable<ScClassScheduleDetail> ScheduleDetail { get; set; }


        public virtual ICollection<ScClassScheduleDetail> DetailCollection { get; set; }
    
    }