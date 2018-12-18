CREATE Proc [dbo].[SP_PLTotalForDashBoard]
@StartDate datetime,
@EndDate datetime,
@FYId int,
@BranchId int =0 
as
--EXEC SP_PLTotalForDashBoard '2013-01-01','2014-01-01',1 
Declare @FromDate datetime 
Declare @DayName Nvarchar(255)
Declare @NoOfDayName int
set @DayName=(select datename(dw,getdate()))
set @NoOfDayName =(select DATEPART(weekday,getdate()))
if (@NoOfDayName=1)
	set @FromDate = convert(Date,getdate())
else if (@NoOfDayName=2)
	set @FromDate = convert(Date,getdate()-@NoOfDayName)
else if (@NoOfDayName=3)
	set @FromDate = convert(Date,getdate()-@NoOfDayName)
else if (@NoOfDayName=4)
	set @FromDate = convert(Date,getdate()-@NoOfDayName)
else if (@NoOfDayName=5)
	set @FromDate = convert(Date,getdate()-@NoOfDayName)
else if (@NoOfDayName=6)
	set @FromDate = convert(Date,getdate()-@NoOfDayName)
else if (@NoOfDayName=7)
	set @FromDate = convert(Date,getdate()-@NoOfDayName)
--select @FromDate
	
Select t.Header,Sum(t.ThisWeekTotal) as ThisWeekTotal ,Sum(t.ThisMonthTotal) as ThisMonthTotal ,Sum(t.ThisYearTotal) as ThisYearTotal from ( 
Select Case When ag.GroupAccountType  = 'E' Then 'Expenditure' Else 'Income' end as Header,
0 ThisWeekTotal,0 ThisMonthTotal,
case when ag.GroupAccountType = 'E' Then SUM(DrAmt) - SUM(Cramt) Else SUM(CrAmt) - SUM(Dramt) End as ThisYearTotal
 from AccountTransaction at
Inner join Ledger l On at.GlCode = l.Id
inner join AccountGroup ag on l.AccountGroupId = ag.Id
Where at.VDate <= @EndDate and (ag.GroupAccountType='E' OR ag.GroupAccountType='I') and at.FYId=@FYId and (@BranchId=0 or at.BranchId=@BranchId)
Group By GroupAccountType
Union All
Select Case When ag.GroupAccountType  = 'E' Then 'Expenditure' Else 'Income' end as Header,0 ThisWeekTotal,
case when ag.GroupAccountType = 'E' Then SUM(DrAmt) - SUM(Cramt) Else SUM(CrAmt) - SUM(Dramt) End as ThisMonthTotal,0 ThisYearTotal
 from AccountTransaction at
Inner join Ledger l On at.GlCode = l.Id
inner join AccountGroup ag on l.AccountGroupId = ag.Id
Where at.VDate <= @EndDate and (ag.GroupAccountType='E' OR ag.GroupAccountType='I') and at.FYId=@FYId and (@BranchId=0 or at.BranchId=@BranchId) 
and  Month(at.VDate)=MONTH(GETDATE())
Group By GroupAccountType,Month(at.VDate)
Union All
Select Case When ag.GroupAccountType  = 'E' Then 'Expenditure' Else 'Income' end as Header,
case when ag.GroupAccountType = 'E' Then SUM(DrAmt) - SUM(Cramt) Else SUM(CrAmt) - SUM(Dramt) End as ThisWeekTotal,0 ThisMonthTotal ,0 ThisYearTotal
 from AccountTransaction at
Inner join Ledger l On at.GlCode = l.Id
inner join AccountGroup ag on l.AccountGroupId = ag.Id
Where at.VDate Between @FromDate and GETDATE() and (ag.GroupAccountType='E' OR ag.GroupAccountType='I') and at.FYId=@FYId and (@BranchId=0 or at.BranchId=@BranchId) 
Group By GroupAccountType,Month(at.VDate)
) t Group By t.Header

GO
----------

