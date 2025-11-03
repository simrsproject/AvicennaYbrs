/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AuditLogSetting ADD
	ExcludeAuditColumn varchar(300) NULL
GO
ALTER TABLE dbo.AuditLogSetting SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO


DELETE AuditLogSetting WHERE IsAuditLog=0;
GO
INSERT INTO [AuditLogSetting]([TableName],[TableDescription],[IsAuditLog],[LastUpdateDateTime],[LastUpdateByUserID],[IsConsolidationBranchToHO],[IsConsolidationHOToBranch],[ExcludeAuditColumn])
VALUES(N'NursingDiagnosaTransDT',N'PPA Notes',1,'2023-11-26 23:02:07.763',N'han',NULL,NULL,NULL);
INSERT INTO [AuditLogSetting]([TableName],[TableDescription],[IsAuditLog],[LastUpdateDateTime],[LastUpdateByUserID],[IsConsolidationBranchToHO],[IsConsolidationHOToBranch],[ExcludeAuditColumn])
VALUES(N'ParamedicConsultRefer',N'Paramedic Consult Refer',1,'2023-11-26 23:02:07.763',N'han',NULL,NULL,NULL);
INSERT INTO [AuditLogSetting]([TableName],[TableDescription],[IsAuditLog],[LastUpdateDateTime],[LastUpdateByUserID],[IsConsolidationBranchToHO],[IsConsolidationHOToBranch],[ExcludeAuditColumn])
VALUES(N'RegistrationInfoMedic',N'Histori SOAP',1,'2023-11-26 23:02:07.763',N'han',0,0,N'Info1Log,Info2Log,Info3Log,Info4Log');
