using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class BillOfMaterialAddViewModel 
    {
        public int Id { get; set; }
        [Remote("CheckCodeInBillOfMaterial", "Entry", AdditionalFields = "Id")]
        [Required(ErrorMessage = "1 ")]
        public string Code { get; set; }
        [Required(ErrorMessage = "2 ")]
        public string Description { get; set; }
        public string CostCenter { get; set; }
        public int CostCenterId { get; set; }
[Required(ErrorMessage = "5 ")]
        public int UnitId { get; set; }

        [Required(ErrorMessage = " 3")]
        public string Unit { get; set; }
       // [Required(ErrorMessage = " ")]
        public decimal? Qty { get; set; }
        [Required(ErrorMessage = " ")]
        public decimal? ConversionFactor { get; set; }

        public string FinishedGood { get; set; }
        [Required(ErrorMessage = "6 ")]
        public int FinishedGoodId { get; set; }
        public string ShortName { get; set; }
        public int? StockQty { get; set; }
        public decimal? TotalQty { get; set; }
        public string Remarks { get; set; }
        public SelectList UnitList { get; set; }
        //public IEnumerable<BillOfMaterialDetail> BillOfMaterialDetails { get; set; }
        public IEnumerable<BillOfMaterialDetailAddViewModel> BillOfMaterialDetailAddViewModels { get; set; }
        public EntryControlInventory EntryControl { get; set; }
        public BillOfMaterial BillOfMaterial { get; set; }
    }

    public class BillOfMaterialDetailAddViewModel
    {
        public int BillOfMaterialId { get; set; }
        //[Required(ErrorMessage = " ")]
        public string RawMaterial { get; set; }
        public int RawMaterialId { get; set; }
        //[Required(ErrorMessage = " ")]
        public string CostCenter { get; set; }
        public int? CostCenterId { get; set; }
       // [Required(ErrorMessage = " ")]
        public string Godown { get; set; }
        public int? GodownId { get; set; }
        public int UnitId { get; set; }
       // [Required(ErrorMessage = " ")]
        public decimal? Quantity { get; set; }
        public SelectList UnitList { get; set; }
        public EntryControlInventory EntryControl { get; set; }
    }

    public class BillOfMaterialViewModel
    {
        public PagedList<BillOfMaterial> BillOfMaterials { get; set; }
        public BillOfMaterialAddViewModel BillOfMaterialAddViewModel { get; set; }
        public EntryControlInventory EntryControl { get; set; }
    }
}