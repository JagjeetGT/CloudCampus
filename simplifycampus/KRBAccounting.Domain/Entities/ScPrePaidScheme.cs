using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScPrePaidScheme
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("AcademyPrePaidSchemesExists", "School", AdditionalFields = "Id")]
        public string Description { get; set; }
        public int ClassId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }

        [ForeignKey("ClassId")]
        public virtual SchClass SchClass { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }
        [NotMapped]
        public IEnumerable<ScPrePaidSchemeDetails> PrePaidSchemeDetialses { get; set; }
        

    }
}
