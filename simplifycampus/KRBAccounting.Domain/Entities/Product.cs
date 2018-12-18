using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("CheckNameInProduct", "Master", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("CheckShortNameInProduct", "Master", AdditionalFields = "Id")]
        public string ShortName { get; set; }
        public int? ProductGroupId { get; set; }
        public int? ProductSubGroupId { get; set; }
        public int ProductType { get; set; }
        public bool IsBatch { get; set; }
        public int ValTech { get; set; }
        public int? Unit { get; set; }
        public int? AltUnit { get; set; }
        public decimal? AltQuantity { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? SalesPrice { get; set; }
        public decimal? MRP { get; set; }
        public decimal? TradePrice { get; set; }
        public decimal? MRRate { get; set; }
        public decimal? MaxStock { get; set; }
        public decimal? ReorderLevel { get; set; }
        public decimal? MinStock { get; set; }
        public decimal? ReorderQuantity { get; set; }
        public decimal? VatRate { get; set; }
        public decimal? BonusPercentage { get; set; }
        public decimal? MinQuantity { get; set; }
        public decimal? MaxQuantity { get; set; }
        public decimal? MinBonus { get; set; }
        public decimal? MaxBonus { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public int BranchId { get; set; }
        public int BarCode { get; set; }
        public int? Medium { get; set; }
        public bool MfgDate { get; set; }
        public bool ExpDate { get; set; }
        //Ledger Mapping
        public int? PurchaseId { get; set; }//Other Type
        public int? SalesId { get; set; }//Other Type
        public int? SalesReturnId { get; set; }//Other Type
        public int? PurchaseReturnId { get; set; }//Other Type
        public int? ExpiredProduct { get; set; }

        public string Class { get; set; }
        public string Level { get; set; }
        //public int OpeningStockPLId { get; set; }
        //public int ClosingStockPLId { get; set; }
        //public int ClosingStockBS { get; set; }

        [ForeignKey("BranchId")]
        public CompanyInfo Branch { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("Unit")]
        public virtual Unit UnitMaster { get; set; }

        [NotMapped]
        public string ProductImage { get; set; }
    }

}
