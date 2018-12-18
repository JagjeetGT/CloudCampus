using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Domain.Entities
{
    public class ScSalaryHead
    {
        [Key]
        public int Id { get; set; }

        public int Head { get; set; }
        public string Operation { get; set; }
        public int DisplayOrder { get; set; }
       
        public int LedgerId { get; set; }
        [ForeignKey("LedgerId")]
        public virtual Ledger Ledger { get; set; }

        [NotMapped]
        public SelectList HeadList { get; set; }
        [NotMapped]
        public SelectList OperationList { get; set; }
    }
}
