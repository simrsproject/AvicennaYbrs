
/****** Object:  Table [dbo].[MedicalDischargeSummaryDiagnose]    Script Date: 03/04/2024 22:08:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MedicalDischargeSummaryDiagnoseCmx](
	[RegistrationNo] [varchar](20) NOT NULL,
	[SequenceNo] [varchar](3) NOT NULL,
	[DiagnoseID] [varchar](20) NOT NULL,
	[SRDiagnoseType] [varchar](20) NOT NULL,
	[DiagnosisText] [varchar](4000) NOT NULL,
	[ExternalCauseID] [varchar](20) NULL,
	[IsOldCase] [bit] NOT NULL,
	[IsVoid] [bit] NOT NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](40) NULL,
	[CreatedByUserID] [varchar](15) NULL,
	[CreatedDateTime] [datetime] NULL,
	[ParamedicID] [varchar](10) NULL,
 CONSTRAINT [PK_MedicalDischargeSummaryDiagnoseCmx] PRIMARY KEY CLUSTERED 
(
	[RegistrationNo] ASC,
	[SequenceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MedicalDischargeSummaryDiagnoseCmx] ADD  CONSTRAINT [DF_MedicalDischargeSummaryDiagnoseCmx_ParamedicID]  DEFAULT ('') FOR [ParamedicID]
GO


