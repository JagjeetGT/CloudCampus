using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScAbsentApplication
    {
        [Key]
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        [Required(ErrorMessage = " ")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = " ")]
        public DateTime DateFrom { get; set; }
        public string MitiFrom { get; set; }
        [Required(ErrorMessage = " ")]
        public DateTime DateTo { get; set; }
        public string MitiTo { get; set; }
        [Required(ErrorMessage = " ")]
        public string IsConfirm { get; set; }
        public string Reason { get; set; }
        [Required(ErrorMessage = " ")]
        public string Status { get; set; }
        public bool IsApplication { get; set; }
        public string Remarks { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }

        [ForeignKey("SectionId")]
        public virtual ScSection Section { get; set; }

        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Student { get; set; }
        [NotMapped]
        public List<SelectListItem> ClassList { get; set; }
        [NotMapped]
        public List<SelectListItem> SectionList { get; set; }
        [NotMapped]
        public SelectList StudentList { get; set; }

     
        [NotMapped]
        public List<SelectListItem> YesNoList { get; set; }

        [NotMapped]
        public List<SelectListItem> HalfFull { get; set; }

        [NotMapped]
        public IEnumerable<string> ReasionList { get; set; }

    }
}
