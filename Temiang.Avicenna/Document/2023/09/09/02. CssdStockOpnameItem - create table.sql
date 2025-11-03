
/****** Object:  Table [dbo].[CssdStockOpnameItem]    Script Date: 9/9/2023 11:48:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CssdStockOpnameItem](
	[TransactionNo] [varchar](20) NOT NULL,
	[SequenceNo] [varchar](5) NOT NULL,
	[PageNo] [int] NOT NULL,
	[ItemID] [varchar](10) NOT NULL,
	[Balance] [numeric](10, 2) NULL,
	[BalanceReceived] [numeric](10, 2) NULL,
	[BalanceDeconImmersion] [numeric](10, 2) NULL,
	[BalanceDeconAbstersion] [numeric](10, 2) NULL,
	[BalanceDeconDrying] [numeric](10, 2) NULL,
	[BalanceFeasibilityTest] [numeric](10, 2) NULL,
	[BalancePackaging] [numeric](10, 2) NULL,
	[BalanceUltrasound] [numeric](10, 2) NULL,
	[BalanceSterilization] [numeric](10, 2) NULL,
	[BalanceDistribution] [numeric](10, 2) NULL,
	[BalanceReturned] [numeric](10, 2) NULL,
	[PrevBalance] [numeric](10, 2) NULL,
	[PrevBalanceReceived] [numeric](10, 2) NULL,
	[PrevBalanceDeconImmersion] [numeric](10, 2) NULL,
	[PrevBalanceDeconAbstersion] [numeric](10, 2) NULL,
	[PrevBalanceDeconDrying] [numeric](10, 2) NULL,
	[PrevBalanceFeasibilityTest] [numeric](10, 2) NULL,
	[PrevBalancePackaging] [numeric](10, 2) NULL,
	[PrevBalanceUltrasound] [numeric](10, 2) NULL,
	[PrevBalanceSterilization] [numeric](10, 2) NULL,
	[PrevBalanceDistribution] [numeric](10, 2) NULL,
	[PrevBalanceReturned] [numeric](10, 2) NULL,
	[Notes] [varchar](500) NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_CssdStockOpnameItem] PRIMARY KEY CLUSTERED 
(
	[TransactionNo] ASC,
	[SequenceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


