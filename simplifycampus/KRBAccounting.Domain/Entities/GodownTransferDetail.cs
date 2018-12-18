using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class GodownTransferDetail

        [ForeignKey("TransferId")]
        public virtual GodownTransfer GodownTransfer { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }