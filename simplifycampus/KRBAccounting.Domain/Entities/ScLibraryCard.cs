using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScLibraryCard
    {
        [Key]
        public int Id { get; set; }

        public int LibraryRegistrationId { get; set; }
        public string CardNo { get; set; }
        public bool IsUse { get; set; }
        [ForeignKey("LibraryRegistrationId")]
        public virtual ScLibraryMemberRegistration LibraryMemberRegistration { get; set; }

    }
}

