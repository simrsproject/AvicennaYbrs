ALTER TABLE dbo.ItemProductMedic ADD
	BpjsMaxQtyOrderIpr INT NULL,
	BpjsMaxQtyOrderOpr INT NULL,
	BpjsMaxQtyOrderEmr INT NULL
GO

UPDATE ItemProductMedic 
SET BpjsMaxQtyOrderIpr = 0, BpjsMaxQtyOrderOpr=0, BpjsMaxQtyOrderEmr = 0
GO