CREATE Proc [dbo].[SP_AccountWatchList]
@StartDate Datetime,
@EndDate DateTime,
@FYId int,
@LedgerCategory varchar(10)='A',
@LedgerList varchar(max)='0',
@TopLedger int=0
as
Begin
--EXEC SP_AccountWatchList '2013-01-01','2014-01-01',1,'A','0'
 if @TopLedger=0
 begin
	select l.Id,l.AccountName,IsNull(SUM(LocalDrAmt),0)-IsNull(SUM(LocalCrAmt),0) Total
	from AccountTransaction as AT Inner Join Ledger as L On L.Id=AT.GlCode
	Where FYId = @FYId --and VDate Between @StartDate and @EndDate 
	and (@LedgerCategory='A' or l.Category in(SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@LedgerCategory, ','))) 
	And (@LedgerList='0' OR l.Id In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@LedgerList, ',')))	
	Group By l.Id,l.AccountName
	order By Total desc,l.Id
end
else
begin 
	select Top(@TopLedger) l.Id,l.AccountName,IsNull(SUM(LocalDrAmt),0)-IsNull(SUM(LocalCrAmt),0) Total
	from AccountTransaction as AT Inner Join Ledger as L On L.Id=AT.GlCode
	Where FYId = @FYId --and VDate Between @StartDate and @EndDate 
	and (@LedgerCategory='A' or l.Category in(SELECT CAST(data as varchar) as id FROM [splitstring_to_table](@LedgerCategory, ','))) 
	And (@LedgerList='0' OR l.Id In ( SELECT CAST(data as int) as id FROM [splitstring_to_table](@LedgerList, ',')))	
	Group By l.Id,l.AccountName
	order By Total desc,l.Id
end
end

GO


---------------------
CREATE Proc [dbo].[SP_BankFlowMonthly]
@StartDate Datetime,@EndDate DateTime,@FYId int
as
Begin
--EXEC SP_BankFlowMonthly '2013-01-01','2014-01-01',1
Declare @DateType int
Select @DateType = (select IsNull(DateType,0) from SystemControl)

if (@DateType=1)
begin
	Select NoOfMonth, SUM(Total) Total from(
		Select 1 NoOfMonth,0 Total
		Union All
		Select 2 NoOfMonth,0 Total
		Union All
		Select 3 NoOfMonth,0 Total
		Union All
		Select 4 NoOfMonth,0 Total
		Union All
		Select 5 NoOfMonth,0 Total
		Union All
		Select 6 NoOfMonth,0 Total
		Union All
		Select 7 NoOfMonth,0 Total
		Union All
		Select 8 NoOfMonth,0 Total
		Union All
		Select 9 NoOfMonth,0 Total
		Union All
		Select 10 NoOfMonth,0 Total
		Union All
		Select 11 NoOfMonth,0 Total
		Union All
		Select 12 NoOfMonth,0 Total
		Union All
		select Month(Vdate)NoOfMonth,--l.Id,l.AccountName,
		IsNull(SUM(LocalDrAmt),0)-IsNull(SUM(LocalCrAmt),0) Total
		from AccountTransaction as AT Inner Join Ledger as L On L.Id=AT.GlCode
		Where VDate Between @StartDate and @EndDate and FYId = @FYId
		and l.IsCashBank=1 and l.IsCashBook<>1
		Group By Month(Vdate)--,l.Id,l.AccountName
	) as aa	 Group by NoOfMonth
	order By NoOfMonth 	
	
end
else
begin
	Select NoOfMonth, SUM(Total) Total from(
		Select 1 NoOfMonth,0 Total
		Union All
		Select 2 NoOfMonth,0 Total
		Union All
		Select 3 NoOfMonth,0 Total
		Union All
		Select 4 NoOfMonth,0 Total
		Union All
		Select 5 NoOfMonth,0 Total
		Union All
		Select 6 NoOfMonth,0 Total
		Union All
		Select 7 NoOfMonth,0 Total
		Union All
		Select 8 NoOfMonth,0 Total
		Union All
		Select 9 NoOfMonth,0 Total
		Union All
		Select 10 NoOfMonth,0 Total
		Union All
		Select 11 NoOfMonth,0 Total
		Union All
		Select 12 NoOfMonth,0 Total
		Union All
		select isnull(Month(VMiti),3) NoOfMonth,--l.Id,l.AccountName,
		IsNull(SUM(LocalDrAmt),0)-IsNull(SUM(LocalCrAmt),0) Total
		from AccountTransaction as AT Inner Join Ledger as L On L.Id=AT.GlCode
		Where VDate Between @StartDate and @EndDate and FYId = @FYId
		and l.IsCashBank=1 and l.IsCashBook<>1
		Group By isnull(Month(VMiti),3)--,l.Id,l.AccountName 	
	) as aa	 Group by NoOfMonth
	order By NoOfMonth 
