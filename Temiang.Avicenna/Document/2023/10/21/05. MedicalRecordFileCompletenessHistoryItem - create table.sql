/****** Object:  Table [dbo].[MedicalRecordFileCompletenessHistoryItem]    Script Date: 10/20/2023 8:36:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MedicalRecordFileCompletenessHistoryItem](
	[TxId] [bigint] NOT NULL,
	[DocumentFilesID] [int] NOT NULL,
	[Notes] [varchar](1000) NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_MedicalRecordFileCompletenessHistoryItem] PRIMARY KEY CLUSTERED 
(
	[TxId] ASC,
	[DocumentFilesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


