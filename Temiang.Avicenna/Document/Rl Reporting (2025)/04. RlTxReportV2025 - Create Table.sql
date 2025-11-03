SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RlTxReportV2025](
	[RlTxReportNo] [varchar](20) NOT NULL,
	[RlMasterReportID] [int] NOT NULL,
	[PeriodYear] [varchar](4) NOT NULL,
	[PeriodMonthStart] [varchar](2) NOT NULL,
	[PeriodMonthEnd] [varchar](2) NOT NULL,
	[IsApproved] [bit] NULL,
	[ApprovedDateTime] [datetime] NULL,
	[ApprovedByUserID] [varchar](15) NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_RlTxReportV2025] PRIMARY KEY CLUSTERED 
(
	[RlTxReportNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO