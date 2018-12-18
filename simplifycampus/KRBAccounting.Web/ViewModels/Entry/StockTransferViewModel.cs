using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Web.Controllers;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class StockTransferAddViewModel
    {
        public int Id { get; set; }
        [Remote("CheckCodeInStockTransfer", "Entry", AdditionalFields = "Id")]
        [Required(ErrorMessage = " ")]
        public string STNo { get; set; }
        public string STDate { get; set; }
        public string STMiti { get; set; }
        public string Godown { get; set; }
        public int GodownId { get; set; }

       // public int? CurrencyId { get; set; }
        public decimal? CurRate { get; set; }
        public string CurCode { get; set; }

        public string Description { get; set; }
        public decimal? FromGodownStockQty { get; set; }
        public decimal? ToGodownStockQty { get; set; }
        public decimal? TotalQty { get; set; }
        public decimal NetAmt { get; set; }
        public string Remarks { get; set; }
        public SelectList UnitList { get; set; }
        //public IEnumerable<BillOfMaterialDetail> BillOfMaterialDetails { get; set; }
        public IEnumerable<StockTransferDetailAddViewModel> StockTransferDetailAddViewModels { get; set; }
        public EntryControlInventory EntryControl { get; set; }
        public StockTransferMaster StockTransfer { get; set; }
    }

    public class StockTransferDetailAddViewModel
    {
        public int StockTransferId { get; set; }
        //[Required(ErrorMessage = " ")]
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        //[Required(ErrorMessage = " ")]
        public string Godown { get; set; }
        public int? GodownId { get; set; }
        public int UnitId { get; set; }
       // [Required(ErrorMessage = " ")]
        public decimal? Quantity { get; set; }
        public SelectList UnitList { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
       // public EntryControlInventory EntryControl { get; set; }
    }

    public class StockTransferViewModel
    {
        public PagedList<StockTransferMaster> StockTransfers { get; set; }
        public StockTransferAddViewModel StockTransferAddViewModel { get; set; }
        public int DateType { get; set; }
        public EntryControlInventory EntryControl { get; set; }
        public StockTransferMaster StockTransferMaster { get; set; }
    }
    public class StockTransferSingleViewModel
    {
        public int Id { get; set; }
        public string STNo { get; set; }
        public int GodownId { get; set; }
        public DateTime STDate { get; set; }
        public string Remarks { get; set; }

        [ForeignKey("GodwonId")]
        public virtual Godown Godown { get; set; }
        
    }
}