end
end

GO
--------------------------

CREATE Proc [dbo].[SP_TopItems]
@StartDate Datetime,
@EndDate DateTime,
@FYId int,
@TopItems int,
@BranchId int
as
Begin
--EXEC SP_TopItems '2013-01-01','2014-01-01',1,5
	Select Top(@TopItems) P.Id,P.ShortName, P.Name,Sum(ST.NetAmt)Amount from StockTransaction as ST
	Inner Join Product as P On P.Id=ST.ProductCode
	Where Source='SB' and FYId = @FYId and VDate Between @StartDate-1 and @EndDate  
	and (@BranchId=0 or ST.BranchId=@BranchId)
	Group by P.Id,P.ShortName,P.Name
	Order by Amount desc
end

GO


--------------------------------
CREATE Proc [dbo].[SP_CashFlowMonthly]
@StartDate Datetime,@EndDate DateTime,@FYId int
as
Begin
--EXEC SP_CashFlowMonthly '2013-01-01','2014-01-01',1  
	Declare @DateType int
	Select @DateType = (select IsNull(DateType,0) from SystemControl)
	Declare @CasbookAc int
	Select @CasbookAc = (select IsNull(CashBook,0) from SystemControl)

	if (@DateType=1)
	begin
		Select NoOfMonth, SUM(Total) Total from(
		Select 1 NoOfMonth,0 Total
		Union All
		Select 2 NoOfMonth,0 Total
		Union All
		Select 3 NoOfMonth,0 Total
		Union All
		Select 4 NoOfMonth,0 Total
		Union All
		Select 5 NoOfMonth,0 Total
		Union All
		Select 6 NoOfMonth,0 Total
		Union All
		Select 7 NoOfMonth,0 Total
		Union All
		Select 8 NoOfMonth,0 Total
		Union All
		Select 9 NoOfMonth,0 Total
		Union All
		Select 10 NoOfMonth,0 Total
		Union All
		Select 11 NoOfMonth,0 Total
		Union All
		Select 12 NoOfMonth,0 Total
		Union All
		select Month(Vdate)NoOfMonth, IsNull(SUM(LocalDrAmt),0)-IsNull(SUM(LocalCrAmt),0) Total
		from AccountTransaction	 as AT Inner Join Ledger as L On L.Id=AT.GlCode
		Where VDate Between @StartDate and @EndDate and GlCode = @CasbookAc and FYId = @FYId
		and l.IsCashBank=1 and l.IsCashBook=1
		Group By Month(Vdate) 
		) as aa	 Group by NoOfMonth
		order By NoOfMonth 
		
	end
	else
	begin
		Select NoOfMonth, SUM(Total) Total from(
		Select 1 NoOfMonth,0 Total
		Union All
		Select 2 NoOfMonth,0 Total
		Union All
		Select 3 NoOfMonth,0 Total
		Union All
		Select 4 NoOfMonth,0 Total
		Union All
		Select 5 NoOfMonth,0 Total
		Union All
		Select 6 NoOfMonth,0 Total
		Union All
		Select 7 NoOfMonth,0 Total
		Union All
		Select 8 NoOfMonth,0 Total
		Union All
		Select 9 NoOfMonth,0 Total
		Union All
		Select 10 NoOfMonth,0 Total
		Union All
		Select 11 NoOfMonth,0 Total
		Union All
		Select 12 NoOfMonth,0 Total
		Union All
		select isnull(Month(VMiti),3) NoOfMonth,IsNull(SUM(LocalDrAmt),0)-IsNull(SUM(LocalCrAmt),0) Total 
		from AccountTransaction as AT Inner Join Ledger as L On L.Id=AT.GlCode		
		Where VDate Between @StartDate and @EndDate and GlCode = @CasbookAc and FYId = @FYId
		and l.IsCashBank=1 and l.IsCashBook=1
		Group By isnull(Month(VMiti),3) 
		) as aa	 Group by NoOfMonth
		order By NoOfMonth 
	end
end

GO


