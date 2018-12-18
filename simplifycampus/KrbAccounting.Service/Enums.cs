using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Service.Helpers;

namespace KRBAccounting.Enums
{
    public enum LedgerCategoryEnum
    {
        [StringValue("Both")]
        BO = 1,
        [StringValue("Customer")]
        CU,
        [StringValue("Other")]
        OT,
        [StringValue("Vendor")]
        VE
    }
   
    public enum AccountGroupTypeEnum
    {
        [StringValue("Balance Sheet")]
        B = 1,
        [StringValue("Profit & Loss")]
        P,
        [StringValue("Trading")]
        T
    }

    public enum BalanceSheetTypeEnum
    {
        [StringValue("Assets")]
        A = 1,
        [StringValue("Liabilities")]
        L

    }

    public enum PLTypeEnum
    {
        [StringValue("Income")]
        I = 1,
        [StringValue("Expenditure")]
        E


    }

    public enum ProductTypeEnum
    {
        [StringValue("Inventory Item")]
        Inventory = 1,
        [StringValue("Service Item")]
        Service

    }

    public enum BonusTypeEnum
    {
        General = 1,
        Percentage
    }

    public enum YesNoEnum
    {
        Yes = 1,
        No
    }

    public enum ValTechEnum
    {
        [StringValue("Fifo")]
        Fifo = 1,
        [StringValue("Wt. Avg")]
        WtAvg,
        [StringValue("Rated Fifo")]
        RatedFifo,
        [StringValue("Monthly Rated")]
        MonthlyRated
    }

    public enum NarrationTypeEnum
    {
        Both = 1,
        Remarks,
        Narration
    }

    public enum BillingTermCategoryEnum
    {
        [StringValue("General")]
        General = 1,
        [StringValue("Additional")]
        Additional,
        [StringValue("Free")]
        Free,
        [StringValue("Rounded Off")]
        RoundedOff
    }

    public enum BillingTermBasisEnum
    {
        Quantity = 1,
        Value
    }

    public enum SignEnum
    {
        [StringValue("+")]
        Plus = 1,
        [StringValue("-")]
        Minus = 2
    }

    public enum AmountTypeEnum
    {
        Receipt = 1,
        Payment
    }

    public enum JVAmountTypeEnum
    {
        [StringValue("Debit")]
        D = 1,
        [StringValue("Credit")]
        C
    }

    public enum ModuleEnum
    {
        [StringValue("Cash/Bank Book")]
        CB = 1,
        [StringValue("Journal Voucher")]
        JV,
        [StringValue("Debit Note")]
        DN,
        [StringValue("Credit Note")]
        CN,
        [StringValue("Purchase Indent")]
        PI,
        [StringValue("Purchase Quotation")]
        PQ,
        [StringValue("Purchase Order")]
        PO,
        [StringValue("Purchase Challan")]
        PC,
        [StringValue("Purchase Order Cancellation")]
        POC,
        [StringValue("Purchase")]
        PB,
        [StringValue("Purchase Additional")]
        PA,
        [StringValue("Purchase Return")]
        PR,
        [StringValue("Purchase Expiry/Breakage Return")]
        PER,
        [StringValue("Sales Order Cancellation")]
        SOC,
        [StringValue("Sales Order")]
        SO,
        [StringValue("Sales Quotation")]
        SQ,
        [StringValue("Sales Challan")]
        SC,
        [StringValue("Sales")]
        SB,
        [StringValue("Sales Additional")]
        SA,
        [StringValue("Sales Return")]
        SR,
        [StringValue("Sales Expiry/Breakage Return")]
        SER,
        [StringValue("Material Issue")]
        MI,
        [StringValue("Material Return")]
        MR,
        [StringValue("Monthly Bill Generation")]
        MBG,
        [StringValue("Fee Receipt")]
        REC,
        [StringValue("Student Library Registration")]
        SLR,
        [StringValue("Library Card Number")]
        LCN,
        [StringValue("Library Book Received Number")]
        LBN,
        [StringValue("Payroll Payment Slip Number")]
     PSN,
        [StringValue("Payroll Generation")]
        PG,
         [StringValue("Library Late Fine")]
        LLF



    }

    public enum DocumentCashBankBookTypeEnum
    {
        [StringValue("All")]
        A = 1,
        [StringValue("Receipt")]
        R,
        [StringValue("Payment")]
        P,
        [StringValue("Contra")]
        C
    }

