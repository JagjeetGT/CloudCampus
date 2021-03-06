
GO
/****** Object:  Table [dbo].[User]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[EmailAddress] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastLoginIp] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CompanyId] [int] NOT NULL,
	[BranchId] [int] NULL,
	[AllBranch] [bit] NULL,
	[LastAccessedBranch] [int] NULL,
	[ImageGuid] [nvarchar](50) NULL,
	[Ext] [nvarchar](10) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDFEntryDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDFEntryDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UdfId] [int] NOT NULL,
	[Value] [nvarchar](max) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDFEntry]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDFEntry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EntryModuleId] [int] NOT NULL,
	[FieldName] [nvarchar](max) NOT NULL,
	[ControlTypeId] [int] NOT NULL,
	[BodyLength] [nvarchar](max) NOT NULL,
	[FieldDecimal] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
	[DuplicateOpt] [bit] NOT NULL,
	[MandatoryOpt] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDFData]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDFData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReferenceId] [int] NOT NULL,
	[Value] [nvarchar](max) NULL,
	[UdfId] [int] NOT NULL,
	[SourceId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Template]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Template](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[TemplateType] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CertificateTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Date]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Date](
	[Nepali] [nvarchar](50) NULL,
	[English] [nvarchar](50) NULL,
	[DATE_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[HOLIDAYS] [varchar](3) NULL,
	[NEP_DATE] [varchar](10) NULL,
	[ENG_DATE] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SystemControl]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemControl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[DateType] [int] NOT NULL,
	[AuditTrial] [nvarchar](max) NULL,
	[UDF] [nvarchar](max) NULL,
	[CurrCode] [nvarchar](max) NULL,
	[CurrDesc] [nvarchar](max) NULL,
	[CurrUnit] [nvarchar](max) NULL,
	[IsAutoPopup] [bit] NOT NULL,
	[IsCurrDate] [bit] NOT NULL,
	[IsConfirmSaving] [bit] NOT NULL,
	[IsConfirmCancel] [bit] NOT NULL,
	[ProfitLossAc] [int] NOT NULL,
	[CashBook] [int] NOT NULL,
	[SalesAc] [int] NOT NULL,
	[SalesReturnAc] [int] NOT NULL,
	[Vat] [int] NOT NULL,
	[PurchaseAc] [int] NOT NULL,
	[PurchaseReturnAc] [int] NOT NULL,
	[OpeningStockPl] [int] NOT NULL,
	[ClosingStockPl] [int] NOT NULL,
	[ClosingingStock] [int] NOT NULL,
	[PageSize] [int] NULL,
	[EnableBranch] [bit] NOT NULL,
	[StudentFeeAc] [int] NOT NULL,
	[EducationTaxAc] [int] NOT NULL,
	[DepositAc] [int] NOT NULL,
	[NoOfFeeReceiptPrint] [int] NOT NULL,
	[PrintDataOnly] [bit] NOT NULL,
	[NoOfBillPrint] [int] NOT NULL,
	[ExpiredProduct] [int] NULL,
	[LibraryLateFine] [int] NULL,
 CONSTRAINT [PK_SystemControl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubLedger]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubLedger](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[PhoneNo] [nvarchar](max) NULL,
	[InterestRate] [decimal](18, 2) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentSlcInfo]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentSlcInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[SlcRegNo] [nvarchar](50) NULL,
	[SlcSymbolNo] [nvarchar](50) NULL,
	[Percentage] [decimal](18, 2) NULL,
	[Division] [nvarchar](50) NULL,
	[PassYearBS] [nvarchar](50) NULL,
	[PassYearAD] [nvarchar](50) NULL,
 CONSTRAINT [PK_StudentSlcInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentDocument]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentDocument](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[DocumentGuid] [nvarchar](500) NULL,
	[DocumentExt] [nvarchar](500) NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_StudentDocumentAdd] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[splitstring_to_table]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[splitstring_to_table]
(
    @string NVARCHAR(MAX),
    @delimiter CHAR(1)
)
RETURNS @output TABLE(
    data NVARCHAR(MAX)
)
BEGIN
    DECLARE @start INT, @end INT
    SELECT @start = 1, @end = CHARINDEX(@delimiter, @string)

    WHILE @start < LEN(@string) + 1 BEGIN
        IF @end = 0 
            SET @end = LEN(@string) + 1

        INSERT INTO @output (data) 
        VALUES(SUBSTRING(@string, @start, @end - @start))
        SET @start = @end + 1
        SET @end = CHARINDEX(@delimiter, @string, @start)
    END
    RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[Split1]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create FUNCTION [dbo].[Split1](@String varchar(8000), @Delimiter char(1),@Count int)
returns @temptable TABLE (items varchar(8000))
as
begin
declare @idx int
declare @slice varchar(8000)
declare @i int
set @i = 1
select @idx = 1
if len(@String)<1 or @String is null return
while @idx!= 0
begin

set @idx = charindex(@Delimiter,@String)
if @idx!=0
set @slice = left(@String,@idx - 1)
else
set @slice = @String

if(len(@slice)>0)
begin
if(@Count = @i)
begin
insert into @temptable(Items) values(@slice)
end
end
set @i = @i + 1
set @String = right(@String,len(@String) - @idx)
if len(@String) = 0 break
end
return
end
GO
/****** Object:  StoredProcedure [dbo].[SP_SubLedgers]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_SubLedgers]
@LedgerCategory varchar(10)='OT',
@StartDate datetime = null,
@EndDate datetime = null,
@LedgerId int,@FYId int,@SubledgerId varchar(max)
as
declare @sql varchar(max)
select @sql = 'Select Distinct sl.Id,sl.Description,sl.ShortName from Ledger l
Inner join AccountTransaction at on l.Id = at.GlCode
 Left Outer Join SubLedger sl on at.SlCode = sl.Id
Where l.Category in (''' + @LedgerCategory + ''') AND at.FYId=' + CAST(@FYId as Varchar(5)) + ' AND at.GlCode =' + CAST(@LedgerId as Varchar(5))
if @SubledgerId <> ''
	Begin
		Set @sql += ' and at.SlCode In (' +  @SubledgerId + ')'	
	End

if @StartDate <> ''
	Begin
		select @sql += ' AND at.VDate >='''  + Cast(@StartDate as varchar(20)) + ''''
	end
	
if @EndDate <> ''
	Begin
		select @sql += ' AND at.VDate <='''  + Cast(@EndDate as varchar(20)) + ''''
	end
	

--select (@sql)
exec (@sql)
GO
/****** Object:  Table [dbo].[StockTransaction]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StockTransaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VNo] [nvarchar](max) NULL,
	[VDate] [date] NOT NULL,
	[VMiti] [nvarchar](max) NULL,
	[GlCode] [int] NOT NULL,
	[AgentCode] [nvarchar](max) NULL,
	[CurrCode] [nvarchar](max) NULL,
	[CurrRate] [decimal](18, 2) NULL,
	[SNo] [int] NOT NULL,
	[BatchSerialNo] [varchar](256) NULL,
	[ProductCode] [int] NOT NULL,
	[Godown] [nvarchar](max) NULL,
	[AltQty] [decimal](18, 2) NULL,
	[AltUnit] [nvarchar](max) NULL,
	[Quantity] [decimal](18, 2) NULL,
	[Unit] [nvarchar](max) NULL,
	[AltStockQty] [decimal](18, 2) NULL,
	[StockQty] [decimal](18, 2) NOT NULL,
	[FreeQty] [decimal](18, 2) NULL,
	[StockFreeQty] [decimal](18, 2) NULL,
	[FreeUnit] [nvarchar](max) NULL,
	[STConvRatio] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TermAmt] [decimal](18, 2) NULL,
	[NetAmt] [decimal](18, 2) NOT NULL,
	[StockVal] [decimal](18, 2) NOT NULL,
	[TransactionType] [nvarchar](max) NULL,
	[Source] [nvarchar](max) NULL,
	[ReferenceId] [int] NOT NULL,
	[FYId] [int] NOT NULL,
	[GdnCode] [nvarchar](max) NULL,
	[DocVal] [decimal](18, 2) NULL,
	[TmpStockVal] [decimal](18, 2) NULL,
	[BillTerm] [decimal](18, 2) NULL,
	[ExtraFreeQty] [decimal](18, 2) NULL,
	[ExtraStockFreeQty] [decimal](18, 2) NULL,
	[ExtraFreeUnit] [nvarchar](max) NULL,
	[BranchId] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StockAdjustmentMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StockAdjustmentMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AdjustmentDate] [datetime] NULL,
	[AdjustmentMiti] [varchar](10) NULL,
	[ClassCode] [int] NULL,
	[Remarks] [nvarchar](max) NULL,
	[BrCode] [varchar](10) NULL,
	[AdjustmentNo] [varchar](20) NULL,
	[PhysicalStockNo] [varchar](25) NULL,
	[CreatedById] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[BranchId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[SP_StudentWiseHouse]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_StudentWiseHouse]
(
	@StartDate datetime,
	@EndDate datetime,
	@ClassId int,
	@HouseId int,
	@StudentId nvarchar(max)	
)
as
--SP_StudentWiseHouse '2013-01-01','2014-02-02','',11,'72,74'
Declare @Sql varchar(max)
select @Sql = ' select hg.Code as HouseCode,hg.Description as HouseName,cl.Description as ClassName,
				si.StdCode as StudentCode,si.StuDesc as StudentName,srd.RollNo,ss.Description as Section,
				hm.StartDate,hm.EndDate
				from ScHouseMapping as hm
				join ScHouseGroup as hg on hm.HouseGroupId = hg.Id
				join SchClass as cl on cl.Id = hm.ClassId
				join ScStudentinfo as si on si.StudentID = hm.StudentId
				join ScStudentRegistrationDetail as srd on srd.StudentId = hm.StudentId
				join ScSection as ss on ss.ID = srd.SectionId
				Where hm.StartDate between ''' + CAST(@StartDate as varchar(12))+'''and''' + CAST(@EndDate as varchar(12))+''''
				if @classId <> ''
				begin
				select @Sql += 'and hm.ClassId = '+ CAST(@ClassId as Varchar(5)) +''
				end
				if @HouseId <> ''
				begin
				select @Sql += 'and hm.HouseGroupId = '+ CAST(@HouseId as Varchar(5)) +''
				end
				if @StudentId <> ''
				Begin 
					select @sql += ' and hm.StudentId In  (' + @StudentId +')'
				End
				exec(@sql)
GO
/****** Object:  StoredProcedure [dbo].[SP_OutstandingSupplierLedgers]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_OutstandingSupplierLedgers]
@LedgerCategory varchar(255)='OT',
@AsOnDate datetime,
@LedgerList varchar(max)='',@FYId int
as
declare @sql varchar(max)
select @sql = 'select Distinct l.Id,l.AccountName,l.ShortName 
from AccountTransaction at Inner join Ledger l on l.Id = at.GlCode
 where  CrAmt <> 0 And  l.Category in(''BO'',''' + @LedgerCategory + ''') AND at.FYId=' + CAST(@FYId as Varchar(5))
select @sql += ' AND at.VDate <='''  + Cast(@AsOnDate as varchar(20)) + ''''
	
if @LedgerList <> ''
	Begin 
		select @sql += ' and l.Id In(' + @LedgerList + ')'
	end
	
	select @sql += 'Group By l.Id,l.AccountName,l.ShortName   having Sum(CrAmt) <> 0'
--select (@sql)
exec (@sql)
GO
/****** Object:  StoredProcedure [dbo].[SP_OutstandingCustomerLedgers]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_OutstandingCustomerLedgers]
@LedgerCategory varchar(255)='OT',
@AsOnDate datetime,
@LedgerList varchar(max)='',@FYId int
as
declare @sql varchar(max)
select @sql = 'select Distinct l.Id,l.AccountName,l.ShortName 
from AccountTransaction at Inner join Ledger l on l.Id = at.GlCode
 where  DrAmt <> 0 And  l.Category in(''BO'',''' + @LedgerCategory + ''') AND at.FYId=' + CAST(@FYId as Varchar(5))
select @sql += ' AND at.VDate <='''  + Cast(@AsOnDate as varchar(20)) + ''''
	
if @LedgerList <> ''
	Begin 
		select @sql += ' and l.Id In(' + @LedgerList + ')'
	end
	
	select @sql += 'Group By l.Id,l.AccountName,l.ShortName   having Sum(DrAmt) <> 0'
--select (@sql)
exec (@sql)
GO
/****** Object:  StoredProcedure [dbo].[SP_PLLedgerTotals]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_PLLedgerTotals]
	@StartDate  Datetime,
	@GroupAccountType Char(1),
	@AccountGroupId int,
	@FYId int 
as
Declare @sql varchar(max)
select @sql = 'select l.Id,l.ShortName,l.AccountName,'
if @GroupAccountType ='E'
Begin
	select @sql += 'SUM(DrAmt) - SUM(Cramt) as Total' 
End
else
Begin
	select @sql += 'SUM(CrAmt) - SUM(Dramt) as Total' 
End
select @sql += ' from AccountTransaction at
Inner join Ledger l on at.GlCode = l.Id
inner join AccountGroup ag on l.AccountGroupId = ag.Id
Where l.AccountGroupId =' + CAST( @AccountGroupId as varchar(25))  + ' AND at.FYId=' + CAST(@FYId as varchar(5))
select @sql += ' and at.Vdate <= ''' + CONVERT(VARCHAR(10), @StartDate , 111) + ''''

select @sql +=  ' and ag.GroupAccountType =''' + @GroupAccountType + ''' Group By l.Id,l.ShortName,l.AccountName Order By l.AccountName,l.ShortName'
--select @sql
exec(@sql)
GO
/****** Object:  StoredProcedure [dbo].[SP_PLAcGroupHeader]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_PLAcGroupHeader]
	@StartDate  Datetime,
	@EndDate  Datetime,
	@GroupAccountType Char(1),
	@FYId int
as
declare @sql varchar(max)

--	Displays profit and losst account group header within the periods accordinng to account group type
select @sql = 'Select ag.Id,ag.Description,ag.GroupAccountType,'
if @GroupAccountType ='E'
Begin
	select @sql += ' SUM(DrAmt) - SUM(Cramt) as Total' 
End
else
Begin
	select @sql += ' SUM(CrAmt) - SUM(Dramt) as Total' 
End
select @sql += ' from AccountTransaction at
	inner join Ledger l on at.GlCode = l.Id
	inner join AccountGroup ag on l.AccountGroupId = ag.Id
	where ag.GroupAccountType =''' + @GroupAccountType + ''' AND  at.FYId=' + CAST(@FYId as Varchar(5))
if  Isnull(@EndDate,'') <> ''
Begin
	select @sql += ' and at.VDate Between ''' + CONVERT(VARCHAR(10), @StartDate , 111) + ''' And ''' + CONVERT(VARCHAR(10), @EndDate, 111)+ ''''
end
else
Begin
	select @sql += ' and at.Vdate <= ''' + CONVERT(VARCHAR(10), @StartDate , 111) + ''''
End
select @sql += ' Group By ag.Id,ag.Description,ag.GroupAccountType'
select @sql += ' Order By ag.Description'
exec (@sql)
GO
/****** Object:  StoredProcedure [dbo].[SP_BookIssued]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_BookIssued](
 @type varchar(max), @cardtype varchar(10), @issuedTo int, @bookId int)
as
Declare @sql varchar(max)
select @sql ='Select * from ScBookIssued as bi
where bi.Type ='''+@type+''' and bi.IsReturn =0'
if(@cardtype <> '')
begin  
select @sql +='and bi.CardType='''+@cardtype+''''
End
if(@issuedTo <>0)
begin
select @sql +='and bi.IssuedTo='+ cast(@issuedTo as varchar(50))+''
End

if(@bookId <>0)
begin
select @sql +='and bi.BookId='+cast(@bookId as varchar(50))+''
End

exec (@sql)
GO
/****** Object:  StoredProcedure [dbo].[SP_MonthlyBillGenerate]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_MonthlyBillGenerate]
@ClassId int ,@BoaderId int , @ShiftId int,@stdId varchar(max) ,@feeitemId varchar(max)	 ,@AYId int 
as

Create Table #FeeTable
(
Id int Identity(1,1),
StudentId int,
StudentName nvarchar(100),
Section nvarchar(100),
RollNo nvarchar(255),
Amount decimal(18,2),
EducationTax decimal(18,2),
[Status] nvarchar(10)
)
Declare @sqlinsert varchar(max)
Declare @sqlSet varchar(max)
Select @sqlinsert='
Insert Into #FeeTable (StudentId ,StudentName ,Section ,RollNo)
select srd.StudentId,si.StuDesc ,sec.Description,srd.RollNo
From ScStudentRegistration sr
inner join ScStudentRegistrationDetail srd On sr.Id = srd.RegMasterId
inner join ScStudentInfo si on srd.StudentId = si.StudentID
Left outer join ScSection sec on srd.SectionId = sec.Id
Where sr.classId ='+CAST(@ClassId as varchar(10))+'
and srd.StudentId in ('+@stdId+')
and srd.BoaderId='+CAST(@BoaderId as varchar(10))+'
and srd.ShiftId ='+CAST(@ShiftId as varchar(10))+'
and sr.AcademicYearId ='+CAST(@AYId as varchar(10))+''
exec (@sqlinsert)

Declare @Id int
Declare @StudentId int
Declare @StudentName nvarchar(100)
Declare @Section nvarchar(100)
Declare @RollNo nvarchar(255)
Declare @Amount decimal(18,2)
Declare @EducationTax decimal(18,2)	
Declare @Status nvarchar(10)

Declare FeeCursor Cursor For
Select Id,StudentId,StudentName,Section,RollNo,Amount,[Status] From #FeeTable
Open FeeCursor
Fetch Next From FeeCursor Into @Id ,@StudentId, @StudentName, @Section, @RollNo , @Amount, @Status
while @@FETCH_STATUS =0
Begin

Create Table #FeeAmount
(
Amount decimal(18,2),
EducationTax decimal(18,2)
)
Select @sqlSet='Insert Into #FeeAmount (Amount,EducationTax)
Select Sum(FeeRate) as FeeAmount,Sum(EducationTax) as  EducationTax
from (
select fm.ClassId,fm.StudentId,fd.FeeItemId,fd.NetAmount as FeeRate,case when fi.EducationTax = 1 then fd.NetAmount * 0.01 else 0 end as EducationTax
from ScStudentFeeRateMaster fm
Inner Join ScStudentFeeRateDetail fd on fm.Id = fd.MasterId
inner join ScFeeItem fi on fd.FeeItemId = fi.Id
where fm.StudentId ='+CAST(@StudentId as varchar(10))+'
and fd.FeeItemId in ('+@feeitemId+')
Union All
select r.ClassId,'+CAST(@StudentId as varchar(10))+' as StudentId, r.FeeItemId,r.FeeRate,case when fi.EducationTax = 1 then r.FeeRate * 0.01 else 0 end as EducationTax 
from scclassfeerate r
inner join ScFeeItem fi on r.FeeItemId = fi.Id
Where r.ClassId = '+CAST(@ClassId as varchar(10))+'
and r.FeeItemId in ('+@feeitemId+')
and r.BoaderId='+CAST(@BoaderId as varchar(10))+'
and r.ShiftId ='+CAST(@ShiftId as varchar(10))+'
and r.FeeItemId
Not In (
select Distinct fd.FeeItemId
from ScStudentFeeRateMaster fm
Inner Join ScStudentFeeRateDetail fd on fm.Id = fd.MasterId
where fm.ClassId ='+CAST(@ClassId as varchar(10))+'
and fm.StudentId ='+CAST(@StudentId as varchar(10))+')) as FeeSummary
Group by StudentId'

exec (@sqlSet)--)	

Set @Amount = (select Amount from #FeeAmount)   
Set @EducationTax = (select ISNULL( EducationTax,0) from #FeeAmount)

Update #FeeTable Set Amount = @Amount,EducationTax = @EducationTax Where Id = @Id
Fetch Next From FeeCursor Into @Id ,@StudentId, @StudentName, @Section, @RollNo , @Amount, @Status
Drop table #FeeAmount
end
CLOSE FeeCursor
DEALLOCATE FeeCursor

Select Id,StudentId ,StudentName ,Section ,RollNo,ISNULL(Amount,0) as Amount,ISNULL( EducationTax,0) EducationTax,[Status] from #feeTable

Drop table #feetable
GO
/****** Object:  StoredProcedure [dbo].[SP_Ledgers]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_Ledgers]
@LedgerCategory varchar(10)='OT',
@StartDate datetime = null,
@EndDate datetime = null,
@LedgerList varchar(max)='',@FYId int
as
declare @sql varchar(max)
select @sql = 'Select Distinct l.Id,l.AccountName,l.ShortName from Ledger l
Inner join AccountTransaction at on l.Id = at.GlCode
Where l.Category in (''' + @LedgerCategory + ''') AND at.FYId=' + CAST(@FYId as Varchar(5))


if @StartDate <> ''
	Begin
		select @sql += ' AND at.VDate >='''  + Cast(@StartDate as varchar(20)) + ''''
	end
	
if @EndDate <> ''
	Begin
		select @sql += ' AND at.VDate <='''  + Cast(@EndDate as varchar(20)) + ''''
	end
	
if @LedgerList <> ''
	Begin 
		select @sql += ' and l.Id In(' + @LedgerList + ')'
	end
--select (@sql)
 Set @sql += ' ORDER BY l.AccountName,l.ShortName'
exec (@sql)
GO
/****** Object:  StoredProcedure [dbo].[Sp_AutoBackUp]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Sp_AutoBackUp]	
				@path nvarchar(max),
				@databasename nvarchar(500)
					 as

BACKUP DATABASE @databasename TO DISK =@path 
WITH FORMAT,
MEDIANAME = @databasename,
NAME ='Full Backup of database'
GO
/****** Object:  StoredProcedure [dbo].[SP_AccountStatement]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_AccountStatement]
@CustomerId int,
@StartDate date,
@EndDate date
as
Create Table #Statement
(
Id int Identity(1,1),
[Date] date,
AccountId int,
[VoucherNo] nvarchar(25),
[Description] nvarchar(255),
CrAmt decimal(18,4),
DrAmt Decimal(18,4),
Balance decimal(18,4)
)

Declare @Id int
Declare @Date date
Declare @AccountId int
Declare @VoucherNo nvarchar(25)
Declare @Description nvarchar(255)
Declare @CrAmt Decimal(18,4)
Declare @DrAmt Decimal(18,4)
Declare @Balance Decimal(18,4)

Insert Into #Statement(Date,AccountId,VoucherNo,[Description],CrAmt,DrAmt)
select [Date],AccountId,VoucherNo,[Description],CrAmt,DrAmt from [VW_AccountStatement]
Where [VW_AccountStatement].AccountId = @CustomerId and [Date] Between @StartDate And @EndDate
Order By [Date]

Declare StatementCursor Cursor For
select * from #Statement order By [Date]
Open StatementCursor
Fetch Next From StatementCursor Into @Id,@Date,@AccountId,@VoucherNo,@Description,@CrAmt,@DrAmt,@Balance
Declare @CurrentBalance Decimal(18,4)= 0
while @@FETCH_STATUS =0
Begin
set @CurrentBalance += (@CrAmt - @DrAmt)
Update #Statement Set Balance = @CurrentBalance Where Id = @Id
Fetch Next From StatementCursor Into @Id,@Date,@AccountId,@VoucherNo,@Description,@CrAmt,@DrAmt,@Balance
end
CLOSE StatementCursor
DEALLOCATE StatementCursor
select * from #Statement
Drop table #Statement
GO
/****** Object:  Table [dbo].[SmsTemplates]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsTemplates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Body] [nvarchar](300) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_SmsTemplates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SMSSettings]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMSSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[AttendanceSMS] [bit] NULL,
	[EventsSMS] [bit] NULL,
	[BirthdaySMS] [bit] NULL,
	[ExamResultScheduleSMS] [bit] NULL,
 CONSTRAINT [PK_SMSSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SmsLog]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReferenceId] [int] NULL,
	[ReferenceType] [int] NULL,
	[SentToNumber] [nvarchar](50) NULL,
	[SentFromId] [int] NULL,
	[Body] [nvarchar](max) NULL,
	[Footer] [nvarchar](300) NULL,
	[Title] [nvarchar](300) NULL,
	[IsSent] [bit] NULL,
	[SendDate] [datetime] NULL,
 CONSTRAINT [PK_SmsLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SmsGroupDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsGroupDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NULL,
	[ReferenceId] [int] NULL,
	[ReferenceType] [int] NULL,
 CONSTRAINT [PK_SmsGroupDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SmsGroup]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_SmsGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShortNameDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShortNameDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Prefix] [varchar](50) NULL,
	[Number] [int] NULL,
	[Module] [varchar](50) NULL,
	[ShortName] [varchar](50) NULL,
 CONSTRAINT [PK_ShortNameDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SecurityRight]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecurityRight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[Role] [int] NOT NULL,
	[Create] [bit] NOT NULL,
	[Edit] [bit] NOT NULL,
	[Delete] [bit] NOT NULL,
	[Navigate] [bit] NOT NULL,
	[Approve] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SP_EmployeeSalaryCreditSlipHeader]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_EmployeeSalaryCreditSlipHeader]
	@EmployeeList varchar(max)=''
as

 --[SP_EmployeeSalaryCreditSlipHeader] '1,2,3'
 
declare @sql varchar(max)
 
select @sql = 'select EI.Id, EI.Code,EI.Description as EName,(Address + isnull(Address1,'''')) as EAddress,DateOfJoin,MitiofJoin,	EP.Description as EPost,ED.Description as EDepartment,EC.Description as EType  from ScEmployeeInfo	as EI  join ScEmployeePost	as EP On EP.Id=EI.EmployeePostId '
select @sql += ' join ScEmployeeDepartment as ED On ED.Id=EI.EmployeeDepartmentId join ScEmployeeCategory as EC On EC.Id=EI.EmployeeCategoryId '
if @EmployeeList <> ''
Begin 
	select @sql += ' Where EI.Id In (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](''' + @EmployeeList + ''', '',''))'
end
Set @sql += ' ORDER BY EI.Id'
--select (@sql)
exec (@sql)
GO
/****** Object:  StoredProcedure [dbo].[SP_EmployeeSalaryCreditSlipDetails]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_EmployeeSalaryCreditSlipDetails]
	@EmployeeList varchar(max)='',
	@ACYID int,
	@Month int,
	@Year int
as
--[SP_EmployeeSalaryCreditSlipDetails]   '1,2,3',2,1

declare @sql varchar(max)
select @sql = 'Select PG.Id, ET.Employeeid,ET.VNo,[Source],SalaryHeadId,Case When SalaryHeadId=1 Then ''Basic Salary''  When SalaryHeadId=2 Then ''Allowance'''
select @sql += ' When SalaryHeadId=3 Then ''Deduction''	 When SalaryHeadId=4 Then ''PF Deduction'''
select @sql += ' When SalaryHeadId=5 Then ''Tax Deduction''  end as SalaryHeadDesc,'
select @sql += ' Amount,CONVERT(Date, ET.CreatedDate)CreatedDate,Operation from EmployeeTransaction as ET Inner Join PyPayrollGeneration as PG On PG.VNo=ET.VNo '
select @sql += ' Inner Join ScSalaryHead as SH On SH.Id= PG.SalaryHeadId where Amount>0 and PG.AcademicYearId = '+ CAST(@ACYID as Varchar(5)) + ' and PG.Month ='+ CAST(@Month as Varchar(5))+ 'and PG.Year= '+ CAST(@Year as Varchar(5))+''

if @EmployeeList <> ''
Begin 
	select @sql += ' and ET.Id In (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](''' + @EmployeeList + ''', '',''))'
end 
Set @sql += ' Order by EmployeeId,ET.CreatedDate'
--select (@sql)
exec (@sql)
GO
/****** Object:  StoredProcedure [dbo].[SP_DayBookSummaryDashboard]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_DayBookSummaryDashboard]
@AsOnDate Date,@Description nvarchar(100),@SystemControlLedgerFieldName varchar(50)
as
Declare @smt nvarchar(max)
set @smt ='SELECT ''' + @Description + ''' as [Description],at.GlCode, l.AccountName,
case When SUM(DrAmt) - SUM(CrAmt) > 0 Then Cast(SUM(DrAmt) - SUM(CrAmt) AS Varchar(100)) + '' Dr''
When SUM(DrAmt) - SUM(CrAmt) < 0 Then Cast(Abs(SUM(DrAmt) - SUM(CrAmt)) AS Varchar(100)) + '' Cr''
When SUM(DrAmt) - SUM(CrAmt) = 0 Then ''''
end as Summary from AccountTransaction at
Inner Join Ledger l on at.GlCode = l.Id
Where at.GlCode = (select ' + @SystemControlLedgerFieldName + ' from SystemControl s)
And VDate =''' + CAST( @AsOnDate as varchar(15)) + ''' Group By at.GlCode,l.AccountName'
--select @smt

execute (@smt)
GO
/****** Object:  StoredProcedure [dbo].[sp_PlacePrinciple]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_PlacePrinciple]
@EmployeeId int,
@LoanCode nvarchar(20)
as begin
select LoanAmount,Duration from mst_EmployeeLoanMaster where EmployeeId=@EmployeeId and LoanCode=@LoanCode
end
GO
/****** Object:  Table [dbo].[ModuleList]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ShortName] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuItem]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MenuItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Action] [varchar](50) NULL,
	[Controller] [varchar](50) NULL,
	[LinkText] [nvarchar](255) NULL,
	[ModuleId] [int] NULL,
	[ParentId] [int] NULL,
	[Schedule] [int] NULL,
	[ModuleKey] [varchar](50) NULL,
 CONSTRAINT [PK_MenuItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductUnitConversion]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductUnitConversion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[Quantity] [decimal](18, 2) NULL,
	[Unit] [nvarchar](50) NULL,
	[BuyPrice] [decimal](18, 2) NULL,
	[SalePrice] [decimal](18, 2) NULL,
	[LowestQuantity] [decimal](18, 2) NULL,
	[LowestUnit] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductUnitConversion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductSubGroup]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductSubGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ProductGroupId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductOpening]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductOpening](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[OpeningDate] [datetime] NULL,
	[OpeningMiti] [nvarchar](50) NULL,
	[Unit] [nvarchar](50) NULL,
	[Quantity] [decimal](18, 2) NULL,
	[AltUnit] [nvarchar](50) NULL,
	[AltQuantity] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[GodownId] [int] NULL,
 CONSTRAINT [PK_ProductOpening] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductLedgerInfo]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductLedgerInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[LedgerId] [int] NULL,
	[Module] [varchar](10) NULL,
 CONSTRAINT [PK_ProductLedgerInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductImages]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[Path] [nvarchar](200) NULL,
 CONSTRAINT [PK_ProductImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductGroup]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](max) NOT NULL,
	[ProductGroupId] [int] NULL,
	[ProductSubGroupId] [int] NULL,
	[ProductType] [int] NOT NULL,
	[IsBatch] [bit] NOT NULL,
	[ValTech] [int] NOT NULL,
	[Unit] [int] NOT NULL,
	[AltUnit] [int] NULL,
	[AltQuantity] [decimal](18, 2) NULL,
	[Quantity] [decimal](18, 2) NULL,
	[StockQuantity] [decimal](18, 2) NULL,
	[BuyPrice] [decimal](18, 2) NULL,
	[SalesPrice] [decimal](18, 2) NULL,
	[MRP] [decimal](18, 2) NULL,
	[TradePrice] [decimal](18, 2) NULL,
	[MRRate] [decimal](18, 2) NULL,
	[MaxStock] [decimal](18, 2) NULL,
	[ReorderLevel] [decimal](18, 2) NULL,
	[MinStock] [decimal](18, 2) NULL,
	[ReorderQuantity] [decimal](18, 2) NULL,
	[VatRate] [decimal](18, 2) NULL,
	[BonusPercentage] [decimal](18, 2) NULL,
	[MinQuantity] [decimal](18, 2) NULL,
	[MaxQuantity] [decimal](18, 2) NULL,
	[MinBonus] [decimal](18, 2) NULL,
	[MaxBonus] [decimal](18, 2) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[BarCode] [int] NULL,
	[IsDeleted] [bit] NULL,
	[PurchaseId] [int] NULL,
	[SalesId] [int] NULL,
	[SalesReturnId] [int] NULL,
	[PurchaseReturnId] [int] NULL,
	[Medium] [int] NULL,
	[MfgDate] [bit] NOT NULL,
	[ExpDate] [bit] NOT NULL,
	[ExpiredProduct] [int] NULL,
	[Class] [nvarchar](100) NULL,
	[Level] [nvarchar](100) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OpeningStudent]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OpeningStudent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LedgerId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[AmountType] [int] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[BranchId] [int] NOT NULL,
	[FiscalYearId] [int] NOT NULL,
	[AcademyId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_OpeningStudent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OpeningLedger]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OpeningLedger](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GlCode] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[AmountType] [int] NOT NULL,
	[LedgerType] [int] NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[BranchId] [int] NULL,
	[FiscalYearId] [int] NULL,
	[SlCode] [int] NULL,
 CONSTRAINT [PK_OpeningLedger] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Narration]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Narration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Type] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseInvoice]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PurchaseInvoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNo] [nvarchar](max) NOT NULL,
	[InvoiceDate] [date] NOT NULL,
	[InvoiceMiti] [nvarchar](max) NULL,
	[PartyInvoiceNo] [nvarchar](max) NULL,
	[PartyInvoiceDate] [date] NULL,
	[PurchaseInvoiceType] [nvarchar](max) NULL,
	[GlCode] [int] NOT NULL,
	[SlCode] [int] NULL,
	[DueDay] [int] NULL,
	[DueDate] [date] NULL,
	[AgentCode] [int] NULL,
	[CurrCode] [nvarchar](max) NULL,
	[CurrRate] [decimal](18, 2) NULL,
	[BasicAmt] [decimal](18, 2) NOT NULL,
	[TermAmt] [decimal](18, 2) NOT NULL,
	[NetAmt] [decimal](18, 2) NOT NULL,
	[Remarks] [nvarchar](max) NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[ApprovedDate] [datetime] NULL,
	[ApprovedBy] [int] NULL,
	[FYId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[DocId] [int] NULL,
	[IsDeleted] [bit] NULL,
	[PartyInvoiceMiti] [varchar](10) NULL,
	[DueMiti] [varchar](10) NULL,
	[ChallanId] [int] NULL,
 CONSTRAINT [PK_PurchaseInvoice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PurchaseImpTransDoc]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PurchaseImpTransDoc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PPDNo] [nvarchar](max) NULL,
	[PPDDate] [datetime] NULL,
	[TaxableAmt] [decimal](18, 2) NULL,
	[Vat] [decimal](18, 2) NULL,
	[CustomName] [nvarchar](max) NULL,
	[Transport] [nvarchar](max) NULL,
	[VehicleNo] [nvarchar](max) NULL,
	[CnNo] [nvarchar](max) NULL,
	[CnDate] [datetime] NULL,
	[DocumentBank] [nvarchar](max) NULL,
	[PurchaseInvoiceId] [int] NOT NULL,
	[CnMiti] [varchar](10) NULL,
	[PPDMiti] [varchar](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PurchaseChallanMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PurchaseChallanMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ChallanNo] [varchar](15) NOT NULL,
	[ChallanDate] [datetime] NOT NULL,
	[QNo] [varchar](15) NULL,
	[QDate] [datetime] NULL,
	[GlCode] [int] NOT NULL,
	[AgentCode] [int] NULL,
	[ClassCode] [int] NULL,
	[CurrencyCode] [int] NULL,
	[CurrencyRate] [decimal](18, 2) NOT NULL,
	[BasicAmt] [decimal](18, 2) NULL,
	[TermAmt] [decimal](18, 2) NULL,
	[NetAmt] [decimal](18, 2) NULL,
	[Remarks] [nvarchar](max) NULL,
	[BrCode] [varchar](10) NULL,
	[PartyName] [varchar](256) NULL,
	[ChequeNo] [varchar](15) NULL,
	[ChequeDate] [datetime] NULL,
	[VatNo] [varchar](25) NULL,
	[ChallanMiti] [varchar](10) NULL,
	[DocId] [int] NULL,
	[BranchId] [int] NULL,
	[IsApproved] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ApprovedBy] [int] NULL,
	[ApprovedDate] [datetime] NULL,
	[FYId] [int] NULL,
	[OrderId] [int] NULL,
	[OrderNo] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PurchaseChallanImpTransDoc]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PurchaseChallanImpTransDoc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PPDNo] [nvarchar](max) NULL,
	[PPDDate] [datetime] NULL,
	[TaxableAmt] [decimal](18, 2) NULL,
	[Vat] [decimal](18, 2) NULL,
	[CustomName] [nvarchar](max) NULL,
	[Transport] [nvarchar](max) NULL,
	[VehicleNo] [nvarchar](max) NULL,
	[CnNo] [nvarchar](max) NULL,
	[CnDate] [datetime] NULL,
	[DocumentBank] [nvarchar](max) NULL,
	[PurchaseChallanId] [int] NOT NULL,
	[PPDMiti] [varchar](10) NULL,
	[CnMiti] [varchar](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PurchaseOrderMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PurchaseOrderMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[OrderMiti] [varchar](10) NOT NULL,
	[OrderNo] [varchar](20) NULL,
	[OANo] [varchar](20) NULL,
	[OADate] [datetime] NULL,
	[GlCode] [int] NULL,
	[AgentCode] [int] NULL,
	[ClassCode] [int] NULL,
	[CurrencyCode] [int] NULL,
	[CurrentyRate] [decimal](18, 2) NULL,
	[ProductAmt] [decimal](18, 2) NULL,
	[TermAmt] [decimal](18, 2) NULL,
	[BasicAmt] [decimal](18, 2) NULL,
	[NetAmt] [decimal](18, 2) NULL,
	[Remarks] [nvarchar](max) NULL,
	[BrCode] [varchar](10) NULL,
	[PartyName] [varchar](256) NULL,
	[ChequeNo] [varchar](15) NULL,
	[ChequeDate] [datetime] NULL,
	[VatNo] [varchar](25) NULL,
	[PbQuoNo] [varchar](255) NULL,
	[TaxGroup] [varchar](20) NULL,
	[IsApproved] [bit] NULL,
	[BranchId] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ApprovedDate] [datetime] NULL,
	[ApprovedBy] [int] NULL,
	[FYId] [int] NULL,
	[DocId] [int] NULL,
 CONSTRAINT [PK__Purchase__3214EC072A363CC5] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PurchaseOrderImpTransDoc]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PurchaseOrderImpTransDoc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PPDNo] [nvarchar](max) NULL,
	[PPDDate] [datetime] NULL,
	[TaxableAmt] [decimal](18, 2) NULL,
	[Vat] [decimal](18, 2) NULL,
	[CustomName] [nvarchar](max) NULL,
	[Transport] [nvarchar](max) NULL,
	[VehicleNo] [nvarchar](max) NULL,
	[CnNo] [nvarchar](max) NULL,
	[CnDate] [datetime] NULL,
	[DocumentBank] [nvarchar](max) NULL,
	[PurchaseOrderId] [int] NOT NULL,
	[PPDMiti] [varchar](10) NULL,
	[CnMiti] [varchar](10) NULL,
 CONSTRAINT [PK_PurchaseOrderImpTransDoc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PyAllowanceMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PyAllowanceMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Type] [int] NULL,
	[IsFlat] [bit] NOT NULL,
	[IsAnnual] [bit] NOT NULL,
	[Value] [decimal](16, 4) NULL,
	[Status] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[FiscalYearId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
 CONSTRAINT [PK_ScAllowanceMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseReturnMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PurchaseReturnMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNo] [nvarchar](max) NOT NULL,
	[InvoiceDate] [datetime] NOT NULL,
	[InvoiceMiti] [nvarchar](max) NULL,
	[RefBillNo] [varchar](50) NOT NULL,
	[RefBillDate] [datetime] NULL,
	[GlCode] [int] NOT NULL,
	[DueDay] [int] NULL,
	[DueDate] [datetime] NULL,
	[AgentCode] [int] NULL,
	[CurrCode] [nvarchar](max) NULL,
	[CurrRate] [decimal](18, 2) NULL,
	[BasicAmt] [decimal](18, 2) NOT NULL,
	[TermAmt] [decimal](18, 2) NULL,
	[NetAmt] [decimal](18, 2) NOT NULL,
	[Remarks] [nvarchar](max) NULL,
	[SlCode] [int] NULL,
	[PartyName] [nvarchar](max) NULL,
	[ChequeNo] [nvarchar](max) NULL,
	[ChequeDate] [datetime] NULL,
	[VatNo] [nvarchar](max) NULL,
	[Posting] [bit] NOT NULL,
	[Export] [bit] NOT NULL,
	[Reconcile] [datetime] NULL,
	[TaxGroupDesc] [nvarchar](max) NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[ApprovedDate] [datetime] NULL,
	[ApprovedBy] [int] NULL,
	[FYId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
	[RefBillMiti] [varchar](10) NULL,
	[DueMiti] [varchar](10) NULL,
 CONSTRAINT [PK_PurchaseReturnMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PurchaseReturnImpTransDoc]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PurchaseReturnImpTransDoc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PPDNo] [nvarchar](max) NULL,
	[PPDDate] [datetime] NULL,
	[TaxableAmt] [decimal](18, 2) NULL,
	[Vat] [decimal](18, 2) NULL,
	[CustomName] [nvarchar](max) NULL,
	[Transport] [nvarchar](max) NULL,
	[VehicleNo] [nvarchar](max) NULL,
	[CnNo] [nvarchar](max) NULL,
	[CnDate] [datetime] NULL,
	[DocumentBank] [nvarchar](max) NULL,
	[PurchaseReturnId] [int] NOT NULL,
	[PPDMiti] [varchar](10) NULL,
	[CnMiti] [varchar](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PurchaseReturnDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseReturnDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SNo] [int] NOT NULL,
	[ProductCode] [int] NOT NULL,
	[Godown] [int] NULL,
	[AltQty] [decimal](18, 2) NULL,
	[AltUnit] [nvarchar](max) NULL,
	[Qty] [decimal](18, 2) NULL,
	[Unit] [nvarchar](max) NULL,
	[AltStockQty] [decimal](18, 2) NULL,
	[StockQty] [decimal](18, 2) NOT NULL,
	[Rate] [decimal](18, 2) NULL,
	[BasicAmt] [decimal](18, 2) NULL,
	[TermAmt] [decimal](18, 2) NULL,
	[NetAmt] [decimal](18, 2) NOT NULL,
	[Remarks] [nvarchar](max) NULL,
	[AddDesc] [nvarchar](max) NULL,
	[FreeQty] [decimal](18, 2) NULL,
	[StockFreeQty] [decimal](18, 2) NULL,
	[FreeUnit] [nvarchar](max) NULL,
	[ConvRatio] [decimal](18, 2) NULL,
	[TaxPriceRate] [decimal](18, 2) NULL,
	[TaxGroupId] [int] NULL,
	[TaxInclusiveItem] [bit] NOT NULL,
	[BillRateInclusive] [bit] NOT NULL,
	[PurchaseReturnId] [int] NOT NULL,
	[Index] [int] NULL,
	[BatchId] [int] NULL,
 CONSTRAINT [PK_PurchaseReturnDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ledger]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ledger](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountName] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](max) NOT NULL,
	[AccountGroupId] [int] NOT NULL,
	[AccountSubGroupId] [int] NULL,
	[IsCashBank] [bit] NOT NULL,
	[IsCashBook] [bit] NOT NULL,
	[IsSubLedger] [bit] NOT NULL,
	[IsBillWiseAdjustment] [bit] NOT NULL,
	[Category] [nvarchar](max) NULL,
	[AreaId] [int] NULL,
	[AgentId] [int] NULL,
	[CurrencyId] [int] NULL,
	[CreditLimit] [decimal](18, 2) NULL,
	[CreditDays] [int] NULL,
	[ChequeReceiptDays] [int] NULL,
	[Scheme] [int] NULL,
	[RateOfInterest] [decimal](18, 2) NULL,
	[Address] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[Country] [nvarchar](max) NULL,
	[PhoneO] [nvarchar](max) NULL,
	[PhoneR] [nvarchar](max) NULL,
	[Fax] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[PanNo] [nvarchar](max) NULL,
	[DLNo] [nvarchar](max) NULL,
	[ContactPerson] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[IsDefault] [bit] NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JournalVoucherDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JournalVoucherDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JVNumber] [nvarchar](max) NULL,
	[GlCode] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Narration] [nvarchar](max) NULL,
	[JVMasterId] [int] NOT NULL,
	[JVAmountType] [int] NOT NULL,
	[SlCode] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JournalVoucher]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JournalVoucher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JVNumber] [nvarchar](max) NOT NULL,
	[JVDate] [datetime] NOT NULL,
	[JVMiti] [nvarchar](max) NULL,
	[CurCode] [nvarchar](max) NULL,
	[CurRate] [decimal](18, 2) NULL,
	[Remarks] [nvarchar](max) NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[ApprovedDate] [datetime] NULL,
	[ApprovedBy] [int] NULL,
	[FYId] [int] NOT NULL,
	[DocId] [int] NULL,
	[BranchId] [int] NOT NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CalendarYearInfo]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalendarYearInfo](
	[CYear] [int] NULL,
	[YearStart] [datetime] NULL,
	[YearEnd] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CalendarMonthInfo]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalendarMonthInfo](
	[nYear] [int] NULL,
	[nMonth] [int] NULL,
	[nNoOfDays] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExpiryBreakageMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExpiryBreakageMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExpBrkNo] [varchar](20) NULL,
	[Date] [datetime] NULL,
	[Miti] [varchar](50) NULL,
	[ClassCode] [int] NULL,
	[Type] [varchar](5) NULL,
	[BrCode] [varchar](10) NULL,
	[Remarks] [nvarchar](max) NULL,
	[CreatedById] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[BranchId] [int] NULL,
 CONSTRAINT [PK__ExpiryBr__3214EC076C040022] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNet_SqlCacheTablesForChangeNotification]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNet_SqlCacheTablesForChangeNotification](
	[tableName] [nvarchar](450) NOT NULL,
	[notificationCreated] [datetime] NOT NULL,
	[changeId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[tableName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[fnNumberToWords]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnNumberToWords]

(

@Number AS BIGINT

) RETURNS VARCHAR(MAX)

AS

BEGIN

DECLARE @Below20 TABLE (ID INT IDENTITY(0,1), Word VARCHAR(32))

DECLARE @Below100 TABLE (ID INT IDENTITY(2,1), Word VARCHAR(32))

DECLARE @BelowHundred AS VARCHAR(126)



INSERT @Below20 (Word) VALUES ('ZERO')

INSERT @Below20 (Word) VALUES ('ONE')

INSERT @Below20 (Word) VALUES ( 'TWO' )

INSERT @Below20 (Word) VALUES ( 'THREE')

INSERT @Below20 (Word) VALUES ( 'FOUR' )

INSERT @Below20 (Word) VALUES ( 'FIVE' )

INSERT @Below20 (Word) VALUES ( 'SIX' )

INSERT @Below20 (Word) VALUES ( 'SEVEN' )

INSERT @Below20 (Word) VALUES ( 'EIGHT')

INSERT @Below20 (Word) VALUES ( 'NINE')

INSERT @Below20 (Word) VALUES ( 'TEN')

INSERT @Below20 (Word) VALUES ( 'ELEVEN' )

INSERT @Below20 (Word) VALUES ( 'TWELVE' )

INSERT @Below20 (Word) VALUES ( 'THIRTEEN' )

INSERT @Below20 (Word) VALUES ( 'FOURTEEN')

INSERT @Below20 (Word) VALUES ( 'FIFTEEN' )

INSERT @Below20 (Word) VALUES ( 'SIXTEEN' )

INSERT @Below20 (Word) VALUES ( 'SEVENTEEN')

INSERT @Below20 (Word) VALUES ( 'EIGHTEEN' )

INSERT @Below20 (Word) VALUES ( 'NINETEEN' )



INSERT @Below100 VALUES ('TWENTY')

INSERT @Below100 VALUES ('THIRTY')

INSERT @Below100 VALUES ('FORTY')

INSERT @Below100 VALUES ('FIFTY')

INSERT @Below100 VALUES ('SIXTY')

INSERT @Below100 VALUES ('SEVENTY')

INSERT @Below100 VALUES ('EIGHTY')

INSERT @Below100 VALUES ('NINETY')



IF @Number > 99

BEGIN

SELECT @belowHundred = dbo.fnNumberToWords( @Number % 100)

END



DECLARE @NumberInWords VARCHAR(MAX)

SET @NumberInWords =

(

SELECT

CASE

WHEN @Number = 0 THEN ''



WHEN @Number BETWEEN 1 AND 19

THEN (SELECT Word FROM @Below20 WHERE ID=@Number)



WHEN @Number BETWEEN 20 AND 99

THEN (SELECT Word FROM @Below100 WHERE ID=@Number/10)+ '-' + dbo.fnNumberToWords( @Number % 10)

WHEN @Number BETWEEN 100 AND 999

THEN (dbo.fnNumberToWords( @Number / 100)) + ' HUNDRED '+

CASE

	WHEN @belowHundred <> ''
	THEN 'AND ' + @belowHundred else @belowHundred
	END

	WHEN @Number BETWEEN 1000 AND 999999
	THEN (dbo.fnNumberToWords( @Number / 1000))+ ' THOUSAND '+ dbo.fnNumberToWords( @Number % 1000)

	WHEN @Number BETWEEN 1000000 AND 999999999
	THEN (dbo.fnNumberToWords( @Number / 1000000)) + ' MILLION '+ dbo.fnNumberToWords( @Number % 1000000)

	WHEN @Number BETWEEN 1000000000 AND 999999999999
	THEN (dbo.fnNumberToWords( @Number / 1000000000))+' BILLION '+ dbo.fnNumberToWords( @Number % 1000000000)

	ELSE ' INVALID INPUT'
END
)
SELECT @NumberInWords = RTRIM(@NumberInWords)
SELECT @NumberInWords = RTRIM(LEFT(@NumberInWords,LEN(@NumberInWords)-1)) WHERE RIGHT(@NumberInWords,1)='-'

RETURN (@NumberInWords)
END
GO
/****** Object:  Table [dbo].[FiscalYear]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FiscalYear](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StartDateNep] [nvarchar](max) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDateNep] [nvarchar](max) NULL,
	[EndDate] [datetime] NOT NULL,
	[CompanyId] [int] NOT NULL,
	[IsDefalut] [bit] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedById] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Godown]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Godown](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[ContactPerson] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Godown] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillingTermDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillingTermDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Source] [nvarchar](max) NULL,
	[ReferenceId] [int] NOT NULL,
	[TermAmount] [decimal](18, 2) NOT NULL,
	[BillingTermId] [int] NOT NULL,
	[Index] [int] NULL,
	[Rate] [decimal](18, 2) NOT NULL,
	[DetailId] [int] NULL,
	[IsProductWise] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillingTerm]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillingTerm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[GeneralLedgerId] [int] NOT NULL,
	[Category] [int] NOT NULL,
	[Basis] [int] NOT NULL,
	[Sign] [int] NOT NULL,
	[IsProductWise] [bit] NOT NULL,
	[IsProfitability] [bit] NOT NULL,
	[SupressIfZero] [bit] NOT NULL,
	[TermBasis] [int] NULL,
	[Rate] [decimal](18, 2) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[Type] [nvarchar](max) NULL,
	[BranchId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DispalyOrder] [int] NOT NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Area]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Area](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AreaName] [nvarchar](max) NOT NULL,
	[ShorName] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Agent]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ShorName] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[PhoneNo] [nvarchar](max) NULL,
	[Fax] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[LedgerId] [int] NULL,
	[SubLedgerId] [int] NULL,
	[Commision] [decimal](18, 2) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[Area] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[BranchId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountTransaction]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AccountTransaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VNo] [varchar](15) NULL,
	[VDate] [date] NULL,
	[CrCode] [varchar](10) NULL,
	[CrRate] [decimal](18, 2) NOT NULL,
	[GlCode] [int] NOT NULL,
	[DrAmt] [decimal](18, 2) NULL,
	[CrAmt] [decimal](18, 2) NULL,
	[LocalDrAmt] [decimal](18, 2) NULL,
	[LocalCrAmt] [decimal](18, 2) NULL,
	[Narration] [nvarchar](1040) NULL,
	[Remarks] [nvarchar](1040) NULL,
	[Source] [varchar](10) NULL,
	[SNO] [int] NULL,
	[CbCode] [int] NULL,
	[CreatedById] [int] NULL,
	[ReferenceId] [int] NULL,
	[FYId] [int] NOT NULL,
	[DueDate] [datetime] NULL,
	[SlCode] [int] NULL,
	[BranchId] [int] NOT NULL,
	[VMiti] [varchar](10) NULL,
	[AgentCode] [varchar](max) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AccountSubGroup]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountSubGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NOT NULL,
	[AccountGroupId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountGroup]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NOT NULL,
	[GroupType] [nvarchar](max) NULL,
	[GroupAccountType] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[IsDefault] [bit] NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AcademicYear]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcademicYear](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StartDateNep] [nvarchar](100) NULL,
	[StartDate] [datetime] NULL,
	[EndDateNep] [nvarchar](100) NULL,
	[EndDate] [datetime] NULL,
	[CompanyId] [int] NULL,
	[FiscalYearId] [int] NULL,
	[IsDefalut] [bit] NULL,
	[CreatedById] [int] NULL,
	[ModifiedById] [int] NULL,
 CONSTRAINT [PK_Academic Year] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EntryControlSales]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntryControlSales](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubLedger] [bit] NOT NULL,
	[Agent] [bit] NOT NULL,
	[Godown] [bit] NOT NULL,
	[Class] [bit] NOT NULL,
	[Unit] [bit] NOT NULL,
	[AltUnit] [bit] NOT NULL,
	[Order] [bit] NOT NULL,
	[Challan] [bit] NOT NULL,
	[Currency] [bit] NOT NULL,
	[BasicAmount] [bit] NOT NULL,
	[AutoDesc] [bit] NOT NULL,
	[Remarks] [bit] NOT NULL,
	[OrderReqd] [bit] NOT NULL,
	[ChallanReqd] [bit] NOT NULL,
	[GodownReqd] [bit] NOT NULL,
	[ClassReqd] [bit] NOT NULL,
	[CurrencyReqd] [bit] NOT NULL,
	[SubLedgerReqd] [bit] NOT NULL,
	[AgentReqd] [bit] NOT NULL,
	[RemarksReqd] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EntryControlPurchase]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntryControlPurchase](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubLedger] [bit] NOT NULL,
	[Agent] [bit] NOT NULL,
	[Godown] [bit] NOT NULL,
	[Class] [bit] NOT NULL,
	[Unit] [bit] NOT NULL,
	[AltUnit] [bit] NOT NULL,
	[Order] [bit] NOT NULL,
	[Challan] [bit] NOT NULL,
	[Currency] [bit] NOT NULL,
	[BasicAmount] [bit] NOT NULL,
	[AutoDesc] [bit] NOT NULL,
	[Remarks] [bit] NOT NULL,
	[UpdateRateBox] [bit] NOT NULL,
	[OrderReqd] [bit] NOT NULL,
	[ChallanReqd] [bit] NOT NULL,
	[GodownReqd] [bit] NOT NULL,
	[ClassReqd] [bit] NOT NULL,
	[CurrencyReqd] [bit] NOT NULL,
	[SubLedgerReqd] [bit] NOT NULL,
	[AgentReqd] [bit] NOT NULL,
	[RemarksReqd] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EntryControlPL]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntryControlPL](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Class] [bit] NOT NULL,
	[ClassItem] [bit] NOT NULL,
	[Currency] [bit] NOT NULL,
	[SubLedger] [bit] NOT NULL,
	[Agent] [bit] NOT NULL,
	[Remarks] [bit] NOT NULL,
	[ClassReqd] [bit] NOT NULL,
	[CurrencyReqd] [bit] NOT NULL,
	[SubLedgerReqd] [bit] NOT NULL,
	[AgentReqd] [bit] NOT NULL,
	[RemarksReqd] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EntryControlInventory]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntryControlInventory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CostCenter] [bit] NOT NULL,
	[CostCenterItem] [bit] NOT NULL,
	[Godown] [bit] NOT NULL,
	[GodownItem] [bit] NOT NULL,
	[Class] [bit] NOT NULL,
	[Unit] [bit] NOT NULL,
	[AltUnit] [bit] NOT NULL,
	[AutoDesc] [bit] NOT NULL,
	[Remarks] [bit] NOT NULL,
	[GodownReqd] [bit] NOT NULL,
	[ClassReqd] [bit] NOT NULL,
	[RemarksReqd] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeTransaction]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmployeeTransaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Month] [int] NULL,
	[MonthMiti] [int] NULL,
	[Year] [int] NULL,
	[YearMiti] [int] NULL,
	[NetAmount] [decimal](18, 2) NULL,
	[Source] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[ReferenceId] [int] NULL,
	[VNo] [varchar](255) NULL,
	[CreateById] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[AcademicYearId] [int] NOT NULL,
 CONSTRAINT [PK_EmployeeTransaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDetailsInBtnSearch]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_GetDetailsInBtnSearch]


as
begin
select a.LoanPaymentCode, b.FirstName +''+ MiddleName +''+ LastName as 'EmployeeName',c.LoanName,c.LoanCode,
a.InstallmentNumber,a.InstallmentAmount,a.PaymentDate from iPayroll.dbo.mst_EmployeeLoanPaymentMaster as a 
join iHRMIS.dbo.EmployeeMaster as b on a.EmployeeId=b.ID
join iPayroll.dbo.mst_LoanTypeMaster as c on a.LoanCode=c.LoanCode
end
GO
/****** Object:  Table [dbo].[EdmMetadata]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EdmMetadata](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModelHash] [nvarchar](max) NULL
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[DtYMD]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[DtYMD] (@Date1 as datetime)
returns varchar(10) as
begin
	DECLARE @strymd as varchar(10)
    set @strymd =cast(year(@date1)  as varchar(4)) + '-'+ right('0'+ cast(month(@date1)  as varchar) ,2) + '-'+ right('0' + cast(day(@date1)  as varchar) ,2)
	return (@strymd)
End
GO
/****** Object:  Table [dbo].[DocumentNumberingScheme]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentNumberingScheme](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleName] [nvarchar](max) NULL,
	[DocDesc] [nvarchar](max) NULL,
	[DocType] [nvarchar](max) NULL,
	[DocStartDate] [datetime] NOT NULL,
	[DocEndDate] [datetime] NOT NULL,
	[DocMode] [nvarchar](max) NULL,
	[DocPrefix] [nvarchar](max) NULL,
	[DocSuffix] [nvarchar](max) NULL,
	[DocBodyLen] [int] NOT NULL,
	[DocTotalLen] [int] NOT NULL,
	[DocNumFill] [bit] NOT NULL,
	[DocCharFill] [nvarchar](max) NULL,
	[DocStartNo] [int] NOT NULL,
	[DocEndNo] [int] NOT NULL,
	[DocCurrNo] [int] NOT NULL,
	[BranchId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DebitNoteMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DebitNoteMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Miti] [nvarchar](max) NULL,
	[GlCode] [int] NOT NULL,
	[AgentCode] [int] NULL,
	[CurCode] [nvarchar](max) NULL,
	[CurRate] [decimal](18, 4) NULL,
	[SlCode] [int] NULL,
	[RefBillNo] [nvarchar](max) NULL,
	[RefBillDate] [datetime] NULL,
	[Remarks] [nvarchar](max) NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[ApprovedDate] [datetime] NULL,
	[ApprovedBy] [int] NULL,
	[FYId] [int] NOT NULL,
	[DocId] [int] NULL,
	[BranchId] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
	[RefBillMiti] [varchar](10) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DebitNoteDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DebitNoteDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SNo] [int] NOT NULL,
	[GlCode] [int] NOT NULL,
	[Amount] [decimal](18, 4) NOT NULL,
	[Narration] [nvarchar](max) NULL,
	[DebitNoteMasterId] [int] NOT NULL,
	[SlCode] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DayBookModule]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DayBookModule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ShortName] [nvarchar](max) NULL,
	[Type] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Unit] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreditNoteMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CreditNoteMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Miti] [nvarchar](max) NULL,
	[GlCode] [int] NOT NULL,
	[AgentCode] [int] NULL,
	[CurCode] [nvarchar](max) NULL,
	[CurRate] [decimal](18, 4) NULL,
	[SlCode] [int] NULL,
	[RefBillNo] [nvarchar](max) NULL,
	[RefBillDate] [datetime] NULL,
	[Remarks] [nvarchar](max) NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[ApprovedDate] [datetime] NULL,
	[ApprovedBy] [int] NULL,
	[FYId] [int] NOT NULL,
	[DocId] [int] NULL,
	[BranchId] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
	[RefBillMiti] [varchar](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CreditNoteDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditNoteDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SNo] [int] NOT NULL,
	[GlCode] [int] NOT NULL,
	[Amount] [decimal](18, 4) NOT NULL,
	[Narration] [nvarchar](max) NULL,
	[CreditNoteMasterId] [int] NOT NULL,
	[SlCode] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CostCenter]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CostCenter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[ContactPerson] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_CostCenter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConsolidateDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConsolidateDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConsolidateId] [int] NULL,
	[ExamId] [int] NULL,
	[SubjectId] [int] NULL,
	[FullMarks] [decimal](18, 2) NULL,
	[PassMarks] [decimal](18, 2) NULL,
	[ObtainedMarks] [decimal](18, 2) NULL,
	[TheoryFullMark] [decimal](18, 2) NULL,
	[PracticalFullMark] [decimal](18, 2) NULL,
	[TheoryPassMark] [decimal](18, 2) NULL,
	[PracticalPassMark] [decimal](18, 2) NULL,
	[TheoryObtainedMark] [decimal](18, 2) NULL,
	[PracticalObtainedMark] [decimal](18, 2) NULL,
	[HighestMarks] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Consolidate]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Consolidate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[Section] [nvarchar](100) NULL,
	[RollNo] [nvarchar](100) NULL,
	[ClassId] [int] NULL,
	[ExamId] [int] NULL,
	[AcademicYearId] [int] NULL,
	[ConsolidateCode] [nvarchar](100) NULL,
	[Division] [nvarchar](100) NULL,
	[Percent] [decimal](18, 2) NULL,
	[TotalFullMarks] [decimal](18, 2) NULL,
	[TotalObtained] [decimal](18, 2) NULL,
	[Result] [varchar](50) NULL,
	[Rank] [varchar](50) NULL,
	[OutOff] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CompanyInfo]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Initial] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Country] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[PhoneNo] [nvarchar](max) NULL,
	[Fax] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[PanNo] [nvarchar](max) NULL,
	[TaxInvoice] [bit] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[ParentId] [int] NULL,
	[StartMiti] [nvarchar](255) NULL,
	[EndMiti] [nvarchar](255) NULL,
	[LogoGuid] [nvarchar](max) NULL,
	[LogoExt] [nvarchar](max) NULL,
	[CreatedById] [int] NULL,
	[UpdatedById] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CashBankMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CashBankMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VoucherNo] [nvarchar](max) NOT NULL,
	[VoucherDate] [datetime] NOT NULL,
	[VoucherMiti] [nvarchar](max) NULL,
	[LedgerId] [int] NOT NULL,
	[CurrentBalance] [decimal](18, 2) NULL,
	[ChequeNo] [nvarchar](max) NULL,
	[ChequeDate] [datetime] NULL,
	[CurrencyId] [int] NULL,
	[Rate] [decimal](18, 2) NULL,
	[Remarks] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[ApprovedDate] [datetime] NULL,
	[ApprovedBy] [int] NULL,
	[DocId] [int] NULL,
	[FYid] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
	[ChequeMiti] [varchar](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CashBankDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CashBankDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LedgerId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Narration] [nvarchar](max) NULL,
	[AmountType] [int] NOT NULL,
	[MasterId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[SlCode] [int] NULL,
	[Index] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScStaffAttendance]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScStaffAttendance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StaffId] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Status] [char](1) NOT NULL,
	[AcademicYearId] [int] NOT NULL,
 CONSTRAINT [PK_ScStaffAttendance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScSalaryHead]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScSalaryHead](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Head] [int] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[LedgerId] [int] NOT NULL,
	[Operation] [varchar](2) NULL,
 CONSTRAINT [PK_ScSalaryHead] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScRollNumberingScheme]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScRollNumberingScheme](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[SectionId] [int] NOT NULL,
	[Mode] [nvarchar](max) NULL,
	[Prefix] [nvarchar](max) NULL,
	[Suffix] [nvarchar](max) NULL,
	[BodyLen] [int] NOT NULL,
	[TotalLen] [int] NOT NULL,
	[NumFill] [bit] NOT NULL,
	[CharFill] [nvarchar](max) NULL,
	[StartNo] [int] NOT NULL,
	[EndNo] [int] NOT NULL,
	[CurrNo] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[AcademyYearId] [int] NOT NULL,
 CONSTRAINT [PK_RollNumberingScheme] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScStudentAttendance]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScStudentAttendance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Status] [char](1) NOT NULL,
	[AcademicYearId] [int] NOT NULL,
	[ClassId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
 CONSTRAINT [PK_ScStudentAttendance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScLibraryStockTransction]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScLibraryStockTransction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransctionType] [varchar](50) NULL,
	[Quantity] [int] NULL,
	[RefId] [int] NULL,
	[Source] [nvarchar](100) NULL,
	[Type] [varchar](50) NULL,
	[IssuedTo] [int] NULL,
	[BookId] [int] NULL,
	[IssueNo] [varchar](100) NULL,
	[IssueDate] [date] NULL,
	[ReturnDate] [date] NOT NULL,
 CONSTRAINT [PK__ScLibrar__3214EC072FEF161B] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScLibrarySetting]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScLibrarySetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookDueDate] [int] NOT NULL,
	[TotalCardIssue] [int] NOT NULL,
	[IsResgistrationNumberUse] [bit] NULL,
 CONSTRAINT [PK_LibrarySetting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScLibraryLateFine]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScLibraryLateFine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DayStart] [int] NULL,
	[DayEnd] [int] NULL,
	[Amount] [decimal](18, 2) NULL,
	[Description] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[Title] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScLibraryCard]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScLibraryCard](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LibraryRegistrationId] [int] NOT NULL,
	[CardNo] [varchar](max) NOT NULL,
	[IsUse] [bit] NOT NULL,
 CONSTRAINT [PK_ScLibraryCard] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScProgramMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScProgramMaster](
	[Code] [varchar](50) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Incharge] [varchar](50) NULL,
	[FacultyDescription] [varchar](100) NULL,
	[LevelDescription] [varchar](50) NULL,
	[UniversityDescription] [varchar](100) NULL,
	[MinStudent] [int] NOT NULL,
	[MaxStudent] [int] NOT NULL,
	[StDate] [datetime] NOT NULL,
	[EnDate] [datetime] NOT NULL,
	[Schedule] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_ScProgramMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SchemeProduct]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SchemeProduct](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductSchemeId] [int] NULL,
	[ProductId] [int] NULL,
	[Qty] [int] NULL,
	[Unit] [varchar](50) NULL,
	[FreeQty] [int] NULL,
	[FreeUnit] [varchar](50) NULL,
 CONSTRAINT [PK_SchemeProduct] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SchemeFreeProduct]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SchemeFreeProduct](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[SchemeProductId] [int] NULL,
	[FreeQty] [int] NULL,
	[FreeUnit] [varchar](50) NULL,
 CONSTRAINT [PK_SchemeFreeProduct] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Scheme]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scheme](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[EffectOn] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_ProductScheme] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScMaterialReturnMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScMaterialReturnMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VoucherNo] [nvarchar](50) NULL,
	[VoucherDate] [datetime] NULL,
	[VoucherMiti] [nvarchar](50) NULL,
	[Remarks] [nvarchar](100) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_ScMaterialReturnMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScBookIssueReturn]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScBookIssueReturn](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CardId] [int] NOT NULL,
	[BookDetailId] [int] NOT NULL,
	[FineAmount] [decimal](18, 2) NOT NULL,
	[ReturnDate] [date] NOT NULL,
	[ReturnMiti] [varchar](50) NULL,
	[CreatedById] [int] NOT NULL,
	[IssueId] [int] NOT NULL,
	[AcademyYearId] [int] NOT NULL,
 CONSTRAINT [PK__ScBookIs__3214EC074277DAAA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScBookIssued]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScBookIssued](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[Miti] [nvarchar](50) NULL,
	[BookReceivedDetailId] [int] NOT NULL,
	[BookDueDate] [date] NULL,
	[BookDueMiti] [nvarchar](50) NULL,
	[CardId] [int] NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[AcademyYearId] [int] NOT NULL,
	[IsReturn] [bit] NOT NULL,
 CONSTRAINT [PK__ScBookIs__3214EC073EA749C6] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScClassScheduleDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScClassScheduleDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Day] [nvarchar](50) NULL,
	[StartTime] [nvarchar](50) NULL,
	[EndTime] [nvarchar](50) NULL,
	[ClassScheduleId] [int] NULL,
	[DisplayOrder] [int] NULL,
 CONSTRAINT [PK_ClassScheduleDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScClassSchedule]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScClassSchedule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NULL,
	[SectionId] [int] NULL,
	[SubjectId] [int] NULL,
 CONSTRAINT [PK_ClassSchedule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScEmployeeDepartment]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScEmployeeDepartment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Code] [varchar](max) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
 CONSTRAINT [PK_ScEmployeeDepartment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScConsolidatedMarksSetup]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScConsolidatedMarksSetup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](max) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[ExamOrder] [int] NOT NULL,
	[Percentage] [decimal](18, 2) NOT NULL,
	[Remarks] [varchar](255) NULL,
	[ClassId] [int] NOT NULL,
	[ExamId] [int] NOT NULL,
	[ExamGrdId] [int] NOT NULL,
 CONSTRAINT [PK_ScConsolidatedMarksSetup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SalesOrderMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesOrderMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[OrderMiti] [varchar](10) NOT NULL,
	[OrderNo] [varchar](20) NULL,
	[OANo] [varchar](20) NULL,
	[OADate] [datetime] NULL,
	[GlCode] [int] NULL,
	[AgentCode] [int] NULL,
	[ClassCode] [int] NULL,
	[CurrencyCode] [int] NULL,
	[CurrentyRate] [decimal](18, 2) NULL,
	[ProductAmt] [decimal](18, 2) NULL,
	[TermAmt] [decimal](18, 2) NULL,
	[BasicAmt] [decimal](18, 2) NULL,
	[NetAmt] [decimal](18, 2) NULL,
	[Remarks] [nvarchar](max) NULL,
	[BrCode] [varchar](10) NULL,
	[PartyName] [varchar](256) NULL,
	[ChequeNo] [varchar](15) NULL,
	[ChequeDate] [datetime] NULL,
	[VatNo] [varchar](25) NULL,
	[PbQuoNo] [varchar](255) NULL,
	[TaxGroup] [varchar](20) NULL,
	[IsApproved] [bit] NULL,
	[BranchId] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ApprovedDate] [datetime] NULL,
	[ApprovedBy] [int] NULL,
	[FYId] [int] NULL,
	[DocId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoleMapping]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMapping](
	[UserId] [int] NOT NULL,
	[Id] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleName] [nvarchar](128) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QueuedJob]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QueuedJob](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobTitle] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[LastRunDate] [datetime] NULL,
	[ScheduleDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PyTaxDeductionPattern]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PyTaxDeductionPattern](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IsAnnual] [bit] NOT NULL,
	[StartAmount] [decimal](16, 4) NOT NULL,
	[EndAmount] [decimal](16, 4) NOT NULL,
	[Percentage] [decimal](16, 4) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[FiscalYearId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
 CONSTRAINT [PK_mst_TaxDeductionPattern_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesReturnOtherDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesReturnOtherDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Transport] [nvarchar](max) NULL,
	[VehicleNo] [nvarchar](max) NULL,
	[CnNo] [nvarchar](max) NULL,
	[CnDate] [datetime] NULL,
	[CnType] [nvarchar](max) NULL,
	[Package] [decimal](18, 2) NULL,
	[DocBank] [nvarchar](max) NULL,
	[DocThru] [nvarchar](max) NULL,
	[PONo] [nvarchar](max) NULL,
	[PODate] [datetime] NULL,
	[CnFreight] [decimal](18, 2) NULL,
	[CnAdvance] [decimal](18, 2) NULL,
	[CnBalance] [decimal](18, 2) NULL,
	[DriverName] [nvarchar](max) NULL,
	[ContractNo] [nvarchar](max) NULL,
	[ContractDate] [datetime] NULL,
	[ExportInvoiceNo] [nvarchar](max) NULL,
	[ExportInvoiceDate] [datetime] NULL,
	[LCNumber] [nvarchar](max) NULL,
	[CustomName] [nvarchar](max) NULL,
	[COFDDesc] [nvarchar](max) NULL,
	[LicenseName] [nvarchar](max) NULL,
	[SalesReturnId] [int] NOT NULL,
	[CnMiti] [varchar](10) NULL,
	[POMiti] [varchar](10) NULL,
	[ContractMiti] [varchar](10) NULL,
	[ExportInvoiceMiti] [varchar](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SalesReturnMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesReturnMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNo] [nvarchar](max) NOT NULL,
	[InvoiceDate] [datetime] NOT NULL,
	[InvoiceMiti] [nvarchar](max) NULL,
	[RefBillNo] [nvarchar](max) NULL,
	[RefBillDate] [datetime] NULL,
	[GlCode] [int] NOT NULL,
	[AgentCode] [int] NULL,
	[CurrCode] [nvarchar](max) NULL,
	[CurrRate] [decimal](18, 2) NULL,
	[BasicAmt] [decimal](18, 2) NOT NULL,
	[TermAmt] [decimal](18, 2) NULL,
	[NetAmt] [decimal](18, 2) NOT NULL,
	[Remarks] [nvarchar](max) NULL,
	[SlCode] [int] NULL,
	[PartyName] [nvarchar](max) NULL,
	[ChequeNo] [nvarchar](max) NULL,
	[ChequeDate] [datetime] NULL,
	[VatNo] [nvarchar](max) NULL,
	[Posting] [bit] NOT NULL,
	[Export] [bit] NOT NULL,
	[Reconcile] [datetime] NULL,
	[BillSys] [nvarchar](max) NULL,
	[TaxGroupDesc] [nvarchar](max) NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[ApprovedDate] [datetime] NULL,
	[ApprovedBy] [int] NULL,
	[FYId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
	[RefBillMiti] [varchar](10) NULL,
 CONSTRAINT [PK_SalesReturnMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PyPFEmployeeMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PyPFEmployeeMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [decimal](16, 4) NULL,
	[FiscalYearId] [int] NOT NULL,
	[IsAnnual] [bit] NOT NULL,
	[Status] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[IsFlat] [bit] NOT NULL,
	[BranchId] [int] NOT NULL,
 CONSTRAINT [PK_mst_PFEmployeeMaster_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PyPFEmployeeMapping]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PyPFEmployeeMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PFEmployeeId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_Table_1_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PyPayrollGenerationDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PyPayrollGenerationDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PayrollGenerationId] [int] NOT NULL,
	[ReferenceId] [int] NOT NULL,
	[Amount] [decimal](18, 4) NOT NULL,
 CONSTRAINT [PK_PyPayrollGenerationDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PyPayrollGeneration]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PyPayrollGeneration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[SalaryHeadId] [int] NOT NULL,
	[Amount] [decimal](18, 4) NOT NULL,
	[FiscalYearId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[YearMiti] [int] NOT NULL,
	[MonthMiti] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[VNo] [varchar](255) NULL,
	[AcademicYearId] [int] NOT NULL,
 CONSTRAINT [PK_PyPayrollGeneration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PyPaymentSlip]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PyPaymentSlip](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[SlipNo] [varchar](50) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[YearMiti] [int] NOT NULL,
	[MonthMiti] [int] NOT NULL,
	[NetAmount] [decimal](18, 4) NOT NULL,
	[LedgerId] [int] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[Remark] [varchar](max) NULL,
 CONSTRAINT [PK_PyPaymentSlip] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PyDeductionMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PyDeductionMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Type] [int] NULL,
	[Amount] [decimal](16, 4) NULL,
	[Rebatable] [bit] NULL,
	[IsAnnual] [bit] NULL,
	[Status] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[FiscalYearId] [int] NOT NULL,
	[IsFlat] [bit] NOT NULL,
	[BranchId] [int] NOT NULL,
 CONSTRAINT [PK_mst_DeductionMaster_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesInvoiceOtherDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesInvoiceOtherDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Transport] [nvarchar](max) NULL,
	[VehicleNo] [nvarchar](max) NULL,
	[CnNo] [nvarchar](max) NULL,
	[CnDate] [datetime] NULL,
	[CnType] [nvarchar](max) NULL,
	[Package] [decimal](18, 2) NULL,
	[DocBank] [nvarchar](max) NULL,
	[DocThru] [nvarchar](max) NULL,
	[PONo] [nvarchar](max) NULL,
	[PODate] [datetime] NULL,
	[CnFreight] [decimal](18, 2) NULL,
	[CnAdvance] [decimal](18, 2) NULL,
	[CnBalance] [decimal](18, 2) NULL,
	[DriverName] [nvarchar](max) NULL,
	[ContractNo] [nvarchar](max) NULL,
	[ContractDate] [datetime] NULL,
	[ExportInvoiceNo] [nvarchar](max) NULL,
	[ExportInvoiceDate] [datetime] NULL,
	[LCNumber] [nvarchar](max) NULL,
	[CustomName] [nvarchar](max) NULL,
	[COFDDesc] [nvarchar](max) NULL,
	[LicenseName] [nvarchar](max) NULL,
	[SalesInvoiceId] [int] NOT NULL,
	[CnMiti] [varchar](10) NULL,
	[POMiti] [varchar](10) NULL,
	[ContractMiti] [varchar](10) NULL,
	[ExportInvoiceMiti] [varchar](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SalesInvoice]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesInvoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNo] [nvarchar](max) NOT NULL,
	[InvoiceDate] [date] NOT NULL,
	[GlCode] [int] NOT NULL,
	[SlCode] [int] NULL,
	[DueDay] [int] NULL,
	[DueDate] [date] NULL,
	[AgentCode] [int] NULL,
	[CurrCode] [nvarchar](max) NULL,
	[CurrRate] [decimal](18, 2) NULL,
	[BasicAmt] [decimal](18, 2) NOT NULL,
	[TermAmt] [decimal](18, 2) NOT NULL,
	[NetAmt] [decimal](18, 2) NOT NULL,
	[TenderAmt] [decimal](18, 2) NOT NULL,
	[ReturnAmt] [decimal](18, 2) NOT NULL,
	[Remarks] [nvarchar](max) NULL,
	[BrCode] [nvarchar](max) NULL,
	[BillSys] [nvarchar](max) NULL,
	[PartyName] [nvarchar](max) NULL,
	[ChequeNo] [nvarchar](max) NULL,
	[ChequeDate] [date] NULL,
	[VatNo] [nvarchar](max) NULL,
	[SalesOrder] [nvarchar](max) NULL,
	[SalesChallan] [nvarchar](max) NULL,
	[EffectiveDate] [datetime] NULL,
	[AuditLock] [bit] NOT NULL,
	[InvoiceMiti] [nvarchar](max) NULL,
	[Posting] [bit] NOT NULL,
	[Export] [bit] NOT NULL,
	[QuotNo] [nvarchar](max) NULL,
	[Reconcile] [datetime] NULL,
	[Suspend] [bit] NOT NULL,
	[SourceModule] [nvarchar](max) NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[ApprovedDate] [datetime] NULL,
	[ApprovedBy] [int] NULL,
	[FYId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[DocId] [int] NULL,
	[IsDeleted] [bit] NULL,
	[DueMiti] [varchar](10) NULL,
	[ChallanId] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SalesDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SNo] [int] NOT NULL,
	[Godown] [int] NULL,
	[ProductCode] [int] NOT NULL,
	[AltQty] [decimal](18, 2) NULL,
	[AltUnit] [nvarchar](max) NULL,
	[Qty] [decimal](18, 2) NULL,
	[Unit] [nvarchar](max) NULL,
	[AltStockQty] [decimal](18, 2) NULL,
	[StockQty] [decimal](18, 2) NOT NULL,
	[Rate] [decimal](18, 2) NULL,
	[BasicAmt] [decimal](18, 2) NULL,
	[TermAmt] [decimal](18, 2) NOT NULL,
	[NetAmt] [decimal](18, 2) NOT NULL,
	[Remarks] [nvarchar](max) NULL,
	[AddDesc] [nvarchar](max) NULL,
	[ChallanNo] [nvarchar](max) NULL,
	[OrderNo] [nvarchar](max) NULL,
	[OrderSNo] [int] NOT NULL,
	[FreeQty] [decimal](18, 2) NULL,
	[StockFreeQty] [decimal](18, 2) NULL,
	[FreeUnit] [nvarchar](max) NULL,
	[ConvRatio] [decimal](18, 2) NULL,
	[CItem] [bit] NOT NULL,
	[DisorderNo] [nvarchar](max) NULL,
	[DisorderSNo] [int] NOT NULL,
	[ExtraFreeQty] [decimal](18, 2) NULL,
	[ExtraStockFreeQty] [decimal](18, 2) NULL,
	[ExtraFreeUnit] [nvarchar](max) NULL,
	[TaxPriceRate] [decimal](18, 2) NULL,
	[TaxGroupId] [nvarchar](max) NULL,
	[CalculateTaxItem] [nvarchar](max) NULL,
	[TaxInclusiveItem] [bit] NOT NULL,
	[BillRateInclusive] [bit] NOT NULL,
	[BillSys] [nvarchar](max) NULL,
	[SalesInvoiceId] [int] NOT NULL,
	[Index] [int] NULL,
	[BatchId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesChallanMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesChallanMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ChallanNo] [varchar](15) NOT NULL,
	[ChallanDate] [datetime] NOT NULL,
	[QNo] [varchar](15) NULL,
	[QDate] [datetime] NULL,
	[GlCode] [int] NOT NULL,
	[AgentCode] [int] NULL,
	[ClassCode] [int] NULL,
	[CurrencyCode] [int] NULL,
	[CurrencyRate] [decimal](18, 2) NOT NULL,
	[BasicAmt] [decimal](18, 2) NULL,
	[TermAmt] [decimal](18, 2) NULL,
	[NetAmt] [decimal](18, 2) NULL,
	[Remarks] [nvarchar](max) NULL,
	[BrCode] [varchar](10) NULL,
	[PartyName] [varchar](256) NULL,
	[ChequeNo] [varchar](15) NULL,
	[ChequeDate] [datetime] NULL,
	[VatNo] [varchar](25) NULL,
	[ChallanMiti] [varchar](10) NULL,
	[DocId] [int] NULL,
	[BranchId] [int] NULL,
	[IsApproved] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedById] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ApprovedBy] [int] NULL,
	[ApprovedDate] [datetime] NULL,
	[FYId] [int] NULL,
	[OrderId] [int] NULL,
	[OrderNo] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SalesChallanImpTransDoc]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesChallanImpTransDoc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PPDNo] [nvarchar](max) NULL,
	[PPDDate] [datetime] NULL,
	[TaxableAmt] [decimal](18, 2) NULL,
	[Vat] [decimal](18, 2) NULL,
	[CustomName] [nvarchar](max) NULL,
	[Transport] [nvarchar](max) NULL,
	[VehicleNo] [nvarchar](max) NULL,
	[CnNo] [nvarchar](max) NULL,
	[CnDate] [datetime] NULL,
	[DocumentBank] [nvarchar](max) NULL,
	[ChallanId] [int] NULL,
	[PPDMiti] [varchar](10) NULL,
	[CnMiti] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SalesChallanDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesChallanDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ChallanId] [int] NULL,
	[SNo] [int] NOT NULL,
	[ProductCode] [int] NOT NULL,
	[GodownCode] [int] NULL,
	[AltQty] [decimal](18, 2) NULL,
	[AltUnit] [varchar](10) NULL,
	[Qty] [decimal](18, 2) NOT NULL,
	[Unit] [varchar](10) NULL,
	[AltStockQty] [decimal](18, 2) NULL,
	[StockQty] [decimal](18, 6) NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[BasicAmt] [decimal](18, 6) NOT NULL,
	[TermAmt] [decimal](18, 6) NOT NULL,
	[NetAmt] [decimal](18, 6) NOT NULL,
	[Remarks] [varchar](1024) NULL,
	[UnitId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SalesChallanBillingAddress]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesChallanBillingAddress](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ChallanId] [int] NULL,
	[Address] [nvarchar](256) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Email] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GodownTransfer]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GodownTransfer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[TransferDate] [date] NULL,
	[TransferMiti] [varchar](10) NULL,
	[FromGodownId] [int] NULL,
	[CreatedById] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SalesReturnDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesReturnDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SNo] [int] NOT NULL,
	[ProductCode] [int] NOT NULL,
	[Godown] [int] NULL,
	[AltQty] [decimal](18, 2) NULL,
	[AltUnit] [nvarchar](max) NULL,
	[Qty] [decimal](18, 2) NULL,
	[Unit] [nvarchar](max) NULL,
	[AltStockQty] [decimal](18, 2) NULL,
	[StockQty] [decimal](18, 2) NOT NULL,
	[Rate] [decimal](18, 2) NULL,
	[BasicAmt] [decimal](18, 2) NULL,
	[TermAmt] [decimal](18, 2) NULL,
	[NetAmt] [decimal](18, 2) NOT NULL,
	[StockRate] [decimal](18, 2) NULL,
	[StockAmt] [decimal](18, 2) NULL,
	[Remarks] [nvarchar](max) NULL,
	[AddDesc] [nvarchar](max) NULL,
	[StockFreeQty] [decimal](18, 2) NOT NULL,
	[FreeUnit] [nvarchar](max) NULL,
	[ConvRatio] [decimal](18, 2) NOT NULL,
	[ExtraFreeQty] [decimal](18, 2) NOT NULL,
	[ExtraStockFreeQty] [decimal](18, 2) NOT NULL,
	[ExtraFreeUnit] [nvarchar](max) NULL,
	[TaxPriceRate] [decimal](18, 2) NOT NULL,
	[TaxGroupId] [nvarchar](max) NULL,
	[CalculateTaxItem] [nvarchar](max) NULL,
	[TaxInclusiveItem] [bit] NOT NULL,
	[BillRateInclusive] [bit] NOT NULL,
	[SalesReturnId] [int] NOT NULL,
	[Index] [int] NULL,
 CONSTRAINT [PK_SalesReturnDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesOrderOtherDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesOrderOtherDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Transport] [nvarchar](max) NULL,
	[VehicleNo] [nvarchar](max) NULL,
	[CnNo] [nvarchar](max) NULL,
	[CnDate] [datetime] NULL,
	[CnType] [nvarchar](max) NULL,
	[Package] [decimal](18, 2) NULL,
	[DocBank] [nvarchar](max) NULL,
	[DocThru] [nvarchar](max) NULL,
	[PONo] [nvarchar](max) NULL,
	[PODate] [datetime] NULL,
	[CnFreight] [decimal](18, 2) NULL,
	[CnAdvance] [decimal](18, 2) NULL,
	[CnBalance] [decimal](18, 2) NULL,
	[DriverName] [nvarchar](max) NULL,
	[ContractNo] [nvarchar](max) NULL,
	[ContractDate] [datetime] NULL,
	[ExportInvoiceNo] [nvarchar](max) NULL,
	[ExportInvoiceDate] [datetime] NULL,
	[LCNumber] [nvarchar](max) NULL,
	[CustomName] [nvarchar](max) NULL,
	[COFDDesc] [nvarchar](max) NULL,
	[LicenseName] [nvarchar](max) NULL,
	[OrderId] [int] NULL,
	[CnMiti] [varchar](10) NULL,
	[POMiti] [varchar](10) NULL,
	[ContractMiti] [varchar](10) NULL,
	[ExportInvoiceMiti] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SalesOrderDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesOrderDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[SNo] [int] NULL,
	[ProductCode] [int] NULL,
	[AltQty] [decimal](18, 2) NULL,
	[AltUnit] [varchar](20) NULL,
	[Qty] [decimal](18, 2) NULL,
	[Unit] [varchar](20) NULL,
	[AltStockQty] [decimal](18, 2) NULL,
	[StockQty] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NULL,
	[Productamt] [decimal](18, 2) NULL,
	[TermAmt] [decimal](18, 2) NULL,
	[NetAmt] [decimal](18, 2) NULL,
	[Remarks] [nvarchar](max) NULL,
	[IssueQty] [decimal](18, 2) NULL,
	[POConvRatio] [decimal](18, 2) NULL,
	[BasicAmt] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SalesOrderAddress]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesOrderAddress](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[Address] [nvarchar](256) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Email] [nvarchar](256) NULL,
	[AddressType] [varchar](2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScMaterialReturnDetails]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScMaterialReturnDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VoucherNo] [nvarchar](50) NULL,
	[MaterialReturnMasterId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[StaffId] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NULL,
	[LocalAmount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_ScMaterialReturnDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[CashBankBookSummary]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[CashBankBookSummary]
@StartDate datetime,
@EndDate datetime,
@CashBookId int,
@FYId int
as
Begin

Declare @VDate datetime
Declare @GlCode int
Declare @Opening decimal(18,2)
Declare @Receive Decimal (18,2)
Declare @Payment decimal(18,2)
Declare @Balance decimal(18,2)

Create Table #CashBank
(
VDate datetime,GlCode int,Opening decimal(18,2),[Receive] decimal(18,2),Payment decimal(18,2),Balance decimal(18,2)
)

Declare CashBookSummary Cursor For
	select at.VDate,at.GlCode, 0 as Opening , SUM(dramt) as Receive , SUM(CrAmt) as Payment , 0 as Balance   from AccountTransaction at
	Where GlCode = @CashBookId And at.VDate Between @StartDate And @EndDate and at.FYId = @FYId
	Group By at.VDate,at.GlCode Order By at.VDate 
	
	Open CashBookSummary
	Declare @counter int
	Fetch Next From CashBookSummary Into @VDate,@GlCode,@Opening,@Receive,@Payment,@Balance
	select @counter  =1
	while @@FETCH_STATUS =0
	Begin
		--Select @V_Date,@Gl_Code,@Opening,@Receive,@Payment,@Balance
		select @Opening = (select Isnull((SUM(DrAmt) - SUM(CrAmt)),0) as Opening1 from AccountTransaction Where GlCode = @CashBookId and AccountTransaction.VDate  < @VDate and AccountTransaction.FYId = @FYId)
		select @Balance = (@Opening + @Receive - @Payment)
	
		Insert Into #CashBank Values(@VDate,@GlCode,@Opening,@Receive,@Payment,@Balance)
		Fetch Next From CashBookSummary Into @VDate,@GlCode,@Opening,@Receive,@Payment,@Balance
	end
	CLOSE CashBookSummary
	DEALLOCATE CashBookSummary
	Select * From #CashBank
	Drop table #CashBank
end
GO
/****** Object:  StoredProcedure [dbo].[AspNet_SqlCacheUpdateChangeIdStoredProcedure]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procEDURE [dbo].[AspNet_SqlCacheUpdateChangeIdStoredProcedure] 
             @tableName NVARCHAR(450) 
         AS

         BEGIN 
             UPDATE dbo.AspNet_SqlCacheTablesForChangeNotification WITH (ROWLOCK) SET changeId = changeId + 1 
             WHERE tableName = @tableName
         END
GO
/****** Object:  StoredProcedure [dbo].[AspNet_SqlCacheUnRegisterTableStoredProcedure]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procEDURE [dbo].[AspNet_SqlCacheUnRegisterTableStoredProcedure] 
             @tableName NVARCHAR(450) 
         AS
         BEGIN

         BEGIN TRAN
         DECLARE @triggerName AS NVARCHAR(3000) 
         DECLARE @fullTriggerName AS NVARCHAR(3000)
         SET @triggerName = REPLACE(@tableName, '[', '__o__') 
         SET @triggerName = REPLACE(@triggerName, ']', '__c__') 
         SET @triggerName = @triggerName + '_AspNet_SqlCacheNotification_Trigger' 
         SET @fullTriggerName = 'dbo.[' + @triggerName + ']' 

         /* Remove the table-row from the notification table */ 
         IF EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = 'AspNet_SqlCacheTablesForChangeNotification' AND type = 'U') 
             IF EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = 'AspNet_SqlCacheTablesForChangeNotification' AND type = 'U') 
             DELETE FROM dbo.AspNet_SqlCacheTablesForChangeNotification WHERE tableName = @tableName 

         /* Remove the trigger */ 
         IF EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = @triggerName AND type = 'TR') 
             IF EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = @triggerName AND type = 'TR') 
             EXEC('DROP TRIGGER ' + @fullTriggerName) 

         COMMIT TRAN
         END
GO
/****** Object:  UserDefinedFunction [dbo].[GetNepaliDate]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create function [dbo].[GetNepaliDate]
(
@Date nvarchar(20)
)
returns nvarchar(20)
as
begin
set @Date = replace(@Date,'-','/')
declare @Year int
declare @Month int									   
declare @Day int
-- if Day MM/DD/YYYY
set @Month = (select top(1) * from dbo.split1(@Date,'/',1))
if(@Month < 13)
begin
set @Day = (select top(1) * from dbo.split1(@Date,'/',2))
set @Year = (select top(1) * from dbo.split1(@Date,'/',3))--replace(@Date,cast(@Day as varchar(20))+'/','')
end
else
begin
set @Year = (select top(1) * from dbo.split1(@Date,'/',1))
-- set @Date = replace(@Date,cast(@Year as varchar(20))+'/','')
set @Month = (select top(1) * from dbo.split1(@Date,'/',2))
set @Day = (select top(1) * from dbo.split1(@Date,'/',3))--replace(@Date,cast(@Month as varchar(20))+'/','')
end
declare @NewDate nvarchar(32)
set @NewDate = cast(cast(@Month as int) as varchar(20))+'/'+cast(cast(@Day as int) as varchar(20))+'/'+cast(cast(@Year as int) as varchar(20))
declare @MyOutput nvarchar(20)
set @MyOutput = (select NEP_DATE from tbl_Date where English=@NewDate)
return @MyOutput
end
GO
/****** Object:  Table [dbo].[FinishedGoodReceive]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FinishedGoodReceive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[ReceivedDate] [date] NULL,
	[ReceivedMiti] [varchar](10) NULL,
	[GoDownId] [int] NULL,
	[CreatedById] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BillOfMaterial]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillOfMaterial](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[CostCenterId] [int] NULL,
	[UnitId] [int] NULL,
	[Qty] [decimal](18, 2) NOT NULL,
	[ConversionFactor] [decimal](18, 2) NULL,
	[BranchId] [int] NULL,
	[CreatedById] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[FinishedGoodId] [int] NOT NULL,
	[Remarks] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AspNet_SqlCacheRegisterTableStoredProcedure]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procEDURE [dbo].[AspNet_SqlCacheRegisterTableStoredProcedure] 
             @tableName NVARCHAR(450) 
         AS
         BEGIN

         DECLARE @triggerName AS NVARCHAR(3000) 
         DECLARE @fullTriggerName AS NVARCHAR(3000)
         DECLARE @canonTableName NVARCHAR(3000) 
         DECLARE @quotedTableName NVARCHAR(3000) 

         /* Create the trigger name */ 
         SET @triggerName = REPLACE(@tableName, '[', '__o__') 
         SET @triggerName = REPLACE(@triggerName, ']', '__c__') 
         SET @triggerName = @triggerName + '_AspNet_SqlCacheNotification_Trigger' 
         SET @fullTriggerName = 'dbo.[' + @triggerName + ']' 

         /* Create the cannonicalized table name for trigger creation */ 
         /* Do not touch it if the name contains other delimiters */ 
         IF (CHARINDEX('.', @tableName) <> 0 OR 
             CHARINDEX('[', @tableName) <> 0 OR 
             CHARINDEX(']', @tableName) <> 0) 
             SET @canonTableName = @tableName 
         ELSE 
             SET @canonTableName = '[' + @tableName + ']' 

         /* First make sure the table exists */ 
         IF (SELECT OBJECT_ID(@tableName, 'U')) IS NULL 
         BEGIN 
             RAISERROR ('00000001', 16, 1) 
             RETURN 
         END 

         BEGIN TRAN
         /* Insert the value into the notification table */ 
         IF NOT EXISTS (SELECT tableName FROM dbo.AspNet_SqlCacheTablesForChangeNotification WITH (NOLOCK) WHERE tableName = @tableName) 
             IF NOT EXISTS (SELECT tableName FROM dbo.AspNet_SqlCacheTablesForChangeNotification WITH (TABLOCKX) WHERE tableName = @tableName) 
                 INSERT  dbo.AspNet_SqlCacheTablesForChangeNotification 
                 VALUES (@tableName, GETDATE(), 0)

         /* Create the trigger */ 
         SET @quotedTableName = QUOTENAME(@tableName, '''') 
         IF NOT EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = @triggerName AND type = 'TR') 
             IF NOT EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = @triggerName AND type = 'TR') 
                 EXEC('CREATE TRIGGER ' + @fullTriggerName + ' ON ' + @canonTableName +'
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N' + @quotedTableName + '
                       END
                       ')
         COMMIT TRAN
         END
GO
/****** Object:  StoredProcedure [dbo].[AspNet_SqlCacheQueryRegisteredTablesStoredProcedure]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procEDURE [dbo].[AspNet_SqlCacheQueryRegisteredTablesStoredProcedure] 
         AS
         SELECT tableName FROM dbo.AspNet_SqlCacheTablesForChangeNotification
GO
/****** Object:  StoredProcedure [dbo].[AspNet_SqlCachePollingStoredProcedure]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procEDURE [dbo].[AspNet_SqlCachePollingStoredProcedure] AS
         SELECT tableName, changeId FROM dbo.AspNet_SqlCacheTablesForChangeNotification
         RETURN 0
GO
/****** Object:  Table [dbo].[ExpiryBreakageDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExpiryBreakageDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExpBrkId] [int] NULL,
	[SNo] [int] NULL,
	[ProductCode] [int] NULL,
	[GodownId] [int] NULL,
	[AltUnit] [decimal](18, 2) NULL,
	[AltQty] [decimal](18, 2) NULL,
	[Qty] [decimal](18, 2) NULL,
	[Unit] [decimal](18, 2) NULL,
	[AltStockQty] [decimal](18, 2) NULL,
	[StockQty] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NULL,
	[NetAmt] [decimal](18, 2) NULL,
	[Description] [nvarchar](max) NULL,
	[ClassCode] [int] NULL,
	[UnitId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[ExeAutoBackUp]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ExeAutoBackUp](@path1 varchar(Max),@databasename1 nvarchar(500))
as
DECLARE	@return_value int

EXEC	@return_value = [dbo].[Sp_AutoBackUp]
		@path = @path1, @databasename = @databasename1

SELECT	'Return Value' = @return_value
GO
/****** Object:  StoredProcedure [dbo].[InsertAccountTransaction]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[InsertAccountTransaction]
@VNo	varchar(255),
@VDate	datetime,
@CrCode	varchar(255),
@CrRate	decimal(18,2),
@GlCode	int,
@DrAmt	decimal(18,2),
@CrAmt	decimal(18,2),
@LocalDrAmt	decimal(18,2),
@LocalCrAmt	decimal(18,2),
@Narration	nvarchar(max),
@Remarks	nvarchar(max),
@Source	varchar(3),
@SNO	int,
@CbCode	int,
@CreatedById	int,
@ReferenceId	int,
@FYId	int,
@DueDate	datetime,
@SlCode	int
as
Begin
	Insert into AccountTransaction (VNo,VDate,CrCode,CrRate,GlCode,DrAmt,CrAmt,LocalDrAmt,LocalCrAmt,Narration,Remarks,[Source],SNO,CbCode,CreatedById,ReferenceId,FYId,DueDate,SlCode) 
	Values(@VNo,@VDate,@CrCode,@CrRate,@GlCode,@DrAmt,@CrAmt,@LocalDrAmt,@LocalCrAmt,@Narration,@Remarks,@Source,
	@SNO,@CbCode,@CreatedById,@ReferenceId,@FYId,@DueDate,@SlCode)
end
GO
/****** Object:  Table [dbo].[PurchaseProductBatch]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PurchaseProductBatch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DetailId] [int] NULL,
	[ProductId] [int] NULL,
	[SerialNo] [nvarchar](512) NOT NULL,
	[Qty] [decimal](18, 2) NOT NULL,
	[Unit] [int] NULL,
	[MFGDate] [date] NULL,
	[EXPDate] [date] NULL,
	[BuyRate] [decimal](18, 2) NULL,
	[SalesRate] [decimal](18, 2) NULL,
	[BranchId] [int] NULL,
	[Godown] [int] NULL,
	[StockQuantity] [decimal](18, 2) NOT NULL,
	[Source] [varchar](2) NULL,
 CONSTRAINT [PK__Purchase__3214EC0756D3D912] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PurchaseOrderDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PurchaseOrderDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[SNo] [int] NULL,
	[ProductCode] [int] NULL,
	[AltQty] [decimal](18, 2) NULL,
	[AltUnit] [varchar](20) NULL,
	[Qty] [decimal](18, 2) NULL,
	[Unit] [varchar](20) NULL,
	[AltStockQty] [decimal](18, 2) NULL,
	[StockQty] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NULL,
	[Productamt] [decimal](18, 2) NULL,
	[TermAmt] [decimal](18, 2) NULL,
	[NetAmt] [decimal](18, 2) NULL,
	[Remarks] [nvarchar](max) NULL,
	[IssueQty] [decimal](18, 2) NULL,
	[POConvRatio] [decimal](18, 2) NULL,
	[BasicAmt] [decimal](18, 2) NULL,
 CONSTRAINT [PK__Purchase__3214EC0732CB82C6] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PurchaseOrderAddress]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PurchaseOrderAddress](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[Address] [nvarchar](256) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Email] [nvarchar](256) NULL,
	[AddressType] [varchar](2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PurchaseChallanDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PurchaseChallanDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ChallanId] [int] NULL,
	[SNo] [int] NOT NULL,
	[ProductCode] [int] NOT NULL,
	[GodownCode] [int] NULL,
	[AltQty] [decimal](18, 2) NULL,
	[AltUnit] [varchar](10) NULL,
	[Qty] [decimal](18, 2) NOT NULL,
	[Unit] [varchar](10) NULL,
	[AltStockQty] [decimal](18, 2) NULL,
	[StockQty] [decimal](18, 6) NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[BasicAmt] [decimal](18, 6) NOT NULL,
	[TermAmt] [decimal](18, 6) NOT NULL,
	[NetAmt] [decimal](18, 6) NOT NULL,
	[Remarks] [varchar](1024) NULL,
 CONSTRAINT [PK__Purchase__3214EC0744EA3301] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PurchaseChallanBillingAddress]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseChallanBillingAddress](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ChallanId] [int] NULL,
	[Address] [nvarchar](256) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Email] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SNo] [int] NOT NULL,
	[PCode] [int] NOT NULL,
	[AltQty] [decimal](18, 2) NULL,
	[AltUnit] [nvarchar](max) NULL,
	[Qty] [decimal](18, 2) NULL,
	[Unit] [nvarchar](max) NULL,
	[FreeUnit] [nvarchar](50) NULL,
	[FreeQty] [decimal](18, 0) NULL,
	[AltStockQty] [decimal](18, 2) NULL,
	[StockQty] [decimal](18, 2) NOT NULL,
	[Rate] [decimal](18, 2) NULL,
	[BasicAmt] [decimal](18, 2) NULL,
	[TermAmt] [decimal](18, 2) NOT NULL,
	[NetAmt] [decimal](18, 2) NOT NULL,
	[Remarks] [nvarchar](max) NULL,
	[PurchaseInvoiceId] [int] NOT NULL,
	[Index] [int] NOT NULL,
	[Godown] [int] NULL,
 CONSTRAINT [PK_PurchaseDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[My_Insert]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[My_Insert]
AS
declare @new_identity int
BEGIN
    SET NOCOUNT ON

    INSERT INTO Role (RoleName)
    VALUES ('test')

    SELECT @new_identity = SCOPE_IDENTITY()

    SELECT @new_identity AS id

    RETURN
END
GO
/****** Object:  Table [dbo].[MaterialIssue]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MaterialIssue](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[FinishedGoodId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[UnitId] [int] NULL,
	[Qty] [decimal](18, 2) NULL,
	[ConversionFactor] [decimal](18, 2) NULL,
	[IssueDate] [date] NULL,
	[IssueMiti] [varchar](10) NULL,
	[BillOfMaterialId] [int] NULL,
	[CostCenterId] [int] NULL,
	[BranchId] [int] NULL,
	[CreatedById] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Remarks] [nvarchar](max) NULL,
 CONSTRAINT [PK__Material__3214EC074B0D20AB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[SP_CashFlow]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_CashFlow]
@StartDate Datetime,@EndDate DateTime,@FYId int
as
Begin 
Declare @CasbookAc int
Select @CasbookAc = (select IsNull(CashBook,0) from SystemControl)
select Vdate, IsNull(SUM(LocalCrAmt),0) as OutTotal,IsNull(SUM(LocalDrAmt),0) as InTotal from AccountTransaction 
Where VDate Between @StartDate and @EndDate and GlCode = @CasbookAc and FYId = @FYId
Group By VDate order By VDate 
end
GO
/****** Object:  StoredProcedure [dbo].[SP_CalculateOpening]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_CalculateOpening]
@ledgerId int ,
@StartDate datetime,
@EndDate datetime = null,
@outputType int =0,
@FYId int
as
Declare @amount decimal(18,4)
Declare @result varchar(255)
Declare @opening1 decimal(18,4)
Declare @opening2 decimal(18,4)
select @opening1 = (select Isnull((SUM(DrAmt) - SUM(CrAmt)),0) as Opening1 from AccountTransaction Where GlCode = @ledgerId and AccountTransaction.VDate  < @StartDate and AccountTransaction.FYId = @FYId)
if Isnull(@EndDate,'') <> ''
Begin
	select @opening2 = (select Isnull((SUM(DrAmt) - SUM(CrAmt)),0) as Opening2 from AccountTransaction Where GlCode = @ledgerId and AccountTransaction.VDate >=  @StartDate and AccountTransaction.VDate <= @EndDate and AccountTransaction.Source='OB' and AccountTransaction.FYId = @FYId)
end
	Select @amount = (select (ISNULL(@opening1,0) + ISNULL( @opening2,0)))
	select @result = cast(@amount as varchar(255))
	
if  @outputType = 1
	Begin
		select @result = @result
	end
else
	begin
	select @result = cast(Abs(@amount) as varchar(255))
	if @amount > 0
			Begin
				select @result = @result + ' Dr'
			end
		else if @amount < 0
			begin
				select @result = @result + ' Cr'
			end
	end
select @result as OpeningBalance
GO
/****** Object:  StoredProcedure [dbo].[SP_BSPeriodic]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_BSPeriodic]
@StartDate Datetime,
@EndDate DateTime,
@FYId int,@Type varchar(1)
as
Begin
Create Table #PeriodBs
(
	LedgerId int,
	AccountGroupId int,
	AccountName varchar(255),
	ShortName Varchar(255),
	Opening decimal(18,2),
	Period decimal(18,2),
	Closing decimal(18,2),
	BSType varchar(1),
	AccountCategory varchar(5)
)
Insert Into #PeriodBs 
--	Opening before start date
select t.LedgerId,t.AccountGroupId,t.AccountName,t.ShortName,SUM(t.Opening) as Opening,SUM(t.Period) as Period,SUM(t.Closing) as Closing,t.BSType,t.AccountCategory from (
Select l.Id as LedgerId,ag.Id as AccountGroupId, l.AccountName AS AccountName,l.ShortName AS ShortName,
SUM(CrAmt) - SUM(DrAmt) as Opening ,0 as Period,0 as Closing,
'L' as BSType,l.Category  as AccountCategory From AccountTransaction at
Inner Join Ledger l on at.GlCode = l.Id
Inner join AccountGroup ag on l.AccountGroupId = ag.Id
Where ag.GroupAccountType = 'L' and at.VDate < @StartDate and at.FYId = 1
Group By L.Id,ag.Id, l.AccountName,l.ShortName,l.Category
Union All
Select l.Id as LedgerId,ag.Id as AccountGroupId, l.AccountName AS AccountName,l.ShortName AS ShortName,
SUM(DrAmt) - SUM(CrAmt) as Opening ,0 as Period,0 as Closing,
'A' as BSType,l.Category  as AccountCategory From AccountTransaction at
Inner Join Ledger l on at.GlCode = l.Id
Inner join AccountGroup ag on l.AccountGroupId = ag.Id
Where ag.GroupAccountType = 'A' and at.VDate < @StartDate and at.FYId = 1
Group By L.Id,ag.Id, l.AccountName,l.ShortName,l.Category
	--	Opening balance within the date
Union ALL
Select l.Id as LedgerId,ag.Id as AccountGroupId, l.AccountName AS AccountName,l.ShortName AS ShortName,
SUM(CrAmt) - SUM(DrAmt) as Opening ,0 as Period,0 as Closing,
'L' as BSType,l.Category  as AccountCategory From AccountTransaction at
Inner Join Ledger l on at.GlCode = l.Id
Inner join AccountGroup ag on l.AccountGroupId = ag.Id
Where ag.GroupAccountType = 'L' and at.VDate Between @StartDate And @EndDate and at.Source='OB' and at.FYId = 1
Group By L.Id,ag.Id, l.AccountName,l.ShortName,l.Category
Union All
Select l.Id as LedgerId,ag.Id as AccountGroupId, l.AccountName AS AccountName,l.ShortName AS ShortName,
SUM(DrAmt) - SUM(CrAmt) as Opening ,0 as Period,0 as Closing,
'A' as BSType,l.Category  as AccountCategory From AccountTransaction at
Inner Join Ledger l on at.GlCode = l.Id
Inner join AccountGroup ag on l.AccountGroupId = ag.Id
Where ag.GroupAccountType = 'A' and at.VDate Between @StartDate And @EndDate and at.Source='OB' and at.FYId = 1
Group By L.Id,ag.Id, l.AccountName,l.ShortName,l.Category )as t
Group By t.LedgerId,t.AccountGroupId,t.AccountName,t.ShortName,t.BSType,t.AccountCategory


	
	-- Profit and Loss
	Declare @PlTotal decimal(18,2)
	select @PlTotal = (Select SUM(t.Expense) - SUM(t.InCome) as PLTotal From (
					select 0 as Expense,SUM(at.CrAmt) - SUM(at.DrAmt) as InCome from AccountTransaction at
					Inner join Ledger l On at.GlCode = l.Id
					Inner join AccountGroup ag on ag.Id= l.AccountGroupId
					Where  ag.GroupAccountType ='I' and at.VDate < @StartDate  and at.FYId = @FYId
					Union All
					select SUM(at.DrAmt) - SUM(at.CrAmt) as Expense , 0 as Income from AccountTransaction at
					Inner join Ledger l On at.GlCode = l.Id
					Inner join AccountGroup ag on ag.Id= l.AccountGroupId
					Where  ag.GroupAccountType ='E' and at.VDate < @StartDate  and at.FYId = @FYId) t)
					
					if @PlTotal <> 0
					Begin
						Insert Into #PeriodBs 
						select l.Id as LedgerId, ag.Id as AccountGroupId,l.AccountName AS AccountName,l.ShortName AS ShortName ,
						case when ag.GroupAccountType ='L' Then -@PlTotal end as Opening,0 as Period,0 as Closing,
						ag.GroupAccountType ,'PL' as AccountCategory from Ledger l
						Inner Join SystemControl s on l.Id= s.ProfitLossAc
						Inner Join AccountGroup ag on l.AccountGroupId = ag.Id
					End
	
	
	--	Periodic
Insert Into #PeriodBs 
	Select l.Id as LedgerId,ag.Id as AccountGroupId, l.AccountName AS AccountName,l.ShortName AS ShortName,
	0 as Opening,SUM(CrAmt) - SUM(DrAmt) as Period ,0 as Closing,
	'L' as BSType,l.Category  as AccountCategory From AccountTransaction at
	Inner Join Ledger l on at.GlCode = l.Id
	Inner join AccountGroup ag on l.AccountGroupId = ag.Id
	Where ag.GroupAccountType = 'L' and at.VDate Between @StartDate And @EndDate  and at.FYId = @FYId and at.Source <> 'OB'
	Group By L.Id,ag.Id, l.AccountName,l.ShortName,l.Category
	Union All
	Select l.Id as LedgerId,ag.Id as AccountGroupId, l.AccountName AS AccountName,l.ShortName AS ShortName,
	0 as Opening,SUM(DrAmt) - SUM(CrAmt) as Period,0 as Closing,
	'A' as BSType,l.Category  as AccountCategory From AccountTransaction at
	Inner Join Ledger l on at.GlCode = l.Id
	Inner join AccountGroup ag on l.AccountGroupId = ag.Id
	Where ag.GroupAccountType = 'A' and at.VDate Between @StartDate And @EndDate  and at.FYId = @FYId and at.Source <> 'OB'
	Group By L.Id,ag.Id, l.AccountName,l.ShortName,l.Category
	
	-- Profit and Loss

	select @PlTotal = (Select SUM(t.Expense) - SUM(t.InCome) as PLTotal From (
					select 0 as Expense,SUM(at.CrAmt) - SUM(at.DrAmt) as InCome from AccountTransaction at
					Inner join Ledger l On at.GlCode = l.Id
					Inner join AccountGroup ag on ag.Id= l.AccountGroupId
					Where  ag.GroupAccountType ='I' and at.VDate Between @StartDate And @EndDate  and at.FYId = @FYId
					Union All
					select SUM(at.DrAmt) - SUM(at.CrAmt) as Expense , 0 as Income from AccountTransaction at
					Inner join Ledger l On at.GlCode = l.Id
					Inner join AccountGroup ag on ag.Id= l.AccountGroupId
					Where  ag.GroupAccountType ='E' and at.VDate Between @StartDate And @EndDate  and at.FYId = @FYId) t)
					if @PlTotal <> 0
					Begin
						Insert Into #PeriodBs
						select l.Id as LedgerId, ag.Id as AccountGroupId,l.AccountName AS AccountName,l.ShortName AS ShortName ,
						0 as Opening,case when ag.GroupAccountType ='L' Then -@PlTotal end as Period,0 as Closing,
						ag.GroupAccountType ,'PL' as AccountCategory from Ledger l
						Inner Join SystemControl s on l.Id= s.ProfitLossAc
						Inner Join AccountGroup ag on l.AccountGroupId = ag.Id
					End

Declare @Sql varchar(max)
Set @Sql = 'Select * From (
	select 0 as LedgerId,#.AccountGroupId,ag.Description  AccountName,'''' as ShortName,SUM(Isnull(Opening,0)) as Opening,SUM(Isnull(Period,0)) as Period,
	SUM(Isnull(Opening,0)) + SUM(Isnull(Period,0)) as Closing,
	BSType,'''' as AccountCategory from #PeriodBs #
	Inner Join AccountGroup ag on #.AccountGroupId = ag.Id
	Group By #.AccountGroupId,ag.Description,BSType
	Union All
	select #.LedgerId,#.AccountGroupId,#.AccountName,#.ShortName,SUM(#.Opening) as Opening,SUM(#.Period) as Period,
	SUM(#.Opening) + SUM(#.Period)  as Closing,#.BSType,#.AccountCategory from #PeriodBs #
	Group By #.LedgerId,#.AccountGroupId,#.AccountName,#.ShortName,#.BSType,#.AccountCategory) as Temp'
	
	If @Type = 'B'
		Begin
			Exec (@sql)
		end
	Else if @Type = 'L'
		Begin
			set @sql += ' Where BsType=''L'''
			Exec (@sql)
		end
	Else if @Type = 'A'
		Begin
			set @sql += ' Where BsType=''A'''
			Exec (@sql)
		end
	
	Drop table #PeriodBs 
	
End
GO
/****** Object:  StoredProcedure [dbo].[SP_BSLedgerWise]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_BSLedgerWise]
@AsOnDate Datetime,
@FYId int,
@Type varchar(1)
as
Begin
Create Table #LedgersTotal
(
	LedgerId int,
	AccountName varchar(255),
	ShortName Varchar(255),
	LedgerTotal decimal(18,2),
	BSType varchar(1),
	AccountCategory varchar(5)
)
Insert Into #LedgersTotal 
	Select l.Id as LedgerId,l.AccountName,l.ShortName,SUM(CrAmt) - SUM(DrAmt) as LedgerTotal ,
	'L' as BSType,l.Category  as AccountCategory From AccountTransaction at
	Inner Join Ledger l on at.GlCode = l.Id
	Inner join AccountGroup ag on l.AccountGroupId = ag.Id
	Where ag.GroupAccountType = 'L' and at.VDate <= @AsOnDate And at.FYId = @FYId
	Group By L.Id ,l.AccountName,l.ShortName,l.Category
	Union All
	Select l.Id as LedgerId,l.AccountName ,l.ShortName,SUM(DrAmt) - SUM(CrAmt) as LedgerTotal ,
	'A' as BSType,l.Category  as AccountCategory From AccountTransaction at
	Inner Join Ledger l on at.GlCode = l.Id
	Inner join AccountGroup ag on l.AccountGroupId = ag.Id
	Where ag.GroupAccountType = 'A' and at.VDate <= @AsOnDate  And at.FYId = @FYId
	Group By L.Id ,l.AccountName,l.ShortName,l.Category
	
	-- Profit and Loss
	Declare @PlTotal decimal(18,2)
	select @PlTotal = (Select SUM(t.Expense) - SUM(t.InCome) as PLTotal From (
					select 0 as Expense,SUM(at.CrAmt) - SUM(at.DrAmt) as InCome from AccountTransaction at
					Inner join Ledger l On at.GlCode = l.Id
					Inner join AccountGroup ag on ag.Id = l.AccountGroupId
					Where  ag.GroupAccountType='I' and at.VDate <= @AsOnDate  And at.FYId = @FYId
					Union All
					select SUM(at.DrAmt) - SUM(at.CrAmt) as Expense , 0 as Income from AccountTransaction at
					Inner join Ledger l On at.GlCode = l.Id
					Inner join AccountGroup ag on ag.Id = l.AccountGroupId
					Where  ag.GroupAccountType='E' and at.VDate <= @AsOnDate  And at.FYId = @FYId) t)
					if @PlTotal <> 0
					begin
						Declare @PlLedgerId int 
						select @PlLedgerId = (select #LedgersTotal.LedgerId from #LedgersTotal inner Join SystemControl s On #LedgersTotal.LedgerId = s.ProfitLossAc)
						if @PlLedgerId <> 0
						begin
							Update #LedgersTotal Set LedgerTotal = LedgerTotal + @PlTotal,AccountCategory='PL' Where LedgerId = @PlLedgerId
						End
						else
						Begin
							Insert Into #LedgersTotal
							select l.Id as LedgerId,l.AccountName,l.ShortName ,
							case when ag.GroupAccountType ='L' Then -@PlTotal end as LedgerTotal,
							ag.GroupAccountType ,'PL' as AccountCategory from Ledger l
							Inner Join SystemControl s on l.Id = s.ProfitLossAC
							Inner Join AccountGroup ag on l.AccountGroupId = ag.Id
						end
					end
	
	
	If @Type = 'B'
		Begin
			Select * From #LedgersTotal Order By BSType Desc , AccountName , ShortName 
		end
	Else if @Type = 'L'
		Begin
			Select * From #LedgersTotal Where #LedgersTotal.BSType = 'L' Order By BSType Desc , AccountName , ShortName 
		end
	Else if @Type = 'A'
		Begin
			Select * From #LedgersTotal Where #LedgersTotal.BSType = 'A' Order By BSType Desc , AccountName , ShortName 
		end
	
	Drop table #LedgersTotal 
End
GO
/****** Object:  StoredProcedure [dbo].[SP_BSAccountGroupWise]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_BSAccountGroupWise]
@AsOnDate Datetime,
@FYId int,@Type varchar(1)
as
Begin
Create Table #LedgersTotal
(
	LedgerId int,
	AccountGroupId int,
	AccountName varchar(255),
	ShortName Varchar(255),
	LedgerTotal decimal(18,2),
	BSType varchar(1),
	AccountCategory varchar(5)
)
Insert Into #LedgersTotal 
	Select l.id as LedgerId,ag.Id as AccountGroupId, l.AccountName AS AccountName,l.ShortName AS ShortName,SUM(CrAmt) - SUM(DrAmt) as LedgerTotal ,'L' as BSType,l.Category  as AccountCategory From AccountTransaction at
	Inner Join Ledger l on at.GlCode = l.Id
	Inner join AccountGroup ag on l.AccountGroupId= ag.Id
	Where ag.GroupAccountType= 'L' and at.VDate <= @AsOnDate And at.FYId = @FYId
	Group By L.Id ,ag.Id, l.AccountName,l.ShortName,l.Category
	Union All
	Select l.id as LedgerId,ag.Id as AccountGroupId, l.AccountName AS AccountName,l.ShortName AS ShortName,SUM(DrAmt) - SUM(CrAmt) as LedgerTotal ,'A' as BSType,l.Category  as AccountCategory From AccountTransaction at
	Inner Join Ledger l on at.GlCode = l.Id
	Inner join AccountGroup ag on l.AccountGroupId= ag.Id
	Where ag.GroupAccountType = 'A' and at.VDate <= @AsOnDate And at.FYId = @FYId
	Group By L.Id ,ag.Id, l.AccountName,l.ShortName,l.Category
	
	-- Profit and Loss
	Declare @PlTotal decimal(18,2)
	select @PlTotal = (Select SUM(t.Expense) - SUM(t.InCome) as PLTotal From (
					select 0 as Expense,SUM(at.CrAmt) - SUM(at.DrAmt) as InCome from AccountTransaction at
					Inner join Ledger l On at.GlCode = l.Id
					Inner join AccountGroup ag on ag.Id = l.AccountGroupId
					Where  ag.GroupAccountType='I' and at.VDate <= @AsOnDate And at.FYId = @FYId
					Union All
					select SUM(at.DrAmt) - SUM(at.CrAmt) as Expense , 0 as Income from AccountTransaction at
					Inner join Ledger l On at.GlCode = l.Id
					Inner join AccountGroup ag on ag.Id= l.AccountGroupId
					Where  ag.GroupAccountType='E' and at.VDate <= @AsOnDate And at.FYId = @FYId) t)
					if @PlTotal <> 0
					begin
						Insert Into #LedgersTotal
						select l.Id as LedgerId, ag.Id as AccountGroupId,l.AccountName AS AccountName,l.ShortName AS ShortName ,
						case when ag.GroupAccountType ='L' Then -@PlTotal end as LedgerTotal,
						ag.GroupAccountType,'PL' as AccountCategory from Ledger l
						Inner Join SystemControl s on l.Id= s.ProfitLossAc
						Inner Join AccountGroup ag on l.AccountGroupId= ag.Id
					end
	Declare @sql varchar(max) 
	Set @sql = 'Select * From (select 0 as LedgerId, #.AccountGroupId ,ag.Description as AccountName,'''' as ShortName, SUM(LedgerTotal) as LedgerTotal,#.BSType,'''' as AccountCategory From #LedgersTotal #
			Inner Join AccountGroup ag On #.AccountGroupId = ag.Id
			Group by #.AccountGroupId ,ag.Description,#.BSType
			Union All
			select * From #LedgersTotal ) #Temp '
	
	If @Type = 'B'
		Begin
			set @sql += ' Order By BSType Desc , AccountName , ShortName'
			Exec (@sql)
		end
	Else if @Type = 'L'
		Begin
			set @sql += 'Where BsType=''L'''
			set @sql += ' Order By BSType Desc , AccountName , ShortName'
			Exec (@sql)
		end
	Else if @Type = 'A'
		Begin
			set @sql += 'Where BsType=''A'''
			set @sql += ' Order By BSType Desc , AccountName , ShortName'
			Exec (@sql)
		end
	
	Drop table #LedgersTotal 
End
GO
/****** Object:  StoredProcedure [dbo].[SP_CBTransactionDetails]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_CBTransactionDetails]
@AsOnDate datetime,
@CashBookId int,
@FYId int
as
Select * From (
select cbm.Id,cbm.VoucherNo,l.AccountName,l.ShortName ,l.Id as LedgerId,cbd.Amount as Receive , 0 as Payment , cbd.AmountType
from CashBankMaster cbm
inner join CashBankDetail cbd on cbm.Id = cbd.MasterId
inner join Ledger l on cbd.LedgerId = l.Id
Where cbm.LedgerId = @CashBookId and AmountType = 1 and cbm.VoucherDate=@AsOnDate and cbm.FYId = @FYId
Union All
select cbm.Id, cbm.VoucherNo,l.AccountName,l.ShortName ,l.Id as LedgerId,0 as Receive , cbd.Amount as Payment , cbd.AmountType
from CashBankMaster cbm
inner join CashBankDetail cbd on cbm.Id = cbd.MasterId
inner join Ledger l on cbd.LedgerId = l.Id
Where cbm.LedgerId = @CashBookId and AmountType = 2 and cbm.VoucherDate =@AsOnDate and cbm.FYId = @FYId
)t  Order by t.Id
GO
/****** Object:  StoredProcedure [dbo].[SP_CBTransactionDateForCashBook]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[SP_CBTransactionDateForCashBook]
@StartDate datetime,
@EndDate datetime,
@CashBookId int,
@FYId int
as
--	Gives the list of distinct date in which cashbook transaction is placed
Select Distinct cbm.VoucherDate from CashBankMaster cbm

Where cbm.VoucherDate Between @StartDate and @EndDate and FYId=@FYId and cbm.LedgerId = @CashBookId
GO
/****** Object:  StoredProcedure [dbo].[SP_CBOpeningByCashBook]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_CBOpeningByCashBook]
@StartDate datetime,
@CashBookId int,
@FYId int
as
select ISNULL(SUM(t.Receive)-SUM(t.Payment),0) as Total,t.AccountName From(
select l1.AccountName,ISNULL(cbd.Amount,0) as Receive , 0 as Payment 
from CashBankMaster cbm
inner join CashBankDetail cbd on cbm.Id = cbd.MasterId
inner join Ledger l on cbd.LedgerId = l.Id
inner join Ledger l1 on cbm.LedgerId = l1.Id
Where cbm.LedgerId = @CashBookId  and cbm.VoucherDate<@StartDate and cbm.FYId = @FYId and cbd.AmountType =1
Union All
select l1.AccountName, 0 as Receive , ISNULL(cbd.Amount,0) as Payment 
from CashBankMaster cbm
inner join CashBankDetail cbd on cbm.Id = cbd.MasterId
inner join Ledger l on cbd.LedgerId = l.Id
inner join Ledger l1 on cbm.LedgerId = l1.Id
Where cbm.LedgerId = @CashBookId  and cbm.VoucherDate<@StartDate and cbm.FYId = @FYId and cbd.AmountType =2
)t Group BY t.AccountName
GO
/****** Object:  StoredProcedure [dbo].[SP_DayBookHeader]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_DayBookHeader]
@AsOnDate DateTime,
@FYId int,
@SourceList varchar(255)
as
Declare @sql varchar(max)
set @sql = 'select at.VDate,at.VNo,At.Source, SUM(DrAmt) AS DrAmt,SUM(CrAmt) as CrAmt
from AccountTransaction at
Where at.VDate =''' + dbo.DtYMD(@AsOnDate) + ''' and at.FYId =' + CAST (@FYId as varchar(5))
if @SourceList <> ''
Begin
set @sql += ' and at.Source in (' + REPLACE( @SourceList,'%','''') + ')'
end
set @sql += ' Group By at.VDate,at.VNo,At.Source Order by at.VDate'

exec (@sql)
GO
/****** Object:  StoredProcedure [dbo].[SP_DayBookDetail]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_DayBookDetail]
@VoucherNo varchar(255),
@Source varchar(3)
as
select l.AccountName,at.VNo,at.Source, l.ShortName,SUM(at.DrAmt) as DrAmt,SUM(at.CrAmt) as CrAmt from AccountTransaction at
inner Join Ledger l On at.GlCode = l.Id
Where at.VNo = @VoucherNo and at.Source = @Source
group By l.AccountName,at.VNo,at.Source ,l.ShortName
GO
/****** Object:  StoredProcedure [dbo].[SP_DayBookDates]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_DayBookDates]
@StartDate datetime,
@EndDate datetime,
@FYId int,
@SourceList varchar(255)
as
Declare @Sql varchar(max)
select @Sql ='select Distinct at.VDate from AccountTransaction at Where at.VDate Between '''+dbo.DtYMD(@StartDate)+''' and '''+ dbo.DtYMD(@EndDate)+''' and at.FYId ='+CAST(@FYId as Varchar(5))+' and at.Source in (' + REPLACE( @SourceList,'%','''') + ') Order by at.VDate'

--select @Sql
exec(@Sql)
GO
/****** Object:  StoredProcedure [dbo].[Sp_ConsolidateUpateRank]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[Sp_ConsolidateUpateRank] @ExamId =45 , @ClassId =162,@AcademicYearId =2

CREATE procedure [dbo].[Sp_ConsolidateUpateRank] 
@ExamId int , @ClassId int	 ,@AcademicYearId int
as
Declare @RankNo int
Declare @Id int
Declare StatementCursor Cursor For
	SELECT Dense_RANK() OVER (
	Order BY [Percent] DESC) AS [RecordRank],ID
	FROM Consolidate   where ExamId = @ExamId and ClassId = @ClassId and Result = 'Pass' and AcademicYearId = @AcademicYearId	order by id
Open StatementCursor
Fetch Next From StatementCursor Into @RankNo,@Id
Declare @rank int= 0
while @@FETCH_STATUS =0
Begin
	Update Consolidate Set [Rank] = @RankNo Where Id = @Id
Fetch Next From StatementCursor Into @RankNo,@Id
end
CLOSE StatementCursor
DEALLOCATE StatementCursor
--Highest Marks

Declare @SubjectId int
Declare @HighestMark nvarchar(255)
Declare SubjectList Cursor For
	Select Distinct cd.ConsolidateId,cd.SubjectId From ConsolidateDetail cd
	Inner Join Consolidate c on cd.ConsolidateId = c.Id
	Where c.AcademicYearId = @AcademicYearId and c.ClassId = @ClassId and c.ExamId = @ExamId
Open SubjectList
Fetch Next From SubjectList InTo @Id,@Subjectid
While @@FETCH_STATUS = 0
Begin											
		Set @HighestMark = (Select MAX(Obtained) HighestMarks from (
		Select c.Id, cd.SubjectId, Sum(TheoryObtainedMark + PracticalObtainedMark) as Obtained From ConsolidateDetail cd
		Inner Join Consolidate c on cd.ConsolidateId = c.Id 
		Where c.ClassId = @ClassId and c.ExamId = @ExamId and c.AcademicYearId = @AcademicYearId
		Group By c.Id, cd.SubjectId	
		) HighMarks Where SubjectId = @SubjectId  Group By SubjectId)
			  
		Update ConsolidateDetail Set HighestMarks = @HighestMark Where ConsolidateId = @Id and SubjectId = @Subjectid
	Fetch Next From SubjectList InTo @Id,@Subjectid
End
CLOSE SubjectList
DEALLOCATE SubjectList
GO
/****** Object:  StoredProcedure [dbo].[SP_JVDatesBet]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_JVDatesBet]
@StartDate datetime,
@EndDate Datetime,
@FYId int
as
select distinct JVDate from JournalVoucher
Where JVDate Between @StartDate And @EndDate and FYId=@FYId Order by JVDate Asc
GO
/****** Object:  StoredProcedure [dbo].[SP_JournalVouchersByDate]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_JournalVouchersByDate]
@AsOnDate datetime,
@FYId int
as
select j.*,u.FullName from JournalVoucher j
Inner Join [User] u on j.CreatedById = u.Id
Where JVDate=@AsOnDate and FYId = @FYId Order By j.Id Desc
GO
/****** Object:  StoredProcedure [dbo].[SP_JournalVoucherDetail]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_JournalVoucherDetail]
@JournalId int
as
Select j.*,l.AccountName,l.ShortName from JournalVoucherDetail j 
Inner join Ledger l On j.GlCode = l.Id
Where j.JVMasterId = @JournalId
Order By j.Id
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertModuleForRoles]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_InsertModuleForRoles]
@ModuleId int
as

declare @RoleId int
Declare Roles Cursor For
	select r.Id  From [Role] As r
	Open Roles
	Fetch Next From Roles Into @RoleId
	while @@FETCH_STATUS =0
	Begin
		insert into securityright(moduleid,[role],[Create],Edit,[Delete],Navigate,Approve)
		values (@ModuleId,@RoleId,1,0,0,0,0)
		Fetch Next From Roles Into @RoleId
	end
	CLOSE Roles
	DEALLOCATE Roles
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertModeule]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_InsertModeule]
(
	@name varchar(255), 
	@shortName varchar(255)
)
as
insert into modulelist
(
	name,shortname
) 
values(@name,@shortName)
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCurrentBalance]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_GetCurrentBalance]
@outputType int =0,
@glCode int=0,
@FYId int
as
declare @balance decimal(18,4)
declare @result varchar(255)
select @balance = (select IsNull((Sum(DrAmt) - Sum(CrAmt)),0) as Balance  from AccountTransaction 
Where GlCode =@glCode AND FYId=@FYId)
--select @balance
select @result = cast(@balance as varchar(255))
if @outputType = 1
	begin
		select @result = @result
	end
else
	begin
		select @result = cast(Abs(@balance) as varchar(255))
		if @balance > 0
			Begin
				select @result = @result + ' Dr'
			end
		else if @balance < 0
			begin
				select @result = @result + ' Cr'
			end
		else if @balance < 0
			begin
				select @result = '0.0000'
			end
	end

select @result as CurrentBalance
GO
/****** Object:  StoredProcedure [dbo].[SP_PartyLedgerSummary]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SP_PartyLedgerSummary @StartDate='2013-05-03',@EndDate='2013-06-19',@LedgerId='0',@Category ='CU,VE,BO,OT'

CREATE Proc [dbo].[SP_PartyLedgerSummary]
@StartDate Date,@EndDate Date,@LedgerId Varchar(max),@Category varchar(15)
as

Create Table #SummaryPartyLedger
(
	GlCode Int ,
	OpnDr Decimal(18,2),OpnCr Decimal(18,2),
	PeriodDr Decimal(18,2),PeriodCr Decimal(18,2),
	CloseDr Decimal(18,2),CloseCr Decimal(18,2),  
)

Insert Into #SummaryPartyLedger(GlCode,OpnDr,OpnCr,PeriodDr,PeriodCr,CloseDr,CloseCr)
Select GlCode,Sum(OpeningDr) OpeningDr ,Sum(OpeningCr) OpeningCr,
Sum(PeriodDr) PeriodDr ,Sum(PeriodCr) PeriodCr,
Sum(CloseDr) CloseDr ,Sum(CloseCr) CloseCr From (
Select GlCode,Sum(DrAmt) OpeningDr ,Sum(CrAmt) OpeningCr,0 PeriodDr,0 PeriodCr,0 CloseDr ,0 CloseCr  From AccountTransaction
Where VDate < @StartDate
And (@LedgerId='0' OR GlCode In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@LedgerId, ',')))
Group By GlCode
UNION ALL
Select GlCode,0 OpeningDr ,0 OpeningCr ,Sum(DrAmt) PeriodDr ,Sum(CrAmt) PeriodCr,0 CloseDr ,0 CloseCr From AccountTransaction
Where VDate Between @StartDate AND @EndDate
And (@LedgerId='0' OR GlCode In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@LedgerId, ',')))
Group By GlCode
UNION ALL
Select GlCode,0 OpeningDr ,0 OpeningCr ,0 PeriodDr ,0 PeriodCr,Sum(DrAmt) CloseDr ,Sum(CrAmt) CloseCr From AccountTransaction
Where VDate <= @EndDate
And (@LedgerId='0' OR GlCode In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@LedgerId, ',')))
Group By GlCode) as temp Group By GlCode

Declare @OpnDr Decimal(18,2)
Declare @OpnCr Decimal(18,2)
Declare @PeriodDr Decimal(18,2)
Declare @PeriodCr Decimal(18,2)
Declare @CloseDr Decimal(18,2)
Declare @CloseCr Decimal(18,2)
Declare @GlCode int
Declare @AccountName nvarchar(255)
	
Declare LedgerSummary Cursor For
	Select L.AccountName, s.* From #SummaryPartyLedger S
	Inner Join Ledger L On s.GlCode = L.Id
	Where Category In ( SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@Category, ','))
	Order by L.AccountName 
	   
Open LedgerSummary
Fetch Next From LedgerSummary InTo @AccountName,@GlCode,@OpnDr,@OpnCr,@PeriodDr,@PeriodCr,@CloseDr,@CloseCr    
While @@FETCH_STATUS = 0 
Begin
	if @CloseDr > @CloseCr 
	Begin
		Update #SummaryPartyLedger Set CloseDr = ABS((@CloseDr - @CloseCr)) ,CloseCr = 0 Where GlCode = @GlCode
	End
	Else if @CloseCr > @CloseDr 
	Begin
		Update #SummaryPartyLedger Set CloseCr = ABS((@CloseDr - @CloseCr)) ,CloseDr = 0 Where GlCode = @GlCode
	End
	else
	Begin
		Update #SummaryPartyLedger Set CloseCr = 0 ,CloseDr = 0 Where GlCode = @GlCode
	End
	
	Fetch Next From LedgerSummary InTo @AccountName,@GlCode,@OpnDr,@OpnCr,@PeriodDr,@PeriodCr,@CloseDr,@CloseCr 
End
CLOSE LedgerSummary
DEALLOCATE LedgerSummary

Select l.Id LedgerId,L.AccountName Ledger, l.AccountGroupId,ag.Description AccountGroup,
l.AccountSubGroupId,sg.Description, s.* From #SummaryPartyLedger s
Inner Join Ledger l On s.GlCode = l.Id
Inner Join AccountGroup ag on l.AccountGroupId = ag.Id
Left Outer Join AccountSubGroup sg on l.AccountSubGroupId = sg.Id
Drop Table #SummaryPartyLedger
GO
/****** Object:  StoredProcedure [dbo].[SP_PartyLedgers]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_PartyLedgers]
@LedgerId int ,
@StartDate datetime ,
@EndDate datetime,@FYId int,@SubLedgerId int =0 
as
SELECT 
	at.VNo,	
	at.VDate AS VDate,	
	at.Source,
	at.GlCode,
	
	SUM(at.DrAmt)-SUM(at.CrAmt) AS Amount
FROM 
	AccountTransaction at
	
	
WHERE 
	at.Source <> 'OPNB'
	AND	at.GlCode = @LedgerId
	AND at.FYId = @FYId
	AND	at.VDate BETWEEN @StartDate AND @EndDate
	AND at.Source <>'OB'
	AND (@SubLedgerId = 0 OR at.SlCode = @SubLedgerId)
GROUP BY 
	at.VNo,	
	at.VDate,	
	at.Source,	
	at.GlCode
ORDER BY 
	VDate ASC
GO
/****** Object:  StoredProcedure [dbo].[SP_OutstandingCustomer]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_OutstandingCustomer]

@Ledger_Id int,
@AsOnDate DateTime
as
begin
Create table #OutStandings
(
LedgerId int,
VoucherNo varchar(255),
Source Varchar(5),
VoucherDate datetime,
Amount Decimal(18,2),
Adjusted decimal(18,2),
Balance decimal (18,2),
DueDate datetime ,
DueAge int 
)

Declare @LedgerId int
Declare @VoucherNo varchar(255)
Declare @Source Varchar(5)
Declare @VoucherDate datetime
Declare @Amount Decimal(18,2)
Declare @Adjusted decimal(18,2)
Declare @Balance decimal (18,2)
Declare @DueDate datetime
Declare @DueAge int 

Insert Into #OutStandings 
select GlCode as LedgerId ,VNo as VoucherNo,Source,VDate as VoucherDate,Sum(DrAmt) - Sum(CrAmt) as Amount , 0 as Adjusted,
0 as Balance , VDate as DueDate, 0 as DueAge from AccountTransaction where GlCode = @Ledger_Id
and VDate <= @AsOnDate
Group By GlCode,VNo,Source,VDate Order by AccountTransaction.Source DESC



Declare @CurrentBalance Decimal(18,2)


select @CurrentBalance = (select SUM(at.CrAmt) as TotalBalance from AccountTransaction at
Where at.GlCode = @Ledger_Id and at.VDate <= @AsOnDate)

Declare @InterestRate Decimal(18,2)
select @InterestRate = Isnull((select Ledger.RateOfInterest from Ledger Where Id = @Ledger_Id),0)
Declare @TotalAdjustmentUsed Decimal(18,2) = 0
Declare OutStandingCursor Cursor For
select LedgerId ,VoucherNo,Source,VoucherDate,Amount ,Adjusted,
Balance , DueDate, DueAge from #OutStandings where LedgerId = @Ledger_Id

	Open OutStandingCursor
	Fetch Next From OutStandingCursor Into @LedgerId,@VoucherNo,@Source,@VoucherDate,@Amount,@Adjusted,@Balance,@DueDate,@DueAge
	while @@FETCH_STATUS =0
	Begin
		Declare @Days int
		select @Days = (SELECT DATEDIFF(Day,ISNULL(@DueDate,getdate()),getdate()))
		if @Amount >= 0
		Begin
			if @CurrentBalance >= @Amount
			Begin
				select @Adjusted = @Amount
				select @TotalAdjustmentUsed += @Amount
				select @CurrentBalance = @CurrentBalance - @Adjusted
			End
			else
			Begin
				select @Adjusted = @CurrentBalance
				select @TotalAdjustmentUsed += @CurrentBalance
				select @CurrentBalance = 0
			End
			select @Balance = @Amount - @Adjusted
		End
		else
		Begin
			--select @TotalAdjustmentUsed 2500
			if @TotalAdjustmentUsed >= abs(@Amount)
			Begin
				select @Adjusted = @Amount
				select @TotalAdjustmentUsed -=  ABS(@Adjusted)
				select @Balance = @Amount - @Adjusted
			End
			else
			Begin
				select @Adjusted = @TotalAdjustmentUsed
				select @TotalAdjustmentUsed = 0
				select @Balance =  @Amount + @Adjusted  
			End
			select @TotalAdjustmentUsed
		End
		
		
		--Calculate interest where due date is not null , if it is null we dont need to calculate interest amount
			Update #OutStandings Set Balance = @Balance , Adjusted = @Adjusted , DueAge = @Days ,DueDate = Isnull(@DueDate,@VoucherDate) 
			Where LedgerId = @LedgerId and VoucherNo = @VoucherNo and Source = @Source
		Fetch Next From OutStandingCursor Into @LedgerId,@VoucherNo,@Source,@VoucherDate,@Amount,@Adjusted,@Balance,@DueDate,@DueAge
	end
	CLOSE OutStandingCursor
	DEALLOCATE OutStandingCursor




select * From #OutStandings
drop table #OutStandings
end
GO
/****** Object:  StoredProcedure [dbo].[SP_OpeningTrialBalance]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_OpeningTrialBalance]
@Type varchar(1)
as
Select * From (
Select l.Id LedgerId,ag.Id AccountGroupId, L.AccountName ,Null as SlCode, Sum(at.CrAmt) Credit, Sum(at.DrAmt) Debit,'L' as Type from AccountTransaction  at
Inner Join Ledger l on at.GlCode = l.Id
inner join AccountGroup ag on l.AccountGroupId = ag.Id
Where Source = 'OB' Group By l.Id,ag.Id, L.AccountName
UNION ALL
Select 0 LedgerId,ag.Id AccountGroupId, ag.Description,NULL as SlCode, Sum(at.CrAmt) Credit, Sum(at.DrAmt) Debit,'G' as Type from AccountTransaction  at
Inner Join Ledger l on at.GlCode = l.Id
inner join AccountGroup ag on l.AccountGroupId = ag.Id
Where Source = 'OB' Group By ag.Id,ag.Description 
UNION ALL
Select l.Id LedgerId,ag.Id AccountGroupId, ISNULL(s.Description,'No Sub Ledger') as [Description],at.SlCode, Sum(at.CrAmt) Credit, Sum(at.DrAmt) Debit,'S' as Type from AccountTransaction  at
Inner Join Ledger l on at.GlCode = l.Id
inner join AccountGroup ag on l.AccountGroupId = ag.Id
left outer join SubLedger s on at.SlCode = s.Id
Where Source = 'OB' Group By l.Id, ag.Id,s.Description ,at.SlCode
) Temp Where Type = @Type Order By AccountName
GO
/****** Object:  StoredProcedure [dbo].[SP_OpeningBalance]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_OpeningBalance]
(@Source varchar(10), @ledgerId int,@FYId int)
as
Select ISNULL(SUM(at.DrAmt)-SUM(at.CrAmt),0) as OpeningBalance from AccountTransaction at where Source=@Source and GlCode = @ledgerId And FYId = @FYId
GO
/****** Object:  StoredProcedure [dbo].[Sp_PurchaseBillTerms]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Sp_PurchaseBillTerms]
@PurchaseInvoiceId int
as
select bt.Description,td.Rate,td.TermAmount from PurchaseInvoice p
inner join BillingTermDetail td on p.Id = td.ReferenceId
Inner join BillingTerm bt on td.BillingTermId = bt.Id
Where Source ='PB'
And  td.ReferenceId = @PurchaseInvoiceId and td.IsProductWise = 0
GO
/****** Object:  StoredProcedure [dbo].[SP_ProductBatchSummary]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[SP_ProductBatchSummary]
as
Select * From Product
GO
/****** Object:  StoredProcedure [dbo].[SP_PriceList]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_PriceList]
@AsOnDate date ,@ProductId varchar(max),@FYId int
as
Create table #PriceList
(
Id Int identity(1,1),
ProductId int,ShortName nvarchar(100), Description nvarchar(255),Unit nvarchar(50),BuyPrice decimal(18,2)
,SalePrice decimal(18,2),LastBuyPrice decimal(18,2),LastSalePrice decimal(18,2),CurrentStock decimal(18,2)
)
Insert into #PriceList(ProductId,ShortName,Description,Unit,BuyPrice,SalePrice)
Select p.Id,p.ShortName,p.Name,Unit.Description, p.BuyPrice,p.SalesPrice From Product p
Left Outer Join Unit On p.Id = Unit.Id
Where (@ProductId='0' OR p.Id In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@ProductId, ',')))
Order by p.Name

Declare @Id int
Declare @Product_Id int 

Declare PriceCursor Cursor For
select Id,ProductId From #PriceList
Open PriceCursor
Fetch Next From PriceCursor Into @Id,@Product_Id
While @@FETCH_STATUS = 0
Begin
	--	Get Product Last price 
	Declare @LastBuyPrice decimal(18,2)
	Declare @LastSalePrice decimal(18,2)
	Declare @CurrentStock decimal(18,2)
	set @LastBuyPrice =  (Select TOP 1 Rate from StockTransaction where TransactionType ='I' and ProductCode =@Product_Id and VDate <= @AsOnDate and FYId = @FYId Order By VDate Desc,Id Desc)
	set @LastSalePrice =  (Select TOP 1 Rate from StockTransaction where TransactionType ='O' and ProductCode =@Product_Id and VDate <= @AsOnDate and FYId = @FYId Order By VDate Desc,Id Desc)
	set @CurrentStock = (Select  SUM(Quantity) as CurrentStock From (
	Select Sum(st.AltQty) as Quantity From StockTransaction st Where st.ProductCode = @Product_Id and st.TransactionType = 'I' and st.VDate <= @AsOnDate and st.FYId = @FYId
	Union ALl
	Select Sum(-st.AltQty) as Quantity From StockTransaction st Where st.ProductCode = @Product_Id and st.TransactionType = 'O' and st.VDate <= @AsOnDate and st.FYId = @FYId
	) as Stock )
	
	Update #PriceList Set LastBuyPrice = @LastBuyPrice ,LastSalePrice = @LastSalePrice,CurrentStock = @CurrentStock Where Id = @Id
	
	
	
	Fetch Next From PriceCursor Into @Id,@Product_Id
End
CLOSE PriceCursor
DEALLOCATE PriceCursor

select * From #PriceList
Drop table #PriceList
GO
/****** Object:  StoredProcedure [dbo].[SP_PLTotalByGroupSide]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_PLTotalByGroupSide]
@StartDate datetime,
@EndDate datetime,
@FYId int 
as
Select t.GroupAccountType,Sum(t.OpeningTotal) as OpeningTotal, Sum(t.PeriodTotal)  as PeriodTotal ,
(Sum(t.OpeningTotal) + Sum(t.PeriodTotal)) as Closing  from (

Select ag.GroupAccountType, l.Id,l.AccountName,l.ShortName,
case when ag.GroupAccountType = 'E' Then SUM(DrAmt) - SUM(Cramt) Else SUM(CrAmt) - SUM(Dramt) End as OpeningTotal

, 0 as PeriodTotal from AccountTransaction at
Inner join Ledger l On at.GlCode = l.Id
inner join AccountGroup ag on l.AccountGroupId = ag.Id
Where at.VDate < @StartDate and (ag.GroupAccountType='E' OR ag.GroupAccountType='I') and at.FYId=@FYId
Group By l.Id,l.AccountName,l.ShortName,GroupAccountType
Union All
--Within the date
Select ag.GroupAccountType,l.Id,l.AccountName,l.ShortName, 0 as OpeningTotal ,
case when ag.GroupAccountType = 'E' Then SUM(DrAmt) - SUM(Cramt) Else SUM(CrAmt) - SUM(Dramt) End as PeriodTotal
from AccountTransaction at
Inner join Ledger l On at.GlCode = l.Id
inner join AccountGroup ag on l.AccountGroupId = ag.Id
Where at.VDate Between @StartDate AND @EndDate and (ag.GroupAccountType='E' OR ag.GroupAccountType='I') and at.FYId=@FYId
Group By l.Id,l.AccountName,l.ShortName,GroupAccountType
) t Group By t.GroupAccountType
GO
/****** Object:  StoredProcedure [dbo].[SP_PLPeriodicLedgers]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_PLPeriodicLedgers]
@StartDate datetime,
@EndDate datetime,
@AccountGroupId int,
@GroupAccountType char(1),
@FYId int 
as

Select t.Id,t.AccountName,t.ShortName,Sum(t.OpeningTotal) as OpeningTotal, Sum(t.PeriodTotal)  as PeriodTotal ,
(Sum(t.OpeningTotal) + Sum(t.PeriodTotal)) as Closing  from (

Select l.Id,l.AccountName,l.ShortName,ag.GroupAccountType,
case when ag.GroupAccountType = 'E' Then SUM(DrAmt) - SUM(Cramt) Else SUM(CrAmt) - SUM(Dramt) End as OpeningTotal 
, 0 as PeriodTotal from AccountTransaction at
Inner join Ledger l On at.GlCode = l.Id
inner join AccountGroup ag on l.AccountGroupId = ag.Id
Where at.VDate < @StartDate and ag.GroupAccountType=@GroupAccountType and l.AccountGroupId = @AccountGroupId and at.FYId=@FYId
Group By l.Id,l.AccountName,l.ShortName,ag.GroupAccountType
Union All
--Within the date
Select l.Id,l.AccountName,l.ShortName,ag.GroupAccountType, 0 as OpeningTotal ,
case when ag.GroupAccountType = 'E' Then SUM(DrAmt) - SUM(Cramt) Else SUM(CrAmt) - SUM(Dramt) End as PeriodTotal 
from AccountTransaction at
Inner join Ledger l On at.GlCode = l.Id
inner join AccountGroup ag on l.AccountGroupId = ag.Id
Where at.VDate  Between @StartDate And @EndDate and ag.GroupAccountType=@GroupAccountType and l.AccountGroupId = @AccountGroupId and at.FYId=@FYId
Group By l.Id,l.AccountName,l.ShortName,ag.GroupAccountType
) t Group By t.Id,t.AccountName,t.ShortName
GO
/****** Object:  StoredProcedure [dbo].[SP_OutstandingSupplier]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_OutstandingSupplier]
@Ledger_Id int,
@AsOnDate DateTime
as
begin
Create table #OutStandings
(
LedgerId int,
VoucherNo varchar(255),
Source Varchar(5),
VoucherDate datetime,
Amount Decimal(18,2),
Adjusted decimal(18,2),
Balance decimal (18,2),
DueDate datetime ,
DueAge int 
)

Declare @LedgerId int
Declare @VoucherNo varchar(255)
Declare @Source Varchar(5)
Declare @VoucherDate datetime
Declare @Amount Decimal(18,2)
Declare @Adjusted decimal(18,2)
Declare @Balance decimal (18,2)
Declare @DueDate datetime
Declare @DueAge int 

Insert Into #OutStandings 
select GlCode as LedgerId ,VNo as VoucherNo,Source,VDate as VoucherDate,Sum(CrAmt) - Sum(DrAmt) as Amount , 0 as Adjusted,
0 as Balance , VDate as DueDate, 0 as DueAge from AccountTransaction where GlCode = @Ledger_Id and CrAmt <> 0
and VDate <= @AsOnDate
Group By GlCode,VNo,Source,VDate having Sum(CrAmt) <> 0



Declare @TotalBalance Decimal(18,2)
select @TotalBalance = (select Case When l.Category = 'VE' Then SUM(at.DrAmt) Else SUM(at.CrAmt) End as TotalBalance from AccountTransaction at
Inner Join Ledger l on at.GlCode = l.Id 
Where at.GlCode = @Ledger_Id and VDate <= @AsOnDate
Group By l.Category)


Declare @InterestRate Decimal(18,2)
select @InterestRate = Isnull((select Ledger.RateOfInterest from Ledger Where Id = @Ledger_Id),0)

Declare OutStandingCursor Cursor For
select LedgerId ,VoucherNo,Source,VoucherDate,Amount ,Adjusted,
Balance , DueDate, DueAge from #OutStandings where LedgerId = @Ledger_Id

	Open OutStandingCursor
	Fetch Next From OutStandingCursor Into @LedgerId,@VoucherNo,@Source,@VoucherDate,@Amount,@Adjusted,@Balance,@DueDate,@DueAge
	while @@FETCH_STATUS =0
	Begin
		Declare @Days int
		select @Days = (SELECT DATEDIFF(Day,ISNULL(@DueDate,getdate()),@AsOnDate))
		
		if @TotalBalance <> 0
		Begin
			if @Amount <= @TotalBalance
			Begin
				select @Adjusted = @Amount
				select @TotalBalance = @TotalBalance - @Amount
				select @Balance = 0
			End
			else
			Begin
				select @Adjusted = @TotalBalance
				select @TotalBalance = 0
				select @Balance = @Amount - @Adjusted
			End
		End
		else
		Begin
			select @Adjusted = 0
			select @TotalBalance = 0
			select @Balance = @Amount
		End
		
		--Calculate interest where due date is not null , if it is null we dont need to calculate interest amount
			Update #OutStandings Set Balance = @Balance , Adjusted = @Adjusted , DueAge = @Days ,DueDate = Isnull(@DueDate,@VoucherDate) 
			Where LedgerId = @LedgerId and VoucherNo = @VoucherNo and Source = @Source
		Fetch Next From OutStandingCursor Into @LedgerId,@VoucherNo,@Source,@VoucherDate,@Amount,@Adjusted,@Balance,@DueDate,@DueAge
	end
	CLOSE OutStandingCursor
	DEALLOCATE OutStandingCursor




select * From #OutStandings
drop table #OutStandings
end
GO
/****** Object:  StoredProcedure [dbo].[SP_PurchaseFooterTermSummary]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[SP_PurchaseFooterTermSummary]
@StartDate datetime,@EndDate datetime
as
select bt.Id, bt.Description,Sum(td.TermAmount) TermAmount from PurchaseInvoice p
inner join BillingTermDetail td on p.Id = td.ReferenceId
Inner join BillingTerm bt on td.BillingTermId = bt.Id
Where Source ='PB' and p.InvoiceDate Between @StartDate and @EndDate
and td.IsProductWise = 0
Group By bt.Id, bt.Description
GO
/****** Object:  StoredProcedure [dbo].[SP_UnitConversion]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_UnitConversion]
@ProudctId int,@Quantity decimal(18,2), @CurrentUnit nvarchar(100),@ConvertTo nvarchar(100),@Quantity_Converted Decimal(18,2) OUTPUT
as
-- Get Current Unit Configuration First
Declare @Multiplier decimal(18,2)
Declare @BaseUnit nvarchar(100)
Declare @ConvertedValue decimal(18,2)
-- Direct Conversions
set @Multiplier = (Select LowestQuantity from ProductUnitConversion Where LOWER(Unit) = LOWER(@CurrentUnit) and LOWER(LowestUnit) = LOWER(@ConvertTo) and ProductId = @ProudctId)
if ISNULL(@Multiplier,0) <> 0
	Begin
		set @ConvertedValue = @Quantity * @Multiplier
	End
Else
	Begin
		set @Multiplier = (Select LowestQuantity from ProductUnitConversion Where LOWER(Unit) = LOWER(@CurrentUnit) and ProductId = @ProudctId)
		Set @BaseUnit = (Select LowestUnit from ProductUnitConversion Where LOWER(Unit) = LOWER(@CurrentUnit) and ProductId = @ProudctId)

		set @Multiplier = @Multiplier * @Quantity

		Declare @Divisor decimal(18,2)
		set @Divisor = (Select LowestQuantity From ProductUnitConversion Where LOWER(LowestUnit) = LOWER(@BaseUnit) and LOWER(Unit) = LOWER(@ConvertTo) and ProductId = @ProudctId)


		set @ConvertedValue = (@Multiplier / @Divisor)
		if isnull(@ConvertedValue,0) =  0 
			begin
				set @ConvertedValue = @Quantity
			end
	end
	Select @Quantity_Converted = @ConvertedValue
GO
/****** Object:  StoredProcedure [dbo].[SP_TrialBalancePeriodic]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_TrialBalancePeriodic]
@StartDate datetime,
@EndDate Datetime,@FYId int 
as
Create Table #TrialBalance
(
	GlCode varchar(50),
	ShortName Varchar(50),
	AccountName varchar(255),
	OpnDebit decimal(18,2),
	OpnCredit decimal(18,2),
	PeriodDebit decimal(18,2),
	PeriodCredit decimal(18,2),
	Balance decimal(18,2)
	
)
-- Opening Balance
insert into #TrialBalance
Select t.GlCode,t.ShortName,t.AccountName , SUM( t.OpnDebit) as OpnDebit,SUM( t.OpnCredit) as OpnCredit,Sum(t.PeriodDebit) as PeriodDebit,Sum(t.PeriodCredit) as PeriodCredit,0 as Balance From (
select at.GlCode,l.ShortName,l.AccountName,  Sum(Isnull(DrAmt,0)) as OpnDebit ,Sum(isnull(CrAmt,0)) as OpnCredit,0 as PeriodDebit , 0 as PeriodCredit from AccountTransaction at
inner join Ledger l on at.GlCode = l.Id
Where at.VDate < @StartDate and at.FYId = @FYId and at.Source = 'OB'
Group by at.GlCode,l.ShortName,l.AccountName
union all
select at.GlCode,l.ShortName,l.AccountName, Sum(Isnull(DrAmt,0)) as OpnDebit,Sum(isnull(CrAmt,0)) as PeriodCredit,0 as PeriodDebit ,0 as OpnCredit  from AccountTransaction at
inner join Ledger l on at.GlCode = l.Id
Where at.VDate Between  @StartDate and @EndDate and at.FYId = @FYId and at.Source = 'OB' 
Group by at.GlCode,l.ShortName,l.AccountName
) as t Group By t.GlCode,t.ShortName,t.AccountName Order By t.AccountName,t.ShortName

--	Trial  Balance excluding opening balance entries

insert into #TrialBalance
Select t.GlCode,t.ShortName,t.AccountName , SUM( t.OpnDebit) as OpnDebit,SUM( t.OpnCredit) as OpnCredit,Sum(t.PeriodDebit) as PeriodDebit,Sum(t.PeriodCredit) as PeriodCredit,0 as Balance From (
select at.GlCode,l.ShortName,l.AccountName,  Sum(Isnull(DrAmt,0)) as OpnDebit ,Sum(isnull(CrAmt,0)) as OpnCredit,0 as PeriodDebit , 0 as PeriodCredit from AccountTransaction at
inner join Ledger l on at.GlCode = l.Id
Where at.VDate < @StartDate and at.FYId = @FYId and at.Source <> 'OB'
Group by at.GlCode,l.ShortName,l.AccountName
union all
select at.GlCode,l.ShortName,l.AccountName, 0 as OpnDebit,0 as OpnCredit,Sum(Isnull(DrAmt,0)) as PeriodDebit ,Sum(isnull(CrAmt,0)) as PeriodCredit from AccountTransaction at
inner join Ledger l on at.GlCode = l.Id
Where at.VDate Between  @StartDate and @EndDate and at.FYId = @FYId and at.Source <> 'OB' 
Group by at.GlCode,l.ShortName,l.AccountName
) as t Group By t.GlCode,t.ShortName,t.AccountName Order By t.AccountName,t.ShortName


select t.GlCode,t.ShortName,t.AccountName ,Sum(t.OpnDebit) as OpnDebit,Sum(t.OpnCredit) as OpnCredit,Sum(t.PeriodDebit) as PeriodDebit, Sum(t.PeriodCredit) as PeriodCredit,
((SUM(t.OpnDebit) - SUM(t.OpnCredit)) + (SUM(t.PeriodDebit) - SUM(t.PeriodCredit))) as Balance from #TrialBalance t
Group By t.GlCode,t.ShortName,t.AccountName
order By t.ShortName,t.AccountName
Drop table #TrialBalance
GO
/****** Object:  StoredProcedure [dbo].[SP_TrialBalanceLedgerWiseAsOnDate]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_TrialBalanceLedgerWiseAsOnDate]
@AsOnDate datetime,@FYId int 
as
select at.GlCode,l.ShortName,l.AccountName, Sum(Isnull(DrAmt,0)) - Sum(isnull(CrAmt,0)) as DebitCredit from AccountTransaction at
inner join Ledger l on at.GlCode = l.Id
Where at.VDate <= @AsOnDate and at.FYId=@FYId
Group by at.GlCode,l.ShortName,l.AccountName

Order By l.AccountName,l.ShortName
GO
/****** Object:  StoredProcedure [dbo].[SP_TrialBalanceAcGroupWiseAsOnDate]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_TrialBalanceAcGroupWiseAsOnDate]
@AsOnDate datetime
as
select ag.Id,ag.Code,ag.Description, Sum(Isnull(DrAmt,0)) - Sum(isnull(CrAmt,0)) as DebitCredit from AccountTransaction at
inner join Ledger l on at.GlCode = l.Id
Inner Join AccountGroup ag on l.AccountGroupId = ag.Id
Where at.VDate <= @AsOnDate
Group by ag.Id,ag.Code,ag.Description

Order By ag.Description,ag.Code
GO
/****** Object:  StoredProcedure [dbo].[SP_SYNCOPENING]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[SP_SYNCOPENING]
as
Delete from AccountTransaction where Source = 'OB' and ReferenceId Not In ( Select Id From OpeningLedger)
Declare @Id int
Declare @Amount decimal(18,2)
Declare @AmountType int
Declare sync cursor For
Select id,Amount,AmountType From OpeningLedger
open sync
Fetch next From sync into @Id,@Amount,@AmountType
While @@FETCH_STATUS = 0
Begin
	--Select @Id,@Amount,@AmountType
	if @AmountType = 1 
	Begin
	   Update AccountTransaction set DrAmt = @Amount ,CrAmt = 0 where Source = 'OB' and ReferenceId = @Id
	End
	else
	Begin
	   
	   Update AccountTransaction set CrAmt = @Amount ,DrAmt= 0 where Source = 'OB' and ReferenceId = @Id
	End
	Fetch next From sync into @Id,@Amount,@AmountType
ENd							
Close sync
Deallocate sync
GO
/****** Object:  Table [dbo].[StockAdjustmentDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StockAdjustmentDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AdjustmentId] [int] NULL,
	[SNo] [int] NULL,
	[ProductCode] [int] NULL,
	[GodownId] [int] NULL,
	[AdjustmentType] [varchar](5) NULL,
	[AltQty] [decimal](18, 2) NULL,
	[AltUnit] [decimal](18, 2) NULL,
	[Qty] [decimal](18, 2) NULL,
	[Unit] [decimal](18, 2) NULL,
	[AltStockQty] [decimal](18, 2) NULL,
	[StockQty] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NULL,
	[NetAmt] [decimal](18, 2) NULL,
	[Description] [nvarchar](max) NULL,
	[ClassCode] [int] NULL,
	[PhysicalStockNo] [varchar](25) NULL,
	[PhysicalStockSNo] [int] NULL,
	[UnitId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[SP_PurchaseReturnGodownHeader]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[SP_PurchaseReturnGodownHeader]
	@StartDate Date ,@EndDate Date,@GoDownId varchar(max)
as
Select Distinct g.Id,g.Name,g.ShortName from PurchaseReturnDetail d
Inner join PurchaseReturnMaster I On d.PurchaseReturnId = I.Id
Inner Join Godown g On d.Godown = g.Id
Where I.InvoiceDate Between @StartDate And @EndDate
And (@GoDownId='0' OR d.Godown In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@GoDownId, ',')))
Order by g.Name , g.Id
GO
/****** Object:  StoredProcedure [dbo].[SP_PurchaseReturnDetailProducts]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[SP_PurchaseReturnDetailProducts]
	@PurchaseReturnInvoiceId int,@GoDownId Int
as
	select pd.PurchaseReturnId,pd.Id,p.ShortName, p.Name,pd.Qty,u.Code as Unit ,pd.Rate,pd.BasicAmt From PurchaseReturnDetail pd
	inner join Product p on pd.ProductCode = p.Id
	left outer join Unit u on p.Unit = u.Id
	Where pd.PurchaseReturnId = @PurchaseReturnInvoiceId AND (@GoDownId = 0 OR pd.Godown = @GoDownId)
	Order By pd.Id
GO
/****** Object:  StoredProcedure [dbo].[SP_PurchaseReturnDetail]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[SP_PurchaseReturnDetail]
	@StartDate datetime,
	@EndDate datetime,@GoDownId int
as
	select Distinct p.Id,p.InvoiceDate,p.InvoiceNo,p.GlCode,l.AccountName,p.NetAmt from PurchaseReturnMaster p 
	Inner Join Ledger l on p.GlCode = l.Id
	Inner Join PurchaseReturnDetail d on p.Id = d.PurchaseReturnId
	Where p.InvoiceDate Between @StartDate And @EndDate 
	And (@GoDownId = 0 OR d.Godown = @GoDownId)
	Order By p.Id
GO
/****** Object:  StoredProcedure [dbo].[SP_SalesProductTerms]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_SalesProductTerms]
@SalesInvoiceDetailId int
as
select bt.Description,td.Rate,td.TermAmount from SalesDetail sd
inner join BillingTermDetail td on sd.Id = td.DetailId
Inner join BillingTerm bt on td.BillingTermId = bt.Id
Where Source ='SB' and sd.Id = @SalesInvoiceDetailId  and td.IsProductWise = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_SalesGodownHeader]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[SP_SalesGodownHeader]
	@StartDate Date ,@EndDate Date,@GoDownId varchar(max)
as
Select Distinct g.Id,g.Name,g.ShortName from SalesDetail d
Inner join SalesInvoice I On d.SalesInvoiceId = I.Id
Inner Join Godown g On d.Godown = g.Id
Where I.InvoiceDate Between @StartDate And @EndDate
And (@GoDownId='0' OR d.Godown In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@GoDownId, ',')))
Order by g.Name , g.Id
GO
/****** Object:  StoredProcedure [dbo].[SP_SalesFooterTermSummary]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[SP_SalesFooterTermSummary]
@StartDate datetime,@EndDate datetime
as
select bt.Id, bt.Description,Sum(td.TermAmount) TermAmount from SalesInvoice s
inner join BillingTermDetail td on s.Id = td.ReferenceId 
Inner join BillingTerm bt on td.BillingTermId = bt.Id
Where Source ='SB' and s.InvoiceDate Between @StartDate and @EndDate
and td.IsProductWise = 0
Group By bt.Id, bt.Description
GO
/****** Object:  StoredProcedure [dbo].[SP_SalesDetailProducts]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_SalesDetailProducts]
@SalesInvoiceId int,@GoDownId int
as
select sd.SalesInvoiceId,sd.Id,p.ShortName,p.Name,sd.Qty,u.Code as Unit ,sd.Rate,sd.BasicAmt From SalesDetail sd
inner join Product p on sd.ProductCode = p.Id
left outer join Unit u on p.Unit = u.Id
Where sd.SalesInvoiceId= @SalesInvoiceId AND (@GoDownId = 0 OR sd.Godown = @GoDownId)
Order By sd.Id
GO
/****** Object:  StoredProcedure [dbo].[SP_SalesDetail]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_SalesDetail]
@StartDate datetime,
@EndDate datetime,@GoDownId int
as
select Distinct s.Id,s.InvoiceDate,s.InvoiceNo,s.GlCode,l.AccountName,s.NetAmt from SalesInvoice s 
Inner Join Ledger l on s.GlCode = l.Id
Inner Join SalesDetail d on s.Id = d.SalesInvoiceId
Where s.InvoiceDate Between @StartDate And @EndDate
And (@GoDownId = 0 OR d.Godown = @GoDownId)
Order By s.Id
GO
/****** Object:  StoredProcedure [dbo].[Sp_SalesBillTerms]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Sp_SalesBillTerms]
@SalesInvoiceId int
as
select bt.Description,td.Rate,td.TermAmount from SalesInvoice s
inner join BillingTermDetail td on s.Id = td.ReferenceId 
Inner join BillingTerm bt on td.BillingTermId = bt.Id
Where Source ='SB'
And  td.ReferenceId = @SalesInvoiceId and td.IsProductWise = 0
GO
/****** Object:  StoredProcedure [dbo].[SP_ReOrderStatus]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_ReOrderStatus]
@AsOnDate date,@ProductId varchar(max),@FYId int
as
Create Table #ReOrder
(
	Id int Identity(1,1),
	ProductId int,
	ShortName nvarchar(100),
	Description nvarchar(255),
	Unit nvarchar(100),
	ReOrderLevel decimal(18,2),
	ReOrderQty decimal(18,2),
	CurrentStock decimal(18,2),
	MinimumStock Decimal(18,2),
	MaximumStock decimal(18,2)
)

Insert into #ReOrder(ProductId,ShortName,Description,Unit,ReOrderLevel,ReOrderQty,MinimumStock,MaximumStock)
Select p.Id,p.ShortName,p.Name,Unit.Description,p.ReorderLevel,p.ReorderQuantity,p.MinStock,p.MaxStock From Product p
Left Outer Join Unit On p.Id = Unit.Id
Where (@ProductId = '0' OR P.Id In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@ProductId, ',')))
Order by p.Name

Declare @Id int
Declare @Product_Id int
Declare ReOrderCursor Cursor For
Select Id,ProductId From #ReOrder
Open ReOrderCursor
Fetch Next From ReOrderCursor Into @Id,@Product_Id
While @@FETCH_STATUS = 0
Begin
	Declare @CurrentStock decimal(18,2)
	set @CurrentStock = (Select  SUM(Quantity) as CurrentStock From (
	Select Sum(st.AltQty) as Quantity From StockTransaction st Where st.ProductCode = @Product_Id and st.TransactionType = 'I' and st.VDate <= @AsOnDate and st.FYId = @FYId
	Union ALL
	Select Sum(-st.AltQty) as Quantity From StockTransaction st Where st.ProductCode = @Product_Id and st.TransactionType = 'O' and st.VDate <= @AsOnDate and st.FYId = @FYId
	) as Stock )
	Update #ReOrder Set CurrentStock = @CurrentStock Where Id = @Id
	Fetch Next From ReOrderCursor Into @Id,@Product_Id
End
CLOSE ReOrderCursor
DEALLOCATE ReOrderCursor

Select * From #ReOrder
Drop table #ReOrder
GO
/****** Object:  StoredProcedure [dbo].[SP_StocLedgerGodowns]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_StocLedgerGodowns]
	@StartDate date,@EndDate Date,@ProductId varchar(max),@GodownId varchar(max)
as
Select Distinct ST.Godown,G.ShortName,G.Name [Description] From StockTransaction ST
Inner Join Godown G on ST.Godown =G.Id
Where st.VDate Between @StartDate AND @EndDate
AND (@ProductId='0' or st.ProductCode In (SELECT CAST(data as int) as id FROM [splitstring_to_table](@ProductId, ','))) 
AND (isnull(@GodownId,0)='0' or st.Godown In (SELECT CAST(data as int) as id FROM [splitstring_to_table](@GodownId, ',')))
Order By G.Name,G.ShortName
GO
/****** Object:  StoredProcedure [dbo].[SP_StockProductOpening]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  Proc [dbo].[SP_StockProductOpening]
@StartDate DateTime,@ProductId varchar(max),@FYId int
as

Create Table #Opening
(
Id int Identity(1,1),
ProductId Int,
Description nvarchar(255),
Unit nvarchar(100),
Opening decimal(18,2)
)
Insert Into #Opening (ProductId,Description,Opening,Unit)
Select ProductCode, Name,  SUM(Qty) as OpeningQty,Unit From (
	Select ProductCode,p.Name,Sum(st.Quantity) as Qty,p.Unit  from StockTransaction st
	Inner Join Product p on st.ProductCode = p.Id
	Where st.TransactionType = 'I' and (@ProductId='0' or p.Id In (SELECT CAST(data as int) as id FROM [splitstring_to_table](@ProductId, ','))) and st.VDate < @StartDate
	and st.FYId = @FYId
	Group By ProductCode,Name,p.Unit
	Union All
	Select ProductCode,p.Name,Sum(-st.Quantity) as Qty,p.Unit from StockTransaction st
	Inner Join Product p on st.ProductCode = p.Id
	Where st.TransactionType = 'O' and (@ProductId='0' or p.Id In (SELECT CAST(data as int) as id FROM [splitstring_to_table](@ProductId, ','))) and st.VDate < @StartDate
	and st.FYId = @FYId
	Group By ProductCode,Name,p.Unit) as Opening 
Group By ProductCode,Name,Unit order by Name
Select * From #Opening
Drop Table #Opening
GO
/****** Object:  StoredProcedure [dbo].[SP_StockLedgerSummary]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_StockLedgerSummary]
@StartDate DateTime,@EndDate DateTime,@ProductId varchar(max),@FYId int
as
Create Table #StockSummary(
Id int Identity(1,1),
ProductId int,ShortName nvarchar(255),
Description nvarchar(255),Unit nvarchar(100),
Opening decimal(18,2),Received decimal(18,2),
Delivered decimal(18,2),Balance decimal(18,2))
-- Part Of Opening
Insert into #StockSummary (ProductId,ShortName,Description,Unit,Opening)
Select ProductCode, ShortName,Name,Unit,SUM(Qty) as OpeningQty From (
	Select ProductCode,p.ShortName,p.Name,Sum(st.AltQty) as Qty,p.Unit  from StockTransaction st
	Inner Join Product p on st.ProductCode = p.Id
	Where st.TransactionType = 'I' and (@ProductId='0' or p.Id In (SELECT CAST(data as int) as id FROM [splitstring_to_table](@ProductId, ','))) and st.VDate < @StartDate
	and st.FYId = @FYId
	Group By ProductCode,p.ShortName,Name,p.Unit
	Union All
	Select ProductCode,p.ShortName,p.Name,Sum(-st.AltQty) as Qty,p.Unit from StockTransaction st
	Inner Join Product p on st.ProductCode = p.Id
	Where st.TransactionType = 'O' and (@ProductId='0' or p.Id In (SELECT CAST(data as int) as id FROM [splitstring_to_table](@ProductId, ','))) and st.VDate < @StartDate
	and st.FYId = @FYId
	Group By ProductCode,p.ShortName,Name,p.Unit) as Opening 
Group By ProductCode,ShortName,Name,Unit order by Name

-- Part of Delivered and received
Insert into #StockSummary (ProductId,ShortName,Description,Unit,Received,Delivered)
Select ProductCode,ShortName,Name,Unit, SUM(Received) as Received,SUM(Delivered) as Delivered  From (

Select st.ProductCode, P.ShortName,p.Name,p.Unit , Sum(st.AltQty) as Received , 0 as Delivered From StockTransaction st
Inner Join Product P on st.ProductCode = p.Id
Where st.VDate Between @StartDate and @EndDate and TransactionType = 'I'
and (@ProductId = '0' OR st.ProductCode In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@ProductId, ',')))
and st.VDate Between @StartDate and @EndDate and st.FYId = @FYId
Group By st.ProductCode, P.ShortName,p.Name,P.Unit
Union ALL
Select st.ProductCode, P.ShortName,P.Name,P.Unit, 0 as Received, Sum(st.AltQty) as Delivered From StockTransaction st
Inner Join Product P on st.ProductCode = p.Id
Where st.VDate Between @StartDate and @EndDate and TransactionType = 'O'
and (@ProductId = '0' OR st.ProductCode In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@ProductId, ',')))
 AND st.VDate Between @StartDate and @EndDate and st.FYId = @FYId
Group By st.ProductCode, P.ShortName,P.Name,P.Unit
) Stock  Group By ProductCode,ShortName,Name,Unit

Select ProductId,ShortName,#StockSummary.Description,Unit.Code as UnitShortName,
SUM(Isnull(Opening,0)) as Opening,SUM(Isnull(Received,0)) as Received,SUM(Isnull(Delivered,0)) as Delivered,
SUM(Isnull(Opening,0)) + SUM(Isnull(Received,0)) - SUM(Isnull(Delivered,0)) as Balance From #StockSummary
Left Outer Join Unit on #StockSummary.Unit = Unit.Id
Group By ProductId,ShortName,#StockSummary.Description,Unit.Code
Order By #StockSummary.Description,#StockSummary.ShortName

Drop Table #StockSummary
GO
/****** Object:  StoredProcedure [dbo].[SP_StockLedgerProductWise]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_StockLedgerProductWise]
@StartDate DateTime,@EndDate DateTime,@ProductId varchar(max),@FYId int,@GoDownId int
as

Select VDate,ProductCode,AccountName,VNo,Source, SUM(IsNull(Received,0)) as Received,SUM(IsNull(Delivered,0)) as Delivered  From (
	Select VDate,st.ProductCode,L.AccountName, VNo,st.Source, Sum(st.AltQty) as Received , 0 as Delivered From StockTransaction st
	Inner Join Product P on st.ProductCode = p.Id
	Inner Join Ledger L on st.GlCode = L.Id
	Where st.VDate Between @StartDate and @EndDate and TransactionType = 'I'
	and (@ProductId = '0' OR st.ProductCode In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@ProductId, ',')))
	and st.VDate Between @StartDate and @EndDate and st.FYId = @FYId
	AND (@GoDownId = 0 OR st.Godown = @GoDownId)
	Group By VDate,st.ProductCode,L.AccountName, VNo,st.Source
Union ALL
	Select VDate,st.ProductCode,L.AccountName, VNo,st.Source, 0 as Received, Sum(IsNull(st.AltQty,0)) as Delivered From StockTransaction st
	Inner Join Product P on st.ProductCode = p.Id
	Inner Join Ledger L on st.GlCode = L.Id
	Where st.VDate Between @StartDate and @EndDate and TransactionType = 'O'
	and (@ProductId = '0' OR st.ProductCode In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@ProductId, ',')))
	 AND st.VDate Between @StartDate and @EndDate and st.FYId = @FYId
	 AND (@GoDownId = 0 OR st.Godown = @GoDownId)
	Group By VDate,st.ProductCode,L.AccountName, VNo,st.Source
) Stock  Group By VDate,ProductCode,AccountName,VNo,Source
GO
/****** Object:  StoredProcedure [dbo].[SP_StockLedgerProducts]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_StockLedgerProducts]
@StartDate date,@EndDate Date,@ProductId varchar(max),@GodownId varchar(max)
as
Select Distinct P.Id,P.ShortName,P.Name [Description],P.Unit UnitId,u.Code From StockTransaction st
Inner Join Product P on st.ProductCode = P.Id
Left Outer Join Unit u on P.Unit = u.Id
Where st.VDate Between @StartDate AND @EndDate
AND (@ProductId='0' or st.ProductCode In (SELECT CAST(data as int) as id FROM [splitstring_to_table](@ProductId, ','))) 
AND (isnull(@GodownId,0)='0' or st.Godown In (SELECT CAST(data as int) as id FROM [splitstring_to_table](@GodownId, ',')))
Order By P.Name,P.ShortName
GO
/****** Object:  StoredProcedure [dbo].[SP_SalesVatRegister]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_SalesVatRegister]
@StartDate Date,@EndDate Date,@BillingTermId Int
as

Create Table #Vat
(
Id int Identity(1,1),
SalesInvoiceId int,
InvoiceNo nVarchar(100),
InvoiceDate Date,
InvoiceMiti Varchar(10),
AccountName nVarchar(255),
VatNo nVarchar(200),
TotalSale Decimal(18,2),
TaxFreeSale Decimal(18,2),
ZeroRatedSale Decimal(18,2),
TaxableValue  Decimal(18,2),
SaleTax Decimal(18,2)
)
Insert Into #Vat (SalesInvoiceId,InvoiceNo,InvoiceDate,InvoiceMiti,AccountName,VatNo,TotalSale)
Select SI.Id,SI.InvoiceNo, SI.InvoiceDate,SI.InvoiceMiti,L.AccountName,SI.VatNo VatNo,  Sum(sd.BasicAmt) TotalSale From SalesInvoice SI
Inner Join Ledger L on SI.GlCode = L.Id
Inner Join SalesDetail sd on SI.Id = sd.SalesInvoiceId
Where SI.InvoiceDate Between @StartDate And @EndDate
Group By SI.Id,SI.InvoiceNo,SI.InvoiceDate,SI.InvoiceMiti,L.AccountName,SI.VatNo
Order By InvoiceDate
-- Ledger 
-- part of cursor

Declare @Id int
Declare @SalesInvoiceId int
Declare @TaxFreeSale Decimal(18,2)
Declare @TaxableValue Decimal(18,2)
Declare @SaleTax Decimal(18,2)
Declare VatCursor Cursor For
Select Id,SalesInvoiceId From #Vat
Open VatCursor
Fetch Next From VatCursor Into @Id,@SalesInvoiceId
While @@FETCH_STATUS =0
Begin
	--Select @Id,@SalesInvoiceId
	-- tax free sale which are not posted in billing term detail => Sales detail basic Amount
	Set @TaxFreeSale = (Select SUM(SD.BasicAmt) From SalesDetail  SD
	Where SD.Id Not In (Select DetailId From BillingTermDetail Where Source = 'SB') 
	AND SalesInvoiceId = @SalesInvoiceId)
	
	--	Reverse Of Above but pass the billing term id 
	Set @TaxableValue = (Select SUM(SD.BasicAmt) From SalesDetail  SD
	Where SD.Id In (Select DetailId From BillingTermDetail Where Source = 'SB' AND BillingTermId = @BillingTermId) 
	AND SalesInvoiceId = @SalesInvoiceId)
	
	Set @SaleTax = (Select SUM(SD.TermAmt) From SalesDetail  SD
	Where SD.Id In (Select DetailId From BillingTermDetail Where Source = 'SB' AND BillingTermId = @BillingTermId) 
	AND SalesInvoiceId = @SalesInvoiceId)
	
	Update #Vat Set TaxableValue = @TaxableValue,TaxFreeSale = @TaxFreeSale,SaleTax = @SaleTax Where SalesInvoiceId = @SalesInvoiceId AND Id = @Id
	Fetch Next From VatCursor Into @Id,@SalesInvoiceId
End
Close VatCursor
Deallocate VatCursor

---------------------------------------------------

Select Id,InvoiceNo, SalesInvoiceId,InvoiceDate,InvoiceMiti,AccountName,VatNo,
ISNULL(TotalSale,0) TotalSale,ISNULL(TaxFreeSale,0) TaxFreeSale,ISNULL(ZeroRatedSale,0) ZeroRatedSale,ISNULL(TaxableValue,0) TaxableValue,ISNULL(SaleTax,0) SaleTax From #Vat
Drop table #Vat
GO
/****** Object:  StoredProcedure [dbo].[SP_SalesSummary]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_SalesSummary]
@StartDate DateTime,@EndDate DateTime
as
Create Table #Sales
(
InvoiceId int,
InvoiceDate Datetime,
InvoiceNo varchar(255),
LedgerId int,
Customer varchar(255),
BasicAmount Decimal(18,2)
)

Insert into #Sales (InvoiceId,InvoiceDate,InvoiceNo,LedgerId,Customer,BasicAmount)
select si.Id, InvoiceDate,InvoiceNo,l.Id, l.AccountName, Sum(sd.BasicAmt) From SalesInvoice  si 
Inner Join SalesDetail sd on si.Id = sd.SalesInvoiceId
inner Join Ledger l on si.GlCode = l.Id
Where si.InvoiceDate Between @StartDate And @EndDate
Group By si.Id, InvoiceDate,InvoiceNo,l.Id, l.AccountName
declare @Id int
Declare @Description varchar(255)
Declare purchaseterm Cursor For
	select Id,Description from BillingTerm where Type='S'
	Open purchaseterm
	Fetch Next From purchaseterm Into @Id,@Description
	while @@FETCH_STATUS =0
	Begin
		Exec('Alter Table #Sales Add [' + @Description + '] Decimal(18,2)')
		--Part of nasted cursor
		Declare @InvoiceId int
		Declare @TermAmount decimal(18,2)
		Declare SalesRows cursor for
		select InvoiceId from #Sales
		Open SalesRows
		Fetch Next From SalesRows Into @InvoiceId
		while @@FETCH_STATUS =0
		Begin
			select @TermAmount = (select IsNull(Sum(bt.TermAmount),0)  TermAmount from SalesInvoice s
				Inner Join BillingTermDetail bt On s.Id = bt.ReferenceId
				Where Source = 'SB' And ReferenceId = @InvoiceId and bt.BillingTermId = @Id)
				Declare @Sql varchar(max)
				select @Sql = 'Update #Sales Set [' + @Description + '] = ' + cast(@TermAmount as varchar(255)) + ' Where InvoiceId =''' +  cast(@InvoiceId as varchar(255)) + ''''
				exec (@Sql)
			Fetch Next From SalesRows Into @InvoiceId
		end	
		CLOSE SalesRows
		DEALLOCATE SalesRows
		--
		Fetch Next From purchaseterm Into @Id,@Description
	end
CLOSE purchaseterm
DEALLOCATE purchaseterm

Alter Table #Sales Add [TotalAmount] Decimal(18,2)
Declare @SalesId int
Declare UpdateTotalAmt Cursor For
	select InvoiceId from #Sales
	Open UpdateTotalAmt
	Fetch Next From UpdateTotalAmt Into @SalesId
	while @@FETCH_STATUS =0
	Begin
		Declare @TotalAmt decimal(18,2)
		select @TotalAmt = (select NetAmt from SalesInvoice Where SalesInvoice.Id = @SalesId)
		Update #Sales Set TotalAmount = @TotalAmt Where InvoiceId = @SalesId
		Fetch Next From UpdateTotalAmt Into @SalesId
	end
CLOSE UpdateTotalAmt
DEALLOCATE UpdateTotalAmt

select * from #Sales
drop table #Sales
GO
/****** Object:  Table [dbo].[StockTransferMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StockTransferMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[STNo] [varchar](20) NULL,
	[STDate] [date] NULL,
	[STMiti] [varchar](10) NULL,
	[GodownId] [int] NULL,
	[Remarks] [nvarchar](max) NULL,
	[BarCode] [nvarchar](10) NULL,
	[Posting] [bit] NULL,
	[Export] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[DocId] [int] NULL,
	[FYId] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StockTransferDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StockTransferDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StockTransferMasterId] [int] NULL,
	[SNo] [int] NULL,
	[Godown] [int] NULL,
	[AltQty] [decimal](18, 2) NULL,
	[AltUnit] [varchar](10) NULL,
	[Qty] [decimal](18, 2) NULL,
	[UnitId] [int] NULL,
	[AltStockQty] [decimal](18, 2) NULL,
	[StockQty] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NULL,
	[NetAmt] [decimal](18, 2) NULL,
	[ProductCode] [int] NULL,
 CONSTRAINT [PK__StockTra__3214EC071AD3FDA4] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[SP_SalesReturnGodownHeader]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[SP_SalesReturnGodownHeader]
	@StartDate Date ,@EndDate Date,@GoDownId varchar(max)
as
Select Distinct g.Id,g.Name,g.ShortName from SalesReturnDetail d
Inner join SalesReturnMaster I On d.SalesReturnId = I.Id
Inner Join Godown g On d.Godown = g.Id
Where I.InvoiceDate Between @StartDate And @EndDate
And (@GoDownId='0' OR d.Godown In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@GoDownId, ',')))
Order by g.Name , g.Id
GO
/****** Object:  StoredProcedure [dbo].[SP_SalesReturnDetailProducts]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[SP_SalesReturnDetailProducts]
	@SalesReturnInvoiceId int,@GoDownId int
as
select sd.SalesReturnId,sd.Id,p.ShortName,p.Name,sd.Qty,u.Code as Unit ,sd.Rate,sd.BasicAmt From SalesReturnDetail sd
inner join Product p on sd.ProductCode = p.Id
left outer join Unit u on p.Unit = u.Id
Where sd.SalesReturnId= @SalesReturnInvoiceId AND (@GoDownId = 0 OR sd.Godown = @GoDownId)
Order By sd.Id
GO
/****** Object:  StoredProcedure [dbo].[SP_SalesReturnDetail]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[SP_SalesReturnDetail]
	@StartDate datetime,
	@EndDate datetime,@GoDownId int
as
select Distinct s.Id,s.InvoiceDate,s.InvoiceNo,s.GlCode,l.AccountName,s.NetAmt from SalesReturnMaster s 
Inner Join Ledger l on s.GlCode = l.Id
Inner Join SalesReturnDetail d on s.Id = d.SalesReturnId
Where s.InvoiceDate Between @StartDate And @EndDate
And (@GoDownId = 0 OR d.Godown = @GoDownId)
Order By s.Id
GO
/****** Object:  StoredProcedure [dbo].[SP_PurhaseProductTerms]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_PurhaseProductTerms]
@PurchaseInvoiceDetailId int
as
select bt.Description,td.Rate,td.TermAmount from PurchaseDetail pd
inner join BillingTermDetail td on pd.Id = td.DetailId
Inner join BillingTerm bt on td.BillingTermId = bt.Id
Where Source ='PB' and pd.Id = @PurchaseInvoiceDetailId and td.IsProductWise = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_PurchaseVatRegister]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_PurchaseVatRegister]
@StartDate Date,@EndDate Date,@BillingTermId Int
as

Create Table #Vat
(
Id int Identity(1,1),
PurchaseInvoiceId int,
InvoiceNo nVarchar(100),
InvoiceDate Date,
InvoiceMiti Varchar(10),
AccountName nVarchar(255),
VatNo nVarchar(200),
TotalPurchase Decimal(18,2),
TaxFreePurchase Decimal(18,2),
ZeroRatedPurchase Decimal(18,2),
TaxableValue  Decimal(18,2),
PurchaseTax Decimal(18,2)
)
Insert Into #Vat (PurchaseInvoiceId,InvoiceNo,InvoiceDate,InvoiceMiti,AccountName,VatNo,TotalPurchase)
Select P.Id,P.InvoiceNo, P.InvoiceDate,P.InvoiceMiti,L.AccountName,'' VatNo,  Sum(pd.BasicAmt) TotalSale From PurchaseInvoice P
Inner Join Ledger L on P.GlCode = L.Id
Inner Join PurchaseDetail pd on P.Id = pd.PurchaseInvoiceId
Where P.InvoiceDate Between @StartDate And @EndDate
Group By P.Id,P.InvoiceNo,P.InvoiceDate,P.InvoiceMiti,L.AccountName
Order By InvoiceDate
-- Ledger 
-- part of cursor

Declare @Id int
Declare @PurchaseInvoiceId int
Declare @TaxFreePurchase Decimal(18,2)
Declare @TaxableValue Decimal(18,2)
Declare @PurchaseTax Decimal(18,2)
Declare VatCursor Cursor For
Select Id,PurchaseInvoiceId From #Vat
Open VatCursor
Fetch Next From VatCursor Into @Id,@PurchaseInvoiceId
While @@FETCH_STATUS =0
Begin
	--Select @Id,@SalesInvoiceId
	-- tax free sale which are not posted in billing term detail => Sales detail basic Amount
	Set @TaxFreePurchase = (Select SUM(PD.BasicAmt) From PurchaseDetail  PD
	Where PD.Id Not In (Select DetailId From BillingTermDetail Where Source = 'PB') 
	AND PurchaseInvoiceId = @PurchaseInvoiceId)
	
	--	Reverse Of Above but pass the billing term id 
	Set @TaxableValue = (Select SUM(PD.BasicAmt) From PurchaseDetail  PD
	Where PD.Id In (Select DetailId From BillingTermDetail Where Source = 'PB' AND BillingTermId = @BillingTermId) 
	AND PurchaseInvoiceId = @PurchaseInvoiceId)
	
	Set @PurchaseTax = (Select SUM(PD.TermAmt) From PurchaseDetail  PD
	Where PD.Id In (Select DetailId From BillingTermDetail Where Source = 'PB' AND BillingTermId = @BillingTermId) 
	AND PurchaseInvoiceId = @PurchaseInvoiceId)
	
	Update #Vat Set TaxableValue = @TaxableValue,TaxFreePurchase = @TaxFreePurchase,PurchaseTax = @PurchaseTax Where PurchaseInvoiceId = @PurchaseInvoiceId AND Id = @Id
	Fetch Next From VatCursor Into @Id,@PurchaseInvoiceId
End
Close VatCursor
Deallocate VatCursor

---------------------------------------------------

Select Id,InvoiceNo, PurchaseInvoiceId,InvoiceDate,InvoiceMiti,AccountName,VatNo,
ISNULL(TotalPurchase,0) TotalPurchase,ISNULL(TaxFreePurchase,0) TaxFreePurchase,ISNULL(ZeroRatedPurchase,0) ZeroRatedPurchase,ISNULL(TaxableValue,0) TaxableValue,ISNULL(PurchaseTax,0) PurchaseTax From #Vat
Drop table #Vat
GO
/****** Object:  StoredProcedure [dbo].[SP_PurchaseSummary]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_PurchaseSummary]
@StartDate DateTime,@EndDate DateTime
as
Create Table #Purchase
(
InvoiceId int,
InvoiceDate Datetime,
InvoiceNo varchar(255),
LedgerId int,
Customer varchar(255),
BasicAmount Decimal(18,2)
)

Insert into #Purchase (InvoiceId,InvoiceDate,InvoiceNo,LedgerId,Customer,BasicAmount)
select p.Id, InvoiceDate,InvoiceNo,l.Id, l.AccountName, Sum(pd.BasicAmt) as BasicAmt From PurchaseInvoice  p 
Inner join PurchaseDetail pd on p.Id = pd.PurchaseInvoiceId
inner Join Ledger l on p.GlCode = l.Id
Where p.InvoiceDate Between @StartDate And @EndDate
Group By p.Id, InvoiceDate,InvoiceNo,l.Id, l.AccountName
declare @Id int
Declare @Description varchar(255)
Declare purchaseterm Cursor For
	select Id,Description from BillingTerm where Type='P'
	Open purchaseterm
	Fetch Next From purchaseterm Into @Id,@Description
	while @@FETCH_STATUS =0
	Begin
		Exec('Alter Table #Purchase Add [' + @Description + '] Decimal(18,2)')
		--Part of nasted cursor
		Declare @InvoiceId int
		Declare @TermAmount decimal(18,2)
		Declare PurchaseRows cursor for
		select InvoiceId from #Purchase
		Open PurchaseRows
		Fetch Next From PurchaseRows Into @InvoiceId
		while @@FETCH_STATUS =0
		Begin
			select @TermAmount = (select IsNull(Sum(bt.TermAmount),0)  TermAmount from PurchaseInvoice p
				Inner Join BillingTermDetail bt On p.Id = bt.ReferenceId
				Where Source = 'PB' And ReferenceId = @InvoiceId and bt.BillingTermId = @Id)
				Declare @Sql varchar(max)
				select @Sql = 'Update #Purchase Set [' + @Description + '] = ' + cast(@TermAmount as varchar(255)) + ' Where InvoiceId =''' +  cast(@InvoiceId as varchar(255)) + ''''
				exec (@Sql)
			Fetch Next From PurchaseRows Into @InvoiceId
		end	
		CLOSE PurchaseRows
		DEALLOCATE PurchaseRows
		--
		Fetch Next From purchaseterm Into @Id,@Description
	end
CLOSE purchaseterm
DEALLOCATE purchaseterm

Alter Table #Purchase Add [TotalAmount] Decimal(18,2)
Declare @PurchaseId int
Declare UpdateTotalAmt Cursor For
	select InvoiceId from #Purchase
	Open UpdateTotalAmt
	Fetch Next From UpdateTotalAmt Into @PurchaseId
	while @@FETCH_STATUS =0
	Begin
		Declare @TotalAmt decimal(18,2)
		select @TotalAmt = (select NetAmt from PurchaseInvoice Where PurchaseInvoice.Id = @PurchaseId)
		Update #Purchase Set TotalAmount = @TotalAmt Where InvoiceId = @PurchaseId
		Fetch Next From UpdateTotalAmt Into @PurchaseId
	end
CLOSE UpdateTotalAmt
DEALLOCATE UpdateTotalAmt

select * from #Purchase
drop table #Purchase
GO
/****** Object:  StoredProcedure [dbo].[SP_PurchaseProductTerms]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Proc [dbo].[SP_PurchaseProductTerms]
@PurchaseInvoiceDetailId int
as
select bt.Description,td.Rate,td.TermAmount from PurchaseDetail pd
inner join BillingTermDetail td on pd.Id = td.DetailId
Inner join BillingTerm bt on td.BillingTermId = bt.Id
Where Source ='PB' and pd.Id = @PurchaseInvoiceDetailId and td.IsProductWise = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_PurchaseGodownHeader]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[SP_PurchaseGodownHeader]
	@StartDate Date ,@EndDate Date,@GoDownId varchar(max)
as
Select Distinct g.Id,g.Name,g.ShortName from PurchaseDetail d
Inner join PurchaseInvoice I On d.PurchaseInvoiceId = I.Id
Inner Join Godown g On d.Godown = g.Id
Where I.InvoiceDate Between @StartDate And @EndDate
And (@GoDownId='0' OR d.Godown In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@GoDownId, ',')))
Order by g.Name , g.Id
GO
/****** Object:  StoredProcedure [dbo].[SP_PurchaseDetailProducts]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_PurchaseDetailProducts]
@PurchaseInvoiceId int,@GoDownId Int
as
select pd.PurchaseInvoiceId,pd.Id,p.ShortName, p.Name,pd.Qty,u.Code as Unit ,pd.Rate,pd.BasicAmt From PurchaseDetail pd
inner join Product p on pd.PCode = p.Id
left outer join Unit u on p.Unit = u.Id
Where pd.PurchaseInvoiceId = @PurchaseInvoiceId AND (@GoDownId = 0 OR pd.Godown = @GoDownId)
Order By pd.Id
GO
/****** Object:  StoredProcedure [dbo].[SP_PurchaseDetail]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_PurchaseDetail]
@StartDate datetime,
@EndDate datetime,@GoDownId int
as
select Distinct p.Id,p.InvoiceDate,p.InvoiceNo,p.GlCode,l.AccountName,p.NetAmt from PurchaseInvoice p 
Inner Join Ledger l on p.GlCode = l.Id
Inner Join PurchaseDetail d on p.Id = d.PurchaseInvoiceId
Where p.InvoiceDate Between @StartDate And @EndDate 
And (@GoDownId = 0 OR d.Godown = @GoDownId)
Order By p.Id
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStock]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Select * From stocktransaction Where ProductCode = 19
--Select * From Product Where Id = 19

--SP_GetStock @ProductId=19 ,@BatchSerial='' ,@BranchId =0,@FYId = 0
CREATE Proc [dbo].[SP_GetStock] 
@ProductId Int,@BatchSerial nvarchar(512),@BranchId Int,@FYId int = 0
as

Declare @TransactionType nvarchar(5)
Declare @Quantity decimal(18,2)
Declare @CurrentUnit nvarchar(255)
Declare @BaseUnit nvarchar(255)
Declare @Quantity_Converted decimal(18,2)
Declare @Stock Decimal(18,2)
set @Stock = 0

Declare Transactions Cursor For
	Select st.TransactionType, Sum(st.Quantity) Quantity,st.Unit CurrentUnit,U.Code BaseUnit From StockTransaction st
	Inner Join Product p on st.ProductCode = p.Id
	Inner Join Unit U on p.Unit = U.Id
	Where st.ProductCode = @ProductId 
	AND (Isnull(@BatchSerial,'') = '' OR st.BatchSerialNo = @BatchSerial)
	AND (@BranchId = 0 OR st.BranchId = @BranchId)
	AND (@FYId = 0 OR st.FYId = @FYId)
	Group By st.ProductCode,st.TransactionType,st.Unit,U.Code
Open Transactions
Fetch Next From Transactions Into @TransactionType,@Quantity,@CurrentUnit,@BaseUnit
While @@FETCH_STATUS = 0
begin
	set @Quantity_Converted = 0
	EXEC SP_UnitConversion @ProductId, @Quantity, @CurrentUnit, @BaseUnit, @Quantity_Converted OUTPUT
	if @TransactionType = 'I'
	Begin
		set @Stock += @Quantity_Converted
	End
	else
	Begin
		set @Stock -= @Quantity_Converted
	End
	--Select @Quantity_Converted Qty , @BaseUnit BaseUnit ,@Quantity ,@CurrentUnit CurrentUnit
	Fetch Next From Transactions Into @TransactionType,@Quantity,@CurrentUnit,@BaseUnit
End
CLOSE Transactions
DEALLOCATE Transactions
Select @Stock
GO
/****** Object:  Table [dbo].[ScSubject]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScSubject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](255) NOT NULL,
	[Code] [varchar](255) NULL,
	[ResultSystem] [int] NOT NULL,
	[ClassType] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[EvaluateForType] [int] NOT NULL,
	[Schedule] [int] NOT NULL,
	[Memo] [varchar](255) NULL,
 CONSTRAINT [PK_ScSubject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MaterialReturn]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MaterialReturn](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[IssueDate] [date] NULL,
	[IssueMiti] [varchar](10) NULL,
	[ReturnDate] [date] NULL,
	[ReturnMiti] [varchar](10) NULL,
	[MaterialIssueId] [int] NULL,
	[CostCenterId] [int] NULL,
	[BranchId] [int] NULL,
	[CreatedById] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK__Material__3214EC0758671BC9] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MaterialIssueDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialIssueDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaterialIssueId] [int] NULL,
	[RawMaterialId] [int] NULL,
	[UnitId] [int] NULL,
	[Quantity] [decimal](18, 2) NULL,
	[GodownId] [int] NULL,
	[CostCenterId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GodownTransferDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GodownTransferDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransferId] [int] NULL,
	[ProductId] [int] NULL,
	[UnitId] [int] NULL,
	[Qty] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillOfMaterialDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillOfMaterialDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BillOfMaterialId] [int] NULL,
	[RawMaterialId] [int] NULL,
	[CostCenterId] [int] NULL,
	[GodownId] [int] NULL,
	[UnitId] [int] NULL,
	[Quantity] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FinishedGoodReturn]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FinishedGoodReturn](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[FinishedGoodReceiveId] [int] NULL,
	[ReturnDate] [date] NULL,
	[ReturnMiti] [varchar](10) NULL,
	[GoDownId] [int] NULL,
	[CreatedById] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedById] [int] NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FinishedGoodReceiveDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinishedGoodReceiveDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FinishedGoodReceiveId] [int] NULL,
	[FinishGoodId] [int] NULL,
	[Qty] [decimal](18, 2) NULL,
	[UnitId] [int] NULL,
	[Rate] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScNationality]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScNationality](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nationality] [varchar](250) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScLibraryMemberRegistration]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScLibraryMemberRegistration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](max) NULL,
	[ClassId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[StaffId] [int] NOT NULL,
	[RegNo] [nvarchar](100) NOT NULL,
	[Narration] [varchar](250) NULL,
	[Remarks] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreateById] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdateById] [int] NULL,
	[AcademyYearId] [int] NOT NULL,
 CONSTRAINT [PK__ScLibrar__3214EC077720AD13] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScHouseGroup]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScHouseGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[Remarks] [varchar](max) NULL,
 CONSTRAINT [PK_ScHouseGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScMaterialIssueMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScMaterialIssueMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VoucherNo] [nvarchar](50) NULL,
	[VoucherDate] [datetime] NULL,
	[VoucherMiti] [nvarchar](50) NULL,
	[Remarks] [nvarchar](100) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_MaterialIssue_Master] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScStudentCategory]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScStudentCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](max) NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[Memo] [varchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
 CONSTRAINT [PK_ScStudentCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ__ScCatego__9D2327C63AA1AEB8] UNIQUE NONCLUSTERED 
(
	[Description] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScReligion]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScReligion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Religion] [varchar](250) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScRoom]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScRoom](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](max) NOT NULL,
	[Seats] [int] NOT NULL,
	[Memo] [varchar](max) NULL,
	[Description] [varchar](max) NOT NULL,
 CONSTRAINT [PK_ClassRoom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScSection]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScSection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](255) NOT NULL,
	[Memo] [varchar](255) NULL,
 CONSTRAINT [PK_ScSection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScShift]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScShift](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](max) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Memo] [varchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DispalyOrder] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScFeeItem]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScFeeItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[GLCode] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[Schedule] [int] NOT NULL,
	[Memo] [varchar](500) NULL,
	[Code] [varchar](50) NULL,
	[EducationTax] [bit] NOT NULL,
 CONSTRAINT [PK_ScFeeItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScExtraActivity]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScExtraActivity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Memo] [varchar](max) NULL,
	[Code] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ScExtraActivity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SchClass]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchClass](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Schedule] [int] NOT NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[MaxStudent] [int] NOT NULL,
	[MinStudent] [int] NOT NULL,
	[Incharge] [nvarchar](100) NULL,
	[ProgramId] [int] NOT NULL,
 CONSTRAINT [PK_SchClass] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScFeeTerm]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScFeeTerm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[GLCode] [int] NOT NULL,
	[Category] [int] NOT NULL,
	[Rounded] [int] NOT NULL,
	[Sign] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[Rate] [decimal](18, 2) NOT NULL,
	[Formula] [varchar](max) NULL,
	[SupressZero] [bit] NOT NULL,
	[Profitablity] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DispalyOrder] [int] NOT NULL,
 CONSTRAINT [PK_ScFeeTerm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScGrade]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScGrade](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](500) NOT NULL,
	[Grade] [nvarchar](50) NULL,
	[Memo] [varchar](500) NULL,
 CONSTRAINT [PK_ScGrade] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SchBuilding]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchBuilding](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK_SchBuilding] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScBoader]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScBoader](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](max) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[FeeItemId] [int] NOT NULL,
	[Memo] [varchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DispalyOrder] [int] NOT NULL,
 CONSTRAINT [PK_ScBoader] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScBook]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScBook](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[ShortName] [varchar](25) NULL,
	[VolumeNumber] [varchar](50) NULL,
	[RackNo] [varchar](50) NULL,
	[BookNumber] [varchar](50) NULL,
	[Language] [varchar](50) NULL,
	[MFNNumber] [varchar](50) NULL,
	[Keywords] [varchar](50) NULL,
	[YearOfPublication] [datetime] NULL,
	[MitiOfPublication] [nvarchar](50) NULL,
	[Edition] [varchar](50) NULL,
	[ImprintPlace] [nvarchar](100) NULL,
	[Publisher] [varchar](200) NULL,
	[CorporateBody] [varchar](200) NULL,
	[PersonalAuthor] [varchar](200) NULL,
	[Pages] [nvarchar](50) NULL,
	[Series] [varchar](200) NULL,
	[BoardSubHeading] [varchar](200) NULL,
	[ISBN] [varchar](100) NULL,
	[ISSN] [varchar](100) NULL,
	[Source] [varchar](200) NULL,
	[LibraryLocation] [varchar](200) NULL,
	[TypeOfMaterial] [varchar](200) NULL,
	[Price] [decimal](18, 2) NULL,
	[Notes] [varchar](200) NULL,
	[CreateById] [int] NULL,
	[UpdateById] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[RowNo] [varchar](50) NULL,
	[IsIssuable] [bit] NULL,
 CONSTRAINT [PK_ScBook] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScDivision]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScDivision](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](550) NOT NULL,
	[PercentageFrom] [numeric](6, 2) NULL,
	[PercentageTo] [numeric](6, 2) NULL,
	[Memo] [varchar](550) NULL,
	[Schedule] [int] NULL,
 CONSTRAINT [PK_ScDivision] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScEmployeeCategory]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScEmployeeCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[Remarks] [varchar](max) NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_ScEmployeeCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScExam]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScExam](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](max) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[StartDate] [datetime] NULL,
	[StartMiti] [varchar](50) NULL,
	[EndDate] [datetime] NULL,
	[EndMiti] [varchar](50) NULL,
	[Type] [int] NOT NULL,
	[Schedule] [int] NOT NULL,
	[Memo] [varchar](max) NULL,
	[IsFinal] [bit] NOT NULL,
 CONSTRAINT [PK_ScExam] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScClassRoomMapping]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScClassRoomMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
	[RoomMappingId] [int] NOT NULL,
 CONSTRAINT [PK_ScClassRoomMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScBusStop]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScBusStop](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Memo] [varchar](500) NULL,
 CONSTRAINT [PK_ScBusStop] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScBus]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScBus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[VehicleNo] [varchar](25) NULL,
	[MaxCapicity] [int] NULL,
	[DriverName] [varchar](25) NULL,
	[DriverLicense] [varchar](25) NULL,
	[HelperName] [varchar](25) NULL,
	[ContactOff] [varchar](25) NULL,
	[ContactPhNo] [varchar](25) NULL,
	[ContactPerson] [varchar](25) NULL,
	[ContactPersonPhNo] [varchar](25) NULL,
	[FeeMemo] [varchar](500) NULL,
 CONSTRAINT [PK_ScBus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScBookReceived]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScBookReceived](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [varchar](100) NULL,
	[Date] [datetime] NULL,
	[Miti] [nvarchar](50) NULL,
	[BookId] [int] NULL,
	[Quantity] [int] NULL,
	[Remarks] [varchar](max) NULL,
	[CreatedById] [int] NULL,
	[UpdatedById] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK__ScBookRe__3214EC0727F8EE98] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [ScBook_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScBook_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScBook]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScBook'
                       END
GO
/****** Object:  Table [dbo].[ScBusRouteDetails]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScBusRouteDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BusRouteDescription] [varchar](50) NOT NULL,
	[ApplicableDate] [datetime] NULL,
	[ApplicableMiti] [varchar](10) NULL,
	[BusId] [int] NOT NULL,
	[Fuel] [varchar](20) NULL,
	[BusStopId] [int] NOT NULL,
	[Picktime] [nvarchar](50) NULL,
	[Droptime] [nvarchar](50) NULL,
	[AMOUNT] [decimal](16, 4) NOT NULL,
	[Narr] [varchar](255) NULL,
	[Remarks] [varchar](500) NULL,
 CONSTRAINT [PK_ScBusRouteDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [ScBus_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScBus_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScBus]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScBus'
                       END
GO
/****** Object:  Table [dbo].[ScClassSubjectMapping]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScClassSubjectMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[SubjectType] [int] NULL,
	[ClassType] [int] NULL,
	[ResultSystem] [int] NULL,
	[EvaluateForType] [int] NULL,
	[Narration] [varchar](255) NULL,
 CONSTRAINT [PK_ScClassSubjectMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [ScClassRoomMapping_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScClassRoomMapping_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScClassRoomMapping]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScClassRoomMapping'
                       END
GO
/****** Object:  Table [dbo].[ScClassFeeRate]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScClassFeeRate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[FeeItemId] [int] NOT NULL,
	[FeeRate] [decimal](18, 2) NOT NULL,
	[BoaderId] [int] NOT NULL,
	[ShiftId] [int] NOT NULL,
	[Narr] [varchar](500) NULL,
	[Remarks] [varchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[AcademyYearId] [int] NOT NULL,
 CONSTRAINT [PK_ScClassFeeRate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [ScBusStop_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScBusStop_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScBusStop]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScBusStop'
                       END
GO
/****** Object:  Table [dbo].[ScEmployeePost]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScEmployeePost](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[EmployeeCategoryId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_ScEmployeePost] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Trigger [ScDivision_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScDivision_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScDivision]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScDivision'
                       END
GO
/****** Object:  Table [dbo].[ScBoaderMapping]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScBoaderMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
	[BoaderId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[Status] [int] NULL,
	[StartDate] [date] NULL,
	[StartMiti] [varchar](10) NULL,
	[EndDate] [date] NULL,
	[EndMiti] [varchar](10) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[Narration] [varchar](255) NULL,
	[AcademicYearId] [int] NULL,
	[Remarks] [varchar](512) NULL,
 CONSTRAINT [PK_ScBoaderMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [ScBoader_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScBoader_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScBoader]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScBoader'
                       END
GO
/****** Object:  Trigger [ScGrade_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScGrade_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScGrade]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScGrade'
                       END
GO
/****** Object:  Table [dbo].[ScFineScheme]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScFineScheme](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ClassId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[CreatedById] [int] NOT NULL,
 CONSTRAINT [PK_ScFineScheme] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Trigger [ScFeeTerm_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScFeeTerm_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScFeeTerm]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScFeeTerm'
                       END
GO
/****** Object:  Table [dbo].[ScExamAttendanceMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScExamAttendanceMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
	[ExamId] [int] NOT NULL,
	[EntryDate] [date] NOT NULL,
	[EntryMiti] [varchar](10) NULL,
	[TotalDays] [int] NOT NULL,
	[AcademicYearId] [int] NULL,
	[UpdatedById] [int] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ScExamAttendanceMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [ScExtraActivity_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScExtraActivity_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScExtraActivity]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScExtraActivity'
                       END
GO
/****** Object:  Trigger [ScFeeItem_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScFeeItem_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScFeeItem]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScFeeItem'
                       END
GO
/****** Object:  Trigger [ScExam_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScExam_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScExam]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScExam'
                       END
GO
/****** Object:  Table [dbo].[ScExamRoutine]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScExamRoutine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExamId] [int] NOT NULL,
	[ClassId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[ResultSystem] [int] NULL,
	[AcademicYearId] [int] NULL,
	[EvaluateForType] [int] NULL,
	[ExamType] [int] NULL,
	[ExamDate] [datetime] NULL,
	[ExamMiti] [varchar](50) NULL,
	[StartTime] [varchar](50) NULL,
	[EndTime] [varchar](50) NULL,
	[ExamHour] [nvarchar](50) NULL,
	[Remarks] [nvarchar](max) NULL,
 CONSTRAINT [PK_ScExamRoutine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScExamMarkSetup]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScExamMarkSetup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExamId] [int] NOT NULL,
	[ClassId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[ResultSystem] [int] NOT NULL,
	[EvaluateForType] [int] NOT NULL,
	[PracticalFullMark] [decimal](16, 2) NOT NULL,
	[PracticalPassMark] [decimal](16, 2) NOT NULL,
	[TheoryFullMark] [decimal](16, 2) NOT NULL,
	[TheoryPassMark] [decimal](16, 2) NOT NULL,
	[Narration] [varchar](500) NULL,
	[SubjectType] [int] NOT NULL,
	[ClassType] [int] NOT NULL,
 CONSTRAINT [PK_ScExamMarkSetup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [ScShift_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScShift_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScShift]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScShift'
                       END
GO
/****** Object:  Trigger [ScSection_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScSection_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScSection]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScSection'
                       END
GO
/****** Object:  Trigger [ScRoom_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScRoom_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScRoom]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScRoom'
                       END
GO
/****** Object:  Trigger [ScReligion_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScReligion_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScReligion]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScReligion'
                       END
GO
/****** Object:  Trigger [ScStaffGroup_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScStaffGroup_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScEmployeeCategory]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScStaffGroup'
                       END
GO
/****** Object:  Trigger [ScHouseGroup_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScHouseGroup_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScHouseGroup]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScHouseGroup'
                       END
GO
/****** Object:  Table [dbo].[ScStudentExtraActivity]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScStudentExtraActivity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
	[ActivityId] [int] NOT NULL,
	[AcademicYearId] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateById] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreateById] [int] NOT NULL,
	[Remarks] [nvarchar](max) NULL,
 CONSTRAINT [PK__ScStuden__3214EC0700DF2177] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Trigger [ScStudentCategory_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScStudentCategory_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScStudentCategory]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScStudentCategory'
                       END
GO
/****** Object:  Table [dbo].[ScMaterialIssueDetails]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScMaterialIssueDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VoucherNo] [nvarchar](50) NULL,
	[MaterialIssueMasterId] [int] NULL,
	[ProductId] [int] NOT NULL,
	[StaffId] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NULL,
	[LocalAmount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_MaterialIssue_Details] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SchBuildingRoomMapping]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchBuildingRoomMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SchBuildingClassMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Trigger [SchBuilding_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[SchBuilding_AspNet_SqlCacheNotification_Trigger] ON [dbo].[SchBuilding]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'SchBuilding'
                       END
GO
/****** Object:  Trigger [SchClass_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[SchClass_AspNet_SqlCacheNotification_Trigger] ON [dbo].[SchClass]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'SchClass'
                       END
GO
/****** Object:  Trigger [ScLibraryMemberRegistration_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScLibraryMemberRegistration_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScLibraryMemberRegistration]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScLibraryMemberRegistration'
                       END
GO
/****** Object:  Table [dbo].[ScPrePaidScheme]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScPrePaidScheme](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ClassId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[CreatedById] [int] NOT NULL,
 CONSTRAINT [PK_ScPrePaidScheme] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Trigger [ScNationality_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScNationality_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScNationality]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScNationality'
                       END
GO
/****** Object:  Table [dbo].[FinishedGoodReturnDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinishedGoodReturnDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FinishedGoodReturnId] [int] NULL,
	[FinishGoodId] [int] NULL,
	[Qty] [decimal](18, 2) NULL,
	[UnitId] [int] NULL,
	[Rate] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialReturnDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialReturnDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaterialReturnId] [int] NULL,
	[RawMaterialId] [int] NULL,
	[UnitId] [int] NULL,
	[Quantity] [decimal](18, 2) NULL,
	[GodownId] [int] NULL,
	[CostCenterId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Trigger [MaterialIssue_Master_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[MaterialIssue_Master_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScMaterialIssueMaster]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'MaterialIssue_Master'
                       END
GO
/****** Object:  Table [dbo].[ScStudentinfo]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScStudentinfo](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[StuDesc] [varchar](250) NULL,
	[Nationality] [varchar](25) NULL,
	[Religion] [varchar](25) NULL,
	[BloodGrp] [varchar](10) NULL,
	[MaritialSt] [varchar](20) NULL,
	[Sex] [varchar](10) NULL,
	[DBO] [datetime] NULL,
	[DBOMiti] [varchar](50) NULL,
	[ApplyDate] [datetime] NULL,
	[ApplyMiti] [varchar](50) NULL,
	[EntryDate] [datetime] NULL,
	[EntryMiti] [varchar](50) NULL,
	[Regno] [varchar](250) NULL,
	[EmailId] [varchar](250) NULL,
	[Phone] [varchar](250) NULL,
	[PrevClsCode] [varchar](250) NULL,
	[Institue] [varchar](250) NULL,
	[PassYear] [datetime] NULL,
	[MPercent] [decimal](18, 6) NULL,
	[CurrClsCode] [varchar](250) NULL,
	[GName] [varchar](250) NULL,
	[GRelation] [varchar](250) NULL,
	[GProff] [varchar](250) NULL,
	[GOff] [varchar](500) NULL,
	[GOffAdd] [varchar](500) NULL,
	[GEmail] [varchar](500) NULL,
	[GMobile] [varchar](250) NULL,
	[GPhoneOff] [varchar](250) NULL,
	[GPhoneRes] [varchar](250) NULL,
	[FName] [varchar](250) NULL,
	[FProff] [varchar](250) NULL,
	[FOff] [varchar](250) NULL,
	[FOffAdd] [varchar](250) NULL,
	[FEmail] [varchar](50) NULL,
	[FMobile] [varchar](250) NULL,
	[FPhoneOff] [varchar](250) NULL,
	[FPhoneRes] [varchar](250) NULL,
	[MName] [varchar](250) NULL,
	[MProff] [varchar](250) NULL,
	[MOff] [varchar](250) NULL,
	[MOffAdd] [varchar](250) NULL,
	[MEmail] [varchar](250) NULL,
	[MMobile] [varchar](150) NULL,
	[MPhoneOff] [varchar](150) NULL,
	[MPhoneRes] [varchar](150) NULL,
	[PerAdd] [varchar](250) NULL,
	[PerWardNo] [varchar](250) NULL,
	[PerCity] [varchar](250) NULL,
	[PerDistrict] [varchar](250) NULL,
	[PerState] [varchar](250) NULL,
	[PerCountry] [varchar](250) NULL,
	[PerPhone] [varchar](150) NULL,
	[TmpAdd] [varchar](20) NULL,
	[TmpWardNo] [varchar](230) NULL,
	[TmpCity] [varchar](250) NULL,
	[TmpDistrict] [varchar](250) NULL,
	[TmpState] [varchar](250) NULL,
	[TmpCountry] [varchar](250) NULL,
	[TmpPhone] [varchar](150) NULL,
	[LedgerNo] [varchar](150) NULL,
	[StHeight] [varchar](100) NULL,
	[StWeight] [varchar](100) NULL,
	[StuMemo] [varchar](500) NULL,
	[StdCategoryId] [int] NOT NULL,
	[StdCode] [varchar](150) NULL,
	[StdPhotoFileName] [varchar](250) NULL,
	[StdPhotoExt] [varchar](200) NULL,
	[ClassId] [int] NOT NULL,
	[PassMiti] [varchar](50) NULL,
	[PrevClassId] [int] NULL,
	[GLCode] [int] NOT NULL,
	[AcademicYearId] [int] NULL,
 CONSTRAINT [PK_ScStudentinfo] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScStudentFeeTerm]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScStudentFeeTerm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FeeTermId] [int] NOT NULL,
	[LocalRate] [decimal](18, 2) NOT NULL,
	[LocalAmount] [decimal](18, 2) NOT NULL,
	[DetailId] [int] NOT NULL,
	[IsItemWise] [bit] NOT NULL,
	[StudentFeeId] [int] NOT NULL,
	[Index] [int] NULL,
 CONSTRAINT [PK_ScStudentFeeTerm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScStudentRegistration]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScStudentRegistration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[AcademicYearId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[Remarks] [varchar](max) NULL,
 CONSTRAINT [PK_ScStudentRegistration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [ScSubject_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScSubject_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScSubject]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScSubject'
                       END
GO
/****** Object:  StoredProcedure [dbo].[SP_BOMRegister]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Select * From BillOfMaterial
--Select * From BillOfMaterialDetail
--SP_BOMRegister '33,35','0'
CREATE Proc [dbo].[SP_BOMRegister]
@FinishedGoodId varchar(max),@CostCenterId varchar(max)
as
Select P.ShortName ShortNameFG,Bom.FinishedGoodId,P.Name FinishedGood,BOM.Qty QtyFG, U.Code UnitFG,Bom.Id BOMID,Bom.Code BomCode, Bom.Description BOM,cc.ShortName ShortNameCC,Bom.CostCenterId, CC.Name CostCenter,
BOMD.RawMaterialId, Rawm.Name RawMaterial,BOMD.Quantity QtyRaw, RU.Code UnitRaw,CCR.Name CostCenterRaw,G.Name GoDownRaw from BillOfMaterial BOM 
Inner Join Product P on BOM.FinishedGoodId = P.Id
Inner Join Unit U on BOM.UnitId = U.Id
left Outer Join CostCenter CC on BOM.CostCenterId = CC.Id
--Below Part is For Masters
Inner join BillOfMaterialDetail BOMD on BOM.Id = BOMD.BillOfMaterialId
Inner Join Product Rawm on BOMD.RawMaterialId = Rawm.Id
Inner Join Unit RU on BOMD.UnitId = RU.Id
Inner Join CostCenter CCR on BOMD.CostCenterId = CCR.Id
Inner Join Godown G On BOMD.GodownId = g.Id
Where BOM.IsDeleted <> 1 AND 
(@FinishedGoodId='0' OR BOM.FinishedGoodId In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@FinishedGoodId, ','))) AND 
(@CostCenterId='0' OR BOM.CostCenterId In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@CostCenterId, ',')))
GO
/****** Object:  StoredProcedure [dbo].[SP_MaterialIssueRegister]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Select * From MaterialIssue
--Select * From MaterialIssueDetail
--SP_MaterialIssueRegister @StartDate='2013-05-07',@EndDate='2013-05-07',@FinishedGoodId='0',@CostCenterId='0',@RawMaterialId='28,39'
CREATE Proc [dbo].[SP_MaterialIssueRegister]
	@StartDate Date,@EndDate Date,
	@FinishedGoodId varchar(max),@CostCenterId varchar(max),@RawMaterialId varchar(max)
as
Select MI.IssueDate, P.ShortName ShortNameFG,MI.FinishedGoodId,P.Name FinishedGood,MI.Qty QtyFG, U.Code UnitFG,MI.Id MIID,MI.Code MICode, MI.Description MI,cc.ShortName ShortNameCC,MI.CostCenterId, CC.Name CostCenter,
MID.RawMaterialId , Rawm.Name RawMaterial,MID.Quantity QtyRaw, RU.Code UnitRaw,CCR.Name CostCenterRaw,G.Name GoDownRaw from MaterialIssue MI 
Inner Join Product P on MI.FinishedGoodId = P.Id
Inner Join Unit U on MI.UnitId = U.Id
left Outer Join CostCenter CC on MI.CostCenterId = CC.Id
--Below Part is For Masters
Inner join MaterialIssueDetail MID on MI.Id = MID.MaterialIssueId
Inner Join Product Rawm on MID.RawMaterialId = Rawm.Id
Inner Join Unit RU on MID.UnitId = RU.Id
Inner Join CostCenter CCR on MID.CostCenterId = CCR.Id
Inner Join Godown G On MID.GodownId = g.Id
Where MI.IsDeleted <> 1 
AND MI.IssueDate Between @StartDate And @EndDate
AND (@FinishedGoodId='0' OR MI.FinishedGoodId In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@FinishedGoodId, ','))) 
AND (@CostCenterId='0' OR MI.CostCenterId In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@CostCenterId, ','))) 
AND (@RawMaterialId='0' OR MID.RawMaterialId In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@RawMaterialId, ',')))
Order By MI.IssueDate
GO
/****** Object:  StoredProcedure [dbo].[SP_MonthlyBillFeeWise]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[SP_MonthlyBillGenerate] 7,0,0,'34,33,49,59','1,2'

--[SP_MonthlyBillFeeWise]  7,0,0,'34,33,49,59','1,2'

CREATE proc [dbo].[SP_MonthlyBillFeeWise]
@ClassId int ,@BoaderId int , @ShiftId int,@stdId varchar(max) ,@feeitem_Id varchar(max)
as

Create Table #FeeTable
(
Id int Identity(1,1),
StudentId int,
StudentName nvarchar(100),
Section nvarchar(100),
RollNo nvarchar(255),
[Status] nvarchar(10)
)

Create Table #FeeItemDetail
(
Id int Identity(1,1),
StudentId int,
Amount decimal(18,2),
NetAmount decimal(18,2),
FeeItemId int
)

Declare @sqlinsert varchar(max)
Declare @sqlSet varchar(max)
Select @sqlinsert='
Insert Into #FeeTable (StudentId ,StudentName ,Section ,RollNo)
select srd.StudentId,si.StuDesc ,sec.Description,srd.RollNo
From ScStudentRegistration sr
inner join ScStudentRegistrationDetail srd On sr.Id = srd.RegMasterId
inner join ScStudentInfo si on srd.StudentId = si.StudentID
Left outer join ScSection sec on srd.SectionId = sec.Id
Where sr.classId ='+CAST(@ClassId as varchar(10))+'
and srd.StudentId in ('+@stdId+')
and srd.BoaderId='+CAST(@BoaderId as varchar(10))+'
and srd.ShiftId ='+CAST(@ShiftId as varchar(10))+''
exec (@sqlinsert)

Declare @Id int
Declare @StudentId int
Declare @StudentName nvarchar(100)
Declare @Section nvarchar(100)
Declare @RollNo nvarchar(255)
Declare @Amount decimal(18,2)
Declare @NetAmount decimal(18,2)
declare @FeeItemId int
Declare @Status nvarchar(10)

Declare FeeCursor Cursor For
Select Id,StudentId,StudentName,Section,RollNo,[Status] From #FeeTable
Open FeeCursor
Fetch Next From FeeCursor Into @Id ,@StudentId, @StudentName, @Section, @RollNo ,  @Status
while @@FETCH_STATUS =0
Begin

select @sqlSet='Insert Into #FeeItemDetail (StudentId,Amount,NetAmount,FeeItemId)
Select ' + CAST(@StudentId as varchar(10))+ ' as StudentId,FeeRate,NetAmount,FeeItemId 
from (
select fm.ClassId,fm.StudentId,fd.FeeItemId,fd.FeeRate,fd.NetAmount
from ScStudentFeeRateMaster fm
Inner Join ScStudentFeeRateDetail fd on fm.Id = fd.MasterId
where fm.StudentId ='+CAST(@StudentId as varchar(10))+'
and fd.FeeItemId in ('+@feeitem_Id+')
Union All
select r.ClassId,'+CAST(@StudentId as varchar(10))+' as StudentId, r.FeeItemId,r.FeeRate,0.00 as NetAmount
from scclassfeerate r
Where r.ClassId = '+CAST(@ClassId as varchar(10))+'
and r.FeeItemId in ('+@feeitem_Id+')
and r.BoaderId='+CAST(@BoaderId as varchar(10))+'
and r.ShiftId ='+CAST(@ShiftId as varchar(10))+'
and r.FeeItemId
Not In (
select Distinct fd.FeeItemId
from ScStudentFeeRateMaster fm
Inner Join ScStudentFeeRateDetail fd on fm.Id = fd.MasterId
where fm.ClassId ='+CAST(@ClassId as varchar(10))+'
and fm.StudentId ='+CAST(@StudentId as varchar(10))+')) as FeeSummary'

exec (@sqlset)

Fetch Next From FeeCursor Into @Id ,@StudentId, @StudentName, @Section, @RollNo ,@Status
end
CLOSE FeeCursor
DEALLOCATE FeeCursor

Select fd.StudentId,f.StudentName,f.Section,f.RollNo,fd.Amount,fd.NetAmount,fd.FeeItemId ,fi.Description from #feeTable f
Inner Join #FeeItemDetail fd on f.StudentId = fd.StudentId 
inner Join ScFeeItem fi on fd.FeeItemId = fi.Id

Drop table #feetable


--Select * from ScStudentFeeRateDetail
GO
/****** Object:  Table [dbo].[ScStudentFeeRateMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScStudentFeeRateMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[Remarks] [varchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[BasicAmount] [decimal](18, 2) NOT NULL,
	[TermAmount] [decimal](18, 2) NOT NULL,
	[AcademicYearId] [int] NULL,
	[NetAmount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_ScStudentFeeRateMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScStudentSubjectMapping]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScStudentSubjectMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[SubjectType] [int] NULL,
	[ClassType] [int] NULL,
	[ResultSystem] [int] NULL,
	[EvaluateForType] [int] NULL,
	[Narration] [varchar](255) NULL,
	[AcademicYearId] [int] NULL,
 CONSTRAINT [PK_ScStudentSubjectMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScTransportMapping]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScTransportMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
	[BusRouteCode] [varchar](15) NULL,
	[StudentId] [int] NOT NULL,
	[BusStopFromId] [int] NOT NULL,
	[BusStopToId] [int] NOT NULL,
	[Status] [varchar](50) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[StartMiti] [varchar](10) NOT NULL,
	[EndMiti] [varchar](10) NOT NULL,
	[Narr] [varchar](255) NULL,
	[TransportAmt] [decimal](16, 4) NOT NULL,
	[AcademicYearId] [int] NULL,
	[Remarks] [varchar](512) NULL,
 CONSTRAINT [PK_ScTransportMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[SP_MaterialReturnRegister]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Select * From MaterialReturn
--Select * From MaterialReturnDetail
--SP_MaterialReturnRegister @StartDate='2013-05-07',@EndDate='2013-05-07',@CostCenterId='0',@RawMaterialId='36,37'
CREATE Proc [dbo].[SP_MaterialReturnRegister] 
	@StartDate Date,@EndDate Date,
	@CostCenterId varchar(max),@RawMaterialId varchar(max)
as
Select MR.IssueDate ReturnDate,MR.Id MaterialReturnId,MR.Code MRCode,cc.ShortName ShortNameCC,MR.CostCenterId, CC.Name CostCenter,
MRD.RawMaterialId , Rawm.Name RawMaterial,MRD.Quantity QtyRaw, RU.Code UnitRaw,CCR.Name CostCenterRaw,G.Name GoDownRaw from MaterialReturn MR 
left Outer Join CostCenter CC on MR.CostCenterId = CC.Id
--Below Part is For Masters
Inner join MaterialReturnDetail MRD on MR.Id = MRD.MaterialReturnId
Inner Join Product Rawm on MRD.RawMaterialId = Rawm.Id
Inner Join Unit RU on MRD.UnitId = RU.Id
Inner Join CostCenter CCR on MRD.CostCenterId = CCR.Id
Inner Join Godown G On MRD.GodownId = g.Id
Where MR.IsDeleted <> 1 
AND MR.IssueDate Between @StartDate And @EndDate
AND (@CostCenterId='0' OR MR.CostCenterId In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@CostCenterId, ','))) 
AND (@RawMaterialId='0' OR MRD.RawMaterialId In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@RawMaterialId, ',')))
Order By MR.IssueDate
GO
/****** Object:  Trigger [ScStudentinfo_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScStudentinfo_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScStudentinfo]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScStudentinfo'
                       END
GO
/****** Object:  Table [dbo].[ScStudentRegistrationDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScStudentRegistrationDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
	[CurrentStatus] [int] NOT NULL,
	[ShiftId] [int] NOT NULL,
	[StudentType] [int] NOT NULL,
	[Narration] [varchar](max) NULL,
	[RollNo] [varchar](max) NOT NULL,
	[RegMasterId] [int] NOT NULL,
	[BoaderId] [int] NOT NULL,
	[userId] [int] NOT NULL,
 CONSTRAINT [PK_ScStudentRegistrationDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [ScStudentRegistration_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScStudentRegistration_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScStudentRegistration]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScStudentRegistration'
                       END
GO
/****** Object:  Trigger [MaterialIssue_Details_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[MaterialIssue_Details_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScMaterialIssueDetails]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'MaterialIssue_Details'
                       END
GO
/****** Object:  Table [dbo].[ScPrePaidSchemeDetails]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScPrePaidSchemeDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MasterId] [int] NOT NULL,
	[Days] [int] NULL,
	[Percentage] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_ScPrePaidSchemeDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScMonthlyBillStudent]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScMonthlyBillStudent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[ClassId] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Date] [datetime] NULL,
	[Remarks] [varchar](max) NULL,
	[BoaderId] [int] NOT NULL,
	[ShiftId] [int] NOT NULL,
	[BillNo] [nvarchar](max) NOT NULL,
	[AcademicYearId] [int] NULL,
	[Miti] [nvarchar](10) NOT NULL,
	[MonthMiti] [int] NOT NULL,
	[YearMiti] [int] NOT NULL,
	[CreatedById] [int] NOT NULL,
 CONSTRAINT [PK_ScMonthlyBillGenerationDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScHouseMapping]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScHouseMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
	[HouseGroupId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[Status] [int] NULL,
	[StartDate] [date] NULL,
	[StartMiti] [varchar](10) NULL,
	[EndDate] [date] NULL,
	[EndMiti] [varchar](10) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[Narration] [varchar](255) NULL,
	[Remarks] [varchar](512) NULL,
	[AcademicYearId] [int] NULL,
 CONSTRAINT [PK_ScHouseMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScStudentExtraActivityDetails]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScStudentExtraActivityDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[ActivitiesStatusId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[Narration] [nvarchar](50) NULL,
	[MasterId] [int] NOT NULL,
	[StartMiti] [varchar](10) NULL,
 CONSTRAINT [PK__ScStuden__3214EC070697FACD] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [ScStudentExtraActivity_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScStudentExtraActivity_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScStudentExtraActivity]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScStudentExtraActivity'
                       END
GO
/****** Object:  Table [dbo].[ScExamMarksEntry]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScExamMarksEntry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExamId] [int] NULL,
	[ClassId] [int] NULL,
	[SubjectId] [int] NULL,
	[Remarks] [nvarchar](50) NULL,
	[CreatedById] [int] NULL,
	[UpdatedById] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[TotalFullMarks] [decimal](18, 2) NOT NULL,
	[TotalObtainedMarks] [decimal](18, 2) NOT NULL,
	[Percentage] [decimal](18, 2) NOT NULL,
	[Result] [varchar](15) NULL,
	[DivisionId] [int] NOT NULL,
	[OutOf] [bigint] NOT NULL,
	[Rank] [bigint] NOT NULL,
	[HighestMarks] [decimal](18, 2) NOT NULL,
	[TheoryObtainedMarks] [decimal](18, 2) NOT NULL,
	[TheoryStatus] [int] NOT NULL,
	[PracticalObtainedMarks] [decimal](18, 2) NOT NULL,
	[PracticalStatus] [int] NOT NULL,
	[Narration] [nvarchar](max) NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[AcademicYearId] [int] NULL,
	[StudentId] [int] NOT NULL,
 CONSTRAINT [PK__ScSubjec__3214EC071A9EF37A] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [ScExamAttendanceMaster_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScExamAttendanceMaster_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScExamAttendanceMaster]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScExamAttendanceMaster'
                       END
GO
/****** Object:  Trigger [ScExamMarkSetup_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScExamMarkSetup_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScExamMarkSetup]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScExamMarkSetup'
                       END
GO
/****** Object:  Trigger [ScExamRoutine_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScExamRoutine_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScExamRoutine]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScExamRoutine'
                       END
GO
/****** Object:  Trigger [SchBuildingRoomMapping_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[SchBuildingRoomMapping_AspNet_SqlCacheNotification_Trigger] ON [dbo].[SchBuildingRoomMapping]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'SchBuildingRoomMapping'
                       END
GO
/****** Object:  Table [dbo].[ScFeeReceipt]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScFeeReceipt](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReceiptNo] [nvarchar](max) NOT NULL,
	[ReceiptDate] [date] NOT NULL,
	[ReceiptMiti] [varchar](10) NULL,
	[ReceiptTimi] [datetime] NOT NULL,
	[GLCode] [int] NOT NULL,
	[ClassId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[BillNo] [varchar](max) NOT NULL,
	[Remarks] [varchar](max) NULL,
	[PaidUpMonthMiti] [int] NOT NULL,
	[FineAmount] [decimal](18, 2) NOT NULL,
	[DiscountAmount] [decimal](18, 2) NOT NULL,
	[ReceiptAmount] [decimal](18, 2) NOT NULL,
	[MonthlyBillStudentId] [int] NOT NULL,
	[AcademicYearId] [int] NOT NULL,
	[PaidUpYear] [int] NOT NULL,
	[PaidUpMonth] [int] NOT NULL,
	[paidUpYearMiti] [int] NOT NULL,
	[CreatedById] [int] NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScFineSchemeDetails]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScFineSchemeDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MasterId] [int] NOT NULL,
	[Days] [int] NULL,
	[Percentage] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_ScFineSchemeDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Trigger [ScFineScheme_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScFineScheme_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScFineScheme]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScFineScheme'
                       END
GO
/****** Object:  Table [dbo].[PyCorporateSalaryMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PyCorporateSalaryMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeePostId] [int] NOT NULL,
	[Salary] [decimal](16, 4) NULL,
	[Status] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[FiscalYearId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
 CONSTRAINT [PK_ScCorporateSalaryMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Trigger [ScBoaderMapping_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScBoaderMapping_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScBoaderMapping]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScBoaderMapping'
                       END
GO
/****** Object:  Table [dbo].[ScAbsentApplication]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScAbsentApplication](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[DateFrom] [date] NOT NULL,
	[MitiFrom] [varchar](10) NULL,
	[DateTo] [date] NOT NULL,
	[MitiTo] [varchar](10) NULL,
	[IsConfirm] [varchar](5) NOT NULL,
	[Reason] [nvarchar](max) NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[IsApplication] [bit] NOT NULL,
	[Remarks] [varchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_ScAbsentApplication] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScBillTransaction]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScBillTransaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[BillAmount] [decimal](18, 2) NOT NULL,
	[ReceiptAmount] [decimal](18, 2) NOT NULL,
	[Source] [varchar](50) NOT NULL,
	[ReferenceId] [int] NOT NULL,
	[FYId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[BillNo] [varchar](max) NULL,
	[Date] [datetime] NOT NULL,
	[BMiti] [nvarchar](10) NOT NULL,
	[AcademicYearId] [int] NULL,
 CONSTRAINT [PK_ScBillTransaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ScExamAttendanceDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScExamAttendanceDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MasterId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[PresentDays] [int] NOT NULL,
	[AbsentDays] [int] NOT NULL,
	[Narration] [varchar](max) NULL,
 CONSTRAINT [PK_ScExamAttendanceDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [ScClassSubjectMapping_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScClassSubjectMapping_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScClassSubjectMapping]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScClassSubjectMapping'
                       END
GO
/****** Object:  Table [dbo].[ScEmployeeInfo]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScEmployeeInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NOT NULL,
	[EmployeeCategoryId] [int] NOT NULL,
	[EmployeeDepartmentId] [int] NOT NULL,
	[EmployeePostId] [int] NOT NULL,
	[Gender] [varchar](50) NOT NULL,
	[MaritalStatus] [varchar](50) NOT NULL,
	[Religion] [varchar](50) NOT NULL,
	[DateOfBirth] [date] NULL,
	[MitiOfBirth] [varchar](10) NULL,
	[FatherName] [varchar](50) NULL,
	[Mobile] [varchar](20) NULL,
	[DateOfJoin] [date] NULL,
	[MitiofJoin] [varchar](10) NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Weekholiday] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Country] [varchar](max) NULL,
	[Address] [varchar](max) NULL,
	[Phone] [nvarchar](15) NULL,
	[Email] [nvarchar](50) NULL,
	[Country1] [varchar](max) NULL,
	[Address1] [varchar](max) NULL,
	[Phone1] [varchar](15) NULL,
	[Email1] [nvarchar](50) NULL,
	[Education] [varchar](max) NULL,
	[Remarks] [varchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedById] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedById] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[LedgerId] [int] NOT NULL,
 CONSTRAINT [PK_ScEmployeeInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [ScClassFeeRate_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScClassFeeRate_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScClassFeeRate]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScClassFeeRate'
                       END
GO
/****** Object:  Trigger [ScBusRouteDetails_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScBusRouteDetails_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScBusRouteDetails]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScBusRouteDetails'
                       END
GO
/****** Object:  Table [dbo].[ScBookReceivedDetails]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScBookReceivedDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccessionNo] [varchar](100) NULL,
	[Remarks] [varchar](max) NULL,
	[MasterId] [int] NULL,
	[Status] [int] NULL,
	[BarCode] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [ScBookReceived_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScBookReceived_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScBookReceived]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScBookReceived'
                       END
GO
/****** Object:  Trigger [ScBookReceivedDetails_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScBookReceivedDetails_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScBookReceivedDetails]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScBookReceivedDetails'
                       END
GO
/****** Object:  Trigger [ScAbsentApplication_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScAbsentApplication_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScAbsentApplication]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScAbsentApplication'
                       END
GO
/****** Object:  Table [dbo].[PyTaxDeductionEmployeeMapping]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PyTaxDeductionEmployeeMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaxDeductionId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_PyTaxDeductionEmployeeMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PyCorporateAllowanceMapping]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PyCorporateAllowanceMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AllowanceId] [int] NOT NULL,
	[CorporateId] [int] NOT NULL,
 CONSTRAINT [PK_PyCorporateAllowanceMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PyEmployeeDeductionMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PyEmployeeDeductionMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[FiscalYearId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
 CONSTRAINT [PK_mst_EmployeeDeductionMaster_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PyEmployeeSalaryMaster]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PyEmployeeSalaryMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[BasicSalary] [decimal](18, 3) NULL,
	[Status] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[FiscalYearId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
 CONSTRAINT [PK_mst_EmployeeSalaryMaster_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Trigger [ScFineSchemeDetails_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScFineSchemeDetails_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScFineSchemeDetails]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScFineSchemeDetails'
                       END
GO
/****** Object:  Trigger [ScExamAttendanceDetail_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScExamAttendanceDetail_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScExamAttendanceDetail]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScExamAttendanceDetail'
                       END
GO
/****** Object:  Trigger [ScExamMarksEntry_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScExamMarksEntry_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScExamMarksEntry]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScExamMarksEntry'
                       END
GO
/****** Object:  Table [dbo].[ScStudentFeeRateDetail]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ScStudentFeeRateDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MasterId] [int] NOT NULL,
	[FeeItemId] [int] NOT NULL,
	[FeeRate] [decimal](18, 2) NOT NULL,
	[Narration] [varchar](max) NULL,
	[TermAmount] [decimal](18, 2) NOT NULL,
	[NetAmount] [decimal](18, 2) NOT NULL,
	[Index] [int] NOT NULL,
 CONSTRAINT [PK_ScStudentFeeRateDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [ScStudentExtraActivityDetails_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScStudentExtraActivityDetails_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScStudentExtraActivityDetails]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScStudentExtraActivityDetails'
                       END
GO
/****** Object:  Table [dbo].[ScStaffSubjectMapping]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScStaffSubjectMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StaffId] [int] NULL,
	[SubjectId] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_ScStaffSubjectMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Trigger [ScStaffMaster_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScStaffMaster_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScEmployeeInfo]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScStaffMaster'
                       END
GO
/****** Object:  Table [dbo].[ScMonthlyBill]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScMonthlyBill](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FeeItemId] [int] NOT NULL,
	[MonthlyBillStudentId] [int] NOT NULL,
	[FeeAmount] [decimal](18, 2) NOT NULL,
	[EducationTaxAmount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_ScMonthlyBillGeneration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Trigger [ScMonthlyBillDetail_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScMonthlyBillDetail_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScMonthlyBillStudent]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScMonthlyBillDetail'
                       END
GO
/****** Object:  StoredProcedure [dbo].[SP_StudentLedgerSummary]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_StudentLedgerSummary]
(
	@StudentId varchar(max),
	@ClassId int,
	@SectionId int,
	@StartDate nvarchar(12),
	@EndDate nvarchar(12),
	@AYId int	
)
as
--SP_StudentLedgerSummary '59',7,1,'2013-01-30','2013-01-31'
Create Table #StudentLedger
(
	Id int Identity(1,1),
	StudentId int,
	StudentName nvarchar(100),
	ClassName nvarchar(100),
	SectionName nvarchar(100),
	RollNo nvarchar(255),
	Opening decimal(18,2),
	DebitAmount decimal(18,2),
	CreditAmount decimal(18,2)
)
Create Table #OpeningAmt
(
	Id int Identity(1,1),
	StudentId int,
	Opening decimal(18,2),
)								  
Declare @sqlinsert varchar(max) 
Declare @sqlSet varchar(max)
--Select @sqlinsert=
set @sqlinsert = 'Insert Into #StudentLedger (StudentId ,StudentName ,ClassName ,SectionName, RollNo, Opening, DebitAmount, CreditAmount)'
set @sqlinsert += 'select distinct  a.StudentID,a.StudentName,a.ClassName,a.SectionName,a.RollNo,a.Opening,sum(a.DebitAmount) as DebitAmount,  
		sum(a.CreditAmount) as CreditAmount from (
select		distinct sbt.Id,si.StudentID,si.StuDesc as StudentName,sc.Description as ClassName,
			ss.Description as SectionName,
			ssr.RollNo,0.00 as Opening,sbt.BillAmount as DebitAmount, sbt.ReceiptAmount as CreditAmount
from		ScStudentinfo as si
join		SchClass as sc on sc.Id = si.ClassId

join		ScStudentRegistrationDetail as ssr on ssr.StudentId = si.StudentID
join		ScSection as ss on ss.ID = ssr.SectionId
join		ScBillTransaction as sbt on sbt.StudentId = si.StudentID
where       sbt.Date >= ''' + @StartDate + ''' and sbt.Date <= ''' + @EndDate + 
''' And si.StudentID in (' + @StudentId +')and sbt.AcademicYearId='+cast(@AYId  as varchar(10)) + ''

if @ClassId <> 0 
Begin
set @sqlinsert += ' And si.ClassId =' + cast(@ClassId  as varchar(10))
End
if @SectionId <> 0 
Begin
	set @sqlinsert += ' And  ssr.SectionId =' + cast(@SectionId  as varchar(10))
End
set @sqlinsert += ') as a group by	a.StudentID,a.StudentName,a.ClassName,a.SectionName,a.RollNo,a.Opening'
--select @sqlinsert
exec (@sqlinsert)

Declare @Id int 
Declare @StudId int 
Declare @StudentName nvarchar(100) 
Declare @ClassName nvarchar(100)
Declare @SectionName nvarchar(100) 
Declare @RollNo nvarchar(255) 
Declare @Opening decimal(18,2)

Declare LedgerCursor Cursor For
select distinct	Id, StudentId From	#StudentLedger  
Open LedgerCursor
Fetch Next From LedgerCursor Into @Id ,@StudId
while @@FETCH_STATUS =0
Begin
Declare @OpeningAmount decimal(18,2)
set @OpeningAmount = (select (sum(sbt.BillAmount) - sum(sbt.ReceiptAmount)) as Opening
from		ScStudentinfo as si
join		ScBillTransaction as sbt on sbt.StudentId = si.StudentID
where       si.StudentID = @StudId and sbt.Date <=@StartDate)
Update #StudentLedger set Opening = @OpeningAmount 
Where #StudentLedger.StudentId = @StudId
Fetch Next From LedgerCursor Into @Id ,@StudId
end
CLOSE LedgerCursor
DEALLOCATE LedgerCursor

Select distinct Id,StudentId,StudentName,ClassName,SectionName,RollNo,ISNULL(Opening,0) as Opening,DebitAmount,CreditAmount 
from #StudentLedger
Drop table #StudentLedger
GO
/****** Object:  StoredProcedure [dbo].[SP_SubjectWiseMarksDetailEdit]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_SubjectWiseMarksDetailEdit](@examId int,@classId int,@SubjectId int)
as
Begin
   Select * from ( Select Distinct S.StudentID,S.StdCode, S.StuDesc,SR.RollNo,
   EMS.PracticalFullMark, 
					EMS.PracticalPassMark,
					EMS.TheoryFullMark,
					EMS.TheoryPassMark,
					EMS.SubjectType, EMS.ClassType,
					EME.TheoryObtainedMarks,
					EME.TheoryStatus,
					EME.PracticalObtainedMarks,
					EME.PracticalStatus,
					EME.Narration,
					EME.Total
   from ScExamMarksEntry as EME 
   
	JOin	ScStudentinfo as S on Eme.StudentId = S.StudentID
	JOin	ScStudentRegistrationDetail as SR ON  Eme.StudentID = SR.StudentId
	join	ScExamMarkSetup as EMS on EMS.SubjectId = EME.SubjectId	and 
		 EMS.ClassId = EME.ClassId	and
		 EMS.ExamId = EME.ExamId
		Where        EME.ExamId =@examId and EME.ClassId =@classId and EME.SubjectId = @SubjectId  
		
		union All 
			Select		distinct SRD.StudentID as StudentID,S.StdCode,S.StuDesc,SRD.RollNo,
				EMS.PracticalFullMark,EMS.PracticalPassMark,
				EMS.TheoryFullMark,EMS.TheoryPassMark,EMS.SubjectType,EMS.ClassType,  0 as TheoryObtainedMarks,
				0 as TheoryStatus,0 as PracticalObtainedMarks,0 as PracticalStatus , CAST(''as varchar(max)) as Narration,0 as Total
			
	from		ScClassSubjectMapping as SCM
	Join		ScExamMarkSetup as EMS on EMS.ClassId = SCM.ClassId
	Join        ScStudentRegistration as SR on SR.ClassId =	SCM.ClassId
	Join		ScStudentRegistrationDetail as SRD on SRD.RegMasterId = SR.Id
	Join		ScStudentinfo as s on s.StudentID = SRD.StudentID
	
	where       EMs.ExamId = @examId and EMs.ClassId = @classId and EMs.SubjectId = @SubjectId and SRD.StudentId not in(
		 Select StudentId from ScExamMarksEntry where ClassId =@ClassId and SubjectId= @SubjectId and examId=@examId)
		) A
		
		 group by A.StudentID,A.StdCode, A.StuDesc,A.RollNo,
   A.PracticalFullMark, 
					A.PracticalPassMark,
					A.TheoryFullMark,
					A.TheoryPassMark,
					A.SubjectType, A.ClassType,
					A.TheoryObtainedMarks,
					A.TheoryStatus,
					A.PracticalObtainedMarks,
					A.PracticalStatus,
					A.Narration,A.Total

   End
GO
/****** Object:  StoredProcedure [dbo].[Sp_SubjectWiseMarksDetail]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Sp_SubjectWiseMarksDetail](@examId int ,@classId int ,@subjectId int, @SubjectType int)
as
begin	
if(@SubjectType = 2)
begin
	
Select		distinct SSM.StudentId as StudentID,SSM.ClassId, S.StdCode,S.StuDesc,SRD.RollNo,
				EMS.PracticalFullMark,EMS.PracticalPassMark,EMS.TheoryFullMark,EMS.TheoryPassMark,EMS.ClassType,EMS.SubjectType,
				SSM.SubjectId
	from		ScStudentSubjectMapping as SSM
	Join		ScExamMarkSetup as EMS on EMS.ClassId = SSM.ClassId	And EMS.SubjectId = SSM.SubjectId
	Join		ScStudentRegistrationDetail as SRD on SRD.StudentID = SSM.StudentID
	Join		ScStudentinfo as s on s.StudentID = SSM.StudentID
	where       EMS.ExamId = @examId and SSM.ClassId = @classId and SSM.SubjectId = @subjectId	and SRD.StudentId not in ( Select EME.StudentId from ScExamMarksEntry as EME where EME.ClassId =@ClassId and EME.SubjectId=@subjectId and EME.ExamId =@examId)
end
else
begin
	Select		distinct SRD.StudentID as StudentID, SCM.ClassId,S.StdCode,S.StuDesc,SRD.RollNo,
				EMS.PracticalFullMark,EMS.PracticalPassMark,EMS.TheoryFullMark,EMS.TheoryPassMark,EMS.ClassType,EMS.SubjectType,
				EMS.SubjectId
	from		ScClassSubjectMapping as SCM
	Join		ScExamMarkSetup as EMS on EMS.ClassId = SCM.ClassId
	Join        ScStudentRegistration as SR on SR.ClassId =	SCM.ClassId
	Join		ScStudentRegistrationDetail as SRD on SRD.RegMasterId = SR.Id
	Join		ScStudentinfo as s on s.StudentID = SRD.StudentID
	where       EMS.ExamId = @examId and SCM.ClassId = @classId and EMS.SubjectId = @subjectId and SRD.StudentId not in ( Select EME.StudentId from ScExamMarksEntry as EME where EME.ClassId =@ClassId and EME.SubjectId=@subjectId and EME.ExamId =@examId)	
end

end
GO
/****** Object:  StoredProcedure [dbo].[SP_StudentWiseMarksDetailEdit]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_StudentWiseMarksDetailEdit](@examId int,@classId int,@studentId int)
   as
Begin

SELECT distinct S.Description as SubDesc, 
				EMS.SubjectId,S.Code, EMS.PracticalFullMark,EMS.PracticalPassMark,
				EMS.TheoryFullMark,	EMS.TheoryPassMark,	EMS.SubjectType	,EMS.ClassType
				,SEME.TheoryObtainedMarks,SEME.TheoryStatus,SEME.PracticalObtainedMarks,
				SEME.PracticalStatus,SEME.Narration,SEME.Total,EMS.ResultSystem,EMS.EvaluateForType				
		from	ScExamMarksEntry as SEME
		Join	ScSubject as s on SEME.SubjectId = s.Id
		Join	ScExamMarkSetup as EMS on SEME.SubjectId=EMS.SubjectId
		and	    EMS.ClassId = SEME.ClassId			 
		and	    EMS.ExamId = SEME.ExamId
		Where    SEME.ExamId =@examId and SEME.ClassId =@classId and SEME.StudentID = @studentId
		Union All
		-- Not In Above
		SELECT * FROM (										
		Select	distinct s.Description as SubDesc,
				CSM.SubjectId,s.Code, EMS.PracticalFullMark,	EMS.PracticalPassMark,
				EMS.TheoryFullMark,	EMS.TheoryPassMark,	EMS.SubjectType	,EMS.ClassType,
				0 as TheoryObtainedMarks,0 as TheoryStatus,0 as PracticalObtainedMarks,
				0 as PracticalStatus,  null as Narration,0 as Total,EMS.ResultSystem,EMS.EvaluateForType				
				from		ScClassSubjectMapping	as CSM	 
				join        ScSubject as s on CSM.SubjectId = s.Id
Join		     ScExamMarkSetup as EMS on EMS.SubjectId = CSM.SubjectId  and CSM.ClassId = EMS.ClassId
				where		CSM.SubjectType =1   and csm.ClassId = @classId
				
				
				
	   Union All
	   Select	distinct s.Description as SubDesc,
				SSM.SubjectId,s.Code, EMS.PracticalFullMark,	EMS.PracticalPassMark,
				EMS.TheoryFullMark,	EMS.TheoryPassMark,	EMS.SubjectType	,EMS.ClassType,
				0 as TheoryObtainedMarks,0 as TheoryStatus,0 as PracticalObtainedMarks,
				0 as PracticalStatus,  null as Narration,0 as Total,EMS.ResultSystem,EMS.EvaluateForType
		from		ScStudentSubjectMapping as SSM	
		join        ScSubject as s on SSM.SubjectId = s.Id 
		Join		     ScExamMarkSetup as EMS on EMS.SubjectId = ssm.SubjectId  and SSM.ClassId = EMS.ClassId
		where		SSM.StudentId=@studentId 
		) A
		where a.SubjectId not in
		(
			SELECT  EMS.SubjectId				
			from	ScExamMarksEntry as SEME
			Join	ScSubject as s on SEME.SubjectId = s.Id
			Join	ScExamMarkSetup as EMS on SEME.SubjectId=EMS.SubjectId
			and	    EMS.ClassId = SEME.ClassId			 
			and	    EMS.ExamId = SEME.ExamId
			Where    SEME.ExamId =@examId and SEME.ClassId =@classId and SEME.StudentID = @studentId
		)
	  
	   
	   
	   
End
GO
/****** Object:  StoredProcedure [dbo].[Sp_StudentWiseMarksDetail]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  proc [dbo].[Sp_StudentWiseMarksDetail](@examId int ,@classId int ,@studentId int)
as
Begin

select	distinct		A.*,S.Description as SubDesc,S.Code,EMS.PracticalFullMark,EMS.PracticalPassMark,EMS.TheoryFullMark,
				EMS.TheoryPassMark
from 
(
	Select	distinct	CSM.ClassId,CSM.SubjectId ,CSM.ClassType,CSM.SubjectType,
				CSM.ResultSystem,CSM.EvaluateForType,CSM.Narration
	from		ScClassSubjectMapping	as CSM	 
	where		CSM.SubjectType =1	
		Union All
	Select	distinct	SSM.ClassId,SSM.SubjectId ,SSM.ClassType,SSM.SubjectType,
				SSM.ResultSystem,SSM.EvaluateForType,SSM.Narration		 
	from		ScStudentSubjectMapping as SSM	 
	where		SSM.StudentId=@studentId 
)	 A
Join			 ScSubject as S on S.Id = A.SubjectId
Join		     ScExamMarkSetup as EMS on EMS.SubjectId = A.SubjectId  and A.ClassId = EMS.ClassId


Where        EMS.ExamId =@examId and A.ClassId=@classId 

End
  --Sp_StudentWiseMarksDetail 8,7,33
--select * from ScExamMarkSetup
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateMarksInfo]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_UpdateMarksInfo]
@ClassId int,@ExamId int,@AcademicYearId int
as

Declare @StudentId int
Declare @StuDesc nvarchar(255)
Declare @TotalFullMarks decimal(18,2)
Declare @FullMarks decimal(18,2)
Declare @TotalObtainedMarks Decimal(18,2)
Declare @Percent Decimal(18,2)
Declare @DivisionId int
Declare @Result nvarchar(10)
Declare @THPM Decimal(18,2)
Declare @PRPM Decimal(18,2)
Declare @THOBT Decimal(18,2)
Declare @PROBT Decimal(18,2)


Declare Student_List Cursor For
	select Distinct rd.StudentId,s.StuDesc from ScStudentRegistration r
	Inner join ScStudentRegistrationDetail rd on r.Id = rd.RegMasterId
	Inner join ScStudentinfo s on rd.StudentId = s.StudentID
	inner join ScExamMarksEntry m on rd.StudentId = m.StudentId
	Where r.ClassId = @ClassId and m.ExamId = @ExamId and m.AcademicYearId = @AcademicYearId
Open Student_List
Fetch Next From Student_List InTo @StudentId,@StuDesc
While @@FETCH_STATUS = 0
Begin
	Set @Result = 'Pass'
	Set @TotalFullMarks = 0
	set @TotalObtainedMarks = 0
	Declare Student_Marks Cursor For
		Select Isnull(ems.TheoryFullMark,0) + Isnull(ems.PracticalFullMark,0) FullMarks, 
		ISNULL(ems.TheoryPassMark,0) THPM,ISNULL(ems.PracticalPassMark,0) PRPM, Isnull(me.TheoryObtainedMarks,0) THOBT,
		Isnull(me.PracticalObtainedMarks,0) PROBT			
		From ScExamMarksEntry	Me 
		Inner Join ScExamMarkSetup ems On Me.SubjectId = ems.SubjectId 
		where StudentId = @StudentId  and 
		ems.ClassId = @ClassId and 
		Me.ClassId = @ClassId and 
		me.AcademicYearId = @AcademicYearId and 
		me.ExamId = @ExamId		 and
		ems.ExamId = @ExamId
	OPEN Student_Marks										  
	Fetch Next From	  Student_Marks Into  @FullMarks,@THPM,@PRPM,@THOBT,@PROBT
	While @@FETCH_STATUS = 0
	Begin 
		Set @TotalFullMarks += @FullMarks
		Set @TotalObtainedMarks += @THOBT + @PROBT
		if (@THOBT < @THPM OR @PROBT < @PRPM)
		Begin
		   Set @Result = 'Fail'
		End		
		Fetch Next From	  Student_Marks Into  @FullMarks,@THPM,@PRPM,@THOBT,@PROBT
	End
	Set @Percent = (@TotalObtainedMarks / @TotalFullMarks) * 100
	Set @DivisionId = (Select Top 1 Id from ScDivision 
	where PercentageFrom < @Percent  and PercentageTo > @Percent)
	Update ScExamMarksEntry Set 
	TotalFullMarks = @TotalFullMarks,TotalObtainedMarks = @TotalObtainedMarks ,
	Percentage = @Percent,
	DivisionId = isNull(@DivisionId,0),Result = @Result
	Where StudentId = @StudentId And ClassId = @ClassId And ExamId = @ExamId and AcademicYearId = @AcademicYearId
	CLOSE Student_Marks
	DEALLOCATE Student_Marks 
	Fetch Next From Student_List InTo @StudentId,@StuDesc
End
CLOSE Student_List
DEALLOCATE Student_List
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateHighestMarks]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_UpdateHighestMarks]
						  (@ExamId int , @ClassId int , @AcademicYearId int)
						  as
Declare @SubjectId int
Declare @Hth nvarchar(255)
Declare SubjectList Cursor For
Select e.SubjectId, MAX(e.TheoryObtainedMarks)as Hth from ScExamMarksEntry as e	where 
e.ClassId=@ClassId and e.ExamId=@ExamId	and e.AcademicYearId = @AcademicYearId  
group by e.SubjectId 
						   Open SubjectList
Fetch Next From SubjectList InTo @SubjectId,@Hth
While @@FETCH_STATUS = 0
Begin			  
Declare @Id int
			Declare MarkEntryList Cursor For
			Select em.Id from ScExamMarksEntry as em	
			where em.ClassId=@ClassId and em.AcademicYearId =@AcademicYearId and			
			em.ExamId=@ExamId	 and em.SubjectId = @SubjectId
	 
			Open MarkEntryList
			Fetch Next From MarkEntryList InTo @Id
			While @@FETCH_STATUS = 0
			Begin
				Update ScExamMarksEntry  Set 
				HighestMarks = @Hth
				Where Id = @Id	  
				
				Fetch Next From MarkEntryList InTo @Id
			End
			CLOSE MarkEntryList
	DEALLOCATE MarkEntryList 
	  Fetch Next From SubjectList InTo @SubjectId,@Hth
End
		 CLOSE SubjectList
	DEALLOCATE SubjectList
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpateRank]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Sp_UpateRank] 
@ExamId int , @ClassId int	 ,@AcademicYearId int
as
Declare @OutOff int


Set @OutOff = (Select Distinct COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
join		ScStudentRegistration as r on rd.RegMasterId = r.Id
where r.ClassId =@ClassId)
				 Update ScExamMarksEntry Set OutOf=@OutOff where ExamId = @ExamId and ClassId = @ClassId and AcademicYearId = @AcademicYearId

Declare @RankNo int
Declare @Id int
Declare StatementCursor Cursor For
	SELECT Dense_RANK() OVER (
	Order BY Percentage DESC) AS [RecordRank],ID
	FROM ScExamMarksEntry   where ExamId = @ExamId and ClassId = @ClassId and Result = 'Pass' and AcademicYearId = @AcademicYearId	order by id
Open StatementCursor
Fetch Next From StatementCursor Into @RankNo,@Id
Declare @rank int= 0
while @@FETCH_STATUS =0
Begin
	Update ScExamMarksEntry Set [Rank] = @RankNo Where Id = @Id
Fetch Next From StatementCursor Into @RankNo,@Id
end
CLOSE StatementCursor
DEALLOCATE StatementCursor

--Highest Marks

Declare @SubjectId int
Declare @Hth nvarchar(255)
Declare SubjectList Cursor For
Select e.SubjectId, MAX(e.TheoryObtainedMarks + e.PracticalObtainedMarks)as Hth from ScExamMarksEntry as e	where 
e.ClassId=@ClassId and e.ExamId=@ExamId	and e.AcademicYearId = @AcademicYearId  
group by e.SubjectId 
 Open SubjectList
Fetch Next From SubjectList InTo @SubjectId,@Hth
While @@FETCH_STATUS = 0
Begin			  
Declare @Ids int
			Declare MarkEntryList Cursor For
			Select em.Id from ScExamMarksEntry as em	
			where em.ClassId=@ClassId and em.AcademicYearId =@AcademicYearId and			
			em.ExamId=@ExamId	 and em.SubjectId = @SubjectId
	 
			Open MarkEntryList
			Fetch Next From MarkEntryList InTo @Ids
			While @@FETCH_STATUS = 0
			Begin
				Update ScExamMarksEntry  Set 
				HighestMarks = @Hth
				Where Id = @Ids	  
				
				Fetch Next From MarkEntryList InTo @Ids
			End
			CLOSE MarkEntryList
	DEALLOCATE MarkEntryList 
	  Fetch Next From SubjectList InTo @SubjectId,@Hth
End
		 CLOSE SubjectList
	DEALLOCATE SubjectList
GO
/****** Object:  Trigger [ScStudentSubjectMapping_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScStudentSubjectMapping_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScStudentSubjectMapping]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScStudentSubjectMapping'
                       END
GO
/****** Object:  Trigger [ScStudentFeeRateMaster_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScStudentFeeRateMaster_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScStudentFeeRateMaster]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScStudentFeeRateMaster'
                       END
GO
/****** Object:  StoredProcedure [dbo].[SP_MarkLedgerNew]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_MarkLedgerNew]
@ClassId Int,@ExamId Int,@AcademicYearId Int
as

Declare @HTML nvarchar(max)


-- Exam Cursor 
Declare @SubjectId int
Declare @Subject nvarchar(100)
Declare @ClassType int


Declare @Header1 nvarchar(max) -- Subject Header
Declare @Header2 nvarchar(max) -- FM,PM,Obt header
Declare @Header3 nvarchar(max) -- Marks Header colspan structure 3,2,2

Set @Header1 = '<table border="1"><tr><th rowspan="3">Student</th><th rowspan="3">Section</th><th rowspan="3">Roll No</th>'
Set @Header2 = '<tr>'
Set @Header3 = '<tr>'
Declare ExamSubjects Cursor For
select ms.SubjectId,s.Description as [Subject],m.ClassType from ScExamMarkSetup ms
Inner Join ScExam e on ms.ExamId = e.Id
inner join ScSubject s on ms.SubjectId = s.Id
Inner Join ScClassSubjectMapping m on ms.SubjectId = m.SubjectId
Where ms.ClassId = @ClassId and ms.ExamId = @ExamId and m.ClassId = @ClassId
Order By s.Id
Open ExamSubjects
Fetch Next From ExamSubjects Into @SubjectId,@Subject,@ClassType

Declare @Sql nvarchar(2000)
while @@FETCH_STATUS =0
Begin
	if @ClassType = 3 
	Begin
		Set @Header1 += '<th colspan="8">' + @Subject + '</th>'
		Set @Header2 += '<th colspan="3">Full Marks<br></th><th colspan="2">Pass Marks</th><th colspan="3">Obtained</th>'
		Set @Header3 +='<th>TH</th><th>PR</th><th>Total</th><th>TH</th><th>PR</th><th>TH</th><th>PR</th><th>total</th>'
	end
	else if @ClassType = 2 
	Begin
		Set @Header1 += '<th colspan="3">' + @Subject + '</th>'
		Set @Header2 += '<th >Full Marks<br></th><th>Pass Marks</th><th>Obtained</th>'
		Set @Header3 +='<th>PR</th><th>PR</th><th>PR</th>'
	End
	else if @ClassType = 1 
	Begin
		Set @Header1 += '<th colspan="3">' + @Subject + '</th>'
		Set @Header2 += '<th >Full Marks<br></th><th>Pass Marks</th><th>Obtained</th>'
		Set @Header3 +='<th>TH</th><th>TH</th><th>TH</th>'
	End
	Fetch Next From ExamSubjects Into @SubjectId,@Subject,@ClassType
End
set @Header1 += '<th rowspan="3">Full Marks</th><th rowspan="3">Obt. Marks</th><th rowspan="3">Percentage</th><th rowspan="3">Result</th><th rowspan="3">Division</th>'
Set @Header1 += '</tr>'
Set @Header2 += '</tr>'
Set @Header3 += '</tr>'

Set @HTML = @Header1 + @Header2 + @Header3

CLOSE ExamSubjects
DEALLOCATE ExamSubjects
--lets Plot the marks
	--Student Cursor
	Declare @Id int
	Declare @StudentId int
	Declare @RollNo nvarchar(255)
	Declare @Section nvarchar(10)
	Declare @Description nvarchar(255)
	Declare @TotalFullMarks decimal(18,2)
	Declare @TotalObtainedMarks decimal(18,2)
	Declare @Percentage decimal(18,2)
	Declare @Result nvarchar(255)
	Declare @Division nvarchar(255)
	
	Declare StudentCursor Cursor For
		select Distinct rd.StudentId,s.StuDesc,sec.Description ,rd.RollNo ,m.TotalFullMarks,m.TotalObtainedMarks,m.Percentage,m.Result,d.Description from ScStudentRegistration r
		Inner join ScStudentRegistrationDetail rd on r.Id = rd.RegMasterId
		Inner join ScStudentinfo s on rd.StudentId = s.StudentID
		inner join ScExamMarksEntry m on rd.StudentId = m.StudentId
		inner join ScDivision d on m.DivisionId = d.Id
		left join ScSection sec on rd.SectionId = sec.ID
		Where r.ClassId = @ClassId and m.ExamId = @ExamId and m.AcademicYearId = @AcademicYearId
	Open StudentCursor
	Fetch Next From StudentCursor Into @StudentId,@Description,@Section,@RollNo,@TotalFullMarks,@TotalObtainedMarks,@Percentage,@Result,@Division
	while @@FETCH_STATUS =0
		Begin
			Declare @row nvarchar(max)
			set @row = ''
			--select @Id,@StudentId,@Description,@RollNo
			set @row += '<tr><td>' + @Description + '</td><td>' + @Section + '</td><td>'+ CAST(@RollNo as Varchar(5)) + '</td>'
			-- Marks Data Table cursor which will plot 8 tds for above student
			-- Exam C
			--Marks Cursor
			Declare @Subject_Id int
			Declare @THFM int
			Declare @PRFM int
			Declare @TOTFM int
			Declare @THPM int
			Declare @PRPM int
			Declare @THOBT int
			Declare @PROBT int
			Declare @TOTAL int
			Declare @Class_Type int
			Declare MarkLedger Cursor For
				Select Marks.*,sm.ClassType From (
					Select ms.SubjectId, M.THFM,M.PRFM,M.TOTFM,M.THPM,M.PRPM,M.THOBT,M.PROBT,M.Total From (
					select m.SubjectId, ms.TheoryFullMark THFM,ms.PracticalFullMark PRFM ,
					(isnull(ms.TheoryFullMark,0)+ Isnull(ms.PracticalFullMark,0)) as TOTFM ,ms.TheoryPassMark as THPM,ms.PracticalPassMark as PRPM ,
					M.TheoryObtainedMarks THOBT,M.PracticalObtainedMarks PROBT,M.Total from ScExamMarksEntry M ,
					ScExamMarkSetup ms Where M.SubjectId = ms.SubjectId and m.ExamId = ms.ExamId and m.ClassId = ms.ClassId
					and M.ExamId = @ExamId and M.ClassId = @ClassId and m.StudentId = @StudentId and m.AcademicYearId = @AcademicYearId
					) M
					Right Outer Join ScExamMarkSetup ms on M.SubjectId = ms.SubjectId
					Where ms.ClassId = @ClassId and ms.ExamId = @ExamId ) Marks 
					Left Join ScClassSubjectMapping sm on Marks.SubjectId = sm.SubjectId
					Where sm.ClassId = @ClassId
					Order By Marks.SubjectId
			Open MarkLedger
			Fetch Next From MarkLedger InTo @Subject_Id,@THFM,@PRFM,@TOTFM,@THPM,@PRPM,@THOBT,@PROBT,@TOTAL,@Class_Type
			while @@FETCH_STATUS =0
			Begin
				If @Class_Type = 3	-- Both
				Begin
					Set @row += '<td>' + CAST(ISNULL(@THFM,' ') as varchar(10)) + '</td><td>' + ISNULL(CAST(@PRFM as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@TOTFM as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@THPM as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@PRPM as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@THOBT as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@PROBT as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@TOTAL as varchar(10)),' ') + '</td>'
				End
				else if @Class_Type = 2 -- Practical
				Begin
					Set @row += '<td>' + CAST(ISNULL(@PRFM,' ') as varchar(10)) + '</td><td>' + ISNULL(CAST(@PRPM as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@PROBT as varchar(10)),' ') + '</td>'
				end
				else if @Class_Type = 1	-- Theory
				Begin
					Set @row += '<td>' + CAST(ISNULL(@THFM,' ') as varchar(10)) + '</td><td>' + ISNULL(CAST(@THPM as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@THOBT as varchar(10)),' ') + '</td>'
				end
				Fetch Next From MarkLedger InTo @Subject_Id,@THFM,@PRFM,@TOTFM,@THPM,@PRPM,@THOBT,@PROBT,@TOTAL,@Class_Type 	
			End
			-- Fixed Columns Data At Last
			Set @row += '<td>' + ISNULL(CAST(@TotalFullMarks as varchar(10)),'') + '</td><td>' + ISNULL(CAST(@TotalObtainedMarks as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@Percentage as varchar(10)),' ') + '</td><td>' + ISNULL(@Result,' ') + '</td><td>' + ISNULL(@Division,' ') + '</td>'
			Set @row += '</tr>'
			set @HTML += @row
			CLOSE MarkLedger
			DEALLOCATE MarkLedger
			Fetch Next From StudentCursor Into @StudentId,@Description,@Section,@RollNo,@TotalFullMarks,@TotalObtainedMarks,@Percentage,@Result,@Division
		end
	CLOSE StudentCursor
	DEALLOCATE StudentCursor
--End Of Table
Select @HTML += '</table>'

--Select * From #MarkLedger
Select @HTML as HTML
GO
/****** Object:  StoredProcedure [dbo].[SP_MarkLedger]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_MarkLedger]
@ClassId Int,@ExamId Int,@AcademicYearId Int
as

Declare @HTML nvarchar(max)


-- Exam Cursor 
Declare @SubjectId int
Declare @Subject nvarchar(100)
Declare @ClassType int


Declare @Header1 nvarchar(max) -- Subject Header
Declare @Header2 nvarchar(max) -- FM,PM,Obt header
Declare @Header3 nvarchar(max) -- Marks Header colspan structure 3,2,2

Set @Header1 = '<table class="listpopup"><thead><tr><th width="250" rowspan="3">Student</th><th rowspan="3">Section</th><th rowspan="3">Roll No</th>'
Set @Header2 = '<tr>'
Set @Header3 = '<tr>'
Declare ExamSubjects Cursor For
select ms.SubjectId,s.Description as [Subject],m.ClassType from ScExamMarkSetup ms
Inner Join ScExam e on ms.ExamId = e.Id
inner join ScSubject s on ms.SubjectId = s.Id
Inner Join ScClassSubjectMapping m on ms.SubjectId = m.SubjectId
Where ms.ClassId = @ClassId and ms.ExamId = @ExamId and m.ClassId = @ClassId
Order By s.Id
Open ExamSubjects
Fetch Next From ExamSubjects Into @SubjectId,@Subject,@ClassType

Declare @Sql nvarchar(2000)

while @@FETCH_STATUS =0
Begin
	if @ClassType = 3 
	Begin
		Set @Header1 += '<th colspan="8">' + @Subject + '</th>'
		Set @Header2 += '<th colspan="3">Full Marks<br></th><th colspan="2">Pass Marks</th><th colspan="3">Obtained</th>'
		Set @Header3 +='<th>TH</th><th>PR</th><th>Total</th><th>TH</th><th>PR</th><th>TH</th><th>PR</th><th>total</th>'
	end
	else if @ClassType = 2 
	Begin
		Set @Header1 += '<th colspan="3">' + @Subject + '</th>'
		Set @Header2 += '<th >Full Marks<br></th><th>Pass Marks</th><th>Obtained</th>'
		Set @Header3 +='<th>PR</th><th>PR</th><th>PR</th>'
	End
	else if @ClassType = 1 
	Begin
		Set @Header1 += '<th colspan="3">' + @Subject + '</th>'
		Set @Header2 += '<th >Full Marks<br></th><th>Pass Marks</th><th>Obtained</th>'
		Set @Header3 +='<th>TH</th><th>TH</th><th>TH</th>'
	End
	Fetch Next From ExamSubjects Into @SubjectId,@Subject,@ClassType
End
set @Header1 += '<th rowspan="3">Full Marks</th><th rowspan="3">Obt. Marks</th><th rowspan="3">Percentage</th><th rowspan="3">Result</th><th rowspan="3">Division</th>'
Set @Header1 += '</tr>'
Set @Header2 += '</tr>'
Set @Header3 += '</tr></thead>'

Set @HTML = @Header1 + @Header2 + @Header3


CLOSE ExamSubjects
DEALLOCATE ExamSubjects
--lets Plot the marks
	--Student Cursor
	Declare @Id int
	Declare @StudentId int
	Declare @RollNo nvarchar(255)
	Declare @Section nvarchar(10)
	Declare @Description nvarchar(255)
	Declare @TotalFullMarks decimal(18,2)
	Declare @TotalObtainedMarks decimal(18,2)
	Declare @Percentage decimal(18,2)
	Declare @Result nvarchar(255)
	Declare @Division nvarchar(255)
	
	Declare StudentCursor Cursor For
		select Distinct rd.StudentId,s.StuDesc,IsNull(sec.Description,'') Description ,rd.RollNo ,m.TotalFullMarks,m.TotalObtainedMarks,m.Percentage,m.Result,d.Description from ScStudentRegistration r
		Inner join ScStudentRegistrationDetail rd on r.Id = rd.RegMasterId
		Inner join ScStudentinfo s on rd.StudentId = s.StudentID
		inner join ScExamMarksEntry m on rd.StudentId = m.StudentId
		left join ScDivision d on m.DivisionId = d.Id
		left join ScSection sec on rd.SectionId = sec.ID
		Where r.ClassId = @ClassId and m.ExamId = @ExamId and m.AcademicYearId = @AcademicYearId  and TotalFullMarks <> 0
	Open StudentCursor
	Fetch Next From StudentCursor Into @StudentId,@Description,@Section,@RollNo,@TotalFullMarks,@TotalObtainedMarks,@Percentage,@Result,@Division
	while @@FETCH_STATUS =0
		Begin
			Declare @row nvarchar(max)
			set @row = ''
			--select @Id,@StudentId,@Description,@RollNo
			set @row += '<tr><td>' + @Description + '</td><td>' + @Section + '</td><td>'+ CAST(@RollNo as Varchar(5)) + '</td>'
			-- Marks Data Table cursor which will plot 8 tds for above student
			-- Exam C
			--Marks Cursor
			Declare @Subject_Id int
			Declare @THFM int
			Declare @PRFM int
			Declare @TOTFM int
			Declare @THPM int
			Declare @PRPM int
			Declare @THOBT int
			Declare @PROBT int
			Declare @TOTAL int
			Declare @Class_Type int
			Declare MarkLedger Cursor For
				Select Marks.*,sm.ClassType From (
					Select ms.SubjectId, IsNull(M.THFM,0) THFM ,IsNull(M.PRFM,0) PRFM, IsNull(M.TOTFM,0) TOTFM,IsNull(M.THPM,0) THPM
					,IsNull (M.PRPM,0) PRPM,IsNull(M.THOBT,0) THOBT,IsNull(M.PROBT,0) PROBT ,IsNull(M.Total,0) Total From (
					select m.SubjectId, ms.TheoryFullMark THFM,ms.PracticalFullMark PRFM ,
					(isnull(ms.TheoryFullMark,0)+ Isnull(ms.PracticalFullMark,0)) as TOTFM ,ms.TheoryPassMark as THPM,ms.PracticalPassMark as PRPM ,
					M.TheoryObtainedMarks THOBT,M.PracticalObtainedMarks PROBT,M.Total from ScExamMarksEntry M ,
					ScExamMarkSetup ms Where M.SubjectId = ms.SubjectId and m.ExamId = ms.ExamId and m.ClassId = ms.ClassId
					and M.ExamId = @ExamId and M.ClassId = @ClassId and m.StudentId = @StudentId and m.AcademicYearId = @AcademicYearId
					) M
					Right Outer Join ScExamMarkSetup ms on M.SubjectId = ms.SubjectId
					Where ms.ClassId = @ClassId and ms.ExamId = @ExamId ) Marks 
					Left Join ScClassSubjectMapping sm on Marks.SubjectId = sm.SubjectId
					Where sm.ClassId = @ClassId
					Order By Marks.SubjectId
			Open MarkLedger
			Fetch Next From MarkLedger InTo @Subject_Id,@THFM,@PRFM,@TOTFM,@THPM,@PRPM,@THOBT,@PROBT,@TOTAL,@Class_Type
			while @@FETCH_STATUS =0
			Begin
				--Select @Subject_Id,@THFM,@PRFM,@TOTFM,@THPM,@PRPM,@THOBT,@PROBT,@TOTAL,@Class_Type
				If @Class_Type = 3	-- Both
				Begin
					Set @row += '<td>' + CAST(ISNULL(@THFM,' ') as varchar(10)) + '</td><td>' + ISNULL(CAST(@PRFM as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@TOTFM as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@THPM as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@PRPM as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@THOBT as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@PROBT as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@TOTAL as varchar(10)),' ') + '</td>'
				End
				else if @Class_Type = 2 -- Practical
				Begin
					Set @row += '<td>' + CAST(ISNULL(@PRFM,' ') as varchar(10)) + '</td><td>' + ISNULL(CAST(@PRPM as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@PROBT as varchar(10)),' ') + '</td>'
				end
				else if @Class_Type = 1	-- Theory
				Begin
					Set @row += '<td>' + CAST(ISNULL(@THFM,' ') as varchar(10)) + '</td><td>' + ISNULL(CAST(@THPM as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@THOBT as varchar(10)),' ') + '</td>'
				end
				Fetch Next From MarkLedger InTo @Subject_Id,@THFM,@PRFM,@TOTFM,@THPM,@PRPM,@THOBT,@PROBT,@TOTAL,@Class_Type 	
			End
			-- Fixed Columns Data At Last
			Set @row += '<td>' + ISNULL(CAST(@TotalFullMarks as varchar(10)),'') + '</td><td>' + ISNULL(CAST(@TotalObtainedMarks as varchar(10)),' ') + '</td><td>' + ISNULL(CAST(@Percentage as varchar(10)),' ') + '</td><td>' + ISNULL(@Result,' ') + '</td><td>' + ISNULL(@Division,' ') + '</td>'
			Set @row += '</tr>'
			set @HTML += @row
			CLOSE MarkLedger
			DEALLOCATE MarkLedger
			Fetch Next From StudentCursor Into @StudentId,@Description,@Section,@RollNo,@TotalFullMarks,@TotalObtainedMarks,@Percentage,@Result,@Division
		end
	CLOSE StudentCursor
	DEALLOCATE StudentCursor
--End Of Table
Select @HTML += '</table>'

--Select * From #MarkLedger
Select @HTML as HTML
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStudentCountByCategoryId]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_GetStudentCountByCategoryId]
as

select sc.Id,sc.Description,COUNT(si.StudentId) as [Count] from ScStudentinfo as si
join ScStudentRegistrationDetail as srd on srd.StudentId = si.StudentID
join ScStudentCategory as sc on sc.Id = si.StdCategoryId
group by sc.Id,sc.Description

--select * from ScStudentCategory
GO
/****** Object:  StoredProcedure [dbo].[SP_EmployeeSalaryStatementDetails]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_EmployeeSalaryStatementDetails]
	@EmpId int,
	@StartDate datetime ,
	@EndDate datetime ,
	@AcademicYearId int
as
		-- [SP_EmployeeSalaryStatementDetails] 1,'2013/01/05','2013/12/20',2
Declare @EmpCode nvarchar(50)
Declare @EmpName nvarchar(512)
Declare @EmpAddress nvarchar(512)
Declare @EmpDOJAD nvarchar(50)
Declare @EmpDOJNP nvarchar(50)
Declare @EmpPost nvarchar(512)
Declare @EmpDepartment nvarchar(512)
Declare @EmpType nvarchar(512)

Declare	@DateCap int 
Set @DateCap= (select DateType from SystemControl)   --	If DateType=1 then 'AD Date Else DateType=2 then 'BS Date'
				--Update SystemControl set DateType=1 
Declare EmployeeHeader Cursor for

	select EI.Id, EI.Code,EI.Description as EName,(Address + isnull(Address1,'')) as EAddress,DateOfJoin,MitiofJoin,
	EP.Description as EPost,ED.Description as EDepartment,EC.Description as EType  from ScEmployeeInfo	as EI  join ScEmployeePost	as EP On EP.Id=EI.EmployeePostId
	join ScEmployeeDepartment as ED On ED.Id=EI.EmployeeDepartmentId join ScEmployeeCategory as EC On EC.Id=EI.EmployeeCategoryId
	Where EI.Id=@EmpId	 

Open EmployeeHeader
Fetch Next From EmployeeHeader InTo @EmpId,@EmpCode,@EmpName,@EmpAddress,@EmpDOJAD,@EmpDOJNP,@EmpPost,@EmpDepartment,@EmpType

CLOSE EmployeeHeader
DEALLOCATE EmployeeHeader

Declare @Header varchar(max)
Declare @Header1 varchar(max)
Declare @Footer varchar(max)

Set @Footer = '<tr style="font-weight:bold;"><td colspan="2">Total</td>'
	 Set @Header = '<table width="100%">
	 <tr>
		 <td>Name Of Employee</td>
		 <td> : '+isNull(@EmpName,'')+'
		 </td>
		 <td>Salary Type</td>
		 <td> : '+isNull(@EmpType,'')+'</td>
	 </tr>	 
	 <tr>
		<td>Address</td>
		<td> : '+ isNull( @EmpAddress,'')+' </td>
		 <td>Date Of Joining </td>
		 <td> : '+  isNull(@EmpDOJNP,'')+' ('+  isNull(@EmpDOJAD,'')+')</td>
	 </tr>
	 <tr>
		<td>Designation</td>
		<td> : '+ isNull( @EmpPost,'')+' </td>
		 <td>Department </td>
		 <td> : '+  isNull(@EmpDepartment,'')+' </td>
	 </tr>
	 <tr>
		<td></td>
		<td></td>
		<td></td>
		<td></td>
	 </tr>
	<tr>
	</tr>
</table>'
Set @Header += '<table  width="100%" class="listpopup" border="0.5"><tr bgcolor="#E6EEEE" align="center" style="font-weight:bold">
        <th>Date</th>
        <th>Vou/Slip No.</th>
        <th colspan="2">Particulars </th>
        <th>Debit Amount</th>
		<th>Credit Amount</th>
		<th>Balance</th>'
		    
  
set @Header +='</tr>'

---- Get Salary Details  List According to Date order
Declare @VouNo nvarchar(50)
Declare @VouDate nvarchar(50)
Declare @VouMiti nvarchar(50)
Declare @Source nvarchar(50)
Declare @Particulars nvarchar(512)
Declare @Operation nvarchar(50) 
Declare @Salary Decimal(18,2)
Declare @Balance Decimal(18,2)
Declare @TotDbAmnt Decimal(18,2)
Declare @TotCrAmnt Decimal(18,2)

Declare @Row nvarchar(max)
Declare @Counter int 
Declare @DetCount int
Set @Counter = 1
Set @DetCount = 1
set @Row = ''
set @Salary = 0
set @Balance = 0
set @TotDbAmnt = 0
set @TotCrAmnt = 0

Declare SalarySumHead Cursor For
	Select EmployeeId,VNo,[Source],ISNULL(NetAmount,0) as NetPaybleSalary,CONVERT(Date, ET.CreatedDate)CreatedDate,
	(select Nep_Date from tbl_date where Eng_Date=CONVERT(Date, ET.CreatedDate)) Nep_Date from EmployeeTransaction as ET
	Inner join ScEmployeeInfo as EI On EI.Id=ET.EmployeeId 
	Where  EmployeeId=@EmpId and CONVERT(Date, ET.CreatedDate) between @StartDate and @EndDate	and ET.AcademicYearId=@AcademicYearId
	Order by EmployeeId,ET.CreatedDate
											
	Open SalarySumHead
	Fetch Next From SalarySumHead InTo @EmpId,@VouNo,@Source,@Salary,@VouDate,@VouMiti
	While @@FETCH_STATUS = 0
	Begin
		if @Source='PG'
			set @Balance +=@Salary 
		else
			if @Source='PSN'
				set @Balance -=@Salary
				
			Set @Row += '<tr  style="font-weight:bold;">'
			if @DateCap	=1
				Set @Row += '<td>' + CAST(@VouDate as varchar(15)) + '</td>'
			else
				Set @Row += '<td>' + CAST(@VouMiti as varchar(15)) + '</td>'
					
			Set @Row += '<td>' + CAST(@VouNo as varchar(50)) + '</td>'
				
			if @Source='PG'
				begin
					set @TotCrAmnt += @Salary
					Set @Row += '<td colspan="2">Net Salary</td><td Align="Right">0</td>
					<td Align="Right">' + CAST(@Salary as varchar(50)) + '</td>'
				end
			else
				if @Source='PSN'
				begin
					set @TotDbAmnt += @Salary
					Set @Row += '<td colspan="2">Paid Salary</td><td Align="Right">' + CAST(@Salary as varchar(50)) + '</td>
					<td Align="Right">0</td>'
				end
				
			if @Balance<0
				Set @Row += ' <td Align="Right">(' + CAST(abs(@Balance) as varchar(50)) + ')</td></tr>'
			else
				Set @Row += ' <td Align="Right">' + CAST(abs(@Balance) as varchar(10)) + '</td></tr>'		
			
				
	Declare SalaryDetails Cursor For 	
		Select ET.Employeeid,ET.VNo,[Source],Case When SalaryHeadId=1 Then 'Basic Salary'  When SalaryHeadId=2 Then 'Allowance'
		 When SalaryHeadId=3 Then 'Deduction'	 When SalaryHeadId=4 Then 'PF Deduction'
		 When SalaryHeadId=5 Then 'Tax Deduction'  end as SalaryHeadDesc,
		Amount,CONVERT(Date, ET.CreatedDate)CreatedDate,Operation from EmployeeTransaction as ET Inner Join PyPayrollGeneration as PG On PG.VNo=ET.VNo 
		Inner Join ScSalaryHead as SH On SH.Id= PG.SalaryHeadId where Amount>0
		and  ET.EmployeeId=@EmpId and PG.VNo=@VouNo and PG.CreatedDate between @StartDate and @EndDate
		Order by EmployeeId,ET.CreatedDate
	
		Open SalaryDetails
		Fetch Next From SalaryDetails InTo @EmpId,@VouNo,@Source,@Particulars,@Salary,@VouDate,@Operation
		While @@FETCH_STATUS = 0
		Begin
												
			Set @Row += '<tr><td colspan="2"></td><td>' + CAST(@Particulars as varchar(512)) + '</td>'
			if @Operation='-'			
				set @row += '<td Align="Right">( ' + CAST(@Salary as varchar(512)) + ')</td><td colspan="3"></td></tr>'
			else
				Set @Row += '<td Align="Right"> ' + CAST(@Salary as varchar(512)) + '</td><td colspan="3"></td></tr>'
			
			
		
		Fetch Next From SalaryDetails InTo @EmpId,@VouNo,@Source,@Particulars,@Salary,@VouDate,@Operation 		
		End
		CLOSE SalaryDetails
		DEALLOCATE SalaryDetails
		Set @DetCount +=1	
		
Fetch Next From SalarySumHead InTo @EmpId,@VouNo,@Source,@Salary,@VouDate ,@VouMiti
End
CLOSE SalarySumHead
DEALLOCATE SalarySumHead
													
	Set @Counter +=1		
Set @Row += '</tr>'
		
Set @Row += '<tr style="font-weight:bold;"><td Colspan="4" Align="Right">Total ->></td>
		<td Align="Right">' + CAST(@TotDbAmnt as varchar(10)) + '</td><td Align="Right">' + CAST(@TotCrAmnt as varchar(10)) + '</td>'
		if @Balance<0
			Set @Row += ' <td Align="Right">(' + CAST(abs(@Balance) as varchar(10)) + ')</td>'
		else
			Set @Row += ' <td Align="Right">' + CAST(abs(@Balance) as varchar(10)) + '</td>'
Set @Row += '</tr>'

set @Header += @Row 
set @Header += '</table>'

 Set @Header += '<table width="100%">
	 <tr>
		 <td></td>
		 <td></td>
		 <td></td>
		 <td></td>
	 </tr>
	 <tr>
	 </tr>
	 <tr>
	 </tr>
	 <tr>
	 </tr>	 
	 <tr>		
		<td></td>
		<td></td>
		<td></td>
		<td colspan="5" Align="Right" >--------------------------           </td>
	 </tr> 	 
	 <tr>
		<td></td>
		<td></td>
		<td></td>
		<td colspan="5" Align="Right" >Accountant Signature  '+     +'    </td>
	 </tr>	
</table>'

Set @Header = REPLACE(@Header,'.00','')

select @Header
GO
/****** Object:  StoredProcedure [dbo].[SP_EmployeeSalaryStatement]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_EmployeeSalaryStatement]
	@EmpId int,
	@StartDate datetime ,
	@EndDate datetime ,
	@AcademicYearId int
as

Declare @EmpCode nvarchar(50)
Declare @EmpName nvarchar(512)
Declare @EmpAddress nvarchar(512)
Declare @EmpDOJAD nvarchar(50)
Declare @EmpDOJNP nvarchar(50)
Declare @EmpPost nvarchar(512)
Declare @EmpDepartment nvarchar(512)
Declare @EmpType nvarchar(512)

Declare EmployeeHeader Cursor for

	select EI.Id, EI.Code,EI.Description as EName,(Address + isnull(Address1,'')) as EAddress,DateOfJoin,MitiofJoin,
	EP.Description as EPost,ED.Description as EDepartment,EC.Description as EType  from ScEmployeeInfo	as EI  join ScEmployeePost	as EP On EP.Id=EI.EmployeePostId
	join ScEmployeeDepartment as ED On ED.Id=EI.EmployeeDepartmentId join ScEmployeeCategory as EC On EC.Id=EI.EmployeeCategoryId
	Where EI.Id=@EmpId	 

Open EmployeeHeader
Fetch Next From EmployeeHeader InTo @EmpId,@EmpCode,@EmpName,@EmpAddress,@EmpDOJAD,@EmpDOJNP,@EmpPost,@EmpDepartment,@EmpType

CLOSE EmployeeHeader
DEALLOCATE EmployeeHeader

Declare @Header varchar(max)
Declare @Header1 varchar(max)
Declare @Footer varchar(max)

Set @Footer = '<tr style="font-weight:bold;"><td colspan="2">Total</td>'
	 Set @Header = '<table width="100%">
	 <tr>
		 <td>Name Of Employee</td>
		 <td> : '+isNull(@EmpName,'')+'
		 </td>
		 <td>Salary Type</td>
		 <td> : '+isNull(@EmpType,'')+'</td>
	 </tr>	 
	 <tr>
		<td>Address</td>
		<td> : '+ isNull( @EmpAddress,'')+' </td>
		 <td>Date Of Joining </td>
		 <td> : '+  isNull(@EmpDOJNP,'')+' </td>
	 </tr>
	 <tr>
		<td>Designation</td>
		<td> : '+ isNull( @EmpPost,'')+' </td>
		 <td>Department </td>
		 <td> : '+  isNull(@EmpDepartment,'')+' </td>
	 </tr>
	 <tr>
		<td></td>
		<td></td>
		<td></td>
		<td></td>
	 </tr>
	<tr>
	</tr>
</table>'
Set @Header += '<table  width="100%" class="listpopup" border="0.5"><tr bgcolor="#E6EEEE" align="center" style="font-weight:bold">
        <th>Date</th>
        <th>Vou/Slip No.</th>
        <th>Particulars </th>
        <th>Netsalary</th>
		<th>Paid Salary</th>
		<th>Salary Payble</th>
		<th>Advance Payment</th>'     
  
set @Header +='</tr>'

---- Get Salary Details  List According to Date order
Declare @VouNo nvarchar(50)
Declare @VouDate nvarchar(50)
Declare @Source nvarchar(50)
Declare @Particulars nvarchar(512)
Declare @Salary Decimal(18,2)
Declare @Balance Decimal(18,2)
Declare @PayableSalary Decimal(18,2)
Declare @PaidSalary Decimal(18,2)
Declare @SalaryPayable decimal(18,2)
declare @AdvancePayment decimal(18,2)
Declare @NetPayableSalary Decimal(18,2)
Declare @NetPaidSalary Decimal(18,2)
Declare @NetSalaryPayable decimal(18,2)
declare @NetAdvancePayment decimal(18,2)

Declare @Row nvarchar(max)
Declare @Counter int 
Set @Counter = 1
set @Row = ''
set @Particulars=''

set @Salary = 0
set @Balance = 0
set @PayableSalary = 0
set @PaidSalary = 0
set @SalaryPayable = 0
set @AdvancePayment = 0
set @NetPayableSalary = 0
set @NetPaidSalary = 0
set @NetSalaryPayable = 0
set @NetAdvancePayment = 0

 Declare SalaryDetails Cursor For
	Select EmployeeId,VNo,[Source],ISNULL(NetAmount,0) as NetPaybleSalary,CONVERT(Date, ET.CreatedDate)CreatedDate from EmployeeTransaction as ET
	Inner join ScEmployeeInfo as EI On EI.Id=ET.EmployeeId 
	Where  EmployeeId=@EmpId and CONVERT(Date, ET.CreatedDate) between @StartDate and @EndDate	and ET.AcademicYearId=@AcademicYearId

	Order by EmployeeId,ET.CreatedDate
	
--Select Employeeid,SalaryHeadId,	 Case When SalaryHeadId=1 Then 'Basic Salary'  When SalaryHeadId=2 Then 'Allowance'
--	 When SalaryHeadId=3 Then 'Deduction'	 When SalaryHeadId=4 Then 'PF Deduction'
--	 When SalaryHeadId=5 Then 'Tax Deduction'  end as SalaryHeadDesc,
--Amount from PyPayrollGeneration as PG Inner Join ScSalaryHead as SH On SH.Id= PG.SalaryHeadId where Amount>0
	
	Open SalaryDetails
	Fetch Next From SalaryDetails InTo @EmpId,@VouNo,@Source,@Salary,@VouDate
	While @@FETCH_STATUS = 0
	Begin
		set @PayableSalary = 0		
		set @PaidSalary = 0
		set @SalaryPayable = 0
		set @AdvancePayment = 0
		
		if @Source='PG'
		BEGIN
			set @PayableSalary = @Salary
			set @Particulars='Salary Payable'
		END
		else
			if @Source='PSN'
			begin
				set @PaidSalary = @Salary
				set @Particulars='Paid Salary'
			end
		set @Balance +=@PayableSalary - @PaidSalary	
		
		if @Balance>0
			set @SalaryPayable = @Balance
		else
			begin
				set @AdvancePayment = abs(@Balance)
				set @Particulars='Paid Salary and Advance'
			end
		
		set @NetPayableSalary += @PayableSalary
		set @NetPaidSalary += @PaidSalary
		set @NetSalaryPayable += @SalaryPayable
		set @NetAdvancePayment += @AdvancePayment
	
		Set @Row += '<tr><td>' + CAST(@VouDate as varchar(50)) + '</td><td>' + CAST(@VouNo as varchar(50)) + '</td>
		<td>' + CAST(@Particulars as varchar(512)) + '</td>
		<td>' + CAST(@PayableSalary as varchar(50)) + '</td><td>' + CAST(@PaidSalary as varchar(50)) + '</td>
		<td>' + CAST(@SalaryPayable as varchar(50)) + '</td><td>' + CAST(@AdvancePayment as varchar(50)) + '</td>'
	
	Fetch Next From SalaryDetails InTo @EmpId,@VouNo,@Source,@Salary,@VouDate		
	End
	CLOSE SalaryDetails
	DEALLOCATE SalaryDetails
											
	Set @Counter +=1		
Set @Row += '</tr>'
		
Set @Row += '<tr style="font-weight:bold;"><td Colspan="3" Align="Right">Total ->></td>
		<td>' + CAST(@NetPayableSalary as varchar(10)) + '</td><td>' + CAST(@NetPaidSalary as varchar(50)) + '</td>
		<td>' + CAST(@NetSalaryPayable as varchar(10)) + '</td><td>' + CAST(@NetAdvancePayment as varchar(50)) + '</td>'
Set @Row += '</tr>'

set @Header += @Row 
set @Header += '</table>'

 Set @Header += '<table width="100%">
	 <tr>
		 <td></td>
		 <td></td>
		 <td></td>
		 <td></td>
	 </tr>
	 <tr>
	 </tr>
	 <tr>
	 </tr>
	 <tr>
	 </tr>	 
	 <tr>		
		<td></td>
		<td></td>
		<td></td>
		<td colspan="5" Align="Right" >--------------------------           </td>
	 </tr> 	 
	 <tr>
		<td></td>
		<td></td>
		<td></td>
		<td colspan="5" Align="Right" >Accountant Signature  '+     +'    </td>
	 </tr>	
</table>'

Set @Header = REPLACE(@Header,'.00','')

select @Header
GO
/****** Object:  StoredProcedure [dbo].[SP_ConsolidateMarksheet]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SP_ConsolidateMarksheet 27,16,4,2	   


CREATE proc [dbo].[SP_ConsolidateMarksheet]
@StudentId int,@ClassId int,@ExamId int,@AcademicYearId int
as

Declare @ConsolidateId int
Declare @Section nvarchar(50)
Declare @RollNo nvarchar(50)
Declare @Class_Id int
Declare @Exam_Id int
Declare @Division nvarchar(255)
Declare @Percent decimal(18,2)
Declare @TotalFullMarks Decimal(18,2)
Declare @ToalObtainedMarks Decimal(18,2)
Declare @ToalObtained Decimal(18,2)
Declare @ExamTotal decimal(18,2)
declare @ExamTotalTHOBT decimal(18,2)
declare @ExamTotalPROBT decimal(18,2)
Declare @StudentName nvarchar(512)
Declare @ClassName nvarchar(512)
declare @Regno nvarchar(512)
Declare @TOTTHFM   Decimal(18,2)
Declare @TOTTHPM  Decimal(18,2)
Declare @TOTPRFM   Decimal(18,2)
Declare @TOTPRPM   Decimal(18,2)
		 Declare @TOTTHOBT   Decimal(18,2)
		 Declare @TOTPROBT   Decimal(18,2)
		 Declare @TOTALMark Decimal (18,2)
		 Declare @FullMark varchar(max)
		 Declare @TotalStudent int
		Declare @Rank varchar(max)
		Declare @Result varchar(max)	
		 
	set @TotalStudent =   (Select Distinct COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
join		ScStudentRegistration as r on rd.RegMasterId = r.Id
where r.ClassId =@ClassId)



Declare MarkHeader Cursor for

Select c.Id,c.Section,c.RollNo,c.ClassId,c.ExamId,c.Division,c.[Percent],
c.TotalFullMarks,c
.TotalObtained,s.StuDesc,cls.Description,s.Regno 
From Consolidate c
Inner Join ScStudentinfo as s on c.StudentId = s.StudentID 
Inner Join SchClass as cls on c.ClassId = cls.Id
Where c.ClassId = @ClassId And c.Examid = @ExamId And c.StudentId = @StudentId

Open MarkHeader
Fetch Next From MarkHeader InTo @ConsolidateId,@Section,@RollNo,@Class_Id,@Exam_Id,@Division,@Percent,@TotalFullMarks,@ToalObtainedMarks,@StudentName  ,@ClassName	,@Regno
--Select @ConsolidateId,@Section,@RollNo,@Class_Id,@Exam_Id,@Division,@Percent,@TotalFullMarks,@ToalObtained
CLOSE MarkHeader
DEALLOCATE MarkHeader

Declare @Header varchar(max)
Declare @Header1 varchar(max)
Declare @Footer varchar(max)

Set @Footer = '<tr style="font-weight:bold;"><td colspan="2">Total</td>'
	 Set @Header = '<table width="100%">
	 <tr>
	 <td>Reg. No.</td>
	 <td>: '+isNull(@Regno,'')+'
	 </td>
	 </tr>
	 <tr>
		<td>Name  </td>
		<td>: '+isNull(@StudentName,'')+'</td>
		<td>Class </td>
		<td> :'+isNull(@ClassName,'')+'</td>
	 </tr>
	 <tr>
		<td>Roll No.</td>
		<td> :'+ isNull( @RollNo,'')+' </td>
		 <td>Section : </td>
		 <td> :'+  isNull(@Section,'')+' </td>
	 </tr>
	 </table>'
Set @Header += '<table Border="1" width="100%" class="listpopup"><tr>
        <th rowspan="2">Sn</th>
        <th rowspan="2">Subject </th>
        <th colspan="2">Full Marks </th>
        <th colspan="2">Pass Marks </th>'
        set @Header1 = '<tr>
        <th>TH</th>
        <th>PR</th>
        <th>TH</th><th>PR</th>'
-- Get Exam List According to order
Declare @ExamDescription nvarchar(255)
Declare @ExamCode nvarchar(255)
Declare @Schedule int
set @TOTTHFM=(Select SUM(ISNULL(TheoryFullMark,0)) From ConsolidateDetail Where ConsolidateId = @ConsolidateId And ExamId = @ExamId)
			set @TOTTHPM=(Select SUM(ISNULL(TheoryPassMark,0)) From ConsolidateDetail Where ConsolidateId = @ConsolidateId And ExamId = @ExamId)
			set @TOTPRFM=(Select SUM(ISNULL(PracticalFullMark,0)) From ConsolidateDetail Where ConsolidateId = @ConsolidateId And ExamId = @ExamId)
			set @TOTPRPM=(Select SUM(ISNULL(PracticalPassMark,0)) From ConsolidateDetail Where ConsolidateId = @ConsolidateId And ExamId = @ExamId)	

								
Set @Footer += '<td>'+ CAST(@TOTTHFM as Varchar(10))+'</td><td>'+ CAST(@TOTPRFM as Varchar(10))+'</td><td>'+ CAST(@TOTTHPM as Varchar(10))+'</td><td>'+ CAST(@TOTPRPM as Varchar(10))+'</td>'

Declare ExamHeader Cursor For
	Select Distinct CD.ExamId,E.Description,E.Code ,E.Schedule 
	From ConsolidateDetail CD
	Inner Join ScExam E  On CD.ExamId = E.Id
	Where ConsolidateId = @ConsolidateId  Order By E.Schedule	
	Open ExamHeader
Fetch Next from ExamHeader Into @Exam_Id,@ExamDescription,@ExamCode,@Schedule
While @@FETCH_STATUS = 0
Begin
	Set @Header += '<th colspan="2">' + @ExamDescription + '</th>'
		Set @ExamTotalTHOBT = (Select SUM(IsNull(d.TheoryObtainedMark,0)) ObtainedMarks from Consolidate c
		Inner join ConsolidateDetail d on c.Id = d.ConsolidateId
		Where ConsolidateId = @ConsolidateId and c.StudentId = @StudentId and ClassId = @ClassId and d.ExamId = @Exam_Id
		Group By d.ExamId)
		Set @ExamTotalPROBT = (Select SUM(IsNull(d.PracticalObtainedMark,0)) ObtainedMarks from Consolidate c
		Inner join ConsolidateDetail d on c.Id = d.ConsolidateId
		Where ConsolidateId = @ConsolidateId and c.StudentId = @StudentId and ClassId = @ClassId and d.ExamId = @Exam_Id
		Group By d.ExamId)
	Set @Footer += '<td>' + CAST(@ExamTotalTHOBT as Varchar(10)) + '</td><td>' + CAST(@ExamTotalPROBT as Varchar(10)) + '</td>'
	
	set @Header1 +='<th>TH</th><th>PR</th>'
	Fetch Next from ExamHeader Into @Exam_Id,@ExamDescription,@ExamCode,@Schedule
End

set @Header += '<th rowspan="2">Total </th><th rowspan="2">Highest Marks</th></tr>'
set @Header1 +='</tr>'
set @Header += @Header1
CLOSE ExamHeader
DEALLOCATE ExamHeader
-- Start getting the subjects for param student consolidate
Declare @SubjectId int
Declare @SubjectDescription nvarchar(255)
Declare @Row nvarchar(max)
Declare @FullMarks Decimal(18,2)
Declare @PassMarks Decimal(18,2)
Declare @THFM   Decimal(18,2)
Declare @THPM Decimal(18,2)
Declare @PRFM Decimal(18,2)
Declare @PRPM Decimal(18,2)
Declare @HIGESTMARKS	Decimal(18,2)
Declare @THOBT Decimal(18,2)
Declare @PROBT Decimal(18,2)

Declare @Counter int
Set @Counter = 1
Declare @ObtMarks Decimal(18,2)

Declare @TotalPassMark decimal(18,2)


set @Row = ''
set @TotalPassMark = 0

Declare StudentSubjects Cursor For
	Select Distinct CD.SubjectId,s.Description,s.Schedule From ConsolidateDetail CD
	Inner Join ScSubject s on CD.SubjectId = s.Id
	Where ConsolidateId = @ConsolidateId Order By Schedule
	Open StudentSubjects
	Fetch Next From StudentSubjects Into @SubjectId,@SubjectDescription,@Schedule
	While @@FETCH_STATUS = 0
	Begin
		Set @ToalObtained = 0
		Set @Row += '<tr><td>' + CAST(@Counter as Varchar(5)) + '</td><td>' + CAST( @SubjectDescription as varchar(max)) + '</td>'
	
		Set @THFM = (Select ISNULL(TheoryFullMark,0) From ConsolidateDetail Where ConsolidateId = @ConsolidateId And ExamId = @ExamId And SubjectId = @SubjectId)
		Set @PRFM = (Select ISNULL(PracticalFullMark,0) From ConsolidateDetail Where ConsolidateId = @ConsolidateId And ExamId = @ExamId And SubjectId = @SubjectId)
		Set @THPM = (Select ISNULL(TheoryPassMark,0) From ConsolidateDetail Where ConsolidateId = @ConsolidateId And ExamId = @ExamId And SubjectId = @SubjectId)
		Set @PRPM = (Select ISNULL(PracticalPassMark,0) From ConsolidateDetail Where ConsolidateId = @ConsolidateId And ExamId = @ExamId And SubjectId = @SubjectId)
		--Set @PassMarks = (Select ISNULL(PassMarks,0) From ConsolidateDetail Where ConsolidateId = @ConsolidateId And ExamId = @ExamId And SubjectId = @SubjectId)
		Set @HIGESTMARKS = (Select ISNULL(HighestMarks,0) From ConsolidateDetail Where ConsolidateId = @ConsolidateId And ExamId = @ExamId And SubjectId = @SubjectId)
		Set @Row += '<td>' + CAST(@THFM as Varchar(10)) + '</td>
					<td>' + CAST(@PRFM as Varchar(10)) + '</td>
					<td>' + CAST(@THPM as Varchar(10)) + '</td>
					<td>' + CAST(@PRPM as Varchar(10)) + '</td>'
																		
		Declare StudentExams Cursor For
			Select Distinct CD.ExamId,E.Description,E.Code ,E.Schedule From ConsolidateDetail CD
			Inner Join ScExam E  On CD.ExamId = E.Id
			Where ConsolidateId = @ConsolidateId  Order By E.Schedule
			Open StudentExams
		Fetch Next From StudentExams InTo @Exam_Id,@ExamDescription,@ExamCode,@Schedule
		While @@FETCH_STATUS = 0
		Begin
			set @THOBT = (Select ISNULL(TheoryObtainedMark,0) From ConsolidateDetail Where ConsolidateId = @ConsolidateId and ExamId = @Exam_Id and SubjectId = @SubjectId)
			set @PROBT = (Select ISNULL(PracticalObtainedMark,0) From ConsolidateDetail Where ConsolidateId = @ConsolidateId and ExamId = @Exam_Id and SubjectId = @SubjectId)
			
			Set @TOTTHOBT += @THOBT 
			Set @TOTPROBT+= @PROBT
			Set @ToalObtained +=  ISNULL(@THOBT,0) + ISNULL(@PROBT,0)
		
			Set @Row += '<td>' + CAST(@THOBT as varchar(10)) + '</td><td>' + CAST(@PROBT as varchar(10)) + '</td>'
		
			Fetch Next From StudentExams InTo @Exam_Id,@ExamDescription,@ExamCode,@Schedule
		End
									
		Set @Row +='<td>' + CAST(@ToalObtained as Varchar(20)) + '</td><td>' + CAST(@HIGESTMARKS as Varchar(20)) + '</td>'
		CLOSE StudentExams
		DEALLOCATE StudentExams		
		Set @Counter +=1
		Fetch Next From StudentSubjects Into @SubjectId,@SubjectDescription,@Schedule
	End
	CLOSE StudentSubjects
	DEALLOCATE StudentSubjects
	
	Set @Row += '</tr>'
	
	--Set @Footer = REPLACE(@Footer,'@TOTTHFM',ISNULL(@TOTTHFM,0))
	--Set @Footer = REPLACE(@Footer,'@TOTPRFM',ISNULL(@TOTPRFM,0))
	--Set @Footer = REPLACE(@Footer,'@TOTTHPM',ISNULL(@TOTTHPM,0))
	--Set @Footer = REPLACE(@Footer,'@TOTPRPM',ISNULL(@TOTPRPM,0))
set	@TOTALMark =  (Select ISNULL(TotalObtained,0) From Consolidate Where Id = @ConsolidateId)
set	@Rank =  (Select [Rank] From Consolidate Where Id = @ConsolidateId)
set	@Result =  (Select Result From Consolidate Where Id = @ConsolidateId)
	Set @Footer += '<td>'+CAST(@TOTALMark as varchar(10))+'</td>'
				
	set @Row += @Footer

set @Header += @Row
set @Header += '</table>'
 Declare  @DivisionHeader    nvarchar(max)
Set @DivisionHeader = '<br/>
 <div style="width: 100%">
                <div style="border: 1px solid; width: 30%; padding: 10px; margin-top: 10px; float: left">
                    <table style="width: 100%;">
                        <tr>
                            <th colspan="2">
                                DIVISION SYSTEM
                            </th>
                        </tr>                       '
Declare	@DivisionDesc varchar(max)
Declare @PercentageFrom varchar(max)
Declare @PercentageTo varchar(max)
Declare DivisionCursor Cursor For
Select d.Description , 
CAST( d.PercentageFrom as varchar(max)) ,
cast (d.PercentageTo as varchar(max))  
from ScDivision as d Order By	 d.Schedule
		  open DivisionCursor
	
		Fetch Next From DivisionCursor InTo @DivisionDesc,@PercentageFrom,@PercentageTo
		While @@FETCH_STATUS = 0
		Begin
		 set @DivisionHeader += '<tr><td>
                                '+ @DivisionDesc +'
                            </td>
                            <td>
                                '+ CAST( @PercentageFrom AS vARCHAR(MAX)) +'% - ' + CAST( @PercentageTo AS vARCHAR(MAX))+	'%
                            </td>
                        </tr>  '
             
			Fetch Next From DivisionCursor InTo @DivisionDesc,@PercentageFrom,@PercentageTo
		End   
	CLOSE DivisionCursor
	DEALLOCATE DivisionCursor	
		set @DivisionHeader += '</table></div>
                
                
                <div style="border: 1px solid; width: 30%; padding: 10px; margin-top: 10px; float: right">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                Result :  '+CAST( @Result as varchar(max))+'
                            </td>
                            <td>
                                Percentage : '+cast(ISNULL( @Percent,'')as varchar(max))+'
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Division :
                                '+CAST(@Division  as varchar(max))+'
                            </td>
                            <td>
                                Rank :	'+CAST( ISNULL(@Rank,'') as varchar(max))+'
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Total No of the Students in the Class:'+CAST( @TotalStudent as varchar(25))+'	

                            </td>
                        </tr>
                        <tr>
                            <td>
                                Remark
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="clearfix">
            </div>
            <br />
            <div style="border-top: 1px solid; text-align: center; width: 170px;
                margin-top: 80px; display:inline-block;">
                PRINCIPAL</div>	<div style="border-top: 1px solid; text-align: center; width: 170px;
                margin-top: 80px; float:right;">
                CLASS TEACHER</div> '
               Set @Header +=@DivisionHeader
Set @Header = REPLACE(@Header,'.00','')
Select @Header
GO
/****** Object:  StoredProcedure [dbo].[SP_ConsolidateLedger]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SP_ConsolidateLedger @ClassId=16,@ExamId=4,@ConsolidateCode='FR-11.',@AcademicYearId=2

CREATE proc [dbo].[SP_ConsolidateLedger]
	@ClassId Int,@ExamId Int,@ConsolidateCode nvarchar(255), @AcademicYearId Int
as
Declare @HTML nvarchar(max)


Declare @Header1 nvarchar(max) -- Subject Header
Declare @Header2 nvarchar(max) -- Exam Header
Declare @Header3 nvarchar(max) -- FM,PM,Obt header
Declare @Header4 nvarchar(max) -- Marks Header colspan structure 3,2,2

Set @Header1 = '<table class="listpopup" border="1"><thead><tr align="center"><th width="25%" rowspan="4">Student</th><th rowspan="4">Section</th><th rowspan="4">Roll No</th>'
Set @Header2 = '<tr>'
Set @Header3 = '<tr>'
Set @Header4 = '<tr>'

Declare @SubjectId int
Declare @Subject nvarchar(255)
Declare @ClassType int

Declare @HCS1 int
Declare @HCS2 int
Declare @HCS3 int
Declare @HCS4 int
				  Declare @StudentOutOff int


Set @StudentOutOff = (Select Distinct COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
join		ScStudentRegistration as r on rd.RegMasterId = r.Id
where r.ClassId =@ClassId and r.AcademicYearId =@AcademicYearId)
Declare ConsolidateSubjects Cursor For
	Select Distinct ms.SubjectId,s.Description as [Subject],m.ClassType From ScConsolidatedMarksSetup  cms
	Inner join ScExamMarkSetup ms on cms.ExamGrdId = ms.ExamId
	inner join ScSubject s on ms.SubjectId = s.Id
	Inner Join ScClassSubjectMapping m on ms.SubjectId = m.SubjectId
	Where cms.ClassId = @ClassId  and cms.Code = @ConsolidateCode and ms.ClassId = @ClassId and m.ClassId = @ClassId
	Order by ms.SubjectId
OPEN ConsolidateSubjects
-- Part of ConsolidateSubjects cursor 
	Fetch Next From ConsolidateSubjects INTO @SubjectId,@Subject,@ClassType
	while @@FETCH_STATUS =0
	BEGIN
		
		--Set @ClassType = 3 -- Delete this 
		if @ClassType = 3 
		Begin
			Set @HCS1 = 6
			Set @HCS3 = 0
			Set @Header1 += '<th colspan="@HCS1">' + @Subject + '</th>'
			set @Header2 += '<th colspan="3" rowspan="2">Full Marks</th><th colspan="2" rowspan="2">Pass marks</th>'
			Set @Header4 += '<th>TH</th><th>PR</th><th>TOT</th><th>TH</th><th>PR</th>'
		End
		Else
		Begin
			Set @HCS1 = 3
			Set @HCS3 = 0
			Set @Header1 += '<th colspan="@HCS1">' + @Subject + '</th>'
			set @Header2 += '<th colspan="1" rowspan="2">Full Marks</th><th colspan="1" rowspan="2">Pass marks</th>'
			if @ClassType = 1
			Begin
				Set @Header4 += '<th>TH</th><th>TH</th>'
			End
			else
			Begin
				Set @Header4 += '<th>PR</th><th>PR</th>'
			End
		End
		
	--	Exam Cursor
	Declare @CMS_ID int
	Declare @CMS_Exam_Id int
	Declare @CMS_ExamDescription nvarchar(255)
		Declare Exam_Cursor Cursor For
			Select cms.Id as CmsID ,cms.ExamGrdId as ExamID , 
			E.Description + ' (' + Replace(Cast(cms.Percentage as varchar(10)),'.00','') + '%)' as [ExamDescription] 
			From ScConsolidatedMarksSetup cms
			inner join ScExam E on cms.ExamGrdId = E.Id
			Where cms.Code = @ConsolidateCode and cms.ExamId = @ExamId
			Order by cms.ExamOrder
		OPEN Exam_Cursor
		Fetch Next From Exam_Cursor InTo @CMS_ID,@CMS_Exam_Id,@CMS_ExamDescription
		While @@Fetch_Status = 0
		Begin
		if @ClassType = 3 
		Begin
			Set @HCS1 += 3
			Set @HCS3 += 3
			Set @Header2 += '<th colspan="3">' + @CMS_ExamDescription +'</th>'
			Set @Header4 += '<th>TH</th><th>PR</th><th>TOT</th>'
		end
		Else
		Begin
			Set @HCS1 += 1
			Set @HCS3 += 1
			Set @Header2 += '<th colspan="1">' + @CMS_ExamDescription +'</th>'
			
			if @ClassType = 1
			Begin
				Set @Header4 += '<th>TH</th>'
			End
			else
			Begin
				Set @Header4 += '<th>PR</th>'
			End
			
		End
			Fetch Next From Exam_Cursor InTo @CMS_ID,@CMS_Exam_Id,@CMS_ExamDescription
		End
		Set @Header2 += '<th rowspan="3">Total</th>'
		Set @Header1 = REPLACE(@Header1,'@HCS1',Cast(@HCS1 as varchar(500)))
		Close Exam_Cursor
		Deallocate Exam_Cursor
	--------------------------------------------------------------------------
		set @Header3 += '<th colspan="' + Cast(@HCS3 as varchar(500)) + '">Obtained Marks<br></th>'
		--Select @Header2
		Fetch Next From ConsolidateSubjects INTO @SubjectId,@Subject,@ClassType
	END
	Set @Header1 += '<th rowspan="4">Total FM</th>'
	Set @Header1 += '<th rowspan="4">Total PM</th>'
	Set @Header1 += '<th rowspan="4">Total OM</th>'
	Set @Header1 += '<th rowspan="4">Percent</th>'
	Set @Header1 += '<th rowspan="4">Result</th>'
	Set @Header1 += '<th rowspan="4">Division</th>'
	Set @Header1 += '<th rowspan="4">Rank</th>'
	Set @Header1 += '<th rowspan="4">Out Off</th>'
	-- Last Column Headers
	
Close ConsolidateSubjects
Deallocate ConsolidateSubjects

set @Header1 += '</tr>'
set @Header2 += '</tr>'
set @Header3 += '</tr>'
set @Header4 += '</tr>'

Set @HTML = @Header1 + @Header2 + @Header3 + @Header4

--lets Plot the marks
	--Student Cursor
	Declare @Id int
	Declare @StudentId int
	Declare @RollNo nvarchar(255)
	Declare @Section nvarchar(10)
	Declare @Description nvarchar(255)
	Declare @TotalFullMarks decimal(18,2)
	Declare @TotalPassMarks decimal(18,2)
	Declare @TotalObtainedMarks decimal(18,2)
	Declare @Percentage decimal(18,2)
	Declare @Result nvarchar(255)
	Declare @Division nvarchar(255)
	Declare @ConsolidateId int
	Declare @Rank nvarchar(255)
	Declare @OutOf nvarchar(255)
	Declare StudentCursor Cursor For
		select Distinct rd.StudentId,s.StuDesc,IsNull(sec.Description,'') Description ,rd.RollNo ,m.TotalFullMarks,
		m.TotalObtainedMarks,m.Percentage,m.Result,d.Description,m.OutOf from ScStudentRegistration r
		Inner join ScStudentRegistrationDetail rd on r.Id = rd.RegMasterId
		inner join ScStudentinfo s on rd.StudentId = s.StudentID
		left join ScExamMarksEntry m on rd.StudentId = m.StudentId
		left join ScDivision d on m.DivisionId = d.Id
		left join ScSection sec on rd.SectionId = sec.ID
		Where r.ClassId = @ClassId and m.ExamId = @ExamId and m.AcademicYearId = @AcademicYearId and  r.AcademicYearId =@AcademicYearId
		Order By rd.RollNo
	Open StudentCursor
	Fetch Next From StudentCursor Into @StudentId,@Description,@Section,@RollNo,@TotalFullMarks,@TotalObtainedMarks,
	@Percentage,@Result,@Division ,@OutOf
	while @@FETCH_STATUS =0
		Begin
			Declare @row nvarchar(max)
			set @TotalFullMarks = 0
			set @TotalPassMarks = 0
			set @TotalObtainedMarks = 0
			set @row = ''
			--select @Id,@StudentId,@Description,@RollNo
			set @row += '<tr class=''Student-'+CAST( @StudentId as varchar(max))+'''><td >' + @Description + '</td><td>' + IsNull(@Section,'') + '</td><td>'+ CAST(@RollNo as Varchar(5)) + '</td>'
			-- Insert To Consolidate Master this will help to create consolidate marksheet
			--	Delete Previous Configuration			 
			Delete From Consolidate Where StudentId = @StudentId And ClassId = @ClassId And ExamId = @ExamId And AcademicYearId=@AcademicYearId AND ConsolidateCode = @ConsolidateCode
			
			Insert Into Consolidate (StudentId,Section,RollNo,ClassId,ExamId,AcademicYearId,ConsolidateCode) 
			Values (@StudentId,@Section,@RollNo,@ClassId,@ExamId,@AcademicYearId,@ConsolidateCode)
			
			Set @ConsolidateId = (Select Id From Consolidate Where Id = @@IDENTITY)
				Declare MarkSubjects Cursor For
					Select Distinct ms.SubjectId,s.Description as [Subject],m.ClassType From ScConsolidatedMarksSetup  cms
					Inner join ScExamMarkSetup ms on cms.ExamGrdId = ms.ExamId
					inner join ScSubject s on ms.SubjectId = s.Id
					Inner Join ScClassSubjectMapping m on ms.SubjectId = m.SubjectId
					Where cms.ClassId = @ClassId  and cms.Code = @ConsolidateCode and ms.ClassId = @ClassId and m.ClassId = @ClassId
					Order by ms.SubjectId
					Open MarkSubjects
					Fetch Next From MarkSubjects INTO @SubjectId,@Subject,@ClassType
					While @@FETCH_STATUS =0
					BEGIN
						-- Get Full Marks and pass marks for exam passed in parameter
						Declare @Subject_Id int
						Declare @THFM int
						Declare @PRFM int
						Declare @TOTFM int
						Declare @THPM int
						Declare @PRPM int
						Declare @THOBT int
						Declare @PROBT int
						Declare @TOTAL int
						Declare @Class_Type int 
						Declare @ConsolidateDetailId int
						
						Declare ExamParamMark Cursor For
							Select Marks.*,sm.ClassType From (
							Select ms.SubjectId, IsNull(M.THFM,0) THFM ,IsNull(M.PRFM,0) PRFM, IsNull(M.TOTFM,0) TOTFM,IsNull(M.THPM,0) THPM
							,IsNull (M.PRPM,0) PRPM,IsNull(M.THOBT,0) THOBT,IsNull(M.PROBT,0) PROBT ,IsNull(M.Total,0) Total From (
							select m.SubjectId, ms.TheoryFullMark THFM,ms.PracticalFullMark PRFM ,
							(isnull(ms.TheoryFullMark,0)+ Isnull(ms.PracticalFullMark,0)) as TOTFM ,ms.TheoryPassMark as THPM,ms.PracticalPassMark as PRPM ,
							M.TheoryObtainedMarks THOBT,M.PracticalObtainedMarks PROBT,M.Total from ScExamMarksEntry M ,
							ScExamMarkSetup ms Where M.SubjectId = ms.SubjectId and m.ExamId = ms.ExamId and m.ClassId = ms.ClassId
							and M.ExamId = @ExamId and M.ClassId = @ClassId and m.StudentId = @StudentId and m.AcademicYearId = @AcademicYearId
							) M
							Right Outer Join ScExamMarkSetup ms on M.SubjectId = ms.SubjectId
							Where ms.ClassId = @ClassId and ms.ExamId = @ExamId ) Marks 
							Left Join ScClassSubjectMapping sm on Marks.SubjectId = sm.SubjectId
							Where sm.ClassId = @ClassId and Marks.SubjectId = @SubjectId
							Order By Marks.SubjectId
						Open ExamParamMark
						Fetch Next From ExamParamMark Into @Subject_Id,@THFM,@PRFM,@TOTFM,@THPM,@PRPM,@THOBT,@PROBT,@TOTAL,@Class_Type
						set @TotalFullMarks  += @TOTFM
										  set @TotalPassMarks +=@THPM +	@PRPM
						If @Class_Type = 3
						Begin
							Set @row += '<td>' + Cast(@THFM as varchar(5)) +'</td><td>' + Cast(@PRFM as varchar(5)) + '</td><td>' + Cast(@TOTFM as varchar(5)) + '</td><td>' + Cast(@THPM as varchar(5)) + '</td><td>' + Cast(@PRPM as varchar(5)) + '</td>'
						End
						Else
						Begin
							if @Class_Type = 1
							Begin
								Set @row += '<td>' + Cast(@THFM as varchar(5)) + '</td><td>' + Cast(@THPM as varchar(5)) + '</td>'
							end
							else
							Begin
								Set @row += '<td>' + Cast(@PRFM as varchar(5)) + '</td><td>' + Cast(@PRPM as varchar(5)) + '</td>'
							End
						End
						--Get ExamMarks For Each Exam Defined in Param Consolidate Exam
						Declare @Exam_GrdId int
						Declare @ConsTotal decimal(18,2)
						set @ConsTotal = 0
						Declare ExamMarks Cursor For
							Select cms.ExamGrdId as ExamID , IsNull(cms.Percentage,0) Percentage From ScConsolidatedMarksSetup cms
							inner join ScExam E on cms.ExamGrdId = E.Id Where cms.Code = @ConsolidateCode and cms.ExamId = @ExamId
							Order by cms.ExamOrder
						Open ExamMarks
						Fetch Next From ExamMarks InTo @Exam_GrdId,@Percentage
						While @@FETCH_STATUS = 0
						Begin
							Set @Percentage = @Percentage /100
							-- Marks Cursor
							Declare GetMarksOfStudent Cursor For
							Select Marks.*,sm.ClassType From (
							Select ms.SubjectId,IsNull(M.THOBT,0) THOBT,IsNull(M.PROBT,0) PROBT ,IsNull(M.Total,0) Total From (
							select m.SubjectId, ms.TheoryFullMark THFM,ms.PracticalFullMark PRFM ,
							(isnull(ms.TheoryFullMark,0)+ Isnull(ms.PracticalFullMark,0)) as TOTFM ,ms.TheoryPassMark as THPM,ms.PracticalPassMark as PRPM ,
							M.TheoryObtainedMarks THOBT,M.PracticalObtainedMarks PROBT,M.Total from ScExamMarksEntry M ,
							ScExamMarkSetup ms Where M.SubjectId = ms.SubjectId and m.ExamId = ms.ExamId and m.ClassId = ms.ClassId
							and M.ExamId = @Exam_GrdId and M.ClassId = @ClassId and m.StudentId = @StudentId and m.AcademicYearId = @AcademicYearId
							) M
							Right Outer Join ScExamMarkSetup ms on M.SubjectId = ms.SubjectId
							Where ms.ClassId = @ClassId and ms.ExamId = @Exam_GrdId ) Marks 
							Left Join ScClassSubjectMapping sm on Marks.SubjectId = sm.SubjectId
							Where sm.ClassId = @ClassId and Marks.SubjectId = @SubjectId
							Order By Marks.SubjectId
							Open GetMarksOfStudent
							Fetch Next From GetMarksOfStudent InTo @SubjectId,@THOBT,@PROBT,@TOTAL,@ClassType
							Set @ConsTotal += @TOTAL * @Percentage
							set @TotalObtainedMarks += @TOTAL * @Percentage
							  
							if @ClassType = 3
							Begin
								Set @row += '<td>' + Cast(@THOBT * @Percentage as varchar(5)) + '</td><td>' + Cast(@PROBT * @Percentage as varchar(5)) + '</td><td>' + Cast(@TOTAL * @Percentage as varchar(5)) + '</td>'
								Insert Into ConsolidateDetail (
								ConsolidateId,SubjectId,FullMarks,PassMarks,ObtainedMarks,ExamId,TheoryFullMark,PracticalFullMark,TheoryPassMark,PracticalPassMark,TheoryObtainedMark,PracticalObtainedMark)
								 Values (@ConsolidateId,@Subject_Id,@TOTFM,@THPM+@PRPM,@TOTAL * @Percentage,@Exam_GrdId,@THFM,@PRFM,@THPM,@PRPM,@THOBT * @Percentage,@PROBT * @Percentage)
							End
							Else
							Begin
								if @ClassType = 1
								Begin
									Set @row += '<td>' + Cast(@THOBT * @Percentage as varchar(5)) + '</td>'
									Insert Into ConsolidateDetail (ConsolidateId,SubjectId,FullMarks,PassMarks,ObtainedMarks,ExamId,TheoryFullMark,PracticalFullMark,TheoryPassMark,PracticalPassMark,TheoryObtainedMark,PracticalObtainedMark) Values 
									(@ConsolidateId,@Subject_Id,@TOTFM,@THPM,@TOTAL * @Percentage,@Exam_GrdId,@THFM,0,@THPM,0,@THOBT * @Percentage,0)
								End
								Else
								Begin
									Set @row += '<td>' + Cast(@PROBT * @Percentage as varchar(5)) + '</td>'
									Insert Into ConsolidateDetail (ConsolidateId,SubjectId,FullMarks,PassMarks,ObtainedMarks,ExamId,TheoryFullMark,PracticalFullMark,TheoryPassMark,PracticalPassMark,TheoryObtainedMark,PracticalObtainedMark)
									 Values (@ConsolidateId,@Subject_Id,@TOTFM,@PRPM,@TOTAL * @Percentage,@Exam_GrdId,0,@PRFM,0,@PRPM,0,@PROBT * @Percentage)
								End
							End
							Set @ConsolidateDetailId = (Select Id From ConsolidateDetail Where Id =@@Identity)
							CLOSE GetMarksOfStudent
							DEALLOCATE GetMarksOfStudent
							-- End Pf Getmarks
							--Update ConsolidateDetail Set ExamId = @Exam_GrdId Where Id = @ConsolidateDetailId

							--
							Fetch Next From ExamMarks InTo @Exam_GrdId,@Percentage
						End	
						CLOSE ExamMarks
						DEALLOCATE ExamMarks
						Set @row += '<td>' + Cast(@ConsTotal  as varchar(50)) + '</td>'					
						-- End of ExamMarks
						CLOSE ExamParamMark
						DEALLOCATE ExamParamMark
						Fetch Next From MarkSubjects INTO @SubjectId,@Subject,@ClassType
					End
				CLOSE MarkSubjects
				DEALLOCATE MarkSubjects
		

						  if @TotalPassMarks < @TotalObtainedMarks 
							  Begin
									set @Result = 'Pass'
							  End  else	   Begin
							  set @Result = 'Fail'
							  End
											
			Set @row += '<td>' + Cast(@TotalFullMarks as varchar(100)) + '</td><td>' + Cast(@TotalPassMarks as varchar(100)) + '</td>'
			Set @row += '<td>' + Cast(@TotalObtainedMarks as varchar(100)) + '</td>'
			set @row += '<td>' + Cast( Cast((@TotalObtainedMarks / @TotalFullMarks) * 100 as decimal(18,2)) as varchar(100)) + '</td>'
			Set @row += '<td>' + @Result + '</td>'
			if @Result ='Pass'
			Begin
			Set @Division = (Select Top 1 Description from ScDivision where PercentageFrom <(@TotalObtainedMarks / @TotalFullMarks) * 100  and PercentageTo > (@TotalObtainedMarks / @TotalFullMarks) * 100)
			End else Begin Set @Division ='Fail'   End
			Set @row += '<td>' + ISNULL(@Division,'') + '</td>'
			set @row += '<td class="studentrank"></td>'
			
							 if @StudentOutOff = 0
							Begin
								set @row += '<td>'+	 ''  +'</td>'
							End				  else
							Begin
										 set @row += '<td>'+ CAST( @StudentOutOff as varchar(50))  +'</td>'
							End
							 
							 
			
			Set @row = REPLACE(@row,'.00','')
			Set @row += '</tr>'
			set @HTML += @row
			
			Update Consolidate Set 
			Division =@Division,
			[Percent] = 	Cast((@TotalObtainedMarks / @TotalFullMarks) * 100 as decimal(18,2)),
			TotalFullMarks = @TotalFullMarks,TotalObtained=@TotalObtainedMarks,
			Result = @Result ,
			OutOff = @StudentOutOff
			Where Id = @ConsolidateId
			
			
			
			

	
			
			Fetch Next From StudentCursor Into @StudentId,@Description,@Section,@RollNo,@TotalFullMarks,@TotalObtainedMarks,@Percentage,@Result,@Division,@OutOf
		end
	Exec Sp_ConsolidateUpateRank @ExamId , @ClassId,@AcademicYearId
	CLOSE StudentCursor
	DEALLOCATE StudentCursor
	
	



	
Set @HTML += '</table>'
Select @HTML
GO
/****** Object:  StoredProcedure [dbo].[SP_Consolidate]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_Consolidate]
as
Create Table #Consolidate
(
Id int identity(1,1) Primary Key,
StudentId int,
[Description] nvarchar(255),
RollNo int ,
ClassId int
)
Insert into #Consolidate (StudentId,Description,RollNo,ClassId)
select rd.StudentId,s.StuDesc,rd.RollNo,r.ClassId from ScStudentRegistration r
Inner join ScStudentRegistrationDetail rd on r.Id = rd.RegMasterId
Inner join ScStudentinfo s on rd.StudentId = s.StudentID
Where r.ClassId = 65

Select * From #Consolidate
Drop table #Consolidate
GO
/****** Object:  StoredProcedure [dbo].[SP_CategoryWise_StudentTotal]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_CategoryWise_StudentTotal]
	@AgeList varchar(max)='',
	@ClassList varchar(max)='',
	@CategoryList varchar(max)='',
	@ACYId int
as	  
--SP_CategoryWise_StudentTotal  '0','1,2,5,6,11,12,13,14,15,16,18','1',2		
--Declare @CategoryList varchar(max)
--set @CategoryList = '1,2,3,4,5'
--Select Distinct id,Description From ScStudentCategory 
--Where Id in (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@CategoryList, ','))

 Declare @sql varchar(max)
 Declare @ClassName nvarchar(512)
 Declare @TotalStudent int
 Declare @TotalBoys int
 Declare @TotalGirl int 
 Declare @Totalcat int
 
Declare @Footer varchar(max)
Declare @Header varchar(max)
Declare @Header1 varchar(max)
Declare @Header2 varchar(max)
declare @CatCounter int

Set @Footer = '<tr style="font-weight:bold;"><td colspan="2">Total</td>'
	 Set @Header = '<table width="100%">	 
	 </table>'
 set @CatCounter=1
 
--Declare @sql varchar(max)
--select @sql = '(Select COUNT(id)*3 from( Select Distinct id,Description From ScStudentCategory Where Id in ('''' + @CategoryList + '''') ) as aa) '
--select (@sql)
--exec (@sql)
set @Totalcat = (Select COUNT(id)*3 from( Select Distinct id,Description From ScStudentCategory Where Id in (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@CategoryList, ','))) as aa)
Set @Header += '<table  width="100%" class="listpopup" border="0.5">
<tr bgcolor="#E6EEEE" align="center" style="font-weight:bold">
        <th rowspan="3">Sn</th>  
        <th rowspan="3">Class</th>
        <th rowspan="3">Age </th>         
        <th colspan="3">Mahal No. '+ CAST(@CatCounter as Varchar(10)) +' </th>'     
        set @Header1 = '<tr bgcolor="#E6EEEE" align="center" style="font-weight:bold">
        <th colspan="3">Total Student</th>'
        
       set @Header2 = '<tr bgcolor="#E6EEEE" align="center" style="font-weight:bold">
       <th>Boys</th><th>Girls</th><th>Total</th>'
     
-- Get Subject List According to order
Declare @CatDescription nvarchar(255)
Declare @CatId nvarchar(255)
Set @CatCounter +=1

Declare CategoryHeader Cursor For	
Select Distinct id,Description From ScStudentCategory Where id in (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@CategoryList, ','))						 
Open CategoryHeader
Fetch Next From CategoryHeader Into @CatId,@CatDescription
While @@FETCH_STATUS = 0
Begin
	Set @Header += '<th colspan="3">Mahal No. '+ CAST(@CatCounter as Varchar(10)) +' </th>'
	Set @Header1 += '<th colspan="3">Total ' + @CatDescription + ' Student</th>'		
	set @Header2 +='<th>Boys</th><th>Girls</th><th>Total</th>'
	Set @CatCounter +=1
	Fetch Next from CategoryHeader Into @CatId,@CatDescription
End
set @Header1 +='</tr>'
set @Header2 +='</tr>'
set @Header += @Header1
set @Header += @Header2
CLOSE CategoryHeader
DEALLOCATE CategoryHeader
 
--  --Start getting the Class for param student consolidate
Declare @Class_Id int,@Age int
Declare @CategoryId int
Declare @ClassSchdule int
Declare @StudentAge int
Declare @AgeCount int
Declare @Row nvarchar(max)
Declare @Counter int
Declare @CounterAge int
Set @Counter = 1
set @Row = ''

 Declare StudentClass Cursor For
	Select * from (select *,(Select Distinct COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
			join ScStudentRegistration as sr on rd.RegMasterId = sr.Id  where sr.ClassId=aa.ClassId)TotalStudent ,
			(Select COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
			join ScStudentRegistration as sr on rd.RegMasterId = sr.Id join ScStudentinfo as si on rd.studentId = si.studentId 
			where Sex='Male' and sr.ClassId=aa.ClassId )TotalBoys,
			(Select COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
			join ScStudentRegistration as sr on rd.RegMasterId = sr.Id join ScStudentinfo as si on rd.studentId = si.studentId 
			where Sex='FeMale' and sr.ClassId=aa.ClassId )TotalGirl			
	from (Select Distinct Id ClassId ,SchClass.Description ClassName,Schedule From  SchClass Inner join ScStudentinfo on ScStudentinfo.ClassId =SchClass.id where DBO is not null
	and SchClass.id in (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@ClassList, ',')) and AcademicYearId ='' + CAST(@ACYId as Varchar(5)) +'' 	
	and ScStudentinfo.StdCategoryId in (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@CategoryList, ','))
	) as aa)as bb where TotalStudent>0  Order By Schedule
	
	 --	and SchClass.id in(1,2,5)
	Open StudentClass
	Fetch Next From StudentClass Into @Class_Id,@ClassName,@TotalStudent,@TotalBoys,@TotalGirl,@ClassSchdule
	While @@FETCH_STATUS = 0
	Begin	
		set @Class_Id=@Class_Id	
		set @AgeCount=1
		set @AgeCount=(Select COUNT(Age) from (Select Distinct ClassId,Convert(Numeric,left(right(GetDate(),12),4))- convert(Numeric,left(right(DBO,12),4))Age
		 from ScStudentinfo where ClassId =@Class_Id and StdCategoryId in (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@CategoryList, ',')) and AcademicYearId ='' + CAST(@ACYId as Varchar(5)) +'' and DBO is not null) as aa)	
		if @AgeCount>1
			set @AgeCount +=  1
			
		Set @Row += '<tr><td rowspan=" '+ CAST(@AgeCount as Varchar(10)) +' ">' + CAST(@Counter as Varchar(5)) + '</td>'											
		Set @Row += '<td rowspan=" '+ CAST(@AgeCount as Varchar(10)) +' ">' + CAST(@ClassName as Varchar(10))  +'</td>'
		set @CounterAge = 1
		
		Declare StudentAge Cursor For	
			Select Distinct ClassId,(ToDayDateYear - DBOYear) Age from (Select ClassId,DBOMiti,DBO ,convert(Numeric,left(right(DBO,12),4))DBOYear,
			Convert(Numeric,left(right(GetDate(),12),4))ToDayDateYear from ScStudentinfo where ClassId=@Class_Id and StdCategoryId in (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@CategoryList, ',')) and AcademicYearId ='' + CAST(@ACYId as Varchar(5)) +''  and DBO is not null) as aa
 
		Open StudentAge
		Fetch Next From StudentAge Into @Class_Id,@StudentAge
		While @@FETCH_STATUS = 0
		Begin	
				set @TotalStudent=(Select isnull(COUNT(StudentID),0) from (Select Distinct StudentID,ClassId,DBOMiti,DBO ,Convert(Numeric,left(right(GetDate(),12),4))- convert(Numeric,left(right(DBO,12),4))Age
									from ScStudentinfo join ScStudentCategory On ScStudentCategory.ID = ScStudentinfo.StdCategoryId where ClassId=@Class_Id and StdCategoryId in (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@CategoryList, ','))  and DBO is not null) as aa  where Age =@StudentAge) 
				set @TotalBoys=(Select isnull(COUNT(StudentID),0) from (Select Distinct StudentID,ClassId,DBOMiti,DBO ,Convert(Numeric,left(right(GetDate(),12),4))- convert(Numeric,left(right(DBO,12),4))Age
									from ScStudentinfo  join ScStudentCategory On ScStudentCategory.ID = ScStudentinfo.StdCategoryId where Sex='Male' and ClassId =@Class_Id and StdCategoryId in (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@CategoryList, ','))  and DBO is not null) as aa where Age =@StudentAge )
				set @TotalGirl=(Select isnull(COUNT(StudentID),0) from (Select Distinct StudentID,ClassId,DBOMiti,DBO ,Convert(Numeric,left(right(GetDate(),12),4))- convert(Numeric,left(right(DBO,12),4))Age
									from ScStudentinfo  join ScStudentCategory On ScStudentCategory.ID = ScStudentinfo.StdCategoryId where Sex='FeMale' and ClassId =@Class_Id and StdCategoryId in (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@CategoryList, ','))  and DBO is not null) as aa where Age =@StudentAge )
				if @CounterAge > 1
					Set @Row += '<tr>'									
									
				Set @Row += '<td>' + CAST(@StudentAge as Varchar(10))   +' </td>
							<td >' + CAST(@TotalBoys as Varchar(10))   +' </td>
							<td >' + CAST(@TotalGirl as Varchar(10))   +' </td>
							<td >' + CAST(@TotalStudent  as Varchar(10)) +' </td>'
						
			set @TotalStudent=0
			set @TotalBoys=0		
			set @TotalGirl=0
						
			Declare CategoryHeaderDetails Cursor For
				 Select Distinct id,Description From ScStudentCategory  Where id in (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@CategoryList, ','))						 
			Open CategoryHeaderDetails
			Fetch Next From CategoryHeaderDetails Into @CatId,@CatDescription
			While @@FETCH_STATUS = 0
			Begin				
				set @TotalStudent=(Select isnull(COUNT(StudentID),0) from (Select Distinct StudentID,ClassId,DBOMiti,DBO ,Convert(Numeric,left(right(GetDate(),12),4))- convert(Numeric,left(right(DBO,12),4))Age
									from ScStudentinfo join ScStudentCategory On ScStudentCategory.ID = ScStudentinfo.StdCategoryId where ClassId=@Class_Id  and StdCategoryId = @CatId  and DBO is not null) as aa  where Age =@StudentAge) 
				set @TotalBoys=(Select isnull(COUNT(StudentID),0) from (Select Distinct StudentID,ClassId,DBOMiti,DBO ,Convert(Numeric,left(right(GetDate(),12),4))- convert(Numeric,left(right(DBO,12),4))Age
									from ScStudentinfo  join ScStudentCategory On ScStudentCategory.ID = ScStudentinfo.StdCategoryId where Sex='Male' and ClassId=@Class_Id   and StdCategoryId = @CatId  and DBO is not null) as aa where Age =@StudentAge )
				set @TotalGirl=(Select isnull(COUNT(StudentID),0) from (Select Distinct StudentID,ClassId,DBOMiti,DBO ,Convert(Numeric,left(right(GetDate(),12),4))- convert(Numeric,left(right(DBO,12),4))Age
									from ScStudentinfo  join ScStudentCategory On ScStudentCategory.ID = ScStudentinfo.StdCategoryId where Sex='FeMale' and ClassId=@Class_Id  and StdCategoryId = @CatId  and DBO is not null) as aa where Age =@StudentAge )

				Set @Row += '<td >' + CAST(@TotalBoys as Varchar(10))  +' </td>
							<td >' + CAST(@TotalGirl as Varchar(10))   +' </td>
							<td >' + CAST(@TotalStudent  as Varchar(10)) +' </td>'
			Fetch Next from CategoryHeaderDetails Into @CatId,@CatDescription
			End
			CLOSE CategoryHeaderDetails
			DEALLOCATE CategoryHeaderDetails
			Set @CounterAge +=1
						
		Fetch Next From StudentAge Into @Class_Id,@StudentAge
		End
		CLOSE StudentAge
		DEALLOCATE StudentAge
		
	  -------Class Total Student--------
	  if @CounterAge > 2  
	  begin
			set @TotalStudent=0
			set @TotalBoys=0		
			set @TotalGirl=0
			set @TotalStudent=(Select isnull(COUNT(StudentID),0) from (Select Distinct StudentID,ClassId,DBOMiti,DBO ,Convert(Numeric,left(right(GetDate(),12),4))- convert(Numeric,left(right(DBO,12),4))Age
								from ScStudentinfo join ScStudentCategory On ScStudentCategory.ID = ScStudentinfo.StdCategoryId where ClassId=@Class_Id and StdCategoryId in (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@CategoryList, ','))  and DBO is not null) as aa ) 
			set @TotalBoys=(Select isnull(COUNT(StudentID),0) from (Select Distinct StudentID,ClassId,DBOMiti,DBO ,Convert(Numeric,left(right(GetDate(),12),4))- convert(Numeric,left(right(DBO,12),4))Age
								from ScStudentinfo  join ScStudentCategory On ScStudentCategory.ID = ScStudentinfo.StdCategoryId where Sex='Male' and ClassId=@Class_Id and StdCategoryId in (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@CategoryList, ','))  and DBO is not null) as aa )
			set @TotalGirl=(Select isnull(COUNT(StudentID),0) from (Select Distinct StudentID,ClassId,DBOMiti,DBO ,Convert(Numeric,left(right(GetDate(),12),4))- convert(Numeric,left(right(DBO,12),4))Age
								from ScStudentinfo  join ScStudentCategory On ScStudentCategory.ID = ScStudentinfo.StdCategoryId where Sex='FeMale' and ClassId=@Class_Id and StdCategoryId in (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@CategoryList, ','))  and DBO is not null) as aa )

			Set @Row += '<tr><td> Total </td>	 
						<td >' + CAST(@TotalBoys as Varchar(10))   +' </td>
						<td >' + CAST(@TotalGirl as Varchar(10))   +' </td>
						<td >' + CAST(@TotalStudent  as Varchar(10)) +' </td>'
	
		-------Class Total Student Category wise--------
			Declare CategoryHeaderDetails Cursor For
				 Select Distinct id,Description From ScStudentCategory	  Where id in (SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@CategoryList, ','))						 
			Open CategoryHeaderDetails
			Fetch Next From CategoryHeaderDetails Into @CatId,@CatDescription
			While @@FETCH_STATUS = 0
			Begin				
				set @TotalStudent=(Select isnull(COUNT(StudentID),0) from (Select Distinct StudentID,ClassId,DBOMiti,DBO ,Convert(Numeric,left(right(GetDate(),12),4))- convert(Numeric,left(right(DBO,12),4))Age
									from ScStudentinfo join ScStudentCategory On ScStudentCategory.ID = ScStudentinfo.StdCategoryId where ClassId=@Class_Id  and StdCategoryId = @CatId  and DBO is not null) as aa  ) 
				set @TotalBoys=(Select isnull(COUNT(StudentID),0) from (Select Distinct StudentID,ClassId,DBOMiti,DBO ,Convert(Numeric,left(right(GetDate(),12),4))- convert(Numeric,left(right(DBO,12),4))Age
									from ScStudentinfo  join ScStudentCategory On ScStudentCategory.ID = ScStudentinfo.StdCategoryId where Sex='Male' and ClassId=@Class_Id   and StdCategoryId = @CatId  and DBO is not null) as aa )
				set @TotalGirl=(Select isnull(COUNT(StudentID),0) from (Select Distinct StudentID,ClassId,DBOMiti,DBO ,Convert(Numeric,left(right(GetDate(),12),4))- convert(Numeric,left(right(DBO,12),4))Age
									from ScStudentinfo  join ScStudentCategory On ScStudentCategory.ID = ScStudentinfo.StdCategoryId where Sex='FeMale' and ClassId=@Class_Id  and StdCategoryId = @CatId  and DBO is not null) as aa )

				Set @Row += '<td >' + CAST(@TotalBoys as Varchar(10))  +' </td>
							<td >' + CAST(@TotalGirl as Varchar(10))   +' </td>
							<td >' + CAST(@TotalStudent  as Varchar(10)) +' </td>'
			Fetch Next from CategoryHeaderDetails Into @CatId,@CatDescription
			End
			CLOSE CategoryHeaderDetails
			DEALLOCATE CategoryHeaderDetails
		End
			-------End Class Total Student--------	
	Set @Counter +=1
 	Fetch Next From StudentClass Into @Class_Id,@ClassName,@TotalStudent,@TotalBoys,@TotalGirl,@ClassSchdule
	End
	CLOSE StudentClass
	DEALLOCATE StudentClass
		Set @Row += '</tr>'
	
	set @Header += @Row 
set @Header += '</table>'
Set @Header = REPLACE(@Header,'.00','')

select @Header
GO
/****** Object:  StoredProcedure [dbo].[SP_Category_AggregateMarkSheet]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_Category_AggregateMarkSheet]
@CategoryId int,@AcademicYearId int,@ExamId int
as
------Temp Check------
--Declare @CategoryId int
--set @CategoryId=1
--Declare @AcademicYearId int
--set @AcademicYearId=2
--Declare @ExamId int
--set @ExamId=1
--SP_Category_AggregateMarkSheet 1,2,1
----------------

 Declare @Class_Id int
 Declare @ClassName nvarchar(512)
 Declare @TotalStudent int
 Declare @TotalBoys int
 Declare @TotalGirl int 
 Declare @NetTotalStudent int
 Declare @NetTotalBoys int
 Declare @NetTotalGirl int 
 Declare @ConsolidateId int
 Declare @TotalSub int
 
Set @NetTotalStudent=0
Set @NetTotalBoys=0
Set @NetTotalGirl=0
Set @TotalStudent=0
Set @TotalBoys=0
Set @TotalGirl=0
Declare MarkHeader Cursor for

select distinct *,(Select Distinct COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
join ScStudentRegistration as sr on rd.RegMasterId = sr.Id  where sr.ClassId=aa.ClassId)TotalStudent ,
(Select COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
join ScStudentRegistration as sr on rd.RegMasterId = sr.Id join ScStudentinfo as si on rd.studentId = si.studentId 
where Sex='Male' and sr.ClassId=aa.ClassId )TotalBoys,
(Select COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
join ScStudentRegistration as sr on rd.RegMasterId = sr.Id join ScStudentinfo as si on rd.studentId = si.studentId 
where Sex='FeMale' and sr.ClassId=aa.ClassId )TotalGirl from (
Select cls.Id as ClassId,cls.Description as ClassName From ScStudentinfo as s 
Join SchClass as cls on s.ClassId = cls.Id
Join ScStudentCategory as SC on SC.id= s.stdCategoryId 
Where s.stdCategoryId = @CategoryId and s.AcademicYearId =@AcademicYearId
) as aa 

Open MarkHeader
Fetch Next From MarkHeader InTo @Class_Id,@ClassName,@TotalStudent,@TotalBoys,@TotalGirl
CLOSE MarkHeader
DEALLOCATE MarkHeader
Declare @Footer varchar(max)
Declare @Header varchar(max)
Declare @Header1 varchar(max)
Declare @Header2 varchar(max)
Set @Footer = '<tr style="font-weight:bold;"><td colspan="2">Total</td>'
	 --Set @Header = '<table width="100%">	 
	 --</table>'
 
set @TotalSub =(Select COUNT(SubjectId) from( Select Distinct EME.SubjectId,s.Description From ScExamMarksEntry EME Inner Join ScSubject s on EME.SubjectId = s.Id where EME.ExamId=@ExamId) as aa) *3 
Set @Header = '<table  width="100%" class="listpopup" border="0.5"><tr bgcolor="#E6EEEE" align="center" style="font-weight:bold">
        <th rowspan="3">Sn</th>  
        <th rowspan="3">Class</th>
        <th colspan="3">No Of Students </th>         
        <th colspan=" '+ CAST(@TotalSub as Varchar(10)) +' ">Marks Obtained By Students According To Subjects (in %) </th>'
        set @Header +='</tr>'
        set @Header1 = '<tr bgcolor="#E6EEEE" align="center" style="font-weight:bold">
        <th rowspan="2">Total</th>        
        <th rowspan="2">Boys</th>
        <th rowspan="2">Girls</th>'
       set @Header2 = '<tr bgcolor="#E6EEEE" align="center" style="font-weight:bold">'
-- Get Subject List According to order
Declare @SubDescription nvarchar(255)
Declare @SubId nvarchar(255)
Declare @Schedule int

Declare SubjectHeader Cursor For
	Select Distinct  EME.SubjectId,s.Description,s.Schedule From ScExamMarksEntry EME
	Inner Join ScSubject s on EME.SubjectId = s.Id	where EME.ExamId=@ExamId  Order By Schedule
	Open SubjectHeader
	Fetch Next From SubjectHeader Into @SubId,@SubDescription,@Schedule
While @@FETCH_STATUS = 0
Begin
	Set @Header1 += '<th colspan="3">' + @SubDescription + '</th>'		
	set @Header2 +='<th>TN</th><th>TB</th><th>TG</th>'
	Fetch Next from SubjectHeader Into @SubId,@SubDescription,@Schedule
End
set @Header1 +='</tr>'
set @Header2 +='</tr>'
set @Header += @Header1
set @Header += @Header2
CLOSE SubjectHeader
DEALLOCATE SubjectHeader
 
  --Start getting the subjects for param student consolidate
Declare @SubjectId int
Declare @SubjectDescription nvarchar(255)
Declare @Row nvarchar(max)
Declare @SubjTFM Decimal(18,2)
Declare @SubjOBT Decimal(18,2)
Declare @SubjBoysOBT Decimal(18,2)
Declare @SubjGirlOBT Decimal(18,2)
Declare @SubjOBTPer Decimal(18,2)
Declare @SubjBoysOBTPer Decimal(18,2)
Declare @SubjGirlOBTPer Decimal(18,2)
Declare @Counter int

Set @Counter = 1
set @Row = ''

 Declare StudentClass Cursor For
	select *,(Select Distinct COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
			join ScStudentRegistration as sr on rd.RegMasterId = sr.Id  where sr.ClassId=aa.ClassId)TotalStudent ,
			(Select COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
			join ScStudentRegistration as sr on rd.RegMasterId = sr.Id join ScStudentinfo as si on rd.studentId = si.studentId 
			where Sex='Male' and sr.ClassId=aa.ClassId )TotalBoys,
			(Select COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
			join ScStudentRegistration as sr on rd.RegMasterId = sr.Id join ScStudentinfo as si on rd.studentId = si.studentId 
			where Sex='FeMale' and sr.ClassId=aa.ClassId )TotalGirl			
	from (Select Distinct  EME.ClassId ,SchClass.Description ClassName From ScExamMarksEntry EME
	Inner Join SchClass On SchClass.id=eme.ClassId Where  EME.ExamId=@ExamId ) as aa Order By ClassId
	
	Open StudentClass
	Fetch Next From StudentClass Into @Class_Id,@ClassName,@TotalStudent,@TotalBoys,@TotalGirl
	While @@FETCH_STATUS = 0
	Begin
		Set @SubjTFM = 0
		Set @SubjOBT = 0	
		
		Set @Row += '<tr><td>' + CAST(@Counter as Varchar(5)) + '</td>'											
		Set @Row += '<td>' + CAST(@ClassName as Varchar(10))   +' </td>
		<td>' + CAST(@TotalStudent as Varchar(10))   +' </td>
		<td>' + CAST(@TotalBoys as Varchar(10))   +' </td>
		<td>' + CAST(@TotalGirl as Varchar(10))   +' </td>'
		set @NetTotalStudent +=@TotalStudent
		set @NetTotalBoys +=@TotalBoys
		set @NetTotalGirl +=@TotalGirl
		
		set @TotalStudent=@TotalStudent
		set @TotalBoys=@TotalBoys
		set @TotalGirl=@TotalGirl
		
		Set @Class_Id=@Class_Id
		
		Declare StudentSubjects Cursor For
			Select Distinct  EME.SubjectId,s.Description,s.Schedule From ScExamMarksEntry EME
			Inner Join ScSubject s on EME.SubjectId = s.Id	Where  EME.ExamId=@ExamId  Order By Schedule
			
			Open StudentSubjects
			Fetch Next From StudentSubjects Into @SubjectId,@SubjectDescription,@Schedule
			While @@FETCH_STATUS = 0
			Begin
			
			set @SubjectId =@SubjectId
			set @SubjTFM=0
			set @SubjOBTPer = 0
			set @SubjBoysOBTPer = 0
			set @SubjGirlOBTPer = 0			
													
			set @SubjTFM = (select ISNULL(sum(PracticalFullMark+TheoryFullMark),0)TotalFullMarks from ScExamMarkSetup as EMS Where SubjectId = @SubjectId and ClassId= @Class_Id and  EMS.ExamId=@ExamId )
			Set @SubjOBT = (Select ISNULL(sum(Total),0) From ScExamMarksEntry as EMS Where SubjectId = @SubjectId and ClassId= @Class_Id and  EMS.ExamId=@ExamId)
			Set @SubjBoysOBT = (select isnull(sum(Total),0)TotalObtainedMarks from ScExamMarksEntry as EME
								inner join ScStudentRegistrationDetail as rd on rd.StudentId=EME.StudentId 
								inner join ScStudentRegistration as sr on rd.RegMasterId = sr.Id 
								inner join ScStudentinfo as SI On SI.StudentID=rd.StudentId
								Where SI.stdCategoryId = @CategoryId and SI.AcademicYearId =@AcademicYearId and SubjectId = @SubjectId and EME.ClassId= @Class_Id and Sex='Male' and  EME.ExamId=@ExamId)		
			Set @SubjGirlOBT = (select isnull(sum(Total),0)TotalObtainedMarks from ScExamMarksEntry as EME
								inner join ScStudentRegistrationDetail as rd on rd.StudentId=EME.StudentId 
								inner join ScStudentRegistration as sr on rd.RegMasterId = sr.Id 
								inner join ScStudentinfo as SI On SI.StudentID=rd.StudentId
								Where SI.stdCategoryId = @CategoryId and SI.AcademicYearId =@AcademicYearId and SubjectId = @SubjectId and EME.ClassId= @Class_Id and Sex='FeMale' and  EME.ExamId=@ExamId)
			if @SubjTFM>0 and @TotalStudent>0					
				set @SubjOBTPer=((@SubjOBT/@TotalStudent)/@SubjTFM)*100
			if @SubjTFM>0 and @TotalBoys>0
				set @SubjBoysOBTPer=((@SubjBoysOBT/@TotalBoys)/@SubjTFM)*100 
			else
				set @SubjBoysOBTPer=0		
			if @SubjTFM>0 and @TotalGirl>0	
				set @SubjGirlOBTPer=((@SubjGirlOBT/@TotalGirl)/@SubjTFM)*100	
			else
				set @SubjGirlOBTPer=0
				
			Set @Row += '<td>' + CAST(@SubjOBTPer as Varchar(10))   +' </td>
				<td> '+ CAST(@SubjBoysOBTPer as Varchar(10))  + '</td>
				<td> '+ CAST(@SubjGirlOBTPer as Varchar(10))  +'</td> '	
		
		Fetch Next From StudentSubjects Into @SubjectId,@SubjectDescription,@Schedule
		End
		CLOSE StudentSubjects
		DEALLOCATE StudentSubjects
	Set @Counter +=1
 	Fetch Next From StudentClass Into @Class_Id,@ClassName,@TotalStudent,@TotalBoys,@TotalGirl
	End
	CLOSE StudentClass
	DEALLOCATE StudentClass
	Set @Row += '</tr>'
	
	Set @Row += '<td Colspan="2">Total </td>
				<td>' + CAST(@NetTotalStudent as Varchar(10))   +' </td>
				<td>' + CAST(@NetTotalBoys as Varchar(10))   +' </td>
				<td>' + CAST(@NetTotalGirl as Varchar(10))   +' </td>'
							
	Set @Row += '</tr>'
	set @Header += @Row
set @Header += '</table>'
Set @Header = REPLACE(@Header,'.00','')

select @Header
GO
/****** Object:  StoredProcedure [dbo].[SP_Category_AggregateConsolidateMarkSheet]    Script Date: 07/22/2013 11:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_Category_AggregateConsolidateMarkSheet]
@CategoryId int,@AcademicYearId int,@ExamId int
as
------Temp Check------
--SP_Category_AggregateConsolidateMarkSheet 1,2,1
----------------

 Declare @Class_Id int
 Declare @ClassName nvarchar(512)
 Declare @TotalStudent int
 Declare @TotalBoys int
 Declare @TotalGirl int 
 Declare @NetTotalStudent int
 Declare @NetTotalBoys int
 Declare @NetTotalGirl int 
 Declare @ConsolidateId int
 Declare @TotalSub int
 
Set @NetTotalStudent=0
Set @NetTotalBoys=0
Set @NetTotalGirl=0
Set @TotalStudent=0
Set @TotalBoys=0
Set @TotalGirl=0
Declare MarkHeader Cursor for

select distinct *,(Select Distinct COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
join ScStudentRegistration as sr on rd.RegMasterId = sr.Id  where sr.ClassId=aa.ClassId)TotalStudent ,
(Select COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
join ScStudentRegistration as sr on rd.RegMasterId = sr.Id join ScStudentinfo as si on rd.studentId = si.studentId 
where Sex='Male' and sr.ClassId=aa.ClassId )TotalBoys,
(Select COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
join ScStudentRegistration as sr on rd.RegMasterId = sr.Id join ScStudentinfo as si on rd.studentId = si.studentId 
where Sex='FeMale' and sr.ClassId=aa.ClassId )TotalGirl from (
Select cls.Id as ClassId,cls.Description as ClassName From ScStudentinfo as s Join SchClass as cls on s.ClassId = cls.Id Join ScStudentCategory as SC on SC.id= s.stdCategoryId 
Where s.stdCategoryId = @CategoryId and s.AcademicYearId =@AcademicYearId
) as aa 

Open MarkHeader
Fetch Next From MarkHeader InTo @Class_Id,@ClassName,@TotalStudent,@TotalBoys,@TotalGirl
CLOSE MarkHeader
DEALLOCATE MarkHeader
Declare @Footer varchar(max)
Declare @Header varchar(max)
Declare @Header1 varchar(max)
Declare @Header2 varchar(max)
Set @Footer = '<tr style="font-weight:bold;"><td colspan="2">Total</td>'
	 --Set @Header = '<table width="100%">	 
	 --</table>'
 
set @TotalSub =(Select COUNT(SubjectId) from( Select Distinct SubjectId,s.Description From Consolidate C Inner Join ConsolidateDetail as CD On CD.ConsolidateId=C.Id
			 Join ScSubject s on CD.SubjectId = s.Id) as aa) *3 
Set @Header = '<table  width="100%" class="listpopup" border="0.5">
<tr bgcolor="#E6EEEE" align="center" style="font-weight:bold">
        <th rowspan="3">Sn</th>  
        <th rowspan="3">Class</th>
        <th colspan="3">No Of Students </th>         
        <th colspan=" '+ CAST(@TotalSub as Varchar(10)) +' ">Marks Obtained By Students According To Subjects (in %) </th>'
        set @Header1 = '<tr bgcolor="#E6EEEE" align="center" style="font-weight:bold">
        <th rowspan="2">Total</th>        
        <th rowspan="2">Boys</th>
        <th rowspan="2">Girls</th>'
       set @Header2 = '<tr bgcolor="#E6EEEE" align="center" style="font-weight:bold">'
-- Get Subject List According to order
Declare @SubDescription nvarchar(255)
Declare @SubId nvarchar(255)
Declare @Schedule int

Declare SubjectHeader Cursor For
	 Select Distinct SubjectId,s.Description,Schedule From Consolidate C Inner Join ConsolidateDetail as CD On CD.ConsolidateId=C.Id
			 Join ScSubject s on CD.SubjectId = s.Id where CD.ExamId=@ExamId  Order By Schedule
	Open SubjectHeader
	Fetch Next From SubjectHeader Into @SubId,@SubDescription,@Schedule
While @@FETCH_STATUS = 0
Begin
	Set @Header1 += '<th colspan="3">' + @SubDescription + '</th>'		
	set @Header2 +='<th>TN</th><th>TB</th><th>TG</th>'
	Fetch Next from SubjectHeader Into @SubId,@SubDescription,@Schedule
End
set @Header1 +='</tr>'
set @Header2 +='</tr>'
set @Header += @Header1
set @Header += @Header2
CLOSE SubjectHeader
DEALLOCATE SubjectHeader
 
  --Start getting the subjects for param student consolidate
Declare @SubjectId int
Declare @SubjectDescription nvarchar(255)
Declare @Row nvarchar(max)
Declare @SubjTFM Decimal(18,2)
Declare @SubjOBT Decimal(18,2)
Declare @SubjBoysOBT Decimal(18,2)
Declare @SubjGirlOBT Decimal(18,2)
Declare @SubjOBTPer Decimal(18,2)
Declare @SubjBoysOBTPer Decimal(18,2)
Declare @SubjGirlOBTPer Decimal(18,2)
Declare @Counter int

Set @Counter = 1
set @Row = ''

 Declare StudentClass Cursor For
	select *,(Select Distinct COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
			join ScStudentRegistration as sr on rd.RegMasterId = sr.Id  where sr.ClassId=aa.ClassId)TotalStudent ,
			(Select COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
			join ScStudentRegistration as sr on rd.RegMasterId = sr.Id join ScStudentinfo as si on rd.studentId = si.studentId 
			where Sex='Male' and sr.ClassId=aa.ClassId )TotalBoys,
			(Select COUNT(rd.StudentId)  from  ScStudentRegistrationDetail	as rd
			join ScStudentRegistration as sr on rd.RegMasterId = sr.Id join ScStudentinfo as si on rd.studentId = si.studentId 
			where Sex='FeMale' and sr.ClassId=aa.ClassId )TotalGirl			
	from (Select Distinct  C.ClassId ,SchClass.Description ClassName From Consolidate as C Inner Join ConsolidateDetail  as CD On CD.ConsolidateId=C.Id
	Inner Join SchClass On SchClass.id=C.ClassId Where CD.ExamId=@ExamId ) as aa Order By ClassId
	
	Open StudentClass
	Fetch Next From StudentClass Into @Class_Id,@ClassName,@TotalStudent,@TotalBoys,@TotalGirl
	While @@FETCH_STATUS = 0
	Begin
		Set @SubjTFM = 0
		Set @SubjOBT = 0	
		
		Set @Row += '<tr><td>' + CAST(@Counter as Varchar(5)) + '</td>'											
		Set @Row += '<td>' + CAST(@ClassName as Varchar(10))   +' </td>
		<td>' + CAST(@TotalStudent as Varchar(10))   +' </td>
		<td>' + CAST(@TotalBoys as Varchar(10))   +' </td>
		<td>' + CAST(@TotalGirl as Varchar(10))   +' </td>'
		set @NetTotalStudent +=@TotalStudent
		set @NetTotalBoys +=@TotalBoys
		set @NetTotalGirl +=@TotalGirl
		
		set @TotalStudent=@TotalStudent
		set @TotalBoys=@TotalBoys
		set @TotalGirl=@TotalGirl
		
		Set @Class_Id=@Class_Id
		
		Declare StudentSubjects Cursor For 			 
			Select Distinct SubjectId,s.Description,Schedule From Consolidate C Inner Join ConsolidateDetail as CD On CD.ConsolidateId=C.Id
			 Join ScSubject s on CD.SubjectId = s.Id where CD.ExamId=@ExamId  Order By Schedule
			 
			Open StudentSubjects
			Fetch Next From StudentSubjects Into @SubjectId,@SubjectDescription,@Schedule
			While @@FETCH_STATUS = 0
			Begin
			
			set @SubjectId =@SubjectId
			set @SubjTFM=0
			set @SubjOBTPer = 0
			set @SubjBoysOBTPer = 0
			set @SubjGirlOBTPer = 0			
													
			set @SubjTFM = (select ISNULL(sum(FullMarks),0)TotalFullMarks from ConsolidateDetail as CD inner join Consolidate as C On C.Id= CD.ConsolidateId Where SubjectId = @SubjectId and C.ClassId= @Class_Id and  CD.ExamId=@ExamId )
			
			Set @SubjOBT = (Select ISNULL(sum(ObtainedMarks),0) From ConsolidateDetail as CD inner join Consolidate as C On C.Id= CD.ConsolidateId Where SubjectId = @SubjectId and C.ClassId= @Class_Id and CD.ExamId=@ExamId)
			Set @SubjBoysOBT = (select isnull(sum(ObtainedMarks),0)TotalObtainedMarks from ConsolidateDetail as CD
								inner join Consolidate as C On C.Id= CD.ConsolidateId
								inner join ScStudentRegistrationDetail as rd on rd.StudentId=C.StudentId 
								inner join ScStudentRegistration as sr on rd.RegMasterId = sr.Id 
								inner join ScStudentinfo as SI On SI.StudentID=rd.StudentId
								Where SI.stdCategoryId = @CategoryId and SI.AcademicYearId =@AcademicYearId and SubjectId = @SubjectId and C.ClassId= @Class_Id and Sex='Male' and  CD.ExamId=@ExamId)		
			Set @SubjGirlOBT = (select isnull(sum(ObtainedMarks),0)TotalObtainedMarks from ConsolidateDetail as CD
								inner join Consolidate as C On C.Id= CD.ConsolidateId
								inner join ScStudentRegistrationDetail as rd on rd.StudentId=C.StudentId 								
								inner join ScStudentRegistration as sr on rd.RegMasterId = sr.Id 
								inner join ScStudentinfo as SI On SI.StudentID=rd.StudentId
								Where SI.stdCategoryId = @CategoryId and SI.AcademicYearId =@AcademicYearId and SubjectId = @SubjectId and C.ClassId= @Class_Id and Sex='FeMale' and  CD.ExamId=@ExamId)
			if @SubjTFM>0 and @TotalStudent>0					
				set @SubjOBTPer=((@SubjOBT/@TotalStudent)/@SubjTFM)*100
			if @SubjTFM>0 and @TotalBoys>0
				set @SubjBoysOBTPer=((@SubjBoysOBT/@TotalBoys)/@SubjTFM)*100 
			else
				set @SubjBoysOBTPer=0		
			if @SubjTFM>0 and @TotalGirl>0	
				set @SubjGirlOBTPer=((@SubjGirlOBT/@TotalGirl)/@SubjTFM)*100	
			else
				set @SubjGirlOBTPer=0
				
			Set @Row += '<td>' + CAST(@SubjOBTPer as Varchar(10))   +' </td>
				<td> '+ CAST(@SubjBoysOBTPer as Varchar(10))  + '</td>
				<td> '+ CAST(@SubjGirlOBTPer as Varchar(10))  +'</td> '	
		
		Fetch Next From StudentSubjects Into @SubjectId,@SubjectDescription,@Schedule
		End
		CLOSE StudentSubjects
		DEALLOCATE StudentSubjects
	Set @Counter +=1
 	Fetch Next From StudentClass Into @Class_Id,@ClassName,@TotalStudent,@TotalBoys,@TotalGirl
	End
	CLOSE StudentClass
	DEALLOCATE StudentClass
	Set @Row += '</tr>'
	
	Set @Row += '<td Colspan="2">Total </td>
				<td>' + CAST(@NetTotalStudent as Varchar(10))   +' </td>
				<td>' + CAST(@NetTotalBoys as Varchar(10))   +' </td>
				<td>' + CAST(@NetTotalGirl as Varchar(10))   +' </td>'
							
	Set @Row += '</tr>'
	set @Header += @Row
set @Header += '</table>'
Set @Header = REPLACE(@Header,'.00','')

select @Header
GO
/****** Object:  Table [dbo].[ScTeacherSchedule]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScTeacherSchedule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StaffId] [int] NULL,
	[ClassScheduleDetailId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_ScTeacherSchedule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Trigger [ScSubjectWiseMarks_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScSubjectWiseMarks_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScExamMarksEntry]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScSubjectWiseMarks'
                       END
GO
/****** Object:  Trigger [ScStudentRegistrationDetail_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScStudentRegistrationDetail_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScStudentRegistrationDetail]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScStudentRegistrationDetail'
                       END
GO
/****** Object:  Trigger [ScStudentFeeRateDetail_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScStudentFeeRateDetail_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScStudentFeeRateDetail]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScStudentFeeRateDetail'
                       END
GO
/****** Object:  Trigger [ScMonthlyBill_AspNet_SqlCacheNotification_Trigger]    Script Date: 07/22/2013 11:02:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[ScMonthlyBill_AspNet_SqlCacheNotification_Trigger] ON [dbo].[ScMonthlyBill]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'ScMonthlyBill'
                       END
GO
/****** Object:  Table [dbo].[PyEmployeeSalaryAllowanceMapping]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PyEmployeeSalaryAllowanceMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeSalaryId] [int] NOT NULL,
	[AllowanceId] [int] NOT NULL,
 CONSTRAINT [PK_PyEmployeeSalaryAllowanceMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PyEmployeeDeductionMapping]    Script Date: 07/22/2013 11:02:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PyEmployeeDeductionMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DeductionId] [int] NOT NULL,
	[EmployeeDeductionId] [int] NOT NULL,
 CONSTRAINT [PK_PyEmployeeDeductionMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF__AccountGr__Branc__51851410]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[AccountGroup] ADD  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__AccountGr__IsDel__52793849]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[AccountGroup] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__AccountSu__IsDel__5090EFD7]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[AccountSubGroup] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__AccountTr__Branc__4F9CCB9E]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[AccountTransaction] ADD  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__Agent__BranchId__4EA8A765]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[Agent] ADD  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__Area__BranchId__4CC05EF3]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[Area] ADD  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__Area__IsDeleted__4DB4832C]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[Area] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__AspNet_Sq__notif__3C89F72A]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[AspNet_SqlCacheTablesForChangeNotification] ADD  DEFAULT (getdate()) FOR [notificationCreated]
GO
/****** Object:  Default [DF__AspNet_Sq__chang__3D7E1B63]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[AspNet_SqlCacheTablesForChangeNotification] ADD  DEFAULT ((0)) FOR [changeId]
GO
/****** Object:  Default [DF__BillingTe__Branc__48EFCE0F]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[BillingTerm] ADD  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__BillingTe__IsAct__49E3F248]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[BillingTerm] ADD  DEFAULT ((0)) FOR [IsActive]
GO
/****** Object:  Default [DF__BillingTe__Dispa__4AD81681]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[BillingTerm] ADD  DEFAULT ((0)) FOR [DispalyOrder]
GO
/****** Object:  Default [DF__BillingTe__IsDel__4BCC3ABA]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[BillingTerm] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__CashBankM__Branc__5B0E7E4A]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[CashBankMaster] ADD  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__CashBankM__IsDel__5C02A283]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[CashBankMaster] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__CostCente__Branc__592635D8]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[CostCenter] ADD  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__CostCente__IsDel__5A1A5A11]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[CostCenter] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__CreditNot__Branc__0BE6BFCF]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[CreditNoteMaster] ADD  CONSTRAINT [DF__CreditNot__Branc__0BE6BFCF]  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__CreditNot__IsDel__5832119F]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[CreditNoteMaster] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Currency__IsDele__5649C92D]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[Currency] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__DebitNote__Branc__116A8EFB]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[DebitNoteMaster] ADD  CONSTRAINT [DF__DebitNote__Branc__116A8EFB]  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__DebitNote__IsDel__3A179ED3]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[DebitNoteMaster] ADD  CONSTRAINT [DF__DebitNote__IsDel__3A179ED3]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__DocumentN__Branc__536D5C82]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[DocumentNumberingScheme] ADD  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__EmployeeT__Creat__6EE06CCD]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[EmployeeTransaction] ADD  CONSTRAINT [DF__EmployeeT__Creat__6EE06CCD]  DEFAULT ((0)) FOR [CreateById]
GO
/****** Object:  Default [DF__EmployeeT__Emplo__6FD49106]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[EmployeeTransaction] ADD  CONSTRAINT [DF__EmployeeT__Emplo__6FD49106]  DEFAULT ((0)) FOR [EmployeeId]
GO
/****** Object:  Default [DF__EmployeeT__Branc__70C8B53F]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[EmployeeTransaction] ADD  CONSTRAINT [DF__EmployeeT__Branc__70C8B53F]  DEFAULT ((0)) FOR [BranchId]
GO
/****** Object:  Default [DF__EmployeeT__Acade__71BCD978]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[EmployeeTransaction] ADD  CONSTRAINT [DF__EmployeeT__Acade__71BCD978]  DEFAULT ((0)) FOR [AcademicYearId]
GO
/****** Object:  Default [DF__Godown__BranchId__3AA1AEB8]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[Godown] ADD  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__Godown__IsDelete__3B95D2F1]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[Godown] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__JournalVo__Branc__1B0907CE]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[JournalVoucher] ADD  CONSTRAINT [DF__JournalVo__Branc__1B0907CE]  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__JournalVo__IsDel__39AD8A7F]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[JournalVoucher] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Ledger__BranchId__36D11DD4]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[Ledger] ADD  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__Ledger__IsDelete__37C5420D]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[Ledger] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Narration__Branc__451F3D2B]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[Narration] ADD  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__Narration__IsDel__46136164]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[Narration] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Product__BranchI__173876EA]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF__Product__BranchI__173876EA]  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__Product__IsDelet__2AD55B43]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF__Product__IsDelet__2AD55B43]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Product__MfgDate__4B622666]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF__Product__MfgDate__4B622666]  DEFAULT ((0)) FOR [MfgDate]
GO
/****** Object:  Default [DF__Product__ExpDate__4C564A9F]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF__Product__ExpDate__4C564A9F]  DEFAULT ((0)) FOR [ExpDate]
GO
/****** Object:  Default [DF__ProductGr__Branc__3F6663D5]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ProductGroup] ADD  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__ProductGr__IsDel__405A880E]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ProductGroup] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__ProductSu__IsDel__3E723F9C]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ProductSubGroup] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__PurchaseI__Branc__0C90CB45]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PurchaseInvoice] ADD  CONSTRAINT [DF__PurchaseI__Branc__0C90CB45]  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__PurchaseI__IsDel__47FBA9D6]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PurchaseInvoice] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__PurchaseR__Branc__011F1899]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PurchaseReturnMaster] ADD  CONSTRAINT [DF__PurchaseR__Branc__011F1899]  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__PurchaseR__IsDel__320C68B7]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PurchaseReturnMaster] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__PyPayment__Creat__30242045]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PyPaymentSlip] ADD  DEFAULT ((0)) FOR [CreatedById]
GO
/****** Object:  Default [DF__PyPayroll__Acade__7C3A67EB]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PyPayrollGeneration] ADD  DEFAULT ((0)) FOR [AcademicYearId]
GO
/****** Object:  Default [DF__SalesChal__UnitI__6497E884]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SalesChallanDetail] ADD  DEFAULT ((1)) FOR [UnitId]
GO
/****** Object:  Default [DF__SalesInvo__Branc__11558062]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SalesInvoice] ADD  CONSTRAINT [DF__SalesInvo__Branc__11558062]  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__SalesInvo__IsDel__33F4B129]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SalesInvoice] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__SalesRetu__Branc__42E1EEFE]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SalesReturnMaster] ADD  CONSTRAINT [DF__SalesRetu__Branc__42E1EEFE]  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__SalesRetu__IsDel__2F2FFC0C]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SalesReturnMaster] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__ScEmploye__Ledge__6B44E613]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScEmployeeInfo] ADD  DEFAULT ((0)) FOR [LedgerId]
GO
/****** Object:  Default [DF__ScExam__IsFinal__668030F6]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScExam] ADD  DEFAULT ('false') FOR [IsFinal]
GO
/****** Object:  Default [DF__ScFeeItem__Educa__658C0CBD]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScFeeItem] ADD  DEFAULT ('false') FOR [EducationTax]
GO
/****** Object:  Default [DF__ScFeeRece__Creat__6A50C1DA]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScFeeReceipt] ADD  DEFAULT ((3)) FOR [CreatedById]
GO
/****** Object:  Default [DF__SchClass__Schedu__3EA749C6]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SchClass] ADD  CONSTRAINT [DF__SchClass__Schedu__3EA749C6]  DEFAULT ((0)) FOR [Schedule]
GO
/****** Object:  Default [DF__SchClass__Progra__1F198FD4]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SchClass] ADD  CONSTRAINT [DF__SchClass__Progra__1F198FD4]  DEFAULT ((0)) FOR [ProgramId]
GO
/****** Object:  Default [DF__ScLibrary__IsDel__34E8D562]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScLibraryLateFine] ADD  DEFAULT ('False') FOR [IsDeleted]
GO
/****** Object:  Default [DF__ScMonthly__Educa__6D2D2E85]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScMonthlyBill] ADD  DEFAULT ((0)) FOR [EducationTaxAmount]
GO
/****** Object:  Default [DF__ScMonthly__Creat__6C390A4C]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScMonthlyBillStudent] ADD  DEFAULT ((3)) FOR [CreatedById]
GO
/****** Object:  Default [DF_RollNumberingScheme_BranchId]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScRollNumberingScheme] ADD  CONSTRAINT [DF_RollNumberingScheme_BranchId]  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__ScStudent__GLCod__695C9DA1]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentinfo] ADD  DEFAULT ((34)) FOR [GLCode]
GO
/****** Object:  Default [DF__ScStudent__userI__08A03ED0]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentRegistrationDetail] ADD  DEFAULT ((0)) FOR [userId]
GO
/****** Object:  Default [DF__SubLedger__Branc__62AFA012]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SubLedger] ADD  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__SubLedger__IsDel__63A3C44B]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SubLedger] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__SystemCon__NoOfF__5FD33367]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SystemControl] ADD  DEFAULT ((0)) FOR [NoOfFeeReceiptPrint]
GO
/****** Object:  Default [DF__SystemCon__Print__60C757A0]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SystemControl] ADD  DEFAULT ((1)) FOR [PrintDataOnly]
GO
/****** Object:  Default [DF__SystemCon__NoOfB__61BB7BD9]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SystemControl] ADD  DEFAULT ((1)) FOR [NoOfBillPrint]
GO
/****** Object:  Default [DF__Unit__IsDeleted__5EDF0F2E]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[Unit] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__User__BranchId__1F98B2C1]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF__User__BranchId__1F98B2C1]  DEFAULT ((1)) FOR [BranchId]
GO
/****** Object:  Default [DF__User__AllBranch__208CD6FA]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF__User__AllBranch__208CD6FA]  DEFAULT ((1)) FOR [AllBranch]
GO
/****** Object:  ForeignKey [FK__BillOfMat__CostC__7993056A]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[BillOfMaterial]  WITH CHECK ADD FOREIGN KEY([CostCenterId])
REFERENCES [dbo].[CostCenter] ([Id])
GO
/****** Object:  ForeignKey [FK__BillOfMat__Creat__7A8729A3]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[BillOfMaterial]  WITH CHECK ADD FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
/****** Object:  ForeignKey [FK__BillOfMat__Modif__7B7B4DDC]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[BillOfMaterial]  WITH CHECK ADD FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
/****** Object:  ForeignKey [FK__BillOfMat__UnitI__7C6F7215]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[BillOfMaterial]  WITH CHECK ADD FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([Id])
GO
/****** Object:  ForeignKey [FK__BillOfMat__BillO__0F824689]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[BillOfMaterialDetail]  WITH CHECK ADD FOREIGN KEY([BillOfMaterialId])
REFERENCES [dbo].[BillOfMaterial] ([Id])
GO
/****** Object:  ForeignKey [FK__BillOfMat__UnitI__10766AC2]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[BillOfMaterialDetail]  WITH CHECK ADD FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([Id])
GO
/****** Object:  ForeignKey [FK__ExpiryBre__ExpBr__71BCD978]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ExpiryBreakageDetail]  WITH CHECK ADD  CONSTRAINT [FK__ExpiryBre__ExpBr__71BCD978] FOREIGN KEY([ExpBrkId])
REFERENCES [dbo].[ExpiryBreakageMaster] ([Id])
GO
ALTER TABLE [dbo].[ExpiryBreakageDetail] CHECK CONSTRAINT [FK__ExpiryBre__ExpBr__71BCD978]
GO
/****** Object:  ForeignKey [FK__FinishedG__Creat__76B698BF]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[FinishedGoodReceive]  WITH CHECK ADD FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
/****** Object:  ForeignKey [FK__FinishedG__GoDow__77AABCF8]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[FinishedGoodReceive]  WITH CHECK ADD FOREIGN KEY([GoDownId])
REFERENCES [dbo].[Godown] ([Id])
GO
/****** Object:  ForeignKey [FK__FinishedG__Modif__789EE131]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[FinishedGoodReceive]  WITH CHECK ADD FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
/****** Object:  ForeignKey [FK__FinishedG__Finis__1AF3F935]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[FinishedGoodReceiveDetail]  WITH CHECK ADD FOREIGN KEY([FinishedGoodReceiveId])
REFERENCES [dbo].[FinishedGoodReceive] ([Id])
GO
/****** Object:  ForeignKey [FK__FinishedG__UnitI__1BE81D6E]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[FinishedGoodReceiveDetail]  WITH CHECK ADD FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([Id])
GO
/****** Object:  ForeignKey [FK__FinishedG__Creat__17236851]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[FinishedGoodReturn]  WITH CHECK ADD FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
/****** Object:  ForeignKey [FK__FinishedG__Finis__18178C8A]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[FinishedGoodReturn]  WITH CHECK ADD FOREIGN KEY([FinishedGoodReceiveId])
REFERENCES [dbo].[FinishedGoodReceive] ([Id])
GO
/****** Object:  ForeignKey [FK__FinishedG__GoDow__190BB0C3]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[FinishedGoodReturn]  WITH CHECK ADD FOREIGN KEY([GoDownId])
REFERENCES [dbo].[Godown] ([Id])
GO
/****** Object:  ForeignKey [FK__FinishedG__Modif__19FFD4FC]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[FinishedGoodReturn]  WITH CHECK ADD FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
/****** Object:  ForeignKey [FK__FinishedG__Finis__20ACD28B]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[FinishedGoodReturnDetail]  WITH CHECK ADD FOREIGN KEY([FinishedGoodReturnId])
REFERENCES [dbo].[FinishedGoodReturn] ([Id])
GO
/****** Object:  ForeignKey [FK__FinishedG__UnitI__21A0F6C4]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[FinishedGoodReturnDetail]  WITH CHECK ADD FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([Id])
GO
/****** Object:  ForeignKey [FK__GodownTra__Creat__7D63964E]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[GodownTransfer]  WITH CHECK ADD FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
/****** Object:  ForeignKey [FK__GodownTra__FromG__7E57BA87]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[GodownTransfer]  WITH CHECK ADD FOREIGN KEY([FromGodownId])
REFERENCES [dbo].[Godown] ([Id])
GO
/****** Object:  ForeignKey [FK__GodownTra__Modif__7F4BDEC0]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[GodownTransfer]  WITH CHECK ADD FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
/****** Object:  ForeignKey [FK__GodownTra__Trans__0D99FE17]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[GodownTransferDetail]  WITH CHECK ADD FOREIGN KEY([TransferId])
REFERENCES [dbo].[GodownTransfer] ([Id])
GO
/****** Object:  ForeignKey [FK__GodownTra__UnitI__0E8E2250]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[GodownTransferDetail]  WITH CHECK ADD FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([Id])
GO
/****** Object:  ForeignKey [FK__MaterialI__CostC__4DE98D56]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[MaterialIssue]  WITH CHECK ADD  CONSTRAINT [FK__MaterialI__CostC__4DE98D56] FOREIGN KEY([CostCenterId])
REFERENCES [dbo].[CostCenter] ([Id])
GO
ALTER TABLE [dbo].[MaterialIssue] CHECK CONSTRAINT [FK__MaterialI__CostC__4DE98D56]
GO
/****** Object:  ForeignKey [FK__MaterialI__Creat__4EDDB18F]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[MaterialIssue]  WITH CHECK ADD  CONSTRAINT [FK__MaterialI__Creat__4EDDB18F] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[MaterialIssue] CHECK CONSTRAINT [FK__MaterialI__Creat__4EDDB18F]
GO
/****** Object:  ForeignKey [FK__MaterialI__Modif__4FD1D5C8]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[MaterialIssue]  WITH CHECK ADD  CONSTRAINT [FK__MaterialI__Modif__4FD1D5C8] FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[MaterialIssue] CHECK CONSTRAINT [FK__MaterialI__Modif__4FD1D5C8]
GO
/****** Object:  ForeignKey [FK__MaterialI__Mater__2F2FFC0C]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[MaterialIssueDetail]  WITH CHECK ADD  CONSTRAINT [FK__MaterialI__Mater__2F2FFC0C] FOREIGN KEY([MaterialIssueId])
REFERENCES [dbo].[MaterialIssue] ([Id])
GO
ALTER TABLE [dbo].[MaterialIssueDetail] CHECK CONSTRAINT [FK__MaterialI__Mater__2F2FFC0C]
GO
/****** Object:  ForeignKey [FK__MaterialI__UnitI__162F4418]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[MaterialIssueDetail]  WITH CHECK ADD FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([Id])
GO
/****** Object:  ForeignKey [FK__MaterialR__CostC__5B438874]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[MaterialReturn]  WITH CHECK ADD  CONSTRAINT [FK__MaterialR__CostC__5B438874] FOREIGN KEY([CostCenterId])
REFERENCES [dbo].[CostCenter] ([Id])
GO
ALTER TABLE [dbo].[MaterialReturn] CHECK CONSTRAINT [FK__MaterialR__CostC__5B438874]
GO
/****** Object:  ForeignKey [FK__MaterialR__Creat__5C37ACAD]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[MaterialReturn]  WITH CHECK ADD  CONSTRAINT [FK__MaterialR__Creat__5C37ACAD] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[MaterialReturn] CHECK CONSTRAINT [FK__MaterialR__Creat__5C37ACAD]
GO
/****** Object:  ForeignKey [FK__MaterialR__Mater__5A4F643B]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[MaterialReturn]  WITH CHECK ADD  CONSTRAINT [FK__MaterialR__Mater__5A4F643B] FOREIGN KEY([MaterialIssueId])
REFERENCES [dbo].[MaterialIssue] ([Id])
GO
ALTER TABLE [dbo].[MaterialReturn] CHECK CONSTRAINT [FK__MaterialR__Mater__5A4F643B]
GO
/****** Object:  ForeignKey [FK__MaterialR__Modif__5D2BD0E6]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[MaterialReturn]  WITH CHECK ADD  CONSTRAINT [FK__MaterialR__Modif__5D2BD0E6] FOREIGN KEY([ModifiedById])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[MaterialReturn] CHECK CONSTRAINT [FK__MaterialR__Modif__5D2BD0E6]
GO
/****** Object:  ForeignKey [FK__MaterialR__Mater__16644E42]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[MaterialReturnDetail]  WITH CHECK ADD  CONSTRAINT [FK__MaterialR__Mater__16644E42] FOREIGN KEY([MaterialReturnId])
REFERENCES [dbo].[MaterialReturn] ([Id])
GO
ALTER TABLE [dbo].[MaterialReturnDetail] CHECK CONSTRAINT [FK__MaterialR__Mater__16644E42]
GO
/****** Object:  ForeignKey [FK__MaterialR__UnitI__1FB8AE52]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[MaterialReturnDetail]  WITH CHECK ADD FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([Id])
GO
/****** Object:  ForeignKey [FK__PurchaseC__Chall__75C27486]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PurchaseChallanBillingAddress]  WITH CHECK ADD FOREIGN KEY([ChallanId])
REFERENCES [dbo].[PurchaseChallanMaster] ([Id])
GO
/****** Object:  ForeignKey [FK__PurchaseC__Chall__46D27B73]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PurchaseChallanDetail]  WITH CHECK ADD  CONSTRAINT [FK__PurchaseC__Chall__46D27B73] FOREIGN KEY([ChallanId])
REFERENCES [dbo].[PurchaseChallanMaster] ([Id])
GO
ALTER TABLE [dbo].[PurchaseChallanDetail] CHECK CONSTRAINT [FK__PurchaseC__Chall__46D27B73]
GO
/****** Object:  ForeignKey [FK_PurchaseDetail_PurchaseInvoice]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetail_PurchaseInvoice] FOREIGN KEY([PurchaseInvoiceId])
REFERENCES [dbo].[PurchaseInvoice] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseDetail] CHECK CONSTRAINT [FK_PurchaseDetail_PurchaseInvoice]
GO
/****** Object:  ForeignKey [FK__PurchaseO__Order__39788055]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PurchaseOrderAddress]  WITH CHECK ADD  CONSTRAINT [FK__PurchaseO__Order__39788055] FOREIGN KEY([OrderId])
REFERENCES [dbo].[PurchaseOrderMaster] ([Id])
GO
ALTER TABLE [dbo].[PurchaseOrderAddress] CHECK CONSTRAINT [FK__PurchaseO__Order__39788055]
GO
/****** Object:  ForeignKey [FK__PurchaseO__Order__34B3CB38]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PurchaseOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK__PurchaseO__Order__34B3CB38] FOREIGN KEY([OrderId])
REFERENCES [dbo].[PurchaseOrderMaster] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseOrderDetail] CHECK CONSTRAINT [FK__PurchaseO__Order__34B3CB38]
GO
/****** Object:  ForeignKey [FK_PurchaseOrderImpTransDoc_PurchaseOrderImpTransDoc]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PurchaseOrderImpTransDoc]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderImpTransDoc_PurchaseOrderImpTransDoc] FOREIGN KEY([Id])
REFERENCES [dbo].[PurchaseOrderImpTransDoc] ([Id])
GO
ALTER TABLE [dbo].[PurchaseOrderImpTransDoc] CHECK CONSTRAINT [FK_PurchaseOrderImpTransDoc_PurchaseOrderImpTransDoc]
GO
/****** Object:  ForeignKey [FK__PurchasePr__Unit__5AA469F6]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PurchaseProductBatch]  WITH CHECK ADD  CONSTRAINT [FK__PurchasePr__Unit__5AA469F6] FOREIGN KEY([Unit])
REFERENCES [dbo].[Unit] ([Id])
GO
ALTER TABLE [dbo].[PurchaseProductBatch] CHECK CONSTRAINT [FK__PurchasePr__Unit__5AA469F6]
GO
/****** Object:  ForeignKey [FK_PyCorporateAllowanceMapping_PyAllowanceMaster]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PyCorporateAllowanceMapping]  WITH CHECK ADD  CONSTRAINT [FK_PyCorporateAllowanceMapping_PyAllowanceMaster] FOREIGN KEY([AllowanceId])
REFERENCES [dbo].[PyAllowanceMaster] ([Id])
GO
ALTER TABLE [dbo].[PyCorporateAllowanceMapping] CHECK CONSTRAINT [FK_PyCorporateAllowanceMapping_PyAllowanceMaster]
GO
/****** Object:  ForeignKey [FK_PyCorporateAllowanceMapping_PyCorporateSalaryMaster]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PyCorporateAllowanceMapping]  WITH CHECK ADD  CONSTRAINT [FK_PyCorporateAllowanceMapping_PyCorporateSalaryMaster] FOREIGN KEY([CorporateId])
REFERENCES [dbo].[PyCorporateSalaryMaster] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PyCorporateAllowanceMapping] CHECK CONSTRAINT [FK_PyCorporateAllowanceMapping_PyCorporateSalaryMaster]
GO
/****** Object:  ForeignKey [FK_PyCorporateSalaryMaster_ScEmployeePost]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PyCorporateSalaryMaster]  WITH CHECK ADD  CONSTRAINT [FK_PyCorporateSalaryMaster_ScEmployeePost] FOREIGN KEY([EmployeePostId])
REFERENCES [dbo].[ScEmployeePost] ([Id])
GO
ALTER TABLE [dbo].[PyCorporateSalaryMaster] CHECK CONSTRAINT [FK_PyCorporateSalaryMaster_ScEmployeePost]
GO
/****** Object:  ForeignKey [FK_PyEmployeeDeductionMapping_PyDeductionMaster]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PyEmployeeDeductionMapping]  WITH CHECK ADD  CONSTRAINT [FK_PyEmployeeDeductionMapping_PyDeductionMaster] FOREIGN KEY([DeductionId])
REFERENCES [dbo].[PyDeductionMaster] ([Id])
GO
ALTER TABLE [dbo].[PyEmployeeDeductionMapping] CHECK CONSTRAINT [FK_PyEmployeeDeductionMapping_PyDeductionMaster]
GO
/****** Object:  ForeignKey [FK_PyEmployeeDeductionMapping_PyEmployeeDeductionMaster]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PyEmployeeDeductionMapping]  WITH CHECK ADD  CONSTRAINT [FK_PyEmployeeDeductionMapping_PyEmployeeDeductionMaster] FOREIGN KEY([EmployeeDeductionId])
REFERENCES [dbo].[PyEmployeeDeductionMaster] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PyEmployeeDeductionMapping] CHECK CONSTRAINT [FK_PyEmployeeDeductionMapping_PyEmployeeDeductionMaster]
GO
/****** Object:  ForeignKey [FK_PyEmployeeDeductionMaster_ScEmployeeInfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PyEmployeeDeductionMaster]  WITH CHECK ADD  CONSTRAINT [FK_PyEmployeeDeductionMaster_ScEmployeeInfo] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[ScEmployeeInfo] ([Id])
GO
ALTER TABLE [dbo].[PyEmployeeDeductionMaster] CHECK CONSTRAINT [FK_PyEmployeeDeductionMaster_ScEmployeeInfo]
GO
/****** Object:  ForeignKey [FK_PyEmployeeSalaryAllowanceMapping_PyAllowanceMaster]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PyEmployeeSalaryAllowanceMapping]  WITH CHECK ADD  CONSTRAINT [FK_PyEmployeeSalaryAllowanceMapping_PyAllowanceMaster] FOREIGN KEY([AllowanceId])
REFERENCES [dbo].[PyAllowanceMaster] ([Id])
GO
ALTER TABLE [dbo].[PyEmployeeSalaryAllowanceMapping] CHECK CONSTRAINT [FK_PyEmployeeSalaryAllowanceMapping_PyAllowanceMaster]
GO
/****** Object:  ForeignKey [FK_PyEmployeeSalaryAllowanceMapping_PyEmployeeSalaryMaster]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PyEmployeeSalaryAllowanceMapping]  WITH CHECK ADD  CONSTRAINT [FK_PyEmployeeSalaryAllowanceMapping_PyEmployeeSalaryMaster] FOREIGN KEY([EmployeeSalaryId])
REFERENCES [dbo].[PyEmployeeSalaryMaster] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PyEmployeeSalaryAllowanceMapping] CHECK CONSTRAINT [FK_PyEmployeeSalaryAllowanceMapping_PyEmployeeSalaryMaster]
GO
/****** Object:  ForeignKey [FK_PyEmployeeSalaryMaster_ScEmployeeInfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PyEmployeeSalaryMaster]  WITH CHECK ADD  CONSTRAINT [FK_PyEmployeeSalaryMaster_ScEmployeeInfo] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[ScEmployeeInfo] ([Id])
GO
ALTER TABLE [dbo].[PyEmployeeSalaryMaster] CHECK CONSTRAINT [FK_PyEmployeeSalaryMaster_ScEmployeeInfo]
GO
/****** Object:  ForeignKey [FK_PyTaxDeductionEmployeeMapping_PyTaxDeductionPattern]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PyTaxDeductionEmployeeMapping]  WITH CHECK ADD  CONSTRAINT [FK_PyTaxDeductionEmployeeMapping_PyTaxDeductionPattern] FOREIGN KEY([TaxDeductionId])
REFERENCES [dbo].[PyTaxDeductionPattern] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PyTaxDeductionEmployeeMapping] CHECK CONSTRAINT [FK_PyTaxDeductionEmployeeMapping_PyTaxDeductionPattern]
GO
/****** Object:  ForeignKey [FK_PyTaxDeductionEmployeeMapping_ScEmployeeInfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[PyTaxDeductionEmployeeMapping]  WITH CHECK ADD  CONSTRAINT [FK_PyTaxDeductionEmployeeMapping_ScEmployeeInfo] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[ScEmployeeInfo] ([Id])
GO
ALTER TABLE [dbo].[PyTaxDeductionEmployeeMapping] CHECK CONSTRAINT [FK_PyTaxDeductionEmployeeMapping_ScEmployeeInfo]
GO
/****** Object:  ForeignKey [FK__SalesChal__Chall__06ED0088]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SalesChallanBillingAddress]  WITH CHECK ADD FOREIGN KEY([ChallanId])
REFERENCES [dbo].[SalesChallanMaster] ([Id])
GO
/****** Object:  ForeignKey [FK__SalesChal__Chall__05F8DC4F]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SalesChallanDetail]  WITH CHECK ADD FOREIGN KEY([ChallanId])
REFERENCES [dbo].[SalesChallanMaster] ([Id])
GO
/****** Object:  ForeignKey [FK__SalesChal__Chall__0504B816]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SalesChallanImpTransDoc]  WITH CHECK ADD FOREIGN KEY([ChallanId])
REFERENCES [dbo].[SalesChallanMaster] ([Id])
GO
/****** Object:  ForeignKey [FK__SalesOrde__Order__09C96D33]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SalesOrderAddress]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[SalesOrderMaster] ([Id])
GO
/****** Object:  ForeignKey [FK__SalesOrde__Order__08D548FA]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SalesOrderDetail]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[SalesOrderMaster] ([Id])
GO
/****** Object:  ForeignKey [FK__SalesOrde__Order__0BB1B5A5]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SalesOrderOtherDetail]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[SalesOrderMaster] ([Id])
GO
/****** Object:  ForeignKey [FK_SalesReturnDetail_SalesReturnMaster]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SalesReturnDetail]  WITH CHECK ADD  CONSTRAINT [FK_SalesReturnDetail_SalesReturnMaster] FOREIGN KEY([SalesReturnId])
REFERENCES [dbo].[SalesReturnMaster] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesReturnDetail] CHECK CONSTRAINT [FK_SalesReturnDetail_SalesReturnMaster]
GO
/****** Object:  ForeignKey [FK_ScAbsentApplication_ScStudentinfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScAbsentApplication]  WITH CHECK ADD  CONSTRAINT [FK_ScAbsentApplication_ScStudentinfo] FOREIGN KEY([StudentId])
REFERENCES [dbo].[ScStudentinfo] ([StudentID])
GO
ALTER TABLE [dbo].[ScAbsentApplication] CHECK CONSTRAINT [FK_ScAbsentApplication_ScStudentinfo]
GO
/****** Object:  ForeignKey [FK_ScBillTransaction_ScStudentinfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScBillTransaction]  WITH CHECK ADD  CONSTRAINT [FK_ScBillTransaction_ScStudentinfo] FOREIGN KEY([StudentId])
REFERENCES [dbo].[ScStudentinfo] ([StudentID])
GO
ALTER TABLE [dbo].[ScBillTransaction] CHECK CONSTRAINT [FK_ScBillTransaction_ScStudentinfo]
GO
/****** Object:  ForeignKey [FK_ScBoaderMapping_ScBoader]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScBoaderMapping]  WITH CHECK ADD  CONSTRAINT [FK_ScBoaderMapping_ScBoader] FOREIGN KEY([BoaderId])
REFERENCES [dbo].[ScBoader] ([Id])
GO
ALTER TABLE [dbo].[ScBoaderMapping] CHECK CONSTRAINT [FK_ScBoaderMapping_ScBoader]
GO
/****** Object:  ForeignKey [FK_ScBoaderMapping_SchClass]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScBoaderMapping]  WITH CHECK ADD  CONSTRAINT [FK_ScBoaderMapping_SchClass] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScBoaderMapping] CHECK CONSTRAINT [FK_ScBoaderMapping_SchClass]
GO
/****** Object:  ForeignKey [FK_ScBookReceived_Product]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScBookReceived]  WITH CHECK ADD  CONSTRAINT [FK_ScBookReceived_Product] FOREIGN KEY([BookId])
REFERENCES [dbo].[ScBook] ([Id])
GO
ALTER TABLE [dbo].[ScBookReceived] CHECK CONSTRAINT [FK_ScBookReceived_Product]
GO
/****** Object:  ForeignKey [FK__ScBookRec__Maste__2DB1C7EE]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScBookReceivedDetails]  WITH CHECK ADD  CONSTRAINT [FK__ScBookRec__Maste__2DB1C7EE] FOREIGN KEY([MasterId])
REFERENCES [dbo].[ScBookReceived] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScBookReceivedDetails] CHECK CONSTRAINT [FK__ScBookRec__Maste__2DB1C7EE]
GO
/****** Object:  ForeignKey [FK_ScBusRouteDetails_ScBus]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScBusRouteDetails]  WITH CHECK ADD  CONSTRAINT [FK_ScBusRouteDetails_ScBus] FOREIGN KEY([BusId])
REFERENCES [dbo].[ScBus] ([Id])
GO
ALTER TABLE [dbo].[ScBusRouteDetails] CHECK CONSTRAINT [FK_ScBusRouteDetails_ScBus]
GO
/****** Object:  ForeignKey [FK_ScBusRouteDetails_ScBusStop]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScBusRouteDetails]  WITH CHECK ADD  CONSTRAINT [FK_ScBusRouteDetails_ScBusStop] FOREIGN KEY([BusStopId])
REFERENCES [dbo].[ScBusStop] ([Id])
GO
ALTER TABLE [dbo].[ScBusRouteDetails] CHECK CONSTRAINT [FK_ScBusRouteDetails_ScBusStop]
GO
/****** Object:  ForeignKey [FK_ScClassFeeRate_ScFeeItem]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScClassFeeRate]  WITH CHECK ADD  CONSTRAINT [FK_ScClassFeeRate_ScFeeItem] FOREIGN KEY([FeeItemId])
REFERENCES [dbo].[ScFeeItem] ([Id])
GO
ALTER TABLE [dbo].[ScClassFeeRate] CHECK CONSTRAINT [FK_ScClassFeeRate_ScFeeItem]
GO
/****** Object:  ForeignKey [FK_ScClassFeeRate_SchClass]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScClassFeeRate]  WITH CHECK ADD  CONSTRAINT [FK_ScClassFeeRate_SchClass] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScClassFeeRate] CHECK CONSTRAINT [FK_ScClassFeeRate_SchClass]
GO
/****** Object:  ForeignKey [FK_ScClassSubjectMapping_SchClass]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScClassSubjectMapping]  WITH CHECK ADD  CONSTRAINT [FK_ScClassSubjectMapping_SchClass] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScClassSubjectMapping] CHECK CONSTRAINT [FK_ScClassSubjectMapping_SchClass]
GO
/****** Object:  ForeignKey [FK_ScClassSubjectMapping_ScSubject]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScClassSubjectMapping]  WITH CHECK ADD  CONSTRAINT [FK_ScClassSubjectMapping_ScSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[ScSubject] ([Id])
GO
ALTER TABLE [dbo].[ScClassSubjectMapping] CHECK CONSTRAINT [FK_ScClassSubjectMapping_ScSubject]
GO
/****** Object:  ForeignKey [FK_ScEmployeeInfo_ScEmployeeCategory]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScEmployeeInfo]  WITH CHECK ADD  CONSTRAINT [FK_ScEmployeeInfo_ScEmployeeCategory] FOREIGN KEY([EmployeeCategoryId])
REFERENCES [dbo].[ScEmployeeCategory] ([Id])
GO
ALTER TABLE [dbo].[ScEmployeeInfo] CHECK CONSTRAINT [FK_ScEmployeeInfo_ScEmployeeCategory]
GO
/****** Object:  ForeignKey [FK_ScEmployeeInfo_ScEmployeeDepartment]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScEmployeeInfo]  WITH CHECK ADD  CONSTRAINT [FK_ScEmployeeInfo_ScEmployeeDepartment] FOREIGN KEY([EmployeeDepartmentId])
REFERENCES [dbo].[ScEmployeeDepartment] ([Id])
GO
ALTER TABLE [dbo].[ScEmployeeInfo] CHECK CONSTRAINT [FK_ScEmployeeInfo_ScEmployeeDepartment]
GO
/****** Object:  ForeignKey [FK_ScEmployeeInfo_ScEmployeePost]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScEmployeeInfo]  WITH CHECK ADD  CONSTRAINT [FK_ScEmployeeInfo_ScEmployeePost] FOREIGN KEY([EmployeePostId])
REFERENCES [dbo].[ScEmployeePost] ([Id])
GO
ALTER TABLE [dbo].[ScEmployeeInfo] CHECK CONSTRAINT [FK_ScEmployeeInfo_ScEmployeePost]
GO
/****** Object:  ForeignKey [FK_ScEmployeePost_ScEmployeeCategory]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScEmployeePost]  WITH CHECK ADD  CONSTRAINT [FK_ScEmployeePost_ScEmployeeCategory] FOREIGN KEY([EmployeeCategoryId])
REFERENCES [dbo].[ScEmployeeCategory] ([Id])
GO
ALTER TABLE [dbo].[ScEmployeePost] CHECK CONSTRAINT [FK_ScEmployeePost_ScEmployeeCategory]
GO
/****** Object:  ForeignKey [FK_ScExamAttendanceDetail_ScExamAttendanceMaster]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScExamAttendanceDetail]  WITH CHECK ADD  CONSTRAINT [FK_ScExamAttendanceDetail_ScExamAttendanceMaster] FOREIGN KEY([MasterId])
REFERENCES [dbo].[ScExamAttendanceMaster] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScExamAttendanceDetail] CHECK CONSTRAINT [FK_ScExamAttendanceDetail_ScExamAttendanceMaster]
GO
/****** Object:  ForeignKey [FK_ScExamAttendanceDetail_ScStudentinfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScExamAttendanceDetail]  WITH CHECK ADD  CONSTRAINT [FK_ScExamAttendanceDetail_ScStudentinfo] FOREIGN KEY([StudentId])
REFERENCES [dbo].[ScStudentinfo] ([StudentID])
GO
ALTER TABLE [dbo].[ScExamAttendanceDetail] CHECK CONSTRAINT [FK_ScExamAttendanceDetail_ScStudentinfo]
GO
/****** Object:  ForeignKey [FK_ScExamAttendanceMaster_ScExam]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScExamAttendanceMaster]  WITH CHECK ADD  CONSTRAINT [FK_ScExamAttendanceMaster_ScExam] FOREIGN KEY([ExamId])
REFERENCES [dbo].[ScExam] ([Id])
GO
ALTER TABLE [dbo].[ScExamAttendanceMaster] CHECK CONSTRAINT [FK_ScExamAttendanceMaster_ScExam]
GO
/****** Object:  ForeignKey [FK_ScExamAttendanceMaster_SchClass]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScExamAttendanceMaster]  WITH CHECK ADD  CONSTRAINT [FK_ScExamAttendanceMaster_SchClass] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScExamAttendanceMaster] CHECK CONSTRAINT [FK_ScExamAttendanceMaster_SchClass]
GO
/****** Object:  ForeignKey [FK_ScExamMarksEntry_ScExam]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScExamMarksEntry]  WITH CHECK ADD  CONSTRAINT [FK_ScExamMarksEntry_ScExam] FOREIGN KEY([ExamId])
REFERENCES [dbo].[ScExam] ([Id])
GO
ALTER TABLE [dbo].[ScExamMarksEntry] CHECK CONSTRAINT [FK_ScExamMarksEntry_ScExam]
GO
/****** Object:  ForeignKey [FK_ScExamMarksEntry_SchClass]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScExamMarksEntry]  WITH CHECK ADD  CONSTRAINT [FK_ScExamMarksEntry_SchClass] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScExamMarksEntry] CHECK CONSTRAINT [FK_ScExamMarksEntry_SchClass]
GO
/****** Object:  ForeignKey [FK_ScExamMarksEntry_ScStudentinfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScExamMarksEntry]  WITH CHECK ADD  CONSTRAINT [FK_ScExamMarksEntry_ScStudentinfo] FOREIGN KEY([StudentId])
REFERENCES [dbo].[ScStudentinfo] ([StudentID])
GO
ALTER TABLE [dbo].[ScExamMarksEntry] CHECK CONSTRAINT [FK_ScExamMarksEntry_ScStudentinfo]
GO
/****** Object:  ForeignKey [FK_ScExamMarkSetup_ScExam]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScExamMarkSetup]  WITH CHECK ADD  CONSTRAINT [FK_ScExamMarkSetup_ScExam] FOREIGN KEY([ExamId])
REFERENCES [dbo].[ScExam] ([Id])
GO
ALTER TABLE [dbo].[ScExamMarkSetup] CHECK CONSTRAINT [FK_ScExamMarkSetup_ScExam]
GO
/****** Object:  ForeignKey [FK_ScExamMarkSetup_SchClass]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScExamMarkSetup]  WITH CHECK ADD  CONSTRAINT [FK_ScExamMarkSetup_SchClass] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScExamMarkSetup] CHECK CONSTRAINT [FK_ScExamMarkSetup_SchClass]
GO
/****** Object:  ForeignKey [FK_ScExamMarkSetup_ScSubject]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScExamMarkSetup]  WITH CHECK ADD  CONSTRAINT [FK_ScExamMarkSetup_ScSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[ScSubject] ([Id])
GO
ALTER TABLE [dbo].[ScExamMarkSetup] CHECK CONSTRAINT [FK_ScExamMarkSetup_ScSubject]
GO
/****** Object:  ForeignKey [FK_ScExamRoutine_ScExam]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScExamRoutine]  WITH CHECK ADD  CONSTRAINT [FK_ScExamRoutine_ScExam] FOREIGN KEY([ExamId])
REFERENCES [dbo].[ScExam] ([Id])
GO
ALTER TABLE [dbo].[ScExamRoutine] CHECK CONSTRAINT [FK_ScExamRoutine_ScExam]
GO
/****** Object:  ForeignKey [FK_ScExamRoutine_SchClass]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScExamRoutine]  WITH CHECK ADD  CONSTRAINT [FK_ScExamRoutine_SchClass] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScExamRoutine] CHECK CONSTRAINT [FK_ScExamRoutine_SchClass]
GO
/****** Object:  ForeignKey [FK_ScExamRoutine_ScSubject]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScExamRoutine]  WITH CHECK ADD  CONSTRAINT [FK_ScExamRoutine_ScSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[ScSubject] ([Id])
GO
ALTER TABLE [dbo].[ScExamRoutine] CHECK CONSTRAINT [FK_ScExamRoutine_ScSubject]
GO
/****** Object:  ForeignKey [FK_FeeReceipt_SchClass]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScFeeReceipt]  WITH CHECK ADD  CONSTRAINT [FK_FeeReceipt_SchClass] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScFeeReceipt] CHECK CONSTRAINT [FK_FeeReceipt_SchClass]
GO
/****** Object:  ForeignKey [FK_FeeReceipt_ScStudentinfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScFeeReceipt]  WITH CHECK ADD  CONSTRAINT [FK_FeeReceipt_ScStudentinfo] FOREIGN KEY([StudentId])
REFERENCES [dbo].[ScStudentinfo] ([StudentID])
GO
ALTER TABLE [dbo].[ScFeeReceipt] CHECK CONSTRAINT [FK_FeeReceipt_ScStudentinfo]
GO
/****** Object:  ForeignKey [FK_ScFineScheme_SchClass]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScFineScheme]  WITH CHECK ADD  CONSTRAINT [FK_ScFineScheme_SchClass] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScFineScheme] CHECK CONSTRAINT [FK_ScFineScheme_SchClass]
GO
/****** Object:  ForeignKey [FK_ScFineSchemeDetails_ScFineScheme]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScFineSchemeDetails]  WITH CHECK ADD  CONSTRAINT [FK_ScFineSchemeDetails_ScFineScheme] FOREIGN KEY([MasterId])
REFERENCES [dbo].[ScFineScheme] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScFineSchemeDetails] CHECK CONSTRAINT [FK_ScFineSchemeDetails_ScFineScheme]
GO
/****** Object:  ForeignKey [FK_SchBuildingRoomMapping_SchBuilding]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SchBuildingRoomMapping]  WITH CHECK ADD  CONSTRAINT [FK_SchBuildingRoomMapping_SchBuilding] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[SchBuilding] ([Id])
GO
ALTER TABLE [dbo].[SchBuildingRoomMapping] CHECK CONSTRAINT [FK_SchBuildingRoomMapping_SchBuilding]
GO
/****** Object:  ForeignKey [FK_SchBuildingRoomMapping_ScRoom]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[SchBuildingRoomMapping]  WITH CHECK ADD  CONSTRAINT [FK_SchBuildingRoomMapping_ScRoom] FOREIGN KEY([RoomId])
REFERENCES [dbo].[ScRoom] ([Id])
GO
ALTER TABLE [dbo].[SchBuildingRoomMapping] CHECK CONSTRAINT [FK_SchBuildingRoomMapping_ScRoom]
GO
/****** Object:  ForeignKey [FK_ScHouseMapping_SchClass]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScHouseMapping]  WITH CHECK ADD  CONSTRAINT [FK_ScHouseMapping_SchClass] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScHouseMapping] CHECK CONSTRAINT [FK_ScHouseMapping_SchClass]
GO
/****** Object:  ForeignKey [FK_ScHouseMapping_ScHouseGroup]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScHouseMapping]  WITH CHECK ADD  CONSTRAINT [FK_ScHouseMapping_ScHouseGroup] FOREIGN KEY([HouseGroupId])
REFERENCES [dbo].[ScHouseGroup] ([Id])
GO
ALTER TABLE [dbo].[ScHouseMapping] CHECK CONSTRAINT [FK_ScHouseMapping_ScHouseGroup]
GO
/****** Object:  ForeignKey [FK_ScHouseMapping_ScStudentinfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScHouseMapping]  WITH CHECK ADD  CONSTRAINT [FK_ScHouseMapping_ScStudentinfo] FOREIGN KEY([StudentId])
REFERENCES [dbo].[ScStudentinfo] ([StudentID])
GO
ALTER TABLE [dbo].[ScHouseMapping] CHECK CONSTRAINT [FK_ScHouseMapping_ScStudentinfo]
GO
/****** Object:  ForeignKey [FK_ScMaterialIssueDetails_ScMaterialIssueMaster]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScMaterialIssueDetails]  WITH CHECK ADD  CONSTRAINT [FK_ScMaterialIssueDetails_ScMaterialIssueMaster] FOREIGN KEY([MaterialIssueMasterId])
REFERENCES [dbo].[ScMaterialIssueMaster] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScMaterialIssueDetails] CHECK CONSTRAINT [FK_ScMaterialIssueDetails_ScMaterialIssueMaster]
GO
/****** Object:  ForeignKey [FK_ScMaterialReturnDetails_ScMaterialReturnMaster]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScMaterialReturnDetails]  WITH CHECK ADD  CONSTRAINT [FK_ScMaterialReturnDetails_ScMaterialReturnMaster] FOREIGN KEY([MaterialReturnMasterId])
REFERENCES [dbo].[ScMaterialReturnMaster] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScMaterialReturnDetails] CHECK CONSTRAINT [FK_ScMaterialReturnDetails_ScMaterialReturnMaster]
GO
/****** Object:  ForeignKey [FK_ScMonthlyBill_ScFeeItem]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScMonthlyBill]  WITH CHECK ADD  CONSTRAINT [FK_ScMonthlyBill_ScFeeItem] FOREIGN KEY([FeeItemId])
REFERENCES [dbo].[ScFeeItem] ([Id])
GO
ALTER TABLE [dbo].[ScMonthlyBill] CHECK CONSTRAINT [FK_ScMonthlyBill_ScFeeItem]
GO
/****** Object:  ForeignKey [FK_ScMonthlyBill_ScMonthlyBillStudent]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScMonthlyBill]  WITH CHECK ADD  CONSTRAINT [FK_ScMonthlyBill_ScMonthlyBillStudent] FOREIGN KEY([MonthlyBillStudentId])
REFERENCES [dbo].[ScMonthlyBillStudent] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScMonthlyBill] CHECK CONSTRAINT [FK_ScMonthlyBill_ScMonthlyBillStudent]
GO
/****** Object:  ForeignKey [FK_ScMonthlyBillStudent_SchClass]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScMonthlyBillStudent]  WITH CHECK ADD  CONSTRAINT [FK_ScMonthlyBillStudent_SchClass] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScMonthlyBillStudent] CHECK CONSTRAINT [FK_ScMonthlyBillStudent_SchClass]
GO
/****** Object:  ForeignKey [FK_ScMonthlyBillStudent_ScStudentinfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScMonthlyBillStudent]  WITH CHECK ADD  CONSTRAINT [FK_ScMonthlyBillStudent_ScStudentinfo] FOREIGN KEY([StudentId])
REFERENCES [dbo].[ScStudentinfo] ([StudentID])
GO
ALTER TABLE [dbo].[ScMonthlyBillStudent] CHECK CONSTRAINT [FK_ScMonthlyBillStudent_ScStudentinfo]
GO
/****** Object:  ForeignKey [FK_ScPrePaidScheme_ScPrePaidScheme]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScPrePaidScheme]  WITH CHECK ADD  CONSTRAINT [FK_ScPrePaidScheme_ScPrePaidScheme] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScPrePaidScheme] CHECK CONSTRAINT [FK_ScPrePaidScheme_ScPrePaidScheme]
GO
/****** Object:  ForeignKey [FK_ScPrePaidSchemeDetails_ScPrePaidScheme]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScPrePaidSchemeDetails]  WITH CHECK ADD  CONSTRAINT [FK_ScPrePaidSchemeDetails_ScPrePaidScheme] FOREIGN KEY([MasterId])
REFERENCES [dbo].[ScPrePaidScheme] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScPrePaidSchemeDetails] CHECK CONSTRAINT [FK_ScPrePaidSchemeDetails_ScPrePaidScheme]
GO
/****** Object:  ForeignKey [FK_ClassRoom_ClassRoom]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScRoom]  WITH CHECK ADD  CONSTRAINT [FK_ClassRoom_ClassRoom] FOREIGN KEY([Id])
REFERENCES [dbo].[ScRoom] ([Id])
GO
ALTER TABLE [dbo].[ScRoom] CHECK CONSTRAINT [FK_ClassRoom_ClassRoom]
GO
/****** Object:  ForeignKey [FK_ScStaffSubjectMapping_ScEmployeeInfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStaffSubjectMapping]  WITH CHECK ADD  CONSTRAINT [FK_ScStaffSubjectMapping_ScEmployeeInfo] FOREIGN KEY([StaffId])
REFERENCES [dbo].[ScEmployeeInfo] ([Id])
GO
ALTER TABLE [dbo].[ScStaffSubjectMapping] CHECK CONSTRAINT [FK_ScStaffSubjectMapping_ScEmployeeInfo]
GO
/****** Object:  ForeignKey [FK_ScStaffSubjectMapping_ScSubject]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStaffSubjectMapping]  WITH CHECK ADD  CONSTRAINT [FK_ScStaffSubjectMapping_ScSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[ScSubject] ([Id])
GO
ALTER TABLE [dbo].[ScStaffSubjectMapping] CHECK CONSTRAINT [FK_ScStaffSubjectMapping_ScSubject]
GO
/****** Object:  ForeignKey [FK__ScStudent__Activ__03BB8E22]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentExtraActivity]  WITH CHECK ADD  CONSTRAINT [FK__ScStudent__Activ__03BB8E22] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[ScExtraActivity] ([Id])
GO
ALTER TABLE [dbo].[ScStudentExtraActivity] CHECK CONSTRAINT [FK__ScStudent__Activ__03BB8E22]
GO
/****** Object:  ForeignKey [FK__ScStudent__Class__02C769E9]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentExtraActivity]  WITH CHECK ADD  CONSTRAINT [FK__ScStudent__Class__02C769E9] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScStudentExtraActivity] CHECK CONSTRAINT [FK__ScStudent__Class__02C769E9]
GO
/****** Object:  ForeignKey [FK__ScStudent__Maste__0880433F]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentExtraActivityDetails]  WITH CHECK ADD  CONSTRAINT [FK__ScStudent__Maste__0880433F] FOREIGN KEY([MasterId])
REFERENCES [dbo].[ScStudentExtraActivity] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScStudentExtraActivityDetails] CHECK CONSTRAINT [FK__ScStudent__Maste__0880433F]
GO
/****** Object:  ForeignKey [FK_ScStudentExtraActivityDetails_ScStudentinfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentExtraActivityDetails]  WITH CHECK ADD  CONSTRAINT [FK_ScStudentExtraActivityDetails_ScStudentinfo] FOREIGN KEY([StudentId])
REFERENCES [dbo].[ScStudentinfo] ([StudentID])
GO
ALTER TABLE [dbo].[ScStudentExtraActivityDetails] CHECK CONSTRAINT [FK_ScStudentExtraActivityDetails_ScStudentinfo]
GO
/****** Object:  ForeignKey [FK_ScStudentFeeRateDetail_ScFeeItem]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentFeeRateDetail]  WITH CHECK ADD  CONSTRAINT [FK_ScStudentFeeRateDetail_ScFeeItem] FOREIGN KEY([FeeItemId])
REFERENCES [dbo].[ScFeeItem] ([Id])
GO
ALTER TABLE [dbo].[ScStudentFeeRateDetail] CHECK CONSTRAINT [FK_ScStudentFeeRateDetail_ScFeeItem]
GO
/****** Object:  ForeignKey [FK_ScStudentFeeRateDetail_ScStudentFeeRateMaster]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentFeeRateDetail]  WITH CHECK ADD  CONSTRAINT [FK_ScStudentFeeRateDetail_ScStudentFeeRateMaster] FOREIGN KEY([MasterId])
REFERENCES [dbo].[ScStudentFeeRateMaster] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScStudentFeeRateDetail] CHECK CONSTRAINT [FK_ScStudentFeeRateDetail_ScStudentFeeRateMaster]
GO
/****** Object:  ForeignKey [FK_ScStudentFeeRateMaster_SchClass]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentFeeRateMaster]  WITH CHECK ADD  CONSTRAINT [FK_ScStudentFeeRateMaster_SchClass] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScStudentFeeRateMaster] CHECK CONSTRAINT [FK_ScStudentFeeRateMaster_SchClass]
GO
/****** Object:  ForeignKey [FK_ScStudentFeeRateMaster_ScStudentinfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentFeeRateMaster]  WITH CHECK ADD  CONSTRAINT [FK_ScStudentFeeRateMaster_ScStudentinfo] FOREIGN KEY([StudentId])
REFERENCES [dbo].[ScStudentinfo] ([StudentID])
GO
ALTER TABLE [dbo].[ScStudentFeeRateMaster] CHECK CONSTRAINT [FK_ScStudentFeeRateMaster_ScStudentinfo]
GO
/****** Object:  ForeignKey [FK_ScStudentFeeTerm_ScFeeTerm]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentFeeTerm]  WITH CHECK ADD  CONSTRAINT [FK_ScStudentFeeTerm_ScFeeTerm] FOREIGN KEY([FeeTermId])
REFERENCES [dbo].[ScFeeTerm] ([Id])
GO
ALTER TABLE [dbo].[ScStudentFeeTerm] CHECK CONSTRAINT [FK_ScStudentFeeTerm_ScFeeTerm]
GO
/****** Object:  ForeignKey [FK_ScStudentinfo_ScStudentCategory]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentinfo]  WITH CHECK ADD  CONSTRAINT [FK_ScStudentinfo_ScStudentCategory] FOREIGN KEY([StdCategoryId])
REFERENCES [dbo].[ScStudentCategory] ([Id])
GO
ALTER TABLE [dbo].[ScStudentinfo] CHECK CONSTRAINT [FK_ScStudentinfo_ScStudentCategory]
GO
/****** Object:  ForeignKey [FK_ScStudentRegistration_SchClass]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentRegistration]  WITH CHECK ADD  CONSTRAINT [FK_ScStudentRegistration_SchClass] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScStudentRegistration] CHECK CONSTRAINT [FK_ScStudentRegistration_SchClass]
GO
/****** Object:  ForeignKey [FK_ScStudentRegistrationDetail_ScStudentinfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentRegistrationDetail]  WITH CHECK ADD  CONSTRAINT [FK_ScStudentRegistrationDetail_ScStudentinfo] FOREIGN KEY([StudentId])
REFERENCES [dbo].[ScStudentinfo] ([StudentID])
GO
ALTER TABLE [dbo].[ScStudentRegistrationDetail] CHECK CONSTRAINT [FK_ScStudentRegistrationDetail_ScStudentinfo]
GO
/****** Object:  ForeignKey [FK_ScStudentRegistrationDetail_ScStudentRegistration]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentRegistrationDetail]  WITH CHECK ADD  CONSTRAINT [FK_ScStudentRegistrationDetail_ScStudentRegistration] FOREIGN KEY([RegMasterId])
REFERENCES [dbo].[ScStudentRegistration] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScStudentRegistrationDetail] CHECK CONSTRAINT [FK_ScStudentRegistrationDetail_ScStudentRegistration]
GO
/****** Object:  ForeignKey [FK_ScStudentSubjectMapping_SchClass]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentSubjectMapping]  WITH CHECK ADD  CONSTRAINT [FK_ScStudentSubjectMapping_SchClass] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScStudentSubjectMapping] CHECK CONSTRAINT [FK_ScStudentSubjectMapping_SchClass]
GO
/****** Object:  ForeignKey [FK_ScStudentSubjectMapping_ScStudentinfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentSubjectMapping]  WITH CHECK ADD  CONSTRAINT [FK_ScStudentSubjectMapping_ScStudentinfo] FOREIGN KEY([StudentId])
REFERENCES [dbo].[ScStudentinfo] ([StudentID])
GO
ALTER TABLE [dbo].[ScStudentSubjectMapping] CHECK CONSTRAINT [FK_ScStudentSubjectMapping_ScStudentinfo]
GO
/****** Object:  ForeignKey [FK_ScStudentSubjectMapping_ScSubject]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScStudentSubjectMapping]  WITH CHECK ADD  CONSTRAINT [FK_ScStudentSubjectMapping_ScSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[ScSubject] ([Id])
GO
ALTER TABLE [dbo].[ScStudentSubjectMapping] CHECK CONSTRAINT [FK_ScStudentSubjectMapping_ScSubject]
GO
/****** Object:  ForeignKey [FK_ScTeacherSchedule_ScClassScheduleDetail]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScTeacherSchedule]  WITH CHECK ADD  CONSTRAINT [FK_ScTeacherSchedule_ScClassScheduleDetail] FOREIGN KEY([ClassScheduleDetailId])
REFERENCES [dbo].[ScClassScheduleDetail] ([Id])
GO
ALTER TABLE [dbo].[ScTeacherSchedule] CHECK CONSTRAINT [FK_ScTeacherSchedule_ScClassScheduleDetail]
GO
/****** Object:  ForeignKey [FK_ScTeacherSchedule_ScEmployeeInfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScTeacherSchedule]  WITH CHECK ADD  CONSTRAINT [FK_ScTeacherSchedule_ScEmployeeInfo] FOREIGN KEY([StaffId])
REFERENCES [dbo].[ScEmployeeInfo] ([Id])
GO
ALTER TABLE [dbo].[ScTeacherSchedule] CHECK CONSTRAINT [FK_ScTeacherSchedule_ScEmployeeInfo]
GO
/****** Object:  ForeignKey [FK_ScTransportMapping_SchClass]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScTransportMapping]  WITH CHECK ADD  CONSTRAINT [FK_ScTransportMapping_SchClass] FOREIGN KEY([ClassId])
REFERENCES [dbo].[SchClass] ([Id])
GO
ALTER TABLE [dbo].[ScTransportMapping] CHECK CONSTRAINT [FK_ScTransportMapping_SchClass]
GO
/****** Object:  ForeignKey [FK_ScTransportMapping_ScStudentinfo]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[ScTransportMapping]  WITH CHECK ADD  CONSTRAINT [FK_ScTransportMapping_ScStudentinfo] FOREIGN KEY([StudentId])
REFERENCES [dbo].[ScStudentinfo] ([StudentID])
GO
ALTER TABLE [dbo].[ScTransportMapping] CHECK CONSTRAINT [FK_ScTransportMapping_ScStudentinfo]
GO
/****** Object:  ForeignKey [FK__StockAdju__Adjus__6F1576F7]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[StockAdjustmentDetail]  WITH CHECK ADD FOREIGN KEY([AdjustmentId])
REFERENCES [dbo].[StockAdjustmentMaster] ([Id])
GO
/****** Object:  ForeignKey [FK_StockTransferDetail_Godown]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[StockTransferDetail]  WITH CHECK ADD  CONSTRAINT [FK_StockTransferDetail_Godown] FOREIGN KEY([Godown])
REFERENCES [dbo].[Godown] ([Id])
GO
ALTER TABLE [dbo].[StockTransferDetail] CHECK CONSTRAINT [FK_StockTransferDetail_Godown]
GO
/****** Object:  ForeignKey [FK_StockTransferDetail_StockTransferMaster]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[StockTransferDetail]  WITH CHECK ADD  CONSTRAINT [FK_StockTransferDetail_StockTransferMaster] FOREIGN KEY([StockTransferMasterId])
REFERENCES [dbo].[StockTransferMaster] ([Id])
GO
ALTER TABLE [dbo].[StockTransferDetail] CHECK CONSTRAINT [FK_StockTransferDetail_StockTransferMaster]
GO
/****** Object:  ForeignKey [FK__StockTran__Godow__70099B30]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[StockTransferMaster]  WITH CHECK ADD FOREIGN KEY([GodownId])
REFERENCES [dbo].[Godown] ([Id])
GO
/****** Object:  ForeignKey [FK__StockTran__Godow__70FDBF69]    Script Date: 07/22/2013 11:02:01 ******/
ALTER TABLE [dbo].[StockTransferMaster]  WITH CHECK ADD FOREIGN KEY([GodownId])
REFERENCES [dbo].[Godown] ([Id])
GO
