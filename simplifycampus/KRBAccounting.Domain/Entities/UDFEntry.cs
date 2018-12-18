using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class UDFEntry
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public int EntryModuleId { get; set; }
        [Required(ErrorMessage = " ")]
        public string FieldName { get; set; }
        public int ControlTypeId { get; set; }
        [Required(ErrorMessage = " ")]
        public string BodyLength { get; set; }
        public string FieldDecimal { get; set; }
        [Required(ErrorMessage = " ")]
        public int DisplayOrder { get; set; }
        public bool DuplicateOpt { get; set; }

        public bool MandatoryOpt { get; set; }
        public virtual ICollection<UDFEntryDetail> UdfEntryDetials { get; set; }
    }
}
