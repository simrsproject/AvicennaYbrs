IF DB_ID('WYNAKOM_LIS_INTEROP') IS NOT NULL
BEGIN
	USE [WYNAKOM_LIS_INTEROP]

	/****** Object:  Table [dbo].[Satusehat_Ordered_Items]    Script Date: 13/12/2024 08:46:22 ******/
	SET ANSI_NULLS ON

	SET QUOTED_IDENTIFIER ON

	CREATE TABLE [dbo].[Satusehat_Ordered_Items](
		[Order_Number] [varchar](20) NOT NULL,
		[Order_Sequence_No] [varchar](3) NOT NULL,
		[Order_Item_ID] [varchar](10) NOT NULL,
		[Order_Item_Name] [varchar](200) NULL,
		[SS_Loinc_Name] [varchar](200) NULL,
		[SS_Loinc_ID] [varchar](20) NULL,
		[SS_Patient_ID] [varchar](20) NULL,
		[SS_Patient_Name] [varchar](200) NULL,
		[SS_Requester_Practioner_ID] [varchar](20) NULL,
		[SS_Requester_Practioner_Name] [varchar](200) NULL,
		[SS_Encounter_ID] [varchar](36) NULL,
		[SS_Service_Request_ID] [varchar](36) NULL,
		[SS_Specimen_ID] [varchar](36) NULL,
	 CONSTRAINT [PK_Satusehat_Ordered_Items_1] PRIMARY KEY CLUSTERED 
	(
		[Order_Number] ASC,
		[Order_Sequence_No] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END

