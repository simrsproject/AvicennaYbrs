/****** Object:  Table [dbo].[VisitPackage]    Script Date: 9/6/2023 4:04:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VisitPackage](
	[VisitPackageID] [varchar](10) NOT NULL,
	[VisitPackageName] [varchar](200) NOT NULL,
	[ServiceUnitID] [varchar](10) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_VisitPackage] PRIMARY KEY CLUSTERED 
(
	[VisitPackageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


