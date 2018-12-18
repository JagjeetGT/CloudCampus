using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class FinishedGoodReturnDetail

        [ForeignKey("FinishedGoodReturnId")]
        public virtual FinishedGoodReturn FinishedGoodReturn { get; set; }
        [ForeignKey("FinishGoodId")]
        public virtual Product FinishGood { get; set; }
        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }