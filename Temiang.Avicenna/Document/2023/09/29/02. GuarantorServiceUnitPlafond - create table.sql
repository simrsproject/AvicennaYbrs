/****** Object:  Table [dbo].[GuarantorServiceUnitPlafond]    Script Date: 9/29/2023 3:50:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GuarantorServiceUnitPlafond](
	[GuarantorID] [varchar](10) NOT NULL,
	[ServiceUnitID] [varchar](10) NOT NULL,
	[PlafondAmount] [numeric](18, 2) NOT NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_GuarantorServiceUnitPlafond] PRIMARY KEY CLUSTERED 
(
	[GuarantorID] ASC,
	[ServiceUnitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


