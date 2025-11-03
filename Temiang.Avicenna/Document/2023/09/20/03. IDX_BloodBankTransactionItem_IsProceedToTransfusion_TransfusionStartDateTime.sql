CREATE NONCLUSTERED INDEX [IDX_BloodBankTransactionItem_IsProceedToTransfusion_TransfusionStartDateTime] ON [dbo].[BloodBankTransactionItem]
(
	[IsProceedToTransfusion] ASC,
	[TransfusionStartDateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 50) ON [PRIMARY]
GO