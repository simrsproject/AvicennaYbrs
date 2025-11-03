ALTER TABLE PatientChildBirthHistory ADD 
	PregnanDurationDay INT NULL, 
	SRBirthType VARCHAR(5)
GO

INSERT INTO AppStandardReference
(
	StandardReferenceID, StandardReferenceName, ItemLength, IsUsedBySystem, IsActive, StandardReferenceGroup, Note, LastUpdateDateTime, LastUpdateByUserID, HasCOA, IsNumericValue
)
VALUES
(
	'BirthType', 'Birth Type', 10, 0, 1, '', '', GETDATE(), 'sci', 0, NULL
)
GO

INSERT INTO AppStandardReferenceItem
(
	StandardReferenceID, ItemID, ItemName, Note, IsUsedBySystem, IsActive, LastUpdateDateTime, LastUpdateByUserID, ReferenceID
)
VALUES
(
	'BirthType', '01', 'Abortus', '', 1, 1, GETDATE(), 'sci', NULL
),
(
	'BirthType', '02', 'Prematur', '', 1, 1, GETDATE(), 'sci', NULL
),
(
	'BirthType', '03', 'Aterm', '', 1, 1, GETDATE(), 'sci', NULL
),
(
	'BirthMethod', '11', 'Sungsang Manual AID', '', 1, 1, GETDATE(), 'sci', NULL
)
GO