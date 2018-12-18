﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class Narration
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("CheckNameInNarration", "Master", AdditionalFields = "Id")]
        [StringLength(2000)]
        public string Name { get; set; }
        public int Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("BranchId")]
        public CompanyInfo Branch { get; set; }
    }
}