
/****** Object:  Table [dbo].[RlTxReport3_10V2025]    Script Date: 30/01/2025 13:24:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RlTxReport3_10V2025](
	[RlTxReportNo] [varchar](20) NOT NULL,
	[RlMasterReportItemID] [int] NOT NULL,
	[RujukanPuskesmas] [int] NULL,
	[RujukanRsLain] [int] NULL,
	[RujukanFasKesLain] [int] NULL,
	[DirujukKePuskesmasAsal] [int] NULL,
	[DirujukKeRsAsal] [int] NULL,
	[DirujukKeFasKesAsal] [int] NULL,
	[DirujukPasienRujukan] [int] NULL,
	[DirujukPasienDtgSendiri] [int] NULL,
	[DirujukDiterimaKembali] [int] NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_RlTxReport3_10V2025] PRIMARY KEY CLUSTERED 
(
	[RlTxReportNo] ASC,
	[RlMasterReportItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


