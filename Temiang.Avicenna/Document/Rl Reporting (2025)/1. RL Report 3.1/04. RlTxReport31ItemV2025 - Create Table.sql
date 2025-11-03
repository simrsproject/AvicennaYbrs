SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RlTxReport31ItemV2025](
	[PeriodMonth] [varchar](2) NOT NULL,
	[PeriodYear] [varchar](4) NOT NULL,
	[HariPerawatanNonIntensif] [int] NULL,
	[HariPerawatanICU] [int] NULL,
	[HariPerawatanNICU] [int] NULL,
	[HariPerawatanPICU] [int] NULL,
	[HariPerawatanIntensifLainnya] [int] NULL,
	[LamaDirawatNonIntensif] [int] NULL,
	[LamaDirawatICU] [int] NULL,
	[LamaDirawatNICU] [int] NULL,
	[LamaDirawatPICU] [int] NULL,
	[LamaDirawatIntensifLainnya] [int] NULL,
	[KeluarNonIntensif] [int] NULL,
	[KeluarICU] [int] NULL,
	[KeluarNICU] [int] NULL,
	[KeluarPICU] [int] NULL,
	[KeluarIntensifLainnya] [int] NULL,
	[KeluarMati48NonIntensif] [int] NULL,
	[KeluarMati48ICU] [int] NULL,
	[KeluarMati48NICU] [int] NULL,
	[KeluarMati48PICU] [int] NULL,
	[KeluarMati48IntensifLainnya] [int] NULL,
	[KeluarMatiNonIntensif] [int] NULL,
	[KeluarMatiICU] [int] NULL,
	[KeluarMatiNICU] [int] NULL,
	[KeluarMatiPICU] [int] NULL,
	[KeluarMatiIntensifLainnya] [int] NULL,
	[TtNonIntensif] [int] NULL,
	[TtICU] [int] NULL,
	[TtNICU] [int] NULL,
	[TtPICU] [int] NULL,
	[TtIntensifLainnya] [int] NULL,
	[HariDlmSatuPeriode] [int] NULL,
	[Kunjungan] [int] NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
	[JTtNonIntensif] [int] NULL,
	[JTtICU] [int] NULL,
	[JTtNICU] [int] NULL,
	[JTtPICU] [int] NULL,
	[JTtIntensifLainnya] [int] NULL,
 CONSTRAINT [PK_RlTxReport31ItemV2025] PRIMARY KEY CLUSTERED 
(
	[PeriodMonth] ASC,
	[PeriodYear] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


