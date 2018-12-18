using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
   public class UDFEntryDetail
    {
       [Key]
       public int Id { get; set; }
       public int UdfId { get; set; }
       public string Value { get; set; }

       [ForeignKey("UdfId")]
       public virtual UDFEntry Entry { get; set; }
      
    }
}
