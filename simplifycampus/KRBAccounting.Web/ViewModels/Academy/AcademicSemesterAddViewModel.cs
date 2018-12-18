using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class AcademicSemesterAddViewModel
    {
        public int classId { get; set; }
        public int Sno { get; set; }
        [Required(ErrorMessage = " ")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        public string SemesterDesc { get; set; }
    }
}