
/****** Object:  Table [dbo].[RlTxReport4_3V2025]    Script Date: 26/01/2025 13:39:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RlTxReport4_3V2025](
	[RlTxReportNo] [varchar](20) NOT NULL,
	[DiagnosaID] [varchar](20) NOT NULL,
	[HidupMatiL] [int] NULL,
	[HidupMatiP] [int] NULL,
	[TotalHidupMati] [int] NULL,
	[KeluarMatiL] [int] NULL,
	[KeluarMatiP] [int] NULL,
	[TotalKeluarMati] [int] NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_RlTxReport4_3V2025] PRIMARY KEY CLUSTERED 
(
	[RlTxReportNo] ASC,
	[DiagnosaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


