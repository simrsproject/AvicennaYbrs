
-- 1. Matikan Identity
SET IDENTITY_INSERT RlMasterReportV2025 ON;

-- 2. Jalankan Insert
INSERT INTO [RlMasterReportV2025]([RlMasterReportID],[RlMasterReportNo],[RlMasterReportName],[IsActive],[LastUpdateDateTime],[LastUpdateByUserID],[Notes])
VALUES(9,N'RL 3.9',N'Rekapitulasi Kegiatan Pelayanan Radiologi',1,GETDATE(),N'Apip',N'Setting Master Item Radiology --> kolom [Report RL ID] & [Report RL Detail ID]');

-- 3. Nyalakan Identity
SET IDENTITY_INSERT RlMasterReportV2025 OFF;