    public enum DocumentNumberStyleTypeEnum
    {
        [StringValue("Auto (Default)")]
        D = 1,
        [StringValue("Alpha Numeric")]
        A,
        [StringValue("Numeric")]
        N
    }

    public enum DateFormatEnum
    {
        [StringValue("Date")]
        D = 1,
        [StringValue("Miti")]
        M

    }


    public enum PurchaseInvoiceType
    {
        [StringValue("Local Invoice")]
        L = 1,
        [StringValue("Import Invoice")]
        I
    }

    public enum TransactionTypeEnum
    {
        [StringValue("In")]
        I = 1,
        [StringValue("Out")]
        O
    }


    public enum CnTypeEnum
    {
        [StringValue("Due")]
        D = 1,
        [StringValue("To Pay")]
        T,
        [StringValue("Paid")]
        P,
        [StringValue("Free")]
        F
    }

    public enum ControlTypeEnum
    {
        [StringValue("String")]
        Textbox = 1,
        [StringValue("Number")]
        Number,
        [StringValue("Date")]
        Date,
        [StringValue("Yes/No")]
        YesNo,
        [StringValue("Memo")]
        Memo,
        [StringValue("List")]
        List
    }

    public enum EntryModuleEnum
    {
        [StringValue("Ledger Master")]
        LedgerMaster = 1,
        [StringValue("Cash/Bank Master")]
        CashBankMaster,
        [StringValue("Cash/Bank Detial")]
        CashBankDetial,
        [StringValue("Journal Voucher Master")]
        JournalVoucherMaster,
        [StringValue("Journal Voucher Detail")]
        JournalVoucherDetial,
        [StringValue("Debit Note Master")]
        DebitNoteMaster,
        [StringValue("Debit Note Detail")]
        DebitNoteDetail,
        [StringValue("Credit Note Master")]
        CreditNoteMaster,
        [StringValue("Credit Note Detial")]
        CreditNoteDetial,
        [StringValue("Purchase Invoice")]
        PurchaseInvoice,
        [StringValue("Purchase Invoice Detail")]
        PurchaseInvoiceDetail,
        [StringValue("Purchase Invoice Return Detail")]
        PurchaseInvoiceReturnDetail,
        [StringValue("Purchase Invoice Return")]
        PurchaseInvoiceReturn,
        [StringValue("Sales Invoice")]
        SalesInvoice,
        [StringValue("Sales Invoice Detail")]
        SalesInvoiceDetail,
        [StringValue("Sales Invoice Return Detail")]
        SalesInvoiceReturnDetail,
        [StringValue("Sales Invoice Return")]
        SalesInvoiceReturn,
        [StringValue("Purchase Order")]
        PurchaseOrder
    }

    public enum TrialBalanceReportTypeEnum
    {
        [StringValue("Normal")]
        Normal = 1,
        [StringValue("Group Wise")]
        GroupWise,
        [StringValue("Periodic")]
        Periodic
    }

    public enum ProfitAndLossReportTypeEnum
    {
        [StringValue("Normal")]
        Normal = 1,
        [StringValue("Periodic")]
        Periodic
    }

    public enum PartyLedgerTypeEnum
    {
        [StringValue("Customer")]
        CU = 1,
        [StringValue("Vendor")]
        VE
    }

    public enum BalanceSheetReportTypeEnum
    {
        [StringValue("Normal")]
        Normal = 1,
        [StringValue("Periodic")]
        Periodic
    }

    public enum BalanceSheetReportSohwEnum
    {
        [StringValue("Both")]
        B,
        [StringValue("Assets")]
        A = 1,
        [StringValue("Liabilities")]
        L
    }
    public enum PLAndBSReportFormatEnum
    {
        [StringValue("Normal")]
        Normal = 1,
        [StringValue("T - Format")]
        Periodic
    }
    public enum EntryControlModuleEnum
    {
        PL = 1,
        SB,
        PB,
        I
    }


    //Cooperative

    public enum DepositNatureEnum
    {
        [StringValue("Daily")]
        D = 1,
        [StringValue("Monthly")]
        M,
        [StringValue("Quarterly")]
        Q,
        [StringValue("Half Yearly")]
        H,
        [StringValue("Yearly")]
        Y,
        [StringValue("Irregular")]
        I
    }

