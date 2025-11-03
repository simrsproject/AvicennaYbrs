ALTER TABLE dbo.ServiceUnitParamedic ADD
	IsAcceptNonBPJS BIT NULL
GO

UPDATE ServiceUnitParamedic SET IsAcceptNonBPJS =  1
GO
