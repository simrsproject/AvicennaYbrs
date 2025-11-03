

INSERT INTO RlMasterReportItemV2025
SELECT 22            RlMasterReportID,
       RIGHT(
           REPLICATE('0', 3) + CAST(ROW_NUMBER() OVER(ORDER BY d.DtdNo) AS VARCHAR(3)),
           3
       )             RlMasterReportItemNo,
       d.DtdNo       RlMasterReportItemCode,
       d.DtdName     RlMasterReportItemName,
       ''            SRParamedicRL1,
       1             IsActive,
       ''            ParameterValue,
       GETDATE()     LastUpdateDateTime,
       'kews'        LastUpdateByUserID
FROM   Dtd        AS d
WHERE  d.DtdName NOT IN ('-', '') 