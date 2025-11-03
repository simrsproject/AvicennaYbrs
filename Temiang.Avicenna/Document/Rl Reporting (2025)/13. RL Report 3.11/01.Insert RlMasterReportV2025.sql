/************************************************************
 * Code formatted by SoftTree SQL Assistant © v12.0.191
 * Time: 13/01/2025 10:06:48
 ************************************************************/

SET IDENTITY_INSERT RlMasterReportV2025 ON
INSERT INTO RlMasterReportV2025(
           RlMasterReportID,	-- this column value is auto-generated
           RlMasterReportNo,
           RlMasterReportName,
           IsActive,
           LastUpdateDateTime,
           LastUpdateByUserID,
           Notes
       )VALUES(
           11,
           N'RL 3.11',
           N'Rekapitulasi Kegiatan Pelayanan Gigi dan Mulut',
           1,
           GETDATE(),
           N'sci',
           N'Setting Master Item Service --> kolom [Report RL ID] & [Report RL Detail ID]'
       );

SET IDENTITY_INSERT RlMasterReportV2025 OFF


