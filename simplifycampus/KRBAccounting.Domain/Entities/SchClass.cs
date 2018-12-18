using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class SchClass
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage=" ")]
        [Remote("AcademyClassCodeExists", "School", AdditionalFields = "Id")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("AcademyClassDescriptionExists","School",AdditionalFields = "Id")]
        public string Description { get; set; }
        public int Schedule { get; set; }
       public string Incharge { get; set; }
        public int ProgramId { get; set; }
    }
}
