/****** Object:  Table [dbo].[CssdStockOpname]    Script Date: 9/9/2023 11:47:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CssdStockOpname](
	[TransactionNo] [varchar](20) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[ServiceUnitID] [varchar](10) NOT NULL,
	[Notes] [varchar](1000) NOT NULL,
	[IsVoid] [bit] NULL,
	[VoidDateTime] [datetime] NULL,
	[VoidByUserID] [varchar](15) NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_CssdStockOpname] PRIMARY KEY CLUSTERED 
(
	[TransactionNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


