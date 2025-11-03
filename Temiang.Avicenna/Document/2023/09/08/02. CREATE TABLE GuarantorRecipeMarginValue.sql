CREATE TABLE [dbo].[GuarantorRecipeMarginValue](
	[GuarantorID] [varchar](10) NOT NULL,
	[CounterID] [int] IDENTITY(1,1) NOT NULL,
	[StartingValue] [numeric](6, 2) NOT NULL,
	[EndingValue] [numeric](6, 2) NOT NULL,
	[RecipeAmount] [numeric](18, 2) NOT NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_GuarantorRecipeMarginValue] PRIMARY KEY CLUSTERED 
(
	[GuarantorID] ASC,
	[CounterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


