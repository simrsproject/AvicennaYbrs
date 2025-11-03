/****** Object:  Table [dbo].[RlTxReport5_4]    Script Date: 2/6/2025 2:15:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RlTxReport5_2V2025](
	[RlTxReportNo] [varchar](20) NOT NULL,
	[DiagnosaID] [varchar](20) NOT NULL,
	[KasusBaruL] [int] NULL,
	[KasusBaruP] [int] NULL,
	[JumlahKasusBaru] [int] NULL,
	[KunjunganL] [int] NULL,
	[KunjunganP] [int] NULL,
	[JumlahKunjungan] [int] NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_RlTxReport5_2V2025] PRIMARY KEY CLUSTERED 
(
	[RlTxReportNo] ASC,
	[DiagnosaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


