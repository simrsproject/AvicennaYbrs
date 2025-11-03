
/****** Object:  Table [dbo].[RlTxReport3_9V2025]    Script Date: 16/01/2025 10:32:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RlTxReport3_9V2025](
	[RlTxReportNo] [varchar](20) NOT NULL,
	[RlMasterReportItemID] [int] NOT NULL,
	[Jumlah] [int] NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_RlTxReport3_9V2025] PRIMARY KEY CLUSTERED 
(
	[RlTxReportNo] ASC,
	[RlMasterReportItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


