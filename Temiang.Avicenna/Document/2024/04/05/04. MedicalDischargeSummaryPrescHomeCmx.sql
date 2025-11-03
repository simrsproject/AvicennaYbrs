
/****** Object:  Table [dbo].[MedicalDischargeSummaryPrescHomeBak]    Script Date: 03/04/2024 22:09:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MedicalDischargeSummaryPrescHomeCmx](
	[MedicationReceiveNo] [bigint] NOT NULL,
	[RegistrationNo] [varchar](20) NOT NULL,
	[ReceiveDateTime] [datetime] NULL,
	[ItemID] [varchar](10) NULL,
	[ItemDescription] [varchar](2000) NULL,
	[RefTransactionNo] [varchar](20) NULL,
	[RefSequenceNo] [varchar](3) NULL,
	[RefQty] [numeric](10, 2) NULL,
	[ReceiveQty] [numeric](10, 2) NOT NULL,
	[SRConsumeUnit] [varchar](20) NULL,
	[ConsumeQty] [numeric](10, 2) NULL,
	[SRConsumeMethod] [varchar](20) NULL,
	[BalanceQty] [numeric](10, 2) NOT NULL,
	[StartDateTime] [datetime] NULL,
	[IsClosed] [bit] NULL,
	[IsVoid] [bit] NULL,
	[IsContinue] [bit] NULL,
	[Note] [varchar](300) NULL,
	[LastUpdateDateTime] [datetime] NOT NULL,
	[LastUpdateByUserID] [varchar](20) NOT NULL,
	[IsAdmissionAppropriate] [bit] NULL,
	[IsTransferAppropriate] [bit] NULL,
	[IsDischargeAppropriate] [bit] NULL,
	[IsBroughtHome] [bit] NULL,
	[BalanceRealQty] [numeric](10, 2) NULL,
	[AdmissionAppropriateDateTime] [datetime] NULL,
	[TransferAppropriateDateTime] [datetime] NULL,
	[DischargeAppropriateDateTime] [datetime] NULL,
	[SRMedicationConsume] [varchar](20) NULL,
	[ServiceUnitID] [varchar](10) NULL,
	[RoomID] [varchar](10) NULL,
	[BedID] [varchar](10) NULL,
	[IsAdmissionPresc] [bit] NULL,
	[IsTransferPresc] [bit] NULL,
	[IsDischargePresc] [bit] NULL,
	[ConsumeQtyInString] [varchar](20) NULL,
	[SRMedicationRoute] [varchar](10) NULL,
 CONSTRAINT [PK_MedicalDischargeSummaryPrescHomeCmx] PRIMARY KEY CLUSTERED 
(
	[MedicationReceiveNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


