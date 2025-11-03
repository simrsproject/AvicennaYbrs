ALTER TABLE dbo.Item ADD
	EconomicLifeInYear INT NULL
GO
UPDATE Item SET EconomicLifeInYear = 0

UPDATE i SET i.EconomicLifeInYear = 0 
FROM Item AS i 
WHERE i.SRItemType IN ('11', '21', '81')

GO
