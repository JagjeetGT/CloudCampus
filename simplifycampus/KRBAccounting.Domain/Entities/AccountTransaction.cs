using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class AccountTransaction
    {
        [Key]
        public int Id { get; set; }
        public string VNo { get; set; }
        public DateTime VDate { get; set; }
        public string VMiti { get; set; }
        public string CrCode { get; set; }
        public decimal CrRate { get; set; }
        public int GlCode { get; set; }
        public decimal DrAmt { get; set; }
        public decimal CrAmt { get; set; }
        public decimal LocalDrAmt { get; set; }
        public decimal LocalCrAmt { get; set; }
        public string Narration { get; set; }
        public string Source { get; set; }
        public int SNo { get; set; }
        public int? CbCode { get; set; }
        public int CreatedById { get; set; }
        public int ReferenceId { get; set; }
        public int FYId { get; set; }
        public int? SlCode { get; set; }
        public int BranchId { get; set; }
        public string AgentCode { get; set; }
        public string Remarks { get; set; }

        [ForeignKey("BranchId")]
        public CompanyInfo Branch { get; set; }
    }
}
