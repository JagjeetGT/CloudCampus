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

    public class ExpBrkViewModel
    {
        public PagedList<ExpiryBreakageMaster> ExpiryBreakage { get; set; }
        public ExpBrkDisplayViewModel ExpBrkDisplay{ get; set; }
        public int DateType { get; set; }
        public EntryControlInventory EntryControl { get; set; }
    }
    public class ExpBrkDisplayViewModel 
    {

        public int Id { get; set; }
        [Remote("CheckCodeInExpiryBreakage", "Entry", AdditionalFields = "Id")]
        [Required(ErrorMessage = " ")]
        public string ExpBrkNo { get; set; }
        public string Date { get; set; }
        public string DateMiti { get; set; }
        public decimal? CurRate { get; set; }
        public string CurCode { get; set; }
        //public int GodownId { get; set; }

        public string Description { get; set; }
        public decimal? FromGodownStockQty { get; set; }
        public decimal? ToGodownStockQty { get; set; }
        public decimal? TotalQty { get; set; }
        public decimal NetAmt { get; set; }
        public string Remarks { get; set; }
        public SelectList UnitList { get; set; }
        public SelectList TypeList { get; set; }
        public string Type { get; set; }
        public EntryControlInventory EntryControl { get; set; }
        //public IEnumerable<BillOfMaterialDetail> BillOfMaterialDetails { get; set; }
        public IEnumerable<ExpBrkDetailAddViewModel> ExpBrkDetailAddViewModels { get; set; }
    }
    public class ExpBrkDetailAddViewModel
    {
        public int ExpBrkId { get; set; }
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
        public SelectList TypeList { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public bool IsDeleted { get; set; }
        public EntryControlInventory EntryControl { get; set; }
      }

    public class ExpBrkSingleViewModel  
    {
        public int Id { get; set; }
        public string STNo { get; set; }
        public string Type { get; set; }
        public DateTime STDate { get; set; }
        public string Remarks { get; set; }
    }
}