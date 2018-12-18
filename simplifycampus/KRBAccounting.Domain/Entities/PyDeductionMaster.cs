using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{    public class PyDeductionMaster
        [Required(ErrorMessage = "*")]
        [Remote("DeductionMastersNameExists", "Payroll", AdditionalFields = "Id")]

        public int BranchId { get; set; }
        public bool IsFlat { get; set; }
        [NotMapped]
        public SelectList TypeList { get; set; }
    [ForeignKey("CreatedBy")]
        public virtual User User { get; set; }
    