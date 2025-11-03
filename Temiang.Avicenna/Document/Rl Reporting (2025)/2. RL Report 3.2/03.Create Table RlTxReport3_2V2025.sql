
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RlTxReport3_2V2025](
	[RlTxReportNo] [varchar](20) NOT NULL,
	[RlMasterReportItemID] [int] NOT NULL,
	[PasienAwal] [int] NULL,
	[PasienMasuk] [int] NULL,
	[PasienKeluarHidup] [int] NULL,
	[PasienLKeluarMatiK48] [int] NULL,
	[PasienPKeluarMatiK48] [int] NULL,
	[PasienLKeluarMatiL48] [int] NULL,
	[PasienPKeluarMatiL48] [int] NULL,
	[LamaRawat] [int] NULL,
	[PasienAkhir] [int] NULL,
	[HariRawat] [int] NULL,
	[Vvip] [int] NULL,
	[Vip] [int] NULL,
	[I] [int] NULL,
	[Ii] [int] NULL,
	[Iii] [int] NULL,
	[KelasKhusus] [int] NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
	[PasienPindahan] [int] NULL,
	[PasienDipindahkan] [int] NULL,
	[AlokasiTT] [int] NULL,
 CONSTRAINT [PK_RlTxReport3_2V2025] PRIMARY KEY CLUSTERED 
(
	[RlTxReportNo] ASC,
	[RlMasterReportItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

