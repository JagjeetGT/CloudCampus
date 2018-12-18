using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
   public class ClassIdPercentage
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public decimal? Percentage { get; set; }
    }
}