    public enum Sex
    {
        [StringValue("Male")]
        M = 1,
        [StringValue("Female")]
        F
    }

    public enum Relation
    {
        Father = 1,
        Mother = 2,
        Son = 3,
        Daughter = 4,
        Brother = 5,
        Sister = 6
    }

    public enum InterestCalculationBase
    {
        [StringValue("Daily")]
        D = 1,
        [StringValue("Monthly Average Balance")]
        M,
        //[StringValue("Quarterly")]
        //Q,
        //[StringValue("Average")]
        //A,
        [StringValue("Minimum")]
        Min
    }

    public enum InterestPostingEnum
    {
        //[StringValue("Monthly")]
        //M = 1,
        [StringValue("Quarterly")]
        Q = 1,
        [StringValue("Half Yearly")]
        HY,
        [StringValue("Yearly")]
        Y
    }

    public enum CollectionType
    {
        [StringValue("Deposit")]
        D = 1,
        [StringValue("Loan")]
        L //,
        //[StringValue("Others")]
        //O
    }

    public enum ChequeType
    {

        Saving = 1,
        Current
    }

    public enum Status
    {

        Active = 1,
        InActive
    }

    public enum ChequeBankOf
    {
        Self = 1,
    }

    public enum PaymentType
    {
        Cash = 1,
        Account
    }

    public enum ShareStatus
    {
        Issued = 1,
        Add,
        Transfer,
        Return,
        Deleted
    }

    public enum DepositType
    {
        Cash = 1,
        Cheque = 2
    }

    public enum LoanSystemEnum
    {
        Flat = 1,
        Diminishing = 2
    }

    public enum DiminishingTypeEnum
    {
        PrincipalSameInstallmentVary = 1,
        PrincipalVaryInstallmentSame = 2
    }


    public enum DefaultReportTypeEnum
    {
        Normal = 1,
        Periodic
    }

    public enum VaultTypeEnum
    {
        MainVault = 1,
        TellerVault = 2
    }

    public enum MaritalStatusEnum
    {
        Married = 1,
        Unmarried = 2
    }

    public enum OccupationTypeEnum
    {
        Business = 1,
        Service = 2,
        Agriculture = 3,
        Others = 4
    }

    public enum AttachedDocumentsEnum
    {
        [StringValue("Identity Card")]
        IdentityCard,
        [StringValue("Nationality")]
        Nationality,
        [StringValue("Driving License")]
        DrivingLicense,
        [StringValue("Passport")]
        Passport,
        [StringValue("Others")]
        Others
    }


    public enum InterestPaymentTypeEnum
    {
        [StringValue("Monthly")]
        Monthly = 1,
        [StringValue("Quartely")]
        Quartely = 2,
        [StringValue("Half Yearly")]
        HalfYearly = 3,
        [StringValue("Yearly")]
        Yearly = 4,
        [StringValue("Lump Sum")]
        LumpSum = 5
    }

    public enum AccountNatureEnum
    {
        Single = 1,
        Joint = 2,
        Company = 3
    }

    public enum FollowUpType
    {
        Phone = 1,
        Email
    }

    public enum PaymentMode
    {
        [StringValue("Cash")]
        C = 1,
        [StringValue("Account")]
        A
    }

    public enum ModuleType
    {
        Reading = 1,
        Writing
    }

    public enum ConsultancyFeeType
    {
        Internal = 1,
        Abroad
    }

    public enum ConsultancyStudentType
    {
        Internal = 1,
        Abroad,
        Both
    }

    public enum Weekholidayenum
    {
        Sunday = 1,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    #region School

    #region Subject

    public enum EvaluateForPass
    {
        Yes = 1,
        No = 2,

    }

    public enum SubjectType
    {
        Compulsory = 1,
        Optional = 2,

    }

    public enum ClassType
    {
        Theory = 1,
        Practical = 2,
        Both = 3

    }

    public enum ResultSystem
    {
        Mark = 1,
        Grade = 2,
    }

    #endregion


    #region Exam

    public enum ExamType
    {
        [StringValue("Class Test")]
        ClassTest = 1,
        [StringValue("Unit Test")]
        UnitTest = 2,
        [StringValue("Terminal Examanation")]
        TerminalExamanation = 3,
        [StringValue("Entrance Examanation")]
        EntranceExamanation = 4,
    }

