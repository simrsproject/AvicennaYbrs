DROP TABLE PatientDialysis
GO

/****** Object:  Table [dbo].[PatientDialysis]    Script Date: 2/28/2024 5:23:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PatientDialysis](
	[PatientID] [varchar](15) NOT NULL,
	[InitialDiagnosis] [varchar](4000) NULL,
	[RefferingHospital] [varchar](4000) NULL,
	[RefferingPhysician] [varchar](4000) NULL,
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
	[FirstHDDate] [datetime] NULL,
	[TransferHDDate] [datetime] NULL,
	[FirstPDDate] [datetime] NULL,
	[TransferPDDate] [datetime] NULL,
	[KidneyTransplantDate] [datetime] NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
	[CreatedDateTime] [datetime] NULL,
	[CreatedByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_PatientDialysis] PRIMARY KEY CLUSTERED 
(
	[PatientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


