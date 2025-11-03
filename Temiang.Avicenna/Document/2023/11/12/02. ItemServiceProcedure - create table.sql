/****** Object:  Table [dbo].[ItemServiceProcedure]    Script Date: 10/7/2023 4:52:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ItemServiceProcedure](
	[ItemID] [varchar](10) NOT NULL,
	[SRProcedure] [varchar](20) NOT NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_ItemServiceProcedure] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC,
	[SRProcedure] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


