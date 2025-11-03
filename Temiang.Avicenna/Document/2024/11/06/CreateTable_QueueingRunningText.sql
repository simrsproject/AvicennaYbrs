
/****** Object:  Table [dbo].[QueueingSound]    Script Date: 04/11/2024 17:13:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[QueueingRunningText](
	[RunningTextID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceUnitID] [varchar](10) NOT NULL,
	[RunningText] [varchar] (1000) NOT NULL,
	[CreateByUserID] [varchar](15) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[LastUpdateByUserID] [varchar](15) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_RunningText] PRIMARY KEY CLUSTERED 
(
	[RunningTextID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


