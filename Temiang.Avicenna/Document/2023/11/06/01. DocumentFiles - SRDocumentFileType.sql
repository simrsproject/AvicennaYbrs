INSERT INTO AppStandardReferenceItem (StandardReferenceID, ItemID, ItemName, Note, IsUsedBySystem, IsActive, LastUpdateDateTime, LastUpdateByUserID)
VALUES
('DocumentFileType', 'Assessment', 'Assessment', NULL, 1, 1, GETDATE(), 'sci'),
('DocumentFileType', 'PHR', 'Patient Health Record', NULL, 1, 1, GETDATE(), 'sci'),
('DocumentFileType', 'Report', 'Report', NULL, 1, 1, GETDATE(), 'sci'),
('DocumentFileType', 'Careplan', 'Care Plan', NULL, 1, 1, GETDATE(), 'sci'),
('DocumentFileType', 'Surgical', 'Surgical History', NULL, 1, 1, GETDATE(), 'sci'),
('DocumentFileType', 'Hais', 'Hais Monitoring', NULL, 1, 1, GETDATE(), 'sci'),
('DocumentFileType', 'Education', 'Education', NULL, 1, 1, GETDATE(), 'sci'),
('DocumentFileType', 'EWS', 'EWS', NULL, 1, 1, GETDATE(), 'sci'),
('DocumentFileType', 'Fluid', 'Fluid Balance', NULL, 1, 1, GETDATE(), 'sci'),
('DocumentFileType', 'Kardex', 'Medication (Kardex)', NULL, 1, 1, GETDATE(), 'sci'),
('DocumentFileType', 'UDDRecon', 'UDD Reconciliation', NULL, 1, 1, GETDATE(), 'sci'),
('DocumentFileType', 'Partograph', 'Partograph', NULL, 1, 1, GETDATE(), 'sci')
GO

ALTER TABLE DocumentFiles 
ADD 
	SRDocumentFileType VARCHAR(12) NULL,
	SRAssessmentType VARCHAR(12) NULL,
	SRHaisMonitoring VARCHAR(4) NULL
GO