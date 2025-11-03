

/****** Object:  Table [dbo].[AppStandardReferenceItemBridging]    Script Date: 29/05/2024 23:20:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AppStandardReferenceItemBridging](
	[StandardReferenceID] [varchar](30) NOT NULL,
	[ItemID] [varchar](50) NOT NULL,
	[SRBridgingType] [varchar](20) NOT NULL,
	[BridgingID] [varchar](255) NOT NULL,
	[BridgingName] [varchar](255) NOT NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](40) NULL,
 CONSTRAINT [PK_AppStandardReferenceItemBridging] PRIMARY KEY CLUSTERED 
(
	[StandardReferenceID] ASC,
	[ItemID] ASC,
	[SRBridgingType] ASC,
	[BridgingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



