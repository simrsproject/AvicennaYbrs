CREATE TABLE [dbo].[MedicalStorage](
	[TransactionNo] [varchar](20) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[ServiceUnitID] [varchar](10) NULL,
	[IsApproved] [bit] NULL,
	[ApprovedDateTime] [datetime] NULL,
	[ApprovedByUserID] [varchar](15) NULL,
	[IsVoid] [bit] NULL,
	[VoidDateTime] [datetime] NULL,
	[VoidByUserID] [varchar](15) NULL,
	[CreatedDateTime] [datetime] NULL,
	[CreatedByUserID] [varchar](15) NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_MedicalStorage] PRIMARY KEY CLUSTERED 
(
	[TransactionNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[MedicalStorageItem](
	[TransactionNo] [varchar](20) NOT NULL,
	[SRMedicalStorage] [varchar](20) NOT NULL,
	[IsYes] [bit] NOT NULL,
	[IsNotApplicable] [bit] NOT NULL,
	[Notes] [varchar](255) NULL,
	[Recommendation] [varchar] (255) NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_MedicalStorageItem] PRIMARY KEY CLUSTERED 
(
	[TransactionNo] ASC,
	[SRMedicalStorage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


