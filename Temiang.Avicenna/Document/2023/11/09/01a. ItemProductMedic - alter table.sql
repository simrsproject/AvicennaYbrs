ALTER TABLE dbo.ItemProductMedic ADD
	FornasRestrictionNotes VARCHAR(1000) NULL
GO

UPDATE x SET x.FornasRestrictionNotes = x.SpecificInfo FROM ItemProductMedic x 
GO

UPDATE x SET x.SpecificInfo = '' FROM ItemProductMedic x 
GO

ALTER TABLE ItemProductMedic ALTER COLUMN SpecificInfo varchar(100)
GO
