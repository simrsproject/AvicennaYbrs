
/****** Object:  Table [dbo].[MedicalDischargeSummary]    Script Date: 03/04/2024 16:31:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MedicalDischargeSummaryCmx](
	[RegistrationNo] [varchar](20) NOT NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](40) NULL,
	[ChiefComplaint] [varchar](4000) NULL,
	[HistOfPresentIllness] [varchar](4000) NULL,
	[Komorbiditas] [varchar](4000) NULL,
	[PhysicalExam] [varchar](4000) NULL,
	[AncillaryExam] [varchar](max) NULL,
	[MedicalProcedures] [varchar](4000) NULL,
	[ProcedureID] [varchar](10) NULL,
	[Medications] [varchar](max) NULL,
	[AdmittingDiagnoseID1] [varchar](10) NULL,
	[AdmittingDiagnoseName1] [varchar](250) NULL,
	[AdmittingDiagnoseID2] [varchar](10) NULL,
	[AdmittingDiagnoseName2] [varchar](250) NULL,
	[FinalDiagnoseID1] [varchar](10) NULL,
	[FinalDiagnoseName1] [varchar](200) NULL,
	[FinalDiagnoseID2] [varchar](10) NULL,
	[FinalDiagnoseName2] [varchar](200) NULL,
	[FinalDiagnoseID3] [varchar](10) NULL,
	[FinalDiagnoseName3] [varchar](200) NULL,
	[PresentStatus] [varchar](4000) NULL,
	[SuggestionFollowUp] [varchar](4000) NULL,
	[TreatmentIndications] [varchar](4000) NULL,
	[PastMedicalHistory] [varchar](4000) NULL,
	[ProcedureName] [varchar](400) NULL,
	[DischargeDate] [datetime] NULL,
	[DischargeTime] [varchar](5) NULL,
	[ParamedicID] [varchar](10) NULL,
	[ParamedicName] [varchar](200) NULL,
	[SRUnitIntended] [varchar](20) NULL,
	[SRDischargeMethod] [varchar](20) NULL,
	[SRDischargeCondition] [varchar](20) NULL,
	[Prognosis] [varchar](4000) NULL,
	[IsRichTextMode] [bit] NULL,
	[AncillaryExamOther] [varchar](max) NULL,
	[Diet] [varchar](4000) NULL,
	[DocumentDate] [datetime] NULL,
	[PpaSign] [image] NULL,
	[PatientSign] [image] NULL,
 CONSTRAINT [PK_MedicalDischargeSummaryCmx] PRIMARY KEY CLUSTERED 
(
	[RegistrationNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.MedicalDischargeSummaryCmx ADD
	HomeCare varchar(500) NULL,
	EducationAtHome varchar(500) NULL,
	Consul varchar(500) NULL,
	MedicalSupport varchar(500) NULL,
	InLocation varchar(500) NULL,
	CollectionDateTime datetime NULL,
	InitialDiagnose varchar(4000) NULL,
	SRTypeOfService varchar(20) NULL,
	SRCauseOfDisease varchar(20) NULL,
	SRCauseOfDevelopDisorder varchar(50) NULL,
	CauseOfDevelopDisorder varchar(1000) NULL,
	SRNatureOfSurgery varchar(20) NULL,
	IsInstruction1 bit NULL,
	IsInstruction2 bit NULL,
	IsInstruction3 bit NULL,
	Instruction3 varchar(500) NULL,
	IsInstruction4 bit NULL,
	Instruction4 varchar(500) NULL,
	IsInstruction5 bit NULL,
	Instruction5 varchar(500) NULL,
	IsInstruction6 bit NULL,
	Instruction6 varchar(500) NULL,
	ControlPlan VARCHAR(MAX) NULL,
	IsApproved BIT NULL
GO
ALTER TABLE dbo.MedicalDischargeSummaryCmx SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
