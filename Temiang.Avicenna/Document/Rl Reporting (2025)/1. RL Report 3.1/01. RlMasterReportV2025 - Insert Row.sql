
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
    1,
    N'RL 3.1',
    N'Indikator Pelayanan',
    1,
    GETDATE(),
    N'fahri',
    N'Data diambil dari Census -> Setting [Parameter Value] sesuai Kode Service Unit, diakhiri dengan tanda koma (,)'
  );

SET IDENTITY_INSERT RlMasterReportV2025 OFF
GO