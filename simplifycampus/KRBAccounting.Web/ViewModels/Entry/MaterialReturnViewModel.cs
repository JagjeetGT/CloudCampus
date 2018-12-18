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
    public class MaterialReturnViewModel 
    {
        public PagedList<MaterialReturn> MaterialReturns { get; set; }
        public MaterialReturnAddViewModel MaterialReturnAddViewModel { get; set; }
        public int DateType { get; set; }
        public EntryControlInventory EntryControl { get; set; }
    }

    public class MaterialReturnAddViewModel 
    {
        public int Id { get; set; }
        [Remote("CheckCodeInMaterialReturn", "Entry", AdditionalFields = "Id")]
        [Required(ErrorMessage = " ")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        public string ReturnDate { get; set; }
        public string ReturnMiti { get; set; }
        //[Required(ErrorMessage = " ")]
        public string IssueDate { get; set; }
        //[Required(ErrorMessage =" ")]
        public string IssueMiti { get; set; }
        [Required(ErrorMessage = " ")]
        public int MaterialIssueId { get; set; }
        [Required(ErrorMessage = " ")]
        public string MaterialIssue { get; set; }
        [Required(ErrorMessage = " ")]
        public int CostCenterId { get; set; }
        public string CostCenter { get; set; }
        public string ShortName { get; set; }
        public int? StockQty { get; set; }
        public decimal? TotalQty { get; set; }
        public string Remarks { get; set; }
        public decimal NetAmount { get; set; }
        public SelectList UnitList { get; set; }
        public IEnumerable<MaterialReturnDetailAddViewModel> MaterialReturnDetailAddViewModels { get; set; }
        public int DateType { get; set; }
        // public string DisplayDate { get; set; }
        public DateTime DisplayDate { get; set; }
        public EntryControlInventory EntryControl { get; set; }
    }

    public class MaterialReturnDetailAddViewModel
    {
      //  [Required(ErrorMessage = " ")]
        public string RawMaterial { get; set; }
        public int RawMaterialId { get; set; }
      //  [Required(ErrorMessage = " ")]
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

    public class MaterialReturnSingleViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
       //public decimal? Qty { get; set; }
        public string  Description { get; set; }
        public string CostCenter { get; set; }

        //[ForeignKey("CostCenterId")]
        //public virtual CostCenter CostCenter { get; set; }
        }
}