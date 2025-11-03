
SET IDENTITY_INSERT RlMasterReportV2025 ON
GO

INSERT INTO [RlMasterReportV2025]
  (
    [RlMasterReportID],
    [RlMasterReportNo],
    [RlMasterReportName],
    [IsActive],
    [LastUpdateDateTime],
    [LastUpdateByUserID],
    [Notes]
  )
VALUES
  (
    3,
    N'RL 3.3',
    N'Rekapitulasi Kegiatan Pelayanan Rawat Darurat',
    1,
    '2025-01-13 15:23:35.550',
    N'kews',
    N'Setting [Luka-luka] -> CustomField pada AppStandardReferenceItem (PatientInCondition) diisi : LK'
  );

SET IDENTITY_INSERT RlMasterReportV2025 OFF
GO