
GO
/****** Object:  Table [dbo].[DeviceLog]    Script Date: 11/12/2013 15:38:32 ******/
DROP TABLE [dbo].[DeviceLog]
GO
/****** Object:  Table [dbo].[Attendance_Details]    Script Date: 11/12/2013 15:38:31 ******/
DROP TABLE [dbo].[Attendance_Details]
GO
/****** Object:  Table [dbo].[ModuleList]    Script Date: 11/12/2013 15:38:32 ******/
DROP TABLE [dbo].[ModuleList]
GO
/****** Object:  Table [dbo].[AttendanceLog]    Script Date: 11/12/2013 15:38:32 ******/
DROP TABLE [dbo].[AttendanceLog]

/****** Object:  Table [dbo].[MenuItem]    Script Date: 11/12/2013 15:38:32 ******/

GO
DROP TABLE [dbo].[MenuItem]
GO
/****** Object:  Table [dbo].[Design]    Script Date: 11/12/2013 15:38:32 ******/
DROP TABLE [dbo].[Design]
GO
/****** Object:  Table [dbo].[Device]    Script Date: 11/12/2013 15:38:32 ******/
DROP TABLE [dbo].[Device]
GO
/****** Object:  Table [dbo].[Device]    Script Date: 11/12/2013 15:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Device](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DeviceID] [int] NOT NULL,
	[ComNo] [varchar](4) NULL,
	[DeviceDescription] [varchar](255) NULL,
	[PortNo] [varchar](10) NULL,
	[IPAddress] [varchar](18) NULL,
	[BaudRate] [varchar](10) NULL,
	[ConMode] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Design]    Script Date: 11/12/2013 15:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Design](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Module] [nvarchar](50) NULL,
	[Header] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[Footer] [nvarchar](max) NULL,
 CONSTRAINT [PK_Design] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuItem]    Script Date: 11/12/2013 15:38:32 ******/
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
	[CategoryId] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_MenuItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[MenuItem] ON
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (1, N'Index', N'Home', N'Dashboard', 255, NULL, 1, N'Accounting', 1)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (2, NULL, N'Master', N'Account Master', 105, NULL, 2, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (3, N'Ledger', N'Master', N'Ledger Master', 1, 2, 1, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (4, N'SubLedger', N'Master', N'Sub Ledger', 2, 2, 2, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (5, N'AccountGroup', N'Master', N'Account Group', 3, 2, 3, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (6, N'AccountSubGroup', N'Master', N'Account Sub Group', 4, 2, 4, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (7, N'Product', N'Master', N'Product', 5, 2, 5, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (8, N'SalesBillingTerm', N'Master', N'Bill Terms', 12, 2, 6, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (9, N'Area', N'Master', N'Area', 15, 2, 7, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (10, N'Agent', N'Master', N'Agent', 14, 2, 8, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (11, N'Currency', N'Master', N'Currency', 20, 2, 9, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (12, N'Narration', N'Master', N'Narration', 22, 2, 10, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (13, N'Udf', N'Master', N'User Defined Field', 10, 2, 11, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (14, N'OpeningLedger', N'Master', N'Opening Ledger', 23, 2, 12, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (15, NULL, N'Entry', N'Entry', 106, NULL, 3, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (16, N'CashBankVoucher', N'Entry', N'Cash/Bank', 34, 15, 1, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (17, N'JournalVoucher', N'Entry', N'Journal Voucher', 35, 15, 2, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (18, N'PurchaseInvoice', N'Entry', N'Purchase', 41, 15, 3, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (19, N'SalesInvoice', N'Entry', N'Sales', 49, 15, 4, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (20, NULL, N'ReportLedger', N'Ledger Report', 270, NULL, 8, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (21, N'CashBankBook', N'ReportLedger', N'Cash/Bank Book', 122, 20, 1, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (22, N'CashBankBookSummary', N'ReportLedger', N'Cash/Bank Summary', 123, 20, 2, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (23, N'JournalVoucher', N'ReportLedger', N'Journal ', 124, 20, 3, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (24, N'DayBook', N'ReportLedger', N'Day Book', 125, 20, 4, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (25, N'LedgerReport', N'ReportLedger', N'Ledger', 126, 20, 5, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (26, N'TrialBalance', N'ReportLedger', N'Trial Balance', 127, 20, 6, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (27, N'ProfitAndLoss', N'ReportLedger', N'Profit & Loss', 128, 20, 7, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (28, N'BalanceSheet', N'ReportLedger', N'Balance Sheet', 129, 20, 8, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (29, NULL, N'ARAP', N'AR/AP Report', 110, NULL, 9, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (30, N'PartyLedger', N'ARAP', N'Party Ledger', 119, 29, 1, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (31, N'OutStandingCustomer', N'ARAP', N'Outstanding Customer', 120, 29, 2, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (32, N'OutstandingSupplier', N'ARAP', N'Outstanding Vender', 121, 29, 3, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (33, N'SalesSummary', N'ARAP', N'Sales', 277, 29, 4, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (35, N'PurchaseSummary', N'ARAP', N'Purchase', 279, 29, 10, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (36, NULL, N'Setup', N'Setup', 111, NULL, 13, N'Accounting', 8)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (37, N'FiscalYear', N'Setup', N'Fiscal Year', 269, 36, 2, N'Accounting', 8)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (38, N'SystemControl', N'Setup', N'System Control', 268, 36, 3, N'Accounting', 8)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (39, N'DocumentNumberingScheme', N'Setup', N'Document Numbering', 28, 36, 4, N'Accounting', 8)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (40, N'Users', N'User', N'User Master', 29, 36, 5, N'Accounting', 8)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (41, NULL, N'Management', N'Management', 0, NULL, 12, N'Accounting', 8)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (42, N'Company', N'Management', N'Company', 153, 41, 1, N'Accounting', 8)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (43, NULL, N'School', N'Academy', 256, NULL, 4, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (44, N'Classes', N'School', N'Class Master', 17, 43, 2, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (45, NULL, N'School', N'Administration', 148, NULL, 5, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (46, N'StudentCategories', N'Student', N'Student Category', 149, 124, 1, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (47, N'Students', N'Student', N'Student Information', 150, 124, 2, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (48, N'StudentRegistrations', N'Student', N'Student Registration', 151, 124, 3, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (49, N'Boaders', N'School', N'Boaders', 153, 45, 5, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (50, N'BoaderMappings', N'School', N'Boader Mapping', 154, 45, 6, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (51, N'Bus', N'Transportation', N'Bus Master', 155, 123, 1, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (52, N'BusStop', N'Transportation', N'Bus Stop ', 156, 123, 2, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (53, N'BusRouteDetails', N'Transportation', N'Bus Routine', 157, 123, 3, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (54, N'BusTransportMapping', N'Transportation', N'Bus transaction Mapping', 158, 123, 4, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (55, N'FeeItems', N'School', N'Fee Item Master', 159, 45, 11, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (56, N'FeeTerms', N'School', N'Fee Term Master', 160, 45, 12, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (57, N'ClassFeeRates', N'School', N'Class Fee Rate', 161, 45, 13, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (58, N'StudentWiseFeeRateSetups', N'School', N'Student Wise Fee Rate Setup', 162, 45, 14, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (61, N'HouseGroups', N'School', N'House Group Master', 165, 45, 17, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (62, N'HouseMappings', N'School', N'House Mapping', 166, 45, 18, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (63, N'StaffSubjectMappings', N'EmployeeManagement', N'Staff Subject Mapping', 248, 125, 6, N'EmployeeManagement_Academy', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (65, N'Buildings', N'School', N'Building Master', 169, 45, 21, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (66, N'Rooms', N'School', N'Room Master', 170, 45, 22, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (67, N'BuildingRoomMappings', N'School', N'Building Room Mapping', 171, 45, 23, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (68, N'ClassRoomMappings', N'School', N'Class Room Mapping', 172, 45, 24, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (70, N'ClassSchedules', N'School', N'Class Schedules', 174, 45, 26, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (71, N'Sections', N'School', N'Section Master', 18, 43, 2, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (72, N'Shifts', N'School', N'Shift Master', 257, 43, 3, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (73, N'Subjects', N'School', N'Subject Master', 258, 43, 4, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (75, N'ClassSubjectMappings', N'School', N'Class Subject Mappings', 136, 43, 6, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (76, N'StudentSubjectMappings', N'School', N'Student Optional Subject Mappings', 137, 43, 7, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (77, N'Exams', N'Exam', N'Exam Master', 138, 122, 1, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (78, N'ExamMarkSetups', N'Exam', N'Exam Mark Setup', 239, 122, 2, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (79, N'ExamRoutines', N'Exam', N'Exam Routines', 140, 122, 3, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (80, N'ExamAttendances', N'Exam', N'Exam Attendance', 141, 122, 4, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (81, N'Grading', N'School', N'Grading', 142, 43, 12, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (82, N'SubjectWiseMarks', N'Exam', N'Subject Wise Marks', 143, 122, 6, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (83, N'StudentWiseMarks', N'Exam', N'Student Wise Marks', 144, 122, 6, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (84, N'Division', N'School', N'Division Master', 145, 43, 15, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (85, N'ExtraActivity', N'School', N'Extra Activity', 146, 43, 16, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (86, N'StudentExtraActivities', N'School', N'Student Extra Activities', 147, 43, 17, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (87, NULL, N'School', N'Transaction', 175, NULL, 6, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (88, N'MonthlyBillGenerationAdd', N'Transaction', N'Monthly Bill Generation', 176, 87, 1, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (89, N'FeeReceipts', N'Transaction', N'Fee Receipt', 177, 87, 2, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (92, NULL, N'Report', N'Academy Report', 184, NULL, 10, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (93, N'StaffList', N'Report', N'Staff List', 180, 92, 2, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (94, N'Masters', N'Report', N'List of Master', 181, 92, 12, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (95, N'StudentLedger', N'Report', N'Student Ledger', 182, 92, 3, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (96, N'StudentCashCollection', N'Report', N'Student Cash Collection', 183, 92, 4, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (97, NULL, N'Library', N'Library', 185, NULL, 7, N'Library', 7)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (98, N'Books', N'Library', N'Books Master', 186, 97, 2, N'Library', 7)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (99, N'BookReceived', N'Library', N'Book Received', 187, 97, 2, N'Library', 7)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (100, N'LibraryMemberRegistrations', N'Library', N'Library Member Registrations', 188, 97, 3, N'Library', 7)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (101, N'BookIssued', N'Library', N'Book Issued', 189, 97, 4, N'Library', 7)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (102, N'BookIssueReturns', N'Library', N'Book Issue Returns', 190, 97, 5, N'Library', 7)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (103, N'SubjectStudentMappings', N'School', N'Optional Subject Student Mappings', 229, 43, 8, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (104, N'AbsentApplications', N'Student', N'Absent Applications', 152, 124, 5, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (105, N'AcademicYear', N'Setup', N'Academic Year', 192, 36, 6, N'Academy', 8)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (106, N'StudentMonthlyBill', N'Report', N'Student Monthly Bill', 193, 92, 5, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (107, N'Students', N'Report', N'Student', 194, 92, 1, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (108, N'Exams', N'Report', N'Exam', 195, 92, 9, N'Academy', 3)
GO
print 'Processed 100 total records'
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (109, N'Fee', N'Report', N'Fee', 196, 92, 6, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (111, N'ConsolidatedMarksSetups', N'Exam', N'Consolidated Marks Setup', 197, 122, 5, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (112, N'ProgramMasters', N'School', N'Program Master', 227, 43, 1, N'College', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (113, N'TemplateIndex', N'School', N'Templates', 199, 45, 27, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (117, N'Result', N'Report', N'Result', 204, 92, 10, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (118, N'StudentAttendances', N'Student', N'Student Attendance', 205, 124, 4, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (119, N'TeacherSchedule', N'EmployeeManagement', N'Teacher Schedule', 249, 125, 28, N'EmployeeManagement_Academy', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (120, N'Printing', N'Report', N'Printing', 207, 92, 11, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (121, N'TeacherSchedule', N'Report', N'Teacher Schedules', 208, 92, 12, N'EmployeeManagement_Academy', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (122, NULL, N'Exam', N'Examination', 209, NULL, 5, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (123, NULL, N'Transportation', N'Transportation', 210, NULL, 5, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (124, NULL, N'Student', N'Student', 211, NULL, 5, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (125, NULL, N'EmployeeManagement', N'Employee Management', 212, NULL, 5, N'EmployeeManagement_Academy', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (126, N'EmployeeCategories', N'EmployeeManagement', N'Employee Category', 167, 125, 1, N'EmployeeManagement_Academy', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (127, N'EmployeeInfos', N'EmployeeManagement', N'Employee Info', 247, 125, 4, N'EmployeeManagement_Academy', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (128, N'StaffAttendances', N'EmployeeManagement', N'Staff Attendance', 213, 125, 5, N'EmployeeManagement_Academy', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (129, N'SalaryHeads', N'Payroll', N'Salary Heads', 246, 125, 6, N'EmployeeManagement_Academy', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (131, NULL, N'Sms', N'SMS Modules', 214, NULL, 5, N'SMS', 6)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (132, N'Groups', N'Sms', N'Groups', 233, 131, 1, N'SMS', 6)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (133, N'Templatess', N'Sms', N'Templates', 236, 131, 2, N'SMS', 6)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (134, N'SendSmsGroup', N'Sms', N'Group SMS', 234, 131, 3, N'SMS', 6)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (135, N'LibrarySetting', N'Library', N'Library Setting', 215, 97, 1, N'Library', 7)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (136, N'SendSms', N'Sms', N'Send SMS', 235, 131, 4, N'SMS', 6)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (137, N'SMSSettings', N'Sms', N'SMS Setting', 237, 131, 5, N'SMS', 6)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (138, N'BookSearch', N'Library', N'Book Search', 216, 97, 6, N'Library', 7)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (139, N'EmployeeDepartments', N'EmployeeManagement', N'Employee Department', 250, 125, 2, N'EmployeeManagement_Academy', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (140, N'EmployeePosts', N'EmployeeManagement', N'Employee Post', 218, 125, 3, N'EmployeeManagement_Academy', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (141, N'Index', N'Payroll', N'Payroll Setting', 219, 125, 8, N'PayRoll', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (142, N'ClassSchedule', N'Report', N'Class Schedule', 262, 92, 13, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (143, N'PayrollGenerations', N'Payroll', N'Payroll Generation', 220, 125, 9, N'PayRoll', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (144, N'LibraryLateFine', N'Library', N'LibraryLateFine', 230, 97, 14, N'Library', 7)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (147, N'PaymentSlips', N'Payroll', N'Payment Slip', 222, 125, 10, N'PayRoll', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (148, N'OpeningTrialBalance', N'ReportLedger', N'Opening Trial Balance Report', 223, 20, 9, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (150, N'ManualBackUp', N'Management', N'Back Up', 225, 41, 2, N'Academy', 8)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (151, N'RollNumberingScheme', N'Management', N'Roll Numbering Scheme', 226, 41, 3, N'Academy', 8)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (152, N'Godown', N'Master', N'Godown', 16, 2, 2, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (153, N'CostCenter', N'Master', N'Cost Center', 21, 2, 2, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (154, N'DebitNote', N'Entry', N'Debit Note', 36, 15, 2, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (155, N'CreditNote', N'Entry', N'Credit Note', 37, 15, 2, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (156, NULL, N'Inventory', N'Inventory', 130, NULL, 8, N'Accounting', 5)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (157, N'StockLedger', N'Inventory', N'Stock Ledger', 272, 156, 1, N'Accounting', 5)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (158, N'PriceList', N'Inventory', N'Price List', 273, 156, 2, N'Accounting', 5)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (159, N'ReOrderStatus', N'Inventory', N'ReOrder Status', 274, 156, 3, N'Accounting', 5)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (160, N'BillOfMaterial', N'Entry', N'Inventory Issue', 134, 15, 4, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (161, N'VatReport', N'ARAP', N'Vat Report', 276, 29, 6, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (162, N'ProductBatchSummary', N'Inventory', N'Product Batch Summary', 271, 156, 0, N'Accounting', 5)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (163, N'CashFlow', N'reportledger', N'Cash Flow', 260, 20, 0, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (164, N'LedgerList', N'ReportLedger', N'Ledger List', 261, 20, 0, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (165, N'ClassTeacherMapping', N'School', N'Class Teacher Mapping', 259, 45, 28, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (166, NULL, NULL, N'Payroll Report', 253, NULL, 10, N'PayRoll', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (167, N'EmployeeStatement', N'PayrollReport', N'Employee Statement', 263, 166, 1, N'PayRoll', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (168, N'SalarySlips', N'PayrollReport', N'Salary Slip', 264, 166, 2, N'PayRoll', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (169, N'PaymentSlips', N'PayrollReport', N'Payment Slip', 265, 166, 3, N'PayRoll', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (170, N'ReRegistration', N'School', N'Upgrade Student', 228, 45, 30, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (171, N'Calendar', N'School', N'Academic Calendar', 254, 45, 31, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (172, N'StudentRegistrationNumbering', N'Setup', N'Student Registration Numbering', 267, 36, 5, N'Academy', 8)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (173, N'Designs', N'Setup', N'Design', 278, 36, 6, N'Academy', 8)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (174, N'Dashboard', N'school', N'Dashboard', 282, 0, 0, N'Academy', 3)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (175, N'Dashboard', N'Entry', N'Dashboard', 288, 0, 0, N'Accounting', 2)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (176, N'Dashboard', N'EmployeeManagement', N'Dashboard', 283, 0, 0, N'EmployeeManagement_Academy', 4)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (177, N'Dashboard', N'inventory', N'Dashboard', 284, 0, 0, N'Accounting', 5)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (178, N'Dashboard', N'sms', N'Dashboard', 285, 0, 0, N'SMS', 6)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (179, N'Dashboard', N'library', N'Dashboard', 286, 0, 0, N'Library', 7)
INSERT [dbo].[MenuItem] ([Id], [Action], [Controller], [LinkText], [ModuleId], [ParentId], [Schedule], [ModuleKey], [CategoryId]) VALUES (180, N'Dashboard', N'setup', N'Dashboard', 287, 0, 0, N'Accounting', 8)
SET IDENTITY_INSERT [dbo].[MenuItem] OFF
/****** Object:  Table [dbo].[AttendanceLog]    Script Date: 11/12/2013 15:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendanceLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EnrollId] [int] NOT NULL,
	[TDate] [date] NULL,
	[InOutMode] [int] NULL,
	[InTime] [time](7) NULL,
	[BreakOut] [time](7) NULL,
	[BreakIn] [time](7) NULL,
	[OutTIme] [time](7) NULL,
	[Verify] [tinyint] NULL,
	[WorkCode] [int] NULL,
	[VerifyOut] [int] NULL,
	[SignInBranch] [int] NULL,
	[SignOutBranch] [int] NULL,
	[OutDate] [date] NULL,
 CONSTRAINT [PK_AttendanceLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AttendanceLog] ON
INSERT [dbo].[AttendanceLog] ([Id], [EnrollId], [TDate], [InOutMode], [InTime], [BreakOut], [BreakIn], [OutTIme], [Verify], [WorkCode], [VerifyOut], [SignInBranch], [SignOutBranch], [OutDate]) VALUES (1, 1, CAST(0x8C370B00 AS Date), NULL, CAST(0x0700CE53A2680000 AS Time), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AttendanceLog] ([Id], [EnrollId], [TDate], [InOutMode], [InTime], [BreakOut], [BreakIn], [OutTIme], [Verify], [WorkCode], [VerifyOut], [SignInBranch], [SignOutBranch], [OutDate]) VALUES (2, 2, CAST(0x8C370B00 AS Date), NULL, CAST(0x07001417C6680000 AS Time), CAST(0x07005ADAE9680000 AS Time), CAST(0x07001882BA7D0000 AS Time), CAST(0x0700A09D0D690000 AS Time), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AttendanceLog] ([Id], [EnrollId], [TDate], [InOutMode], [InTime], [BreakOut], [BreakIn], [OutTIme], [Verify], [WorkCode], [VerifyOut], [SignInBranch], [SignOutBranch], [OutDate]) VALUES (3, 3, CAST(0x8C370B00 AS Date), NULL, CAST(0x07001417C6680000 AS Time), CAST(0x0700B0BD58750000 AS Time), CAST(0x07001882BA7D0000 AS Time), CAST(0x0700A09D0D690000 AS Time), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AttendanceLog] ([Id], [EnrollId], [TDate], [InOutMode], [InTime], [BreakOut], [BreakIn], [OutTIme], [Verify], [WorkCode], [VerifyOut], [SignInBranch], [SignOutBranch], [OutDate]) VALUES (4, 4, CAST(0x8D370B00 AS Date), NULL, CAST(0x0700E66031690000 AS Time), CAST(0x0700B0BD58750000 AS Time), CAST(0x07001882BA7D0000 AS Time), CAST(0x0700FE6DC0690000 AS Time), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AttendanceLog] ([Id], [EnrollId], [TDate], [InOutMode], [InTime], [BreakOut], [BreakIn], [OutTIme], [Verify], [WorkCode], [VerifyOut], [SignInBranch], [SignOutBranch], [OutDate]) VALUES (5, 5, CAST(0x8D370B00 AS Date), NULL, CAST(0x0700E66031690000 AS Time), CAST(0x0700B0BD58750000 AS Time), CAST(0x07001882BA7D0000 AS Time), CAST(0x0700FE6DC0690000 AS Time), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AttendanceLog] ([Id], [EnrollId], [TDate], [InOutMode], [InTime], [BreakOut], [BreakIn], [OutTIme], [Verify], [WorkCode], [VerifyOut], [SignInBranch], [SignOutBranch], [OutDate]) VALUES (6, 6, CAST(0xA4370B00 AS Date), NULL, CAST(0x07004431E4690000 AS Time), CAST(0x0700B0BD58750000 AS Time), CAST(0x07001882BA7D0000 AS Time), CAST(0x07005C3E736A0000 AS Time), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AttendanceLog] ([Id], [EnrollId], [TDate], [InOutMode], [InTime], [BreakOut], [BreakIn], [OutTIme], [Verify], [WorkCode], [VerifyOut], [SignInBranch], [SignOutBranch], [OutDate]) VALUES (7, 7, CAST(0x8D370B00 AS Date), NULL, CAST(0x0700E66031690000 AS Time), CAST(0x0700B0BD58750000 AS Time), CAST(0x07001882BA7D0000 AS Time), CAST(0x0700FE6DC0690000 AS Time), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AttendanceLog] ([Id], [EnrollId], [TDate], [InOutMode], [InTime], [BreakOut], [BreakIn], [OutTIme], [Verify], [WorkCode], [VerifyOut], [SignInBranch], [SignOutBranch], [OutDate]) VALUES (8, 8, CAST(0x8D370B00 AS Date), NULL, CAST(0x0700E66031690000 AS Time), CAST(0x0700B0BD58750000 AS Time), CAST(0x07001882BA7D0000 AS Time), CAST(0x0700FE6DC0690000 AS Time), NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[AttendanceLog] OFF
/****** Object:  Table [dbo].[ModuleList]    Script Date: 11/12/2013 15:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ShortName] [nvarchar](max) NULL,
 CONSTRAINT [PK_ModuleList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ModuleList] ON
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (1, N'Ledger', N'GL')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (2, N'Sub Ledger', N'SL')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (3, N'AccountGroup', N'AG')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (4, N'Account Sub Group', N'ASG')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (5, N'Item/Product', N'IP')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (6, N'Product Group', N'PG')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (7, N'Product Sub Group', N'PSG')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (8, N'Product Rate', N'PRT')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (9, N'Unit', N'UT')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (10, N'User Defined Field', N'UDF')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (11, N'Product Closing Rate', N'PCR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (12, N'Sales Bill Term', N'SBT')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (13, N'Purchase Bill Term', N'PBT')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (14, N'Agent', N'AT')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (15, N'Area', N'AR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (16, N'Godown', N'GD')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (17, N'Class Master', N'ScCls')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (18, N'Section Master', N'ScSec')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (20, N'Currency', N'CUR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (21, N'Cost Center', N'CC')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (22, N'Narration Master', N'NM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (23, N'Opening Balance Ledger', N'OBL')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (24, N'Opening Balance Product', N'OBP')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (25, N'Opening Balance Customer/Vendor', N'OBC')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (26, N'Opening Balance SubLedger Open', N'OBSO')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (28, N'Document Numbering Scheme', N'DNS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (29, N'User Master', N'UM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (30, N'Security Right Grouping', N'SRG')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (31, N'Purchase Additional Invoice', N'PAI')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (32, N'Sales Addititonal Invoice', N'SAI')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (33, N'Sales Abbreviated Tax Invoice', N'SATI')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (34, N'Cash/Bank Voucher', N'CB')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (35, N'Journal Voucher', N'JV')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (36, N'Debit Note', N'DN')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (37, N'Credit Note', N'CN')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (38, N'Purchase Quotation', N'PQ')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (39, N'Purchase Order', N'PO')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (40, N'Purchase Challan', N'PC')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (41, N'Purchase Invoice', N'PB')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (42, N'Purchase Return', N'PR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (43, N'Purchase Expiry/Breakage Return', N'PER')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (44, N'Purchase Order Cancellation', N'POC')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (45, N'Sales Quotation', N'SQ')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (46, N'Sales Order', N'SO')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (47, N'Sales Dispatch Order', N'SDO')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (48, N'Sales Challan', N'SC')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (49, N'Sales Invoice', N'SB')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (50, N'Sales Return', N'SR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (105, N'Master', N'MASTER')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (106, N'Entry', N'Entry')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (107, N'Ledger', N'LEDGER')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (110, N'AR/AP', N'AR/AP')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (111, N'Setup', N'SETUP')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (112, N'Mangement', N'MGMT')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (119, N'AR/AP Report Party Ledger Report', N'AR-PLR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (120, N'AR/AP Report Outstanding Customer Report', N'AR-OCR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (121, N'AR/AP Report Outstanding Vender Report', N'AR-OVR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (122, N'Ledger Report Cash/Bank Book Report', N'LR-CBR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (123, N'Ledger Report Cash/Bank Summary Report', N'LR-CSR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (124, N'Ledger Report Journal Voucher Report', N'LR-JVR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (125, N'Ledger Report Day Book Report', N'LR-DBR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (126, N'Ledger Report Ledger Report', N'LR-LR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (127, N'Ledger Report Trial Balance Report', N'LR-TBR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (128, N'Ledger Report Profit and Loss Report', N'LR-PLR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (129, N'Ledger Report Balance Sheet Report', N'LR-BSR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (130, N'Report Inventory', N'RI')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (131, N'Bill Of Material', N'BOM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (132, N'Material Issue', N'MI')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (133, N'Material Return', N'MR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (134, N'Inventory Issue', N'II')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (135, N'Stock Transfer', N'ST')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (136, N'Academy Class Subject Mapping', N'ScCSM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (137, N'Academy Student Optional Subject Mapping', N'SCSSM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (138, N'Examination', N'scE')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (140, N'Academy Exam Routines', N'ScER')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (141, N'Academy Exam Attendances', N'Sc EA')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (142, N'Academy Grading ', N'ScG')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (143, N'Academy Subject Wise Mark', N'ScSWS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (144, N'Academy Student Wise Mark', N'ScSWSs')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (145, N'Academy Division', N'ScD')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (146, N'Academy Extra Activity', N'scEA')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (147, N'Academy Student ExtraActiveties', N'scSE')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (148, N'Administration', N'Administration')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (149, N' Student Category', N'ScSC')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (150, N'Student Information', N'ScStinfo')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (151, N'Student Registration', N'scStdReg')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (152, N'Absent Application', N'ScAb')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (153, N'Administration Boaders', N'ScBO')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (154, N'Administration Boader Mapping', N'ScBOM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (155, N'Administration Bus', N'ScB')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (156, N'Administration Bus Stop', N'ScBS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (157, N'Administration Bus Routine Details', N'ScBRD')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (158, N'Administration Bus Transportation Mapping', N'ScBTM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (159, N'Administration Fee Item', N'ScFI')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (160, N'Administration Fee Term', N'ScFT')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (161, N'Administration Class Fee Rate', N'ScCFR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (162, N'Administration Student Wise Fee Rate', N'ScSwF')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (163, N'Administration Fine Schemes', N'ScFS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (164, N'Administration PrePaid Schemes', N'ScPS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (165, N'Administration House Group', N'ScHG')
GO
print 'Processed 100 total records'
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (166, N'Administration House Mapping ', N'ScHM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (167, N'Employee Category', N'EMPCa')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (168, N'Administration Staff Master Setup', N'ScSMs')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (169, N'Administration Building', N'ScBld')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (170, N'Administration Room', N'ScRoom')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (171, N'Administration Building Room Mapping', N'ScBM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (172, N'Administration Class Room Mapping', N'ScCRM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (174, N'Administration Class Schedules', N'ScCS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (175, N'Transaction', N'Transaction')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (176, N'Transaction Monthly Bill Generation', N'ScMBG')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (177, N'Transaction Fee Receipt', N'ScFR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (180, N'Report Staff List', N'ScSl')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (181, N'Report List Of Master', N'ScLOM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (182, N'Report Student Ledger', N'ScSld')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (183, N'Report Student Cas Collection ', N'ScSCC')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (184, N'Report', N'Report')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (185, N'Library', N'Library')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (186, N'Library Books', N'ScBos')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (187, N'Library Book Received', N'ScBR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (188, N'Library Member Registration', N'ScLMR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (189, N'Library Book Issued', N'ScBI')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (190, N'Library Book Issue Returns', N'ScBIR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (192, N'Academic Year', N'SCAy')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (193, N'Report StudentMonthlyBill', N'ScRSMB')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (194, N'Report Student', N'ScReStd')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (195, N'Report Exam', N'ScReE')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (196, N'Report Fee', N'SCFee')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (197, N'School ConsolidatedMarksSetup', N'ScCMS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (199, N'Certificate Template', N'CRTMP')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (200, N'Inventory', N'ScInv')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (203, N'Exam Routine Footer', N'ERF')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (204, N'Report Result', N'ScResult')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (205, N'Student Attendances', N'ScSA')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (207, N'Report Printing', N'ScPrint')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (208, N'Report Teacher Schedules', N'RpTSchl')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (209, N'Exam', N'Exam')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (210, N'Transportation', N'Transportation')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (211, N'Student', N'ScStudent')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (212, N'Employee Management', N'ScEmployeeManagement')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (213, N'Staff Attendace', N'ScHRMSA')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (214, N'SMS System', N'SMSSTM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (215, N'Library Setting', N'SCLMS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (216, N'LIbrary Book Search', N'ScLBS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (218, N'Employee Post', N'Employee Post')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (219, N'Payroll Setting', N'ScPy')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (220, N'Payroll Generation', N'ScPygn')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (222, N'Payroll Payment Slip', N'Pyps')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (223, N'Opening Trial Balance Report', N'OTBR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (224, N'Opening Student', N'OS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (225, N'Auto Backup', N'ABup')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (226, N'Roll Numbering Scheme', N'RNS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (227, N'Academy Program Master', N'ScPM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (228, N'Student Reregistration', N'ScStdRereg')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (229, N'Subject Wise Student', N'ScSubStd')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (230, N'Library Late Fine', N'LibLF')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (231, N'StudentSearchMaster', N'STDSM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (233, N'SMS Group', N'SMSGrp')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (234, N'Send SMS Group', N'SMSGS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (235, N'Send SMS', N'SMSS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (236, N'Template SMS', N'SMSTML')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (237, N' SMS Setting', N'SMSStg')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (238, N' Exam Routine', N'ER')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (239, N' Exam Mark Setup', N'EMS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (240, N' CorporateSalaryMaster', N'CSM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (241, N' Deduction Master', N'PyDM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (242, N' Employee Deduction Master', N'PyEDM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (243, N' Employee Salary Master', N'PyESM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (244, N' PF Employee Master', N'PyPFEM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (245, N' Tax Deduction Pattern', N'PyTDP')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (246, N'Salary Head', N'PySH')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (247, N'Employee Information', N'EI')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (248, N'Staff Subject Mapping', N'SSM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (249, N'Teacher Schedule', N'TS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (250, N'Employee Department', N'ED')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (253, N'PayrollReport', N'PyReport')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (254, N'Academic Calendar', N'ACCal')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (255, N'HomeDashBoard', N'HD')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (256, N'Academy', N'Academy')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (257, N'Academy Shift Master', N'ScSh')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (258, N'Academy Subject Master', N'ScSub')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (259, N'Class Teacher Mapping', N'ScCTM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (260, N'Ledger Report Cash flow', N'LRCF')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (261, N'Leder Report Ledger List', N'LRLL')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (262, N'Report Class Schedule', N'RpCSchl')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (263, N'Payroll Report Employee Statement', N'RppyES')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (264, N'Payroll Report Salary Slip', N'RppySS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (265, N'Payroll Report Payment Slip', N'RppyPS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (266, N'Management Company', N'MgmtCom')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (267, N'Student Registration Numbering', N'StdRN')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (268, N'System Control', N'SysCon')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (269, N'Fiscal Year', N'FiscalYear')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (270, N'Ledger Report', N'RPLedger')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (271, N'Inventory Product Batch Summary', N'INVPBS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (272, N'Inventory Stock Ledger', N'INVSL')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (273, N'Inventory Price List', N'INVPL')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (274, N'Inventory ReOrder Status', N'INVROS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (275, N'ARAP Sales', N'ARAPS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (276, N'ARAP Vat Report', N'ARRPVR')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (277, N'ARAP Sales Summarry', N'ARAPSS')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (278, N'Design', N'D')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (279, N'ARAP Purchase Summary', N'ARAPPS')
GO
print 'Processed 200 total records'
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (280, N'BOMRegister', N'INBOM')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (281, N'Godown INentory', N'INGod')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (282, N'School Dashboard', N'SD')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (283, N'Employee Dashboard', N'ED')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (284, N'inventory Dashboard', N'ID')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (285, N'SMS Dashboard', N'SMSD')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (286, N'Library Dashboard', N'LD')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (287, N'Setup Dashboard', N'SetD')
INSERT [dbo].[ModuleList] ([Id], [Name], [ShortName]) VALUES (288, N'Accouting Dashboard', N'AccD')
SET IDENTITY_INSERT [dbo].[ModuleList] OFF
/****** Object:  Table [dbo].[Attendance_Details]    Script Date: 11/12/2013 15:38:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance_Details](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Card_Number] [int] NULL,
	[UserId] [int] NULL,
	[LogInDate] [datetime] NULL,
	[Remarks] [nvarchar](512) NULL,
	[AttendanceStatus] [nvarchar](50) NULL,
 CONSTRAINT [PK_Attendance_Details_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Trigger [AttendanceLog_AspNet_SqlCacheNotification_Trigger]    Script Date: 11/12/2013 15:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create TRIGGER [dbo].[AttendanceLog_AspNet_SqlCacheNotification_Trigger] ON [dbo].[AttendanceLog]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'AttendanceLog'
                       END
GO
/****** Object:  Trigger [Attendance_Details_AspNet_SqlCacheNotification_Trigger]    Script Date: 11/12/2013 15:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create TRIGGER [dbo].[Attendance_Details_AspNet_SqlCacheNotification_Trigger] ON [dbo].[Attendance_Details]
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'Attendance_Details'
                       END
GO
/****** Object:  Table [dbo].[DeviceLog]    Script Date: 11/12/2013 15:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EnrollID] [int] NULL,
	[VerifyMode] [int] NULL,
	[InOutMode] [int] NULL,
	[LogDate] [date] NULL,
	[LogTime] [time](7) NULL,
	[ExtractedFrom] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Trigger [TR_InsertModuleList]    Script Date: 11/12/2013 15:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create trigger [dbo].[TR_InsertModuleList] On [dbo].[ModuleList]
For Insert
as
Begin
Declare @modid int
select @modid = (select Id From Inserted)
exec SP_InsertModuleForRoles @modid
end
GO
/****** Object:  Trigger [TR_DeleteModuleList]    Script Date: 11/12/2013 15:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create trigger [dbo].[TR_DeleteModuleList] On [dbo].[ModuleList]
For Delete
as
Begin
Declare @modid int
select @modid = (select Id From DELETED)
Delete from securityright where ModuleId in(@modid)
end
GO
/****** Object:  Trigger [trgAfterInsertDeviceLog]    Script Date: 11/12/2013 15:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create TRIGGER [dbo].[trgAfterInsertDeviceLog] ON [dbo].[DeviceLog]
FOR INSERT
AS
	declare @Id int, @EnrollID int,	@InOutMode int, @LogDate date, @logTime time(7);
	select @EnrollID=i.EnrollID, @InOutMode=i.InOutMode,@LogDate=i.LogDate , @logTime=i.logTime from inserted i;	
	
	declare @rowCnt as int=0, @InTime as time(7), @BreakIn as time(7), @BreakOut as time(7), @OutTime as time(7);
	
	
	
	select @rowCnt= count(*) from AttendanceLog where  EnrollId=@EnrollID and TDate=@LogDate;
	
	
	if @rowCnt>0
		begin
		select @InTime=convert(time(7),isnull(InTime,'00:00:00')), @BreakIn=convert(time(7),isnull(BreakIn,'00:00:00')), @BreakOut=convert(time(7),isnull(BreakOut,'00:00:00')), @OutTime=convert(time(7),isnull(OutTime,'00:00:00')) from AttendanceLog where  EnrollId=@EnrollID and TDate=@LogDate
			
			if @InOutMode=0 and @InTime=convert(time(7),isnull((null),'00:00:00'))
			begin 
			update AttendanceLog set InTime =@logTime where EnrollId=@EnrollID and Tdate=@LogDate
			end
			else if @InOutMode=2 and @BreakOut=convert(time(7),isnull((null),'00:00:00'))
			begin 
			update AttendanceLog set BreakOut =@logTime where EnrollId=@EnrollID and Tdate=@LogDate
			end
			else if @InOutMode=3 
			begin 
			update AttendanceLog set BreakIn =@logTime where EnrollId=@EnrollID and Tdate=@LogDate 
			end
			else if @InOutMode=1
			begin 
			update AttendanceLog set OutTime =@logTime where EnrollId=@EnrollID and Tdate=@LogDate
			end
		end
	else
		begin 
		
		
			if @InOutMode=0 
			begin 
				insert into AttendanceLog (EnrollId, TDate, InTime) values (@EnrollID,@LogDate, @logTime )
			end
			else if @InOutMode=2
			begin 
			insert into AttendanceLog (EnrollId, TDate, BreakOut) values (@EnrollID,@LogDate, @logTime )
			end
			else if @InOutMode=3 
			begin 
			insert into AttendanceLog (EnrollId, TDate, BreakIn) values (@EnrollID,@LogDate, @logTime )
			end
			else if @InOutMode=1
			begin 
			insert into AttendanceLog (EnrollId, TDate, OutTime) values (@EnrollID,@LogDate, @logTime )
			end
		end
GO
