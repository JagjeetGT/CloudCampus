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

    public class StockAdjustmentViewModel
    {
        public PagedList<StockAdjustmentMaster> StockAdjustment { get; set; }
        public StockAdjustmenDisplayViewModel StockAdjustmentDisplay { get; set; }
        public int DateType { get; set; }
        public EntryControlInventory EntryControl { get; set; }
    }
    public class StockAdjustmenDisplayViewModel
    {

        public int Id { get; set; }
        [Remote("CheckCodeInStockAdjust", "Entry", AdditionalFields = "Id")]
        [Required(ErrorMessage = " ")]
        public string AdjustmentNo { get; set; }
        public string AdjustmentDate { get; set; }
        public string AdjustmentMiti { get; set; }

        public string Godown { get; set; }
        //public int GodownId { get; set; }

        public string Description { get; set; }
        public decimal? FromGodownStockQty { get; set; }
        public decimal? ToGodownStockQty { get; set; }
        public decimal? TotalQty { get; set; }
        public decimal NetAmt { get; set; }
        public string Remarks { get; set; }

        public decimal? CurRate { get; set; }
        public string CurCode { get; set; }
        public SelectList UnitList { get; set; }
        public SelectList AdjustmentTypeList { get; set; }
        public EntryControlInventory EntryControl { get; set; }

        //public IEnumerable<BillOfMaterialDetail> BillOfMaterialDetails { get; set; }
        public IEnumerable<StockAdjustmentDetailAddViewModel> StockAdjustmentDetailAddViewModels { get; set; }
    }
    public class StockAdjustmentDetailAddViewModel
    {
        public int StockAdjustmentId { get; set; }
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
        public SelectList AdjustmentTypeList { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public bool IsDeleted { get; set; }
        public string Adjustment { get; set; }
        public EntryControlInventory EntryControl { get; set; }
    }

    public class StockAdjustmentSingleViewModel
    {
        public int Id { get; set; }
        public string STNo { get; set; }

        public DateTime STDate { get; set; }
        public string Remarks { get; set; }



    }
}