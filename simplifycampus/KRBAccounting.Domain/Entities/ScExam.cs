using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScExam
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("AcademyExamCodeExists", "Exam", AdditionalFields = "Id")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("AcademyExamDescriptionExists", "Exam", AdditionalFields = "Id")]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public string StartMiti { get; set; }
        public DateTime EndDate { get; set; }
        public string EndMiti { get; set; }
        [Required(ErrorMessage = " ")]
        public int Type { get; set; }

        [Required(ErrorMessage = " ")]
        public int Schedule { get; set; }

        public string Memo { get; set; }
        public bool IsFinal { get; set; }

        [NotMapped]
        public int DateType { get; set; }



    }
}
