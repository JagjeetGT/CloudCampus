using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScBoaderMapping
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public int ClassId { get; set; }

        public int SectionId { get; set; }

        public int BoaderId { get; set; }
        public int StudentId { get; set; }
        public int Status { get; set; }
        public int AcademicYearId { get; set; }
        public DateTime StartDate { get; set; }
        public string StartMiti { get; set; }
        public DateTime EndDate { get; set; }
        public string EndMiti { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        
        public string Narration { get; set; }
     
        public string Remarks { get; set; }
        [ForeignKey("BoaderId")]
        public virtual ScBoader ScBoader { get; set; }
        [ForeignKey("ClassId")]
        public virtual SchClass Classes { get; set; }
        [ForeignKey("SectionId")]
        public virtual ScSection Sections { get; set; }
        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Studentinfo { get; set; }
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }

        [NotMapped]
        public IEnumerable<ScBoaderMapping> ScBoaderMappings { get; set; }

        [NotMapped]
        public int DateType { get; set; }

        [NotMapped]
        public int OLDCLassId { get; set; }
        [NotMapped]
        public int OLDSectionId { get; set; }
        [NotMapped]
        public int OLDBoaderId { get; set; }
    }
}
