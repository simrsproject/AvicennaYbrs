/****** Object:  Table [dbo].[ItemProductFabric]    Script Date: 9/25/2023 10:49:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ItemProductFabric](
	[ItemID] [varchar](10) NOT NULL,
	[FabricID] [varchar](10) NOT NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_ItemProductFabric] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC,
	[FabricID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


