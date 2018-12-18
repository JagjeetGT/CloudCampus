using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{    public class OpeningStudent
        public int CategoryId { get; set; }
        [NotMapped]
        public SelectList AmountTypeList { get; set; }
        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Studentinfo { get; set; }
        [ForeignKey("LedgerId")]
        public virtual Ledger Ledger { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User User { get; set; }