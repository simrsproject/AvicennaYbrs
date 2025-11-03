
/****** Object:  Table [dbo].[RlTxReport3_3V2025]    Script Date: 21/01/2025 12:05:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RlTxReport3_3V2025](
	[RlTxReportNo] [varchar](20) NOT NULL,
	[RlMasterReportItemID] [int] NOT NULL,
	[PasienRujukan] [int] NULL,
	[PasienNonRujukan] [int] NULL,
	[DiRawat] [int] NULL,
	[DiRujuk] [int] NULL,
	[Pulang] [int] NULL,
	[MatiDiUgdLaki] [int] NULL,
	[DoaLaki] [int] NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
	[MatiDiUgdPerempuan] [int] NULL,
	[DoaPerempuan] [int] NULL,
	[LukaLaki] [int] NULL,
	[LukaPerempuan] [int] NULL,
	[FalseEmergency] [int] NULL,
 CONSTRAINT [PK_RlTxReport3_3V2025] PRIMARY KEY CLUSTERED 
(
	[RlTxReportNo] ASC,
	[RlMasterReportItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


