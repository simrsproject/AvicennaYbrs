SET IDENTITY_INSERT [RlMasterReportV2025] ON


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
    15,
    N'RL 3.15',
    N'Rekapitulasi Kegiatan Pelayanan Kesehatan Jiwa',
    1,
    '2025-01-08 10:36:15.093',
    N'sci',
    N'Setting Master Item Service --> kolom [Report RL ID] & [Report RL Detail ID]'
  );

    SET IDENTITY_INSERT [RlMasterReportV2025] OFF
