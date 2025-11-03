
SET IDENTITY_INSERT [RlMasterReportV2025] ON
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
    22,
    N'RL 51',
    N'Kompilasi Morbiditas Pasien Rawat Jalan',
    1,
    GETDATE(),
    N'kews',
    ''
  );
  
SET IDENTITY_INSERT [RlMasterReportV2025] OFF
GO
