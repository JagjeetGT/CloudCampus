using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class ExpiryBreakageDetail
        public int UnitId { get; set; }

        [ForeignKey("ExpBrkId")]
        public virtual ExpiryBreakageMaster ExpiryBreakageMaster { get; set; }