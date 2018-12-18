using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Domain.Entities
{
    public class SystemControl
    {
        [Key]
        public int Id { get; set; }

        public int? ExpiredProduct { get; set; }
        public int PageSize { get; set; }
        public bool EnableBranch { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual CompanyInfo CompanyInfo { get; set; }

        public int EducationTaxAc { get; set; }
        public int LibraryLateFine { get; set; }
        public int StudentFeeAc { get; set; }
        public int DepositAc { get; set; }
        public int NoOfFeeReceiptPrint { get; set; }
        public int NoOfBillPrint { get; set; }
        public bool PrintDataOnly { get; set; }

       public int NegativeStock { get; set; }
       public int PurchaseDeriveFrom { get; set; }
       public int SalesDeriveFrom { get; set; }
        #region Optiontabs

        [Display(Name = "Date Format")]
        public int DateType { get; set; } // Option Tabs
        [Display(Name = "Audit Trail")]
        public string AuditTrial { get; set; } // Option Tabs
        public string UDF { get; set; } // Option Tabs
        [Display(Name = "Currency Code")]
        public string CurrCode { get; set; } // Option tabs
        [Display(Name = "Description")] // Currency Description
        public string CurrDesc { get; set; } // Option tabs
        [Display(Name = "Unit")] // Currency Unit
        public string CurrUnit { get; set; } // Option tabs
        [Display(Name = "Auto PopUp")]
        public bool IsAutoPopup { get; set; } // Option tabs
        [Display(Name = "Current Date")]
        public bool IsCurrDate { get; set; } // Option tabs 
        [Display(Name = "Confirm Saving")]
        public bool IsConfirmSaving { get; set; } // Option tabs
        [Display(Name = "Confirm Cancel")]
        public bool IsConfirmCancel { get; set; } // Option tabs
        #endregion

        #region PL tabs
        [Display(Name = "Profit & Loss")]
        public int ProfitLossAc { get; set; } // PL tabs
        [Display(Name = "Cash Book")]
        public int CashBook { get; set; } // PL tabs
        #endregion
        #region Sales tabs
        [Display(Name = "Sales A/C")]
        public int SalesAc { get; set; }// Sales tabs
        [Display(Name = "Sales Return A/C")]
        public int SalesReturnAc { get; set; }

        [Display(Name = "Vat A/C")]
        public int Vat { get; set; }



        #endregion
        #region Purchase tabs
        [Display(Name = "Purchase A/c")]
        public int PurchaseAc { get; set; }

        [Display(Name = "Purchase Return A/C")]
        public int PurchaseReturnAc { get; set; }

        #endregion

        #region Inventory
        [Display(Name = "Opening Stock P/L")]
        public int OpeningStockPl { get; set; }

        [Display(Name = "Closing Stock P/L")]
        public int ClosingStockPl { get; set; }

        [Display(Name = "Closinging Stock")]
        public int ClosingingStock { get; set; }
        #endregion

        
    }
}
