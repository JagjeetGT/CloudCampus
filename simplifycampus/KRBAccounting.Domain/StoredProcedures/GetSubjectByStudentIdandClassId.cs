using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
   public class GetSubjectByStudentIdandClassId
    {
       public int SubjectId { get; set; }
       public string Description { get; set; }
       public string SubjectName { get; set; }
       public int ClassId { get; set; }
       public string ClassName { get; set; }
    }
}
