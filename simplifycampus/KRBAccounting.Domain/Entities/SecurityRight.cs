using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class SecurityRight
    {
        [Key]
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public int Role { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Navigate { get; set; }
        public bool Approve { get; set; }
        [ForeignKey("ModuleId")]
        public virtual ModuleList ModuleList { get; set; }
        [NotMapped]
        public int? MenuItemId { get; set; }

        [NotMapped]
        public int? ParentId { get; set; }

        [NotMapped]
        public string LinkName { get; set; }

        [NotMapped]
        public int? Schedule { get; set; }

    }
}
