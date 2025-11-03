

/****** Object:  Table [dbo].[PatientDialysis]    Script Date: 26/01/2024 09:42:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PatientDialysis](
	[PatientID] [varchar](15) NOT NULL,
	[SequenceNo] [varchar](12) NOT NULL,
	[InitialDiagnosis] [varchar](max) NULL,
	[PhysicianSenders] [varchar](4000) NULL,
	[RSSender] [varchar](4000) NULL,
	[Hb] [varchar](4000) NULL,
	[hbDate] [datetime] NULL,
	[Urea] [varchar](4000) NULL,
	[UreaDate] [datetime] NULL,
	[Creatinine] [varchar](4000) NULL,
	[CreatinineDate] [datetime] NULL,
	[HBsAg] [varchar](4000) NULL,
	[HBsAgDate] [datetime] NULL,
	[AntiHCV] [varchar](4000) NULL,
	[AntiHCVDate] [datetime] NULL,
	[AntiHIV] [varchar](4000) NULL,
	[AntiHIVDate] [datetime] NULL,
	[KidneyUltrasound] [varchar](4000) NULL,
	[KidneyUltrasoundDate] [datetime] NULL,
	[ECHO] [varchar](4000) NULL,
	[ECHODate] [datetime] NULL,
	[HDDate] [varchar](4000) NULL,
	[TransferHDDate1] [varchar](4000) NULL,
	[TransferHDDate2] [varchar](4000) NULL,
	[TransferHDDate3] [varchar](4000) NULL,
	[PDDate] [varchar](4000) NULL,
	[TransferPDDate] [varchar](4000) NULL,
	[KidneytransplantDate] [varchar](4000) NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_PatientDialysis] PRIMARY KEY CLUSTERED 
(
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


