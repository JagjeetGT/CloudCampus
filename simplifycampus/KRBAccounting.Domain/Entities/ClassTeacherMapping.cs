using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
   public  class ClassTeacherMapping
    {
       [Key]
       public int Id { get; set; }
       public int ClassId { get; set; }
       public int TeacherId { get; set; }
       public int SectionId { get; set; }

    }
}
