
/****** Object:  Table [dbo].[MedicalDischargeSummaryBodyDiagram]    Script Date: 03/04/2024 22:05:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MedicalDischargeSummaryBodyDiagramCmx](
	[RegistrationNo] [varchar](20) NOT NULL,
	[BodyID] [varchar](10) NOT NULL,
	[BodyImage] [image] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDateTime] [datetime] NULL,
	[CreatedByUserID] [varchar](20) NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](20) NULL,
	[Notes] [varchar](4000) NULL,
 CONSTRAINT [PK_MedicalDischargeSummaryBodyDiagramCmx] PRIMARY KEY CLUSTERED 
(
	[RegistrationNo] ASC,
	[BodyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


