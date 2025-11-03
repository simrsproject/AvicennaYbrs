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
DROP INDEX IX_Patient_FullName ON dbo.Patient
GO
DROP INDEX IX_Patient_ReverseOldMedicalNo ON dbo.Patient
GO
DROP INDEX IX_Patient_ReverseMedicalNo ON dbo.Patient
GO
ALTER TABLE dbo.Patient
	DROP COLUMN FullName, ReverseOldMedicalNo, ReverseMedicalNo
GO
ALTER TABLE dbo.Patient SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO


ALTER TABLE Patient ADD ReverseMedicalNo AS CONVERT(NVARCHAR(15), REVERSE(REPLACE(MedicalNo,'-','')))
GO
ALTER TABLE Patient ADD ReverseOldMedicalNo AS CONVERT(NVARCHAR(20),REVERSE(REPLACE(OldMedicalNo,'-','')))
GO
ALTER TABLE Patient ADD FullName AS CONVERT(NVARCHAR(150),TRIM(FirstName) + (CASE WHEN MiddleName<>'' THEN ' '+ TRIM(MiddleName) ELSE '' END) + (CASE WHEN LastName<>'' THEN ' '+ TRIM(LastName) ELSE '' END)) 
GO

CREATE INDEX IX_Patient_ReverseMedicalNo ON dbo.Patient	(ReverseMedicalNo)
GO
CREATE INDEX IX_Patient_ReverseOldMedicalNo ON dbo.Patient	(ReverseOldMedicalNo)
GO
CREATE INDEX IX_Patient_FullName ON dbo.Patient	(FullName)