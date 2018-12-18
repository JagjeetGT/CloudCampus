using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ShortNameDetail
    {
        [Key]
        public int Id { get; set; }
        public string Prefix { get; set; }
        public int? Number { get; set; }
        public string Module { get; set; }
        public string ShortName { get; set; }
    }
}
