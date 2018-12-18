using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Domain.Entities
{    public class ScMaterialReturnDetails

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("StaffId")]
        public virtual ScEmployeeInfo Staff { get; set; }

        [ForeignKey("MaterialReturnMasterId")]
        public virtual ScMaterialReturnMaster MaterialReturnMaster { get; set; }