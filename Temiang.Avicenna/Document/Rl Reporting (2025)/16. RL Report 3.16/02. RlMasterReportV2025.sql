/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 02/02/2025 02:58:02
 ************************************************************/

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
    16,
    N'RL 3.16',
    N'Rekapitulasi Kegiatan Pelayanan Keluarga Berencana',
    1,
    GETDATE(),
    N'sci',
    N'Data diambil dari PHR Keluarga Berencana'
  );

  SET IDENTITY_INSERT RlMasterReportV2025 ON