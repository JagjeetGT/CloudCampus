using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
   public  class AcademicBackground
    {
       public int Id { get; set; }
       public string Title { get; set; }
       public string Institution { get; set; }
       public int PassedYear { get; set; }
       public decimal Percentage { get; set; }
       public int? DivisionId { get; set; }
       public int StudentId { get; set; }
       public string Country { get; set; }
       public string Board { get; set; }

       [ForeignKey("DivisionId")]
       public virtual ScDivision Division { get; set; }
       [ForeignKey("StudentId")]
       public virtual ScStudentinfo Studentinfo { get; set; }

       [NotMapped]
       public SelectList DivisionList { get; set; }


    }
}
