SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DiagnoseSynonym] (
    [DiagnoseID] [varchar](20) NOT NULL,
    [SequenceNo] [varchar](4) NOT NULL,
    [SynonymText] [varchar](200) NOT NULL,
    [CreateDateTime] [datetime] NOT NULL,
    [CreateByUserID] [varchar](40) NOT NULL,
    [LastUpdateDateTime] [datetime] NOT NULL,
    [LastUpdateByUserID] [varchar](40) NOT NULL,
    CONSTRAINT [PK_DiagnoseSynonymID] PRIMARY KEY CLUSTERED
    (
        [DiagnoseID] ASC,
		[SequenceNo] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProcedureSynonym] (
    [ProcedureID] [varchar](20) NOT NULL,
    [SequenceNo] [varchar](4) NOT NULL,
    [SynonymText] [varchar](200) NOT NULL,
    [CreateDateTime] [datetime] NOT NULL,
    [CreateByUserID] [varchar](40) NOT NULL,
    [LastUpdateDateTime] [datetime] NOT NULL,
    [LastUpdateByUserID] [varchar](40) NOT NULL,
    CONSTRAINT [PK_ProcedureSynonymID] PRIMARY KEY CLUSTERED
    (
        [ProcedureID] ASC,
		[SequenceNo] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE EpisodeDiagnose ADD DiagnoseSynonym VARCHAR(200) NULL
GO
ALTER TABLE MedicalDischargeSummaryDiagnose ADD DiagnoseSynonym VARCHAR(200) NULL
GO
ALTER TABLE MedicalDischargeSummaryDiagnoseCmx ADD DiagnoseSynonym VARCHAR(200) NULL
GO
ALTER TABLE MedicalDischargeSummaryDiagnoseBak ADD DiagnoseSynonym VARCHAR(200) NULL
GO
ALTER TABLE EpisodeDiagnoseInaGroupper ADD DiagnoseSynonym VARCHAR(200) NULL
GO
ALTER TABLE RegistrationInfoMedicDiagnose ADD DiagnoseSynonym VARCHAR(200) NULL
GO
ALTER TABLE EpisodeProcedure ADD ProcedureSynonym VARCHAR(200) NULL
GO
ALTER TABLE MedicalDischargeSummaryProcedure ADD ProcedureSynonym VARCHAR(200) NULL
GO
ALTER TABLE MedicalDischargeSummaryProcedureCmx ADD ProcedureSynonym VARCHAR(200) NULL
GO
ALTER TABLE MedicalDischargeSummaryProcedureBak ADD ProcedureSynonym VARCHAR(200) NULL
GO
ALTER TABLE EpisodeProcedureInaGroupper ADD ProcedureSynonym VARCHAR(200) NULL
GO