
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
	19, -- Nilai spesifik untuk RlMasterReportID
	'RL 4.1',
	'Kompilasi Penyakit/Morbiditas Pasien Rawat Inap',
	1,
	GETDATE(),
	'rama',
	''
);

-- Matikan kembali IDENTITY_INSERT untuk tabel
SET IDENTITY_INSERT RlMasterReportV2025 OFF;


