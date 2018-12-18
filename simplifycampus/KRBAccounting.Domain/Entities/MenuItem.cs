using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string LinkText { get; set; }
        public int ModuleId { get; set; }
        public int? ParentId { get; set; }
        public int Schedule { get; set; }
        public string ModuleKey { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("ModuleId")]
        public virtual ModuleList ModuleList { get; set; }


    }
}
