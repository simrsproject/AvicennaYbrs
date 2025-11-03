DROP TABLE VitalSignEwsLevel
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VitalSignEwsLevel](
	[SREwsType] [varchar](5) NOT NULL,
	[VitalSignID] [varchar](20) NOT NULL,
	[StartAgeInDay] [int] NOT NULL,
	[StartValue] [numeric](18, 4) NOT NULL,
	[EwsLevel] [int] NOT NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](20) NULL,
 CONSTRAINT [PK_VitalSignEwsLevel] PRIMARY KEY CLUSTERED 
(
	[SREwsType] ASC,
	[VitalSignID] ASC,
	[StartAgeInDay] ASC,
	[StartValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[VitalSignEwsLevel] ADD  CONSTRAINT [DF_VitalSignEwsLevel_SREwsType]  DEFAULT ('EWS') FOR [SREwsType]
GO


