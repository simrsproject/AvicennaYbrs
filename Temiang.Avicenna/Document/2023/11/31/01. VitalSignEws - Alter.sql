DROP TABLE VitalSignEws
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VitalSignEws](
	[SREwsType] [varchar](5) NOT NULL,
	[VitalSignID] [varchar](20) NOT NULL,
	[StartAgeInDay] [int] NOT NULL,
	[EndAgeInDay] [int] NOT NULL,
	[IndexNo] [int] NOT NULL,
	[ChartMinValue] [numeric](4, 1) NOT NULL,
	[ChartMaxValue] [numeric](4, 1) NOT NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](20) NULL,
	[IsExcludeFromScoreEws] [bit] NULL,
	[ChartYAxisStep] [numeric](4, 1) NULL,
 CONSTRAINT [PK_VitalSignEws] PRIMARY KEY CLUSTERED 
(
	[SREwsType] ASC,
	[VitalSignID] ASC,
	[StartAgeInDay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[VitalSignEws] ADD  CONSTRAINT [DF_VitalSignEws_SREwsType]  DEFAULT ('EWS') FOR [SREwsType]
GO

ALTER TABLE [dbo].[VitalSignEws] ADD  CONSTRAINT [DF_VitalSignEws_EndAgeInDay]  DEFAULT ((100000)) FOR [EndAgeInDay]
GO