    #endregion

    #region Fee

    public enum FeeType
    {
        Admission = 1,
        Monthly = 2,
        Exam = 3,
        Others = 4
    }

    public enum FeeTermCategory
    {
        [StringValue("General")]
        General = 1,
        [StringValue("Rounded Off")]
        RoundedOff = 2,

    }

    public enum FeeTermRoundedOffType
    {
        Near = 1,
        Higher = 2,
        Lower = 3
    }

    public enum FeeTermType
    {
        //[StringValue("Bill Wise")]
        //BillWise = 1,
        [StringValue("Fee Item Wise")]
        FeeItemWise = 2,
    }

    #endregion

    public enum StatusEnum
    {
        Running = 1,
        OutDate,
        Leave,
    }

    public enum PostEnum
    {
        Teacher = 1,
        Helper,
        Guard,
        HostelTeacher,


    }

    public enum StudentStatus
    {
        Active = 1,
        Enactive
    }

    public enum StudentCurrentStatus
    {
        Current = 1,
        PassedOut,
        LeaveOut,
        Supended,
        Others
    }

    public enum StudentType
    {
        New = 1,
        OLd,

    }

    public enum GroupType
    {
        Staff = 1,
        Student,

    }

    public enum ActivityStatus
    {
        [StringValue("Excellent")]
        Excellent = 1,
        [StringValue("Good")]
        Good,
        [StringValue("Satisfactory")]
        Satisfactory,
        [StringValue("Un-Satisfactory")]
        UnSatisfactory,
    }

    public enum ExamAttendanceStatus
    {
        [StringValue("Present")]
        Present = 1,
        [StringValue("Absent")]
        Absent,
        [StringValue("WithHeld")]
        WithHeld,
        [StringValue("Suspend")]
        Suspend,
    }

    public enum CertificateTypeEnum
    {
        [StringValue("Character Certificate SLC")]
        CharacterCertificateSLC = 1,
        [StringValue("Transfer Certificate")]
        TransferCertificate=2,
        [StringValue("Character Certificate")]
        CharacterCertificate=5,
    }

    public enum TemplateType
    {
        [StringValue("Character Certificate SLC")]
        CharacterCertificateSLC = 1,
        [StringValue("Transfer Certificate")]
        TransferCertificate,
        [StringValue("Exam Routine Footer")]
        ExamRoutineFooter,
        [StringValue("Result Footer")]
        ResultFooter,
        [StringValue("Character Certificate")]
        CharacterCertificate,
    }

    #endregion

    public enum LibraryBookStatus
    {
        Available = 1,
        Borrowed,
        Reserved,
        Lost
    }

    public enum AllowanceType
    {
        [StringValue("All purpose allowance")]
        A = 1,
        [StringValue("Call out allowance")]
        B,
        [StringValue("Camping allowance")]
        C,
        [StringValue("Car allowance")]
        D,
        [StringValue("Climatic and zone allowance")]
        E,
        [StringValue("District allowance")]
        F,
        [StringValue("First aid allowance")]
        G,
        [StringValue("Follow The Job allowance")]
        H,
        [StringValue("Industry allowance")]
        I,
        [StringValue("Leading Hand allowance")]
        J,
        [StringValue("Living Away From Home allowance")]
        K,
        [StringValue("Meal allowance")]
        L,
        [StringValue("Shift allowance")]
        M,
        [StringValue("Site allowance")]
        N
    }
    public enum DeductionType
    {
        [StringValue("Loan")]
        L = 1,
        [StringValue("Advance")]
        A,
        [StringValue("Insurance")]
        I,
        [StringValue("Others")]
        O,

    }
    public enum StudentOpeningCategory
    {
        [StringValue("Due")]
        Due = 1,
        [StringValue("Security Deposit")]
        SecurityDeposit = 2,
    }
    public enum SalaryHeadType
    {
        [StringValue("Basic Salary")]
        BasicSalary = 1,
        [StringValue("Allowance")]
        Allowance,
        [StringValue("Deduction")]
        Deduction,
        [StringValue("PF Deduction")]
        PFDeduction, 
        [StringValue("Tax Deduction")]
        TaxDeduction,
    }
    public enum ReportCountList
    {
        Category=1,
        Address,
        Rel
    }
    public enum BloodGroupDropDown
    {
        [StringValue("None")]
        None=0,
         [StringValue("A+")]
        Apve = 1,
         [StringValue("B+")]
         Bpve =2,
         [StringValue("AB+")]
         ABpve = 3,
         [StringValue("O+")]
         Opve = 4,
         [StringValue("A-")]
         Anve = 5,
         [StringValue("B-")]
         Bnve = 6,
         [StringValue("AB-")]
         ABnve = 7,
         [StringValue("O-")]
         Onve = 8,
       
    }
    public enum ExpBrkType
    {
        [StringValue("Expiry")]
        E = 1,
        [StringValue("Breakage")]
        B
    }
    public enum ExpiredProduct
    {
        Block = 1,
        Alert = 2,
        Ignore = 3
    }
    public enum JVSalesTypeEnum
    {
        [StringValue("Sales")]
        S = 1,
        [StringValue("Purchase")]
        P
    }
    public enum Medium
    {
        Nepali = 1,
        English,
        Hindi
    }
    public enum GodownInventoryTypeEnum
    {
        [StringValue("Godown/Product")]
        PG = 1,
        [StringValue("Product/Godown")]
        GP
    }
    public enum OpeningReportTypeEnum
    {
        [StringValue("Include Summary")]
        IS = 1,
        [StringValue("Include Details")]
        ID = 2,
        [StringValue("Opening Only")]
        OO = 3
    }

    public enum LedgerReportGroupByEnum
    {
        [StringValue("Ledger")]
        LE = 1,
        [StringValue("Agent")]
        AG,
        [StringValue("Area")]
        AR,
        [StringValue("Account Group")]
        AC,
        [StringValue("Account Sub Group")]
        ASC
    }
    public enum SalesReportGroupByEnum
    {
        [StringValue("Date")]
        D = 1,
        [StringValue("Customer")]
        CU,
        [StringValue("Agent")]
        AG,
        [StringValue("Area")]
        AR,
        [StringValue("Product")]
        P,
        [StringValue("Product Group")]
        PG,
        [StringValue("Product Sub Group")]
        PSG
    }
    public enum PurchaseReportGroupByEnum
    {
        [StringValue("Date")]
        D = 1,
        [StringValue("Vendor")]
        CU,
        [StringValue("Agent")]
        AG,
        [StringValue("Area")]
        AR,
        [StringValue("Product")]
        P,
        [StringValue("Product Group")]
        PG,
        [StringValue("Product Sub Group")]
        PSG
    }
    public enum LedgerReportTypeEnum
    {
        [StringValue("All Ledger")]
        A = 1,
        [StringValue("Customer")]
        CU = 2,
        [StringValue("Vendor")]
        VE = 3,
        [StringValue("Both")]
        BO = 4,
        [StringValue("Other")]
        OT = 5
    }
    public enum PeriodicType
    {
        [StringValue("Monthly")]
        M = 1,
        [StringValue("Yearly")]
        Y
    }
    public enum Periodic_Divisor
    {
        [StringValue("Ones")]
        One = 1,
        [StringValue("Hundreds")]
        Hun = 100,
        [StringValue("Thousands")]
        Tho = 1000,
        [StringValue("Ten Thousands")]
        TenThou = 10000,
        [StringValue("Lakhs")]
        Lakh = 100000,
        [StringValue("Ten Lakhs")]
        TenLa = 1000000
    }
    public enum TP_List
    {
        Students=1,
        ClassRoutine,
        ExamRoutine,
        ExamMarkSetup,
        MarkEntry
    }
    public enum SP_UserType
    {
        Student=1,
        Teacher
    }
   public enum HeaderMenu
   {
       Dashboard=1,
       Accounting,
       Academy,
       Payroll,
       Inventory,
       SMS,
       Library,
       Setting,

   }
   public enum NepaliMonth
   {
       Baisakh = 1,
       Jestha,
       Asadh,
       Shrawan,
       Bhadra,
       Asoj,
       Kartik,
       Mangsir,
       Poush,
       Magh,
       Falgun,
       Chaitra
   }
   public enum EnglishMonth
   {
       Janaury = 1,
       Febraury,
       March,
       April, 
       May,
       June,
       July,
       August,
       September,
       October,
       Noverber,
       December
     
      
   }

    public enum DurationType
    {
        Year=1,
        Semester,
        Trimester
    }
}
