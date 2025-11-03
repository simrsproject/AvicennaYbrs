UPDATE rmrv
SET    rmrv.Notes = 
       'Setting [Parameter Value] sesuai ID SMF di menu Patient Management -> Master -> Medical Record -> RL Master Report (2025) , diakhiri dengan tanda koma (,) contoh (001,)'
FROM   RlMasterReportV2025 AS rmrv
WHERE  rmrv.RlMasterReportID = 12
