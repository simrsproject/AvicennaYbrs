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
CREATE TABLE dbo.Tmp_VitalSign
	(
	VitalSignID varchar(20) NOT NULL,
	VitalSignName varchar(50) NOT NULL,
	VitalSignInitial varchar(50) NULL,
	SRVitalSignGroup varchar(20) NULL,
	RowIndexInGroup int NULL,
	ValueType varchar(3) NULL,
	StandardReferenceID varchar(20) NULL,
	EntryMask varchar(50) NULL,
	VitalSignUnit varchar(20) NULL,
	NumType varchar(20) NULL,
	NumDecimalDigits int NULL,
	NumMinValue int NULL,
	NumMaxValue int NULL,
	NumMaxLength int NULL,
	IsMonitoring bit NULL,
	IsChart bit NULL,
	ChartColor int NULL,
	ChartMinValue numeric(8, 1) NULL,
	ChartMaxValue numeric(8, 1) NULL,
	LastUpdateDateTime datetime NULL,
	LastUpdateByUserID varchar(20) NULL,
	QuestionID varchar(20) NULL,
	ParentVitalSignID varchar(20) NULL,
	ChartYAxisStep numeric(8, 1) NULL,
	RowIndex int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_VitalSign SET (LOCK_ESCALATION = TABLE)
GO
DECLARE @v sql_variant 
SET @v = N'Untuk keperluan import data dari PHR'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_VitalSign', N'COLUMN', N'QuestionID'
GO
IF EXISTS(SELECT * FROM dbo.VitalSign)
	 EXEC('INSERT INTO dbo.Tmp_VitalSign (VitalSignID, VitalSignName, VitalSignInitial, SRVitalSignGroup, RowIndexInGroup, ValueType, StandardReferenceID, EntryMask, VitalSignUnit, NumType, NumDecimalDigits, NumMinValue, NumMaxValue, NumMaxLength, IsMonitoring, IsChart, ChartColor, ChartMinValue, ChartMaxValue, LastUpdateDateTime, LastUpdateByUserID, QuestionID, ParentVitalSignID, ChartYAxisStep, RowIndex)
		SELECT VitalSignID, VitalSignName, VitalSignInitial, SRVitalSignGroup, RowIndexInGroup, ValueType, StandardReferenceID, EntryMask, VitalSignUnit, NumType, NumDecimalDigits, NumMinValue, NumMaxValue, NumMaxLength, IsMonitoring, IsChart, ChartColor, ChartMinValue, ChartMaxValue, LastUpdateDateTime, LastUpdateByUserID, QuestionID, ParentVitalSignID, CONVERT(numeric(4, 1), ChartYAxisStep), RowIndex FROM dbo.VitalSign WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.VitalSign
GO
EXECUTE sp_rename N'dbo.Tmp_VitalSign', N'VitalSign', 'OBJECT' 
GO
ALTER TABLE dbo.VitalSign ADD CONSTRAINT
	PK_VitalSign PRIMARY KEY CLUSTERED 
	(
	VitalSignID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
