using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScSubject
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("AcademySubjectDescriptionExists", "School", AdditionalFields = "Id")]
        public string Description { get; set; }
        [Required(ErrorMessage = " ")]
         [Remote("AcademySubjectCodeExists", "School", AdditionalFields = "Id")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        public int ResultSystem { get; set; }
        [Required(ErrorMessage = " ")]
        public int ClassType { get; set; }
        [Required(ErrorMessage = " ")]
        public int Type { get; set; }
        [Required(ErrorMessage = " ")]
        public int EvaluateForType { get; set; }
        [Required(ErrorMessage = " ")]
        public int Schedule { get; set; }
      
        public string Memo { get; set; }

       
    }
}
