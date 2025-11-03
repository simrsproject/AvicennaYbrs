/****** Object:  Table [dbo].[MedicalRecordFileCompletenessHistory]    Script Date: 10/20/2023 8:36:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MedicalRecordFileCompletenessHistory](
	[TxId] [bigint] IDENTITY(1,1) NOT NULL,
	[RegistrationNo] [varchar](20) NULL,
	[SubmitDate] [datetime] NULL,
	[SubmitNotes] [varchar](1000) NULL,
	[ReturnDate] [datetime] NULL,
	[ReturnNotes] [varchar](1000) NULL,
	[SubmitDateTime] [datetime] NULL,
	[SubmitByUserID] [varchar](15) NULL,
	[ReturnDateTime] [datetime] NULL,
	[ReturnByUserID] [varchar](15) NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_MedicalRecordFileCompletenessHistory] PRIMARY KEY CLUSTERED 
(
	[TxId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


