using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class GodownTransfer

        [ForeignKey("FromGodownId")]
        public virtual Godown Godown { get; set; }