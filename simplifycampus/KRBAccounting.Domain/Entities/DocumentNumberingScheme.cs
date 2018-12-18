using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class DocumentNumberingScheme
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Module")]
        public string ModuleName { get; set; }
        [Display(Name = "Description")]
        public string DocDesc { get; set; }
        [Display(Name = "Type")]
        public string DocType { get; set; }
        [Display(Name = "Start Date")]
        public DateTime DocStartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime DocEndDate { get; set; }
        [Display(Name = "Numbering Style")]
        public string DocMode { get; set; }
        [Display(Name = "Prefix")]
        public string DocPrefix { get; set; }
        [Display(Name = "Suffix")]
        public string DocSuffix { get; set; }

        [Display(Name = "Body Length")]
        [Required]
       

        public int DocBodyLen { get; set; }

        [Display(Name = "Toltal Length")]
        [Required]
      
        public int DocTotalLen { get; set; }

        [Display(Name = "Numeric Left Fill")]
        public bool DocNumFill { get; set; }
        [Display(Name = "Fill Character")]
        public string DocCharFill { get; set; }
        [Display(Name = "Start No.")]
        public int DocStartNo { get; set; }
        [Display(Name = "End No.")]
        public int DocEndNo { get; set; }
        [Display(Name = "Current No.")]
        public int DocCurrNo { get; set; }
    }
}
