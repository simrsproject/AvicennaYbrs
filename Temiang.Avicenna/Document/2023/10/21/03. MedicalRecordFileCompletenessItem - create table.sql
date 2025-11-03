/****** Object:  Table [dbo].[MedicalRecordFileCompletenessItem]    Script Date: 10/20/2023 8:35:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MedicalRecordFileCompletenessItem](
	[RegistrationNo] [varchar](20) NOT NULL,
	[DocumentFilesID] [int] NOT NULL,
	[IsComplete] [bit] NOT NULL,
	[IsNotApplicable] [bit] NOT NULL,
	[Notes] [varchar](1000) NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_MedicalRecordFileCompletenessItem] PRIMARY KEY CLUSTERED 
(
	[RegistrationNo] ASC,
	[DocumentFilesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


