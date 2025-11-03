/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 24/01/2025 15:31:22
 ************************************************************/

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
    21,
    N'RL 4.3',
    N'10 Besar Kematian Penyakit Rawat Inap ',
    1,
    GETDATE(),
    N'kews',
    ''
  );
  
SET IDENTITY_INSERT [RlMasterReportV2025] OFF
GO