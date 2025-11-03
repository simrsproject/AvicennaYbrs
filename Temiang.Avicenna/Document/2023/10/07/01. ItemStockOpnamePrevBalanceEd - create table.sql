/****** Object:  Table [dbo].[ItemStockOpnamePrevBalanceEd]    Script Date: 10/7/2023 3:41:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ItemStockOpnamePrevBalanceEd](
	[TransactionNo] [varchar](20) NOT NULL,
	[SequenceNo] [varchar](5) NOT NULL,
	[ItemID] [varchar](10) NOT NULL,
	[ExpiredDate] [datetime] NULL,
	[BatchNumber] [varchar](50) NOT NULL,
	[Quantity] [numeric](10, 2) NOT NULL,
	[SRItemUnit] [varchar](20) NOT NULL,
	[CostPrice] [numeric](18, 2) NOT NULL,
	[QtyAtApprove] [numeric](10, 2) NOT NULL,
 CONSTRAINT [PK_ItemStockOpnamePrevBalanceEd_1] PRIMARY KEY CLUSTERED 
(
	[TransactionNo] ASC,
	[SequenceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ItemStockOpnamePrevBalanceEd] ADD  CONSTRAINT [DF_ItemStockOpnamePrevBalanceEd_TransactionNo]  DEFAULT ('') FOR [TransactionNo]
GO

ALTER TABLE [dbo].[ItemStockOpnamePrevBalanceEd] ADD  CONSTRAINT [DF_ItemStockOpnamePrevBalanceEd_SequenceNo]  DEFAULT ('') FOR [SequenceNo]
GO

ALTER TABLE [dbo].[ItemStockOpnamePrevBalanceEd] ADD  CONSTRAINT [DF_ItemStockOpnamePrevBalanceEd_ItemID]  DEFAULT ('') FOR [ItemID]
GO

ALTER TABLE [dbo].[ItemStockOpnamePrevBalanceEd] ADD  CONSTRAINT [DF_ItemStockOpnamePrevBalanceEd_BatchNumber]  DEFAULT ('') FOR [BatchNumber]
GO

ALTER TABLE [dbo].[ItemStockOpnamePrevBalanceEd] ADD  CONSTRAINT [DF_ItemStockOpnamePrevBalanceEd_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO

ALTER TABLE [dbo].[ItemStockOpnamePrevBalanceEd] ADD  CONSTRAINT [DF_ItemStockOpnamePrevBalanceEd_SRItemUnit]  DEFAULT ('') FOR [SRItemUnit]
GO

ALTER TABLE [dbo].[ItemStockOpnamePrevBalanceEd] ADD  CONSTRAINT [DF_ItemStockOpnamePrevBalanceEd_CostPrice]  DEFAULT ((0)) FOR [CostPrice]
GO

ALTER TABLE [dbo].[ItemStockOpnamePrevBalanceEd] ADD  CONSTRAINT [DF_ItemStockOpnamePrevBalanceEd_QtyAtApprove]  DEFAULT ((0)) FOR [QtyAtApprove]
GO


