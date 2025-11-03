

/****** Object:  Table [dbo].[ReferExternal]    Script Date: 03/04/2024 22:28:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReferExternalCmx](
	[RegistrationNo] [varchar](20) NOT NULL,
	[ReferralID] [varchar](10) NULL,
	[SRReferReason] [varchar](10) NULL,
	[ReferReasonOther] [varchar](500) NULL,
	[OtherInformation] [varchar](1500) NULL,
	[ReferralAgreedBy] [varchar](100) NULL,
	[ReferralAgreedTime] [datetime] NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
	[ContactOfficer] [varchar](200) NULL,
	[UnitOfficer] [varchar](200) NULL,
	[ContactTime] [datetime] NULL,
	[SRReferralServiceUnit] [varchar](10) NULL,
 CONSTRAINT [PK_ReferExternalCmx] PRIMARY KEY CLUSTERED 
(
	[RegistrationNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


