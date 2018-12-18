using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class ScMaterialIssueMaster
        [Required(ErrorMessage = " ")]
        public virtual ICollection<ScMaterialIssueDetails> ScMaterialIssueDetailses { get; set; }
        [NotMapped]
        public string DisplayDate { get; set; }

        [NotMapped]
        public SystemControl SystemControl { get; set; }

        [NotMapped]
        public decimal NetAmount { get; set; }

        [NotMapped]
        public IEnumerable<ScMaterialIssueDetails> MaterialIssueDetailses { get; set; } 