CREATE VIEW vw_ItemProductFabric
AS
SELECT x.ItemID,x.FabricID 
FROM 
(
SELECT ipm.ItemID, ipm.FabricID FROM ItemProductMedic AS ipm WITH (NOLOCK)
WHERE ISNULL(ipm.FabricID, '') <> ''
UNION ALL
SELECT ipm.ItemID, ipm.FabricID FROM ItemProductNonMedic AS ipm WITH (NOLOCK)
WHERE ISNULL(ipm.FabricID, '') <> ''
UNION ALL
SELECT ipf.ItemID, ipf.FabricID FROM ItemProductFabric AS ipf WITH (NOLOCK)
) x
GROUP BY x.ItemID, x.FabricID
