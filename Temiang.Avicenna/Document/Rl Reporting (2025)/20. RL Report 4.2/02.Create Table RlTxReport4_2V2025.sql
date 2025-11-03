/****** Object:  Table [dbo].[RlTxReport4_2V2025]    Script Date: 20/01/2025 11:04:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RlTxReport4_2V2025](
	[RlTxReportNo] [varchar](20) NOT NULL,
	[DiagnosaID] [varchar](20) NOT NULL,
	[KeluarHidupL] [int] NULL,
	[KeluarHidupP] [int] NULL,
	[KeluarMatiL] [int] NULL,
	[KeluarMatiP] [int] NULL,
	[Total] [int] NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_RlTxReport4_2V2025] PRIMARY KEY CLUSTERED 
(
	[RlTxReportNo] ASC,
	[DiagnosaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

