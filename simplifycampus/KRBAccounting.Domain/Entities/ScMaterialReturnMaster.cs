using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScMaterialReturnMaster
    {
        [Key]
        public int Id { get; set; }
        public string VoucherNo { get; set; }
        [Required(ErrorMessage = " ")]
        public DateTime VoucherDate { get; set; }
        public string VoucherMiti { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public virtual ICollection<ScMaterialReturnDetails> ScMaterialReturnDetailses { get; set; }
        [NotMapped]
        public string DisplayDate { get; set; }

        [NotMapped]
        public SystemControl SystemControl { get; set; }

        [NotMapped]
        public decimal NetAmount { get; set; }

        [NotMapped]
        public IEnumerable<ScMaterialReturnDetails> MaterialReturnDetailses { get; set; }
    }
}
