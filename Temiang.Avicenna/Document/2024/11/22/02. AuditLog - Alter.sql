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
CREATE TABLE dbo.Tmp_AuditLog
	(
	AuditLogID int NOT NULL IDENTITY (1, 1),
	TableName varchar(100) NOT NULL,
	AuditActionType char(1) NOT NULL,
	PrimaryKeyData varchar(500) NOT NULL,
	ActionByUserID varchar(40) NOT NULL,
	LogDateTime datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_AuditLog SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_AuditLog ON
GO
IF EXISTS(SELECT * FROM dbo.AuditLog)
	 EXEC('INSERT INTO dbo.Tmp_AuditLog (AuditLogID, TableName, AuditActionType, PrimaryKeyData, ActionByUserID, LogDateTime)
		SELECT AuditLogID, TableName, AuditActionType, PrimaryKeyData, ActionByUserID, LogDateTime FROM dbo.AuditLog WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_AuditLog OFF
GO
DROP TABLE dbo.AuditLog
GO
EXECUTE sp_rename N'dbo.Tmp_AuditLog', N'AuditLog', 'OBJECT' 
GO
ALTER TABLE dbo.AuditLog ADD CONSTRAINT
	PK_AuditLog PRIMARY KEY CLUSTERED 
	(
	AuditLogID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT