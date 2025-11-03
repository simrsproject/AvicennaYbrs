SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RlTxReport3_1V2025](
	[RlTxReportNo] [varchar](20) NOT NULL,
	[BorNonIntensif] [numeric](18, 2) NULL,
	[BorICU] [numeric](18, 2) NULL,
	[BorNICU] [numeric](18, 2) NULL,
	[BorPICU] [numeric](18, 2) NULL,
	[BorIntensifLainnya] [numeric](18, 2) NULL,
	[LosNonIntensif] [numeric](18, 2) NULL,
	[LosICU] [numeric](18, 2) NULL,
	[LosNICU] [numeric](18, 2) NULL,
	[LosPICU] [numeric](18, 2) NULL,
	[LosIntensifLainnya] [numeric](18, 2) NULL,
	[BtoNonIntensif] [numeric](18, 2) NULL,
	[BtoICU] [numeric](18, 2) NULL,
	[BtoNICU] [numeric](18, 2) NULL,
	[BtoPICU] [numeric](18, 2) NULL,
	[BtoIntensifLainnya] [numeric](18, 2) NULL,
	[ToiNonIntensif] [numeric](18, 2) NULL,
	[ToiICU] [numeric](18, 2) NULL,
	[ToiNICU] [numeric](18, 2) NULL,
	[ToiPICU] [numeric](18, 2) NULL,
	[ToiIntensifLainnya] [numeric](18, 2) NULL,
	[NdrNonIntensif] [numeric](18, 2) NULL,
	[NdrICU] [numeric](18, 2) NULL,
	[NdrNICU] [numeric](18, 2) NULL,
	[NdrPICU] [numeric](18, 2) NULL,
	[NdrIntensifLainnya] [numeric](18, 2) NULL,
	[GdrNonIntensif] [numeric](18, 2) NULL,
	[GdrICU] [numeric](18, 2) NULL,
	[GdrNICU] [numeric](18, 2) NULL,
	[GdrPICU] [numeric](18, 2) NULL,
	[GdrIntensifLainnya] [numeric](18, 2) NULL,
	[RataKunjungan] [numeric](18, 2) NULL,
	[RataRata] [numeric](18, 2) NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_RlTxReport3_1_V2025] PRIMARY KEY CLUSTERED 
(
	[RlTxReportNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


