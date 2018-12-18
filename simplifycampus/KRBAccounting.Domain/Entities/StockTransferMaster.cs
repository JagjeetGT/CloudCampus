using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class StockTransferMaster
    {
        [Key]
        public int Id { get; set; }
        
        public string STNo { get; set; }

        
        public DateTime STDate { get; set; }
        public string STMiti { get; set; }
        public int GodownId { get; set; }
        public string Remarks { get; set; }
        public string BarCode { get; set; }
        public bool Posting { get; set; }
        public bool Export { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public int BranchId { get; set; }
        public int DocId { get; set; }
        public int FYId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public decimal? CurRate { get; set; }
        public string CurCode { get; set; }
        [ForeignKey("GodownId")]
        public virtual Godown Godown { get; set; }

        public virtual ICollection<StockTransferDetail> StockTransferDetails { get; set; }

        [NotMapped]
        public int CurrencyId { get; set; }
    }
}
