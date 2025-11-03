SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RlMasterReportItemV2025](
	[RlMasterReportItemID] [int] IDENTITY(1,1) NOT NULL,
	[RlMasterReportID] [int] NOT NULL,
	[RlMasterReportItemNo] [varchar](10) NOT NULL,
	[RlMasterReportItemCode] [varchar](50) NOT NULL,
	[RlMasterReportItemName] [varchar](300) NOT NULL,
	[SRParamedicRL1] [varchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ParameterValue] [varchar](4000) NOT NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[LastUpdateByUserID] [varchar](40) NOT NULL,
 CONSTRAINT [PK_RlMaster_D549D19D6F21FA9DV2025] PRIMARY KEY CLUSTERED 
(
	[RlMasterReportItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO