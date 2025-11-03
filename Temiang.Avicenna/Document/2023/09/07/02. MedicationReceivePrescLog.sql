
/****** Object:  Table [dbo].[MedicationReceivePrescLog]    Script Date: 9/6/2023 9:15:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MedicationReceivePrescLog](
	MedRecPrescLogID bigint NOT NULL IDENTITY (1, 1),
	MedicationReceiveNo bigint NOT NULL,
	PrescriptionNo varchar(20) NOT NULL,
	SequenceNo varchar(3) NOT NULL,
	IsPrescriptionVoid bit NOT NULL,
	CreatedDateTime datetime NOT NULL,
	CreatedByUserID varchar(20) NOT NULL
 CONSTRAINT [PK_MedicationReceivePrescLog] PRIMARY KEY CLUSTERED 
(
	[MedRecPrescLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE NONCLUSTERED INDEX [IX_MedicationReceivePrescLog_MedicationReceiveNo] ON [dbo].[MedicationReceivePrescLog]
(
	[MedicationReceiveNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO