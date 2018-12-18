using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class ScStaffSubjectMapping
{
        [Key]
        public int Id {get;set;}
        public int StaffId {get;set;}
        public int SubjectId {get;set;}
        public bool IsActive {get;set;}

        [ForeignKey("StaffId")]
        public virtual ScEmployeeInfo Staff { get; set; }

        [ForeignKey("SubjectId")]
        public virtual ScSubject Subject { get; set; }
    }
}
