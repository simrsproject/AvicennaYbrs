
/****** Object:  Table [dbo].[GuarantorAutoBillItem]    Script Date: 10/19/2023 2:01:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GuarantorAutoBillItem](
	[GuarantorID] [varchar](10) NOT NULL,
	[SRRegistrationType] [varchar](20) NOT NULL,
	[ItemID] [varchar](10) NOT NULL,
	[Quantity] [numeric](10, 2) NOT NULL,
	[IsGenerateOnRegistration] [bit] NOT NULL,
	[IsGenerateOnNewRegistration] [bit] NOT NULL,
	[IsGenerateOnReferral] [bit] NOT NULL,
	[IsGenerateOnFirstRegistration] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_GuarantorAutoBillItem] PRIMARY KEY CLUSTERED 
(
	[GuarantorID] ASC,
	[SRRegistrationType] ASC,
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


