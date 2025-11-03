/****** Object:  Table [dbo].[EmployeeTaxCalculation]    Script Date: 2/7/2024 2:21:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmployeeTaxCalculation](
	[PersonID] [int] NOT NULL,
	[WageProcessTypeID] [int] NOT NULL,
	[SPTYear] [int] NOT NULL,
	[SPTMonth] [int] NOT NULL,
	[GrossIncome] [numeric](18, 4) NOT NULL,
	[TaxRate] [numeric](10, 2) NOT NULL,
	[TaxAmount] [numeric](18, 4) NOT NULL,
	[Deduction] [numeric](18, 4) NOT NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_EmployeeTaxCalculation] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC,
	[WageProcessTypeID] ASC,
	[SPTYear] ASC,
	[SPTMonth] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


