

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
(
	12,
	'RL 3.12',
	'Rekapitulasi Kegiatan Pelayanan Pembedahan',
	1,
	GETDATE(),
	'sci',
	''
)

SET IDENTITY_INSERT RlMasterReportV2025 ON