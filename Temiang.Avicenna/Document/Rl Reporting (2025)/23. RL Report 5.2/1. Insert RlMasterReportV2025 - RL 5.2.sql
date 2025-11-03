INSERT INTO RlMasterReportV2025
       (
       	RlMasterReportID, -- this column value is auto-generated
       	RlMasterReportNo,
       	RlMasterReportName,
       	IsActive,
       	LastUpdateDateTime,
       	LastUpdateByUserID,
       	Notes
       )
       VALUES
       (23,
       	'RL 5.2',
       	'10 Besar Kasus Baru Penyakit Rawat Jalan',
       	1,
       	GETDATE(),
       	'sci',
       	'Setting Master Item Service --> kolom [Report RL ID] & [Report RL Detail ID]'
       )
       
       SET IDENTITY_INSERT RlMasterReportV2025 ON