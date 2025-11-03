
/****** Object:  Table [dbo].[RlTxReport4_1V2025]    Script Date: 04/02/2025 09:43:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RlTxReport4_1V2025](
	[RlTxReportNo] [varchar](20) NOT NULL,
	[RlMasterReportItemID] [int] NOT NULL,
	[L1j] [int] NULL,
	[P1j] [int] NULL,
	[L123j] [int] NULL,
	[P123j] [int] NULL,
	[L0107h] [int] NULL,
	[P0107h] [int] NULL,
	[L0828h] [int] NULL,
	[P0828h] [int] NULL,
	[L29h03b] [int] NULL,
	[P29h03b] [int] NULL,
	[L0306b] [int] NULL,
	[P0306b] [int] NULL,
	[L0611b] [int] NULL,
	[P0611b] [int] NULL,
	[L0104t] [int] NULL,
	[P0104t] [int] NULL,
	[L0509t] [int] NULL,
	[P0509t] [int] NULL,
	[L1014t] [int] NULL,
	[P1014t] [int] NULL,
	[L1519t] [int] NULL,
	[P1519t] [int] NULL,
	[L2024t] [int] NULL,
	[P2024t] [int] NULL,
	[L2529t] [int] NULL,
	[P2529t] [int] NULL,
	[L3034t] [int] NULL,
	[P3034t] [int] NULL,
	[L3539t] [int] NULL,
	[P3539t] [int] NULL,
	[L4044t] [int] NULL,
	[P4044t] [int] NULL,
	[L4549t] [int] NULL,
	[P4549t] [int] NULL,
	[L5054t] [int] NULL,
	[P5054t] [int] NULL,
	[L5559t] [int] NULL,
	[P5559t] [int] NULL,
	[L6064t] [int] NULL,
	[P6064t] [int] NULL,
	[L6569t] [int] NULL,
	[P6569t] [int] NULL,
	[L7074t] [int] NULL,
	[P7074t] [int] NULL,
	[L7579t] [int] NULL,
	[P7579t] [int] NULL,
	[L8084t] [int] NULL,
	[P8084t] [int] NULL,
	[L85t] [int] NULL,
	[P85t] [int] NULL,
	[TotalPasienHidupL] [int] NULL,
	[TotalPasienHidupP] [int] NULL,
	[TotalPasienHidup] [int] NULL,
	[TotalPasienMatiL] [int] NULL,
	[TotalPasienMatiP] [int] NULL,
	[TotalPasienMati] [int] NULL,
	[LastUpdateDateTime] [datetime] NULL,
	[LastUpdateByUserID] [varchar](15) NULL,
 CONSTRAINT [PK_RlTxReport4_1V2025] PRIMARY KEY CLUSTERED 
(
	[RlTxReportNo] ASC,
	[RlMasterReportItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


