using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_GetStudentCountByCategoryId
    {
        //sc.Id,sc.Description,COUNT(si.StudentId) as [Count]
        public int Id { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
    }
}
