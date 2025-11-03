ALTER TABLE Patient ADD ReverseMedicalNo AS CONVERT(VARCHAR(15), REVERSE(REPLACE(MedicalNo,'-','')))
GO
ALTER TABLE Patient ADD ReverseOldMedicalNo AS CONVERT(VARCHAR(20),REVERSE(REPLACE(OldMedicalNo,'-','')))
GO
ALTER TABLE Patient ADD FullName AS TRIM(FirstName) + (CASE WHEN MiddleName<>'' THEN ' '+ TRIM(MiddleName) ELSE '' END) + (CASE WHEN LastName<>'' THEN ' '+ TRIM(LastName) ELSE '' END) 
GO

CREATE INDEX IX_Patient_ReverseMedicalNo ON dbo.Patient	(ReverseMedicalNo)
GO
CREATE INDEX IX_Patient_ReverseOldMedicalNo ON dbo.Patient	(ReverseOldMedicalNo)
GO
CREATE INDEX IX_Patient_FullName ON dbo.Patient	(FullName)