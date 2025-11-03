

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
	7,
	'RL 3.7',
	'Formulir RL 3.7 Rekapitulasi Kegiatan Pelayanan Neonatal, Bayi, dan Balita',
	1,
	GETDATE(),
	'sci',
	'SOP di lapangan bayi lahir di luar RS dibawah umur 28 hari tetap diceklis NewBorn.Untuk poin 4 dari death condition menu PBR dan disesuaikan master std ref-nya ReferenceID diisi 01 - 07 urutannya mengikuti yang di RL (untuk Lainnya diisi 99)'
)

SET IDENTITY_INSERT RlMasterReportV2025 OFF