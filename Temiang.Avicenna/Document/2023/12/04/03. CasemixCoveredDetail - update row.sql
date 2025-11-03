UPDATE c
SET c.IsInclude = 0
FROM CasemixCoveredDetail c
INNER JOIN Item AS i ON i.ItemID = c.ItemID
WHERE i.SRItemType IN ('01', '31', '41')