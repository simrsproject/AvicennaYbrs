/****** Object:  Table [dbo].[RlTxReport3_6V2025]    Script Date: 22/01/2025 16:02:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RlTxReport3_6V2025](
	[RlTxReportNo] [varchar](20) NOT NULL,
	[RlMasterReportItemID] [int] NOT NULL,
	[RmRumahSakit] [int] NULL,
	[RmBidan] [int] NULL,
	[RmPuskesmas] [int] NULL,
	[RmFasKesLain] [int] NULL,
	[RmHidup] [int] NULL,
	[RmMati] [int] NULL,
	[RmTotal] [int] NULL,
	[RnmHidup] [int] NULL,
	[RnmMati] [int] NULL,
	[RnmTotal] [int] NULL,
	[NrHidup] [int] NULL,
	[NrMati] [int] NULL,
	[NrTotal] [int] NULL,
	[DiRujuk] [int] NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_RlTxReport3_6V2025] PRIMARY KEY CLUSTERED 
(
	[RlTxReportNo] ASC,
	[RlMasterReportItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

