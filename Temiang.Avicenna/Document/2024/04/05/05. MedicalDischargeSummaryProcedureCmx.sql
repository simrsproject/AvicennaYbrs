
/****** Object:  Table [dbo].[MedicalDischargeSummaryProcedure]    Script Date: 03/04/2024 22:12:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MedicalDischargeSummaryProcedureCmx](
	[RegistrationNo] [varchar](20) NOT NULL,
	[SequenceNo] [varchar](3) NOT NULL,
	[ProcedureID] [varchar](10) NOT NULL,
	[ProcedureName] [varchar](1000) NOT NULL,
	[ParamedicID] [varchar](10) NOT NULL,
	[IsVoid] [bit] NOT NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](40) NULL,
 CONSTRAINT [PK_MedicalDischargeSummaryProcedureCmx] PRIMARY KEY CLUSTERED 
(
	[RegistrationNo] ASC,
	[SequenceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


