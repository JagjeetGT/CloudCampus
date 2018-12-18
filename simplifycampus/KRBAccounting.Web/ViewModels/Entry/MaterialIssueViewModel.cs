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
    public class MaterialIssueViewModel
    {
        public PagedList<MaterialIssue> MaterialIssues { get; set; }
        public MaterialIssueAddViewModel MaterialIssueAddViewModel { get; set; }
        public int DateType { get; set; }
        public EntryControlInventory EntryControl { get; set; }
    }

    public class MaterialIssueAddViewModel
    {
        public int Id { get; set; }
        [Remote("CheckCodeInMaterialIssue", "Entry", AdditionalFields = "Id")]
        [Required(ErrorMessage = " ")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        public string IssueDate { get; set; }
        //[Required(ErrorMessage =" ")]
        public string IssueMiti { get; set; }
        [Required(ErrorMessage = " ")]
        public int BillOfMaterialId { get; set; }
        [Required(ErrorMessage = " ")]
        public string BillOfMaterial { get; set; }
        [Required(ErrorMessage = " ")]
        public int FinishedGoodId { get; set; }
        [Required(ErrorMessage = " ")]
        public string FinishedGood { get; set; }
        [Required(ErrorMessage = " ")]
        public int CostCenterId { get; set; }

        public string CostCenter { get; set; }
        [Required(ErrorMessage = " ")]
        public string Description { get; set; }
        public int UnitId { get; set; }
        [Required(ErrorMessage = " ")]
        public string Unit { get; set; }
        [Required(ErrorMessage = " ")]
        public decimal? Qty { get; set; }
        [Required(ErrorMessage = " ")]
        public decimal? ConversionFactor { get; set; }
        public string ShortName { get; set; }
        public int? StockQty { get; set; }
        public decimal? TotalQty { get; set; }
        public string Remarks { get; set; }
        public decimal NetAmount { get; set; }
        public SelectList UnitList { get; set; }
        public IEnumerable<MaterialIssueDetailAddViewModel> MaterialIssueDetailAddViewModels { get; set; }
        public EntryControlInventory EntryControl { get; set; }
  
        
    }

    public class MaterialIssueDetailAddViewModel
    {
        // [Required(ErrorMessage = " ")]
        public string RawMaterial { get; set; }
        public int RawMaterialId { get; set; }
        // [Required(ErrorMessage = " ")]
        public string CostCenter { get; set; }
        public int? CostCenterId { get; set; }
        // [Required(ErrorMessage = " ")]
        public string Godown { get; set; }
        public int? GodownId { get; set; }
        public int UnitId { get; set; }
        //   [Required(ErrorMessage = " ")]
        public decimal? Quantity { get; set; }
        public SelectList UnitList { get; set; }
        public EntryControlInventory EntryControl { get; set; }
    }

    public class MaterialIssueSingleViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public decimal? Qty { get; set; }
        public decimal? ConversionFactor { get; set; }
        public string Description { get; set; }
    }
}