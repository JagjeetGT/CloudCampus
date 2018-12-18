using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScRollNumberingScheme
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Class Name")]
        public int ClassId { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Type")]
        public int SectionId { get; set; }
        //[Display(Name = "Start Date")]
        //public DateTime StartDate { get; set; }
        //[Display(Name = "End Date")]
        //public DateTime EndDate { get; set; }
        [Display(Name = "Numbering Style")]
        public string Mode { get; set; }
        [Display(Name = "Prefix")]
        public string Prefix { get; set; }
        [Display(Name = "Suffix")]
        public string Suffix { get; set; }

        [Display(Name = "Body Length")]
        [Required]


        public int BodyLen { get; set; }

        [Display(Name = "Toltal Length")]
        [Required]

        public int TotalLen { get; set; }
        [Display(Name = "Numeric Left Fill")]
        public bool NumFill { get; set; }
        [Display(Name = "Fill Character")]
        public string CharFill { get; set; }
        [Display(Name = "Start No.")]
        public int StartNo { get; set; }
        [Display(Name = "End No.")]
        public int EndNo { get; set; }
        [Display(Name = "Current No.")]
        public int CurrNo { get; set; }
        public int BranchId { get; set; }
        public int AcademyYearId { get; set; }
        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }
        [ForeignKey("SectionId")]
        public virtual ScSection Section { get; set; } 
    }
}
