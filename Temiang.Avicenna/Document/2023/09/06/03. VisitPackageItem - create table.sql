/****** Object:  Table [dbo].[VisitPackageItem]    Script Date: 9/6/2023 4:04:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VisitPackageItem](
	[VisitPackageID] [varchar](10) NOT NULL,
	[ItemID] [varchar](10) NOT NULL,
	[Qty] [numeric](10, 2) NOT NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_VisitPackageItem] PRIMARY KEY CLUSTERED 
(
	[VisitPackageID] ASC,
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


