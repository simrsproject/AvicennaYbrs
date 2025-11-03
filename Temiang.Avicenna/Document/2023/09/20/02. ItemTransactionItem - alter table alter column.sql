ALTER TABLE [dbo].[ItemTransactionItem] DROP CONSTRAINT [DF__ItemTrans__Expir__204DAB2F]
GO

ALTER TABLE [dbo].[ItemTransactionItem]
ALTER COLUMN ExpiredDate DATETIME
GO

ALTER TABLE [dbo].[ItemTransactionItem] ADD  CONSTRAINT [DF__ItemTrans__Expir__204DAB2F]  DEFAULT ('') FOR [ExpiredDate]
GO