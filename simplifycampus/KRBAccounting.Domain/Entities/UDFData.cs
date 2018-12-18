using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
   public class UDFData
    {
       [Key]
       public int Id { get; set; }

       public int ReferenceId { get; set; }
       public string Value { get; set; }
       public int UdfId { get; set; }
       public int SourceId { get; set; }
       [ForeignKey("UdfId")]
       public virtual UDFEntry UdfEntry { get; set; }
    }
}
