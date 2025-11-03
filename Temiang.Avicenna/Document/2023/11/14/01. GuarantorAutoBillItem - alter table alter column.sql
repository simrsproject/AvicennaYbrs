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
CREATE TABLE dbo.Tmp_GuarantorAutoBillItem
	(
	GuarantorID varchar(10) NOT NULL,
	ServiceUnitID varchar(10) NOT NULL,
	ItemID varchar(10) NOT NULL,
	Quantity numeric(10, 2) NOT NULL,
	IsGenerateOnRegistration bit NOT NULL,
	IsGenerateOnNewRegistration bit NOT NULL,
	IsGenerateOnReferral bit NOT NULL,
	IsGenerateOnFirstRegistration bit NOT NULL,
	IsActive bit NOT NULL,
	LastUpdateDateTime datetime NULL,
	LastUpdateByUserID varchar(15) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_GuarantorAutoBillItem SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.GuarantorAutoBillItem)
	 EXEC('INSERT INTO dbo.Tmp_GuarantorAutoBillItem (GuarantorID, ServiceUnitID, ItemID, Quantity, IsGenerateOnRegistration, IsGenerateOnNewRegistration, IsGenerateOnReferral, IsGenerateOnFirstRegistration, IsActive, LastUpdateDateTime, LastUpdateByUserID)
		SELECT GuarantorID, CONVERT(varchar(10), SRRegistrationType), ItemID, Quantity, IsGenerateOnRegistration, IsGenerateOnNewRegistration, IsGenerateOnReferral, IsGenerateOnFirstRegistration, IsActive, LastUpdateDateTime, LastUpdateByUserID FROM dbo.GuarantorAutoBillItem WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.GuarantorAutoBillItem
GO
EXECUTE sp_rename N'dbo.Tmp_GuarantorAutoBillItem', N'GuarantorAutoBillItem', 'OBJECT' 
GO
ALTER TABLE dbo.GuarantorAutoBillItem ADD CONSTRAINT
	PK_GuarantorAutoBillItem PRIMARY KEY CLUSTERED 
	(
	GuarantorID,
	ServiceUnitID,
	ItemID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
