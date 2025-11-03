
-- Aktifkan IDENTITY_INSERT untuk tabel
SET IDENTITY_INSERT RlMasterReportV2025 ON;

-- Lakukan INSERT dengan nilai spesifik untuk RlMasterReportID
INSERT INTO RlMasterReportV2025
(
	RlMasterReportID, -- Kolom auto-generated
	RlMasterReportNo,
	RlMasterReportName,
	IsActive,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Notes
)
VALUES
(
	10, -- Nilai spesifik untuk RlMasterReportID
	'RL 3.10',
	'Rekapitulasi Kegiatan Pelayanan Rujukan',
	1,
	GETDATE(),
	'rama',
	'Setting [SMF]'
);

-- Matikan kembali IDENTITY_INSERT untuk tabel
SET IDENTITY_INSERT RlMasterReportV2025 OFF;

