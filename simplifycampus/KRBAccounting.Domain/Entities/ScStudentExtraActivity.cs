using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScStudentExtraActivity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = " ")]
        public int ClassId { get; set; }
        [Required(ErrorMessage = " ")]
        public int SectionId { get; set; }
        [Required(ErrorMessage = " ")]
        public int ActivityId { get; set; }
        public DateTime CreateDate { get; set; }
        public int UpdateById { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateById { get; set; }
        public string Remarks { get; set; }
        public int AcademicYearId { get; set; }

        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }
        [ForeignKey("SectionId")]
        public virtual ScSection Section { get; set; }
        [ForeignKey("ActivityId")]
        public virtual ScExtraActivity ExtraActivity { get; set; }
        [ForeignKey("UpdateById")]
        public virtual User UpdateBy { get; set; }
        [ForeignKey("CreateById")]
        public virtual User CreateBy{ get; set; }
        [NotMapped]
        public IEnumerable<ScStudentExtraActivityDetails> ActivityDetailses { get; set; } 
    }
}
