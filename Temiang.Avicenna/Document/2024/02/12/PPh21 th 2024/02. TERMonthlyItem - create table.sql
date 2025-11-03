/****** Object:  Table [dbo].[TERMonthlyItem]    Script Date: 2/5/2024 12:46:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TERMonthlyItem](
	[TERMonthlyItemID] [int] IDENTITY(1,1) NOT NULL,
	[TERMonthlyID] [int] NOT NULL,
	[LowerLimit] [numeric](18, 2) NOT NULL,
	[UpperLimit] [numeric](18, 2) NOT NULL,
	[TaxRate] [numeric](10, 2) NOT NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_TERMonthlyItem] PRIMARY KEY CLUSTERED 
(
	[TERMonthlyItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


