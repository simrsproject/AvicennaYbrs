ALTER TABLE dbo.Guarantor ADD
	IsItemServiceRestrictionStatusAllowed BIT NULL,
	IsItemLabRestrictionStatusAllowed BIT NULL,
	IsItemRadRestrictionStatusAllowed BIT NULL,
	IsItemProductRestrictionStatusAllowed BIT NULL
GO

UPDATE Guarantor 
SET IsItemServiceRestrictionStatusAllowed = 1, 
	IsItemLabRestrictionStatusAllowed = 1, 
	IsItemRadRestrictionStatusAllowed = 1, 
	IsItemProductRestrictionStatusAllowed = 1
GO
