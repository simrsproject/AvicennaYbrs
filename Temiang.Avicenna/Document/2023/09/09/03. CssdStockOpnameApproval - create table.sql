/****** Object:  Table [dbo].[CssdStockOpnameApproval]    Script Date: 9/9/2023 11:49:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CssdStockOpnameApproval](
	[TransactionNo] [varchar](20) NOT NULL,
	[PageNo] [int] NOT NULL,
	[IsApproved] [bit] NULL,
	[ApprovedDateTime] [datetime] NULL,
	[ApprovedByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_CssdStockOpnameApproval] PRIMARY KEY CLUSTERED 
(
	[TransactionNo] ASC,
	[PageNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


