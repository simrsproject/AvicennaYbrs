--01 ALTER TABLE PatientBirthRecord 
ALTER TABLE PatientBirthRecord ADD SRBirthMethodPhr VARCHAR(20) NULL
ALTER TABLE PatientBirthRecord ADD MethodOther VARCHAR(200) NULL
--02 Insert AppStandardReference BirthMethodPhr--
INSERT INTO AppStandardReference
(
	StandardReferenceID,
	StandardReferenceName,
	ItemLength,
	IsUsedBySystem,
	IsActive,
	StandardReferenceGroup,
	Note,
	LastUpdateDateTime,
	LastUpdateByUserID,
	HasCOA,
	IsNumericValue
)
VALUES
(
	'BirthMethodPhr',
	'Birth Method From PHR',
	'20',
	1,
	1,
	'',
	'',
	GETDATE(),
	'dewi_sci',
	0,
	NULL
)

--03 Insert Into AppStandardReferenceItem BirthMethodPhr--
INSERT INTO AppStandardReferenceItem
(
	StandardReferenceID,
	ItemID,
	ItemName,
	Note,
	IsUsedBySystem,
	IsActive,
	LastUpdateDateTime,
	LastUpdateByUserID,
	ReferenceID,
	coaID,
	subledgerID,
	CustomField,
	LineNumber,
	NumericValue,
	CustomField2
)
VALUES
('BirthMethodPhr','1','Spontan','',1,1,GETDATE(),'dewi_sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('BirthMethodPhr','2','Vaccum','',1,1,GETDATE(),'dewi_sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('BirthMethodPhr','3','Sectio','',1,1,GETDATE(),'dewi_sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('BirthMethodPhr','4','Forcep','',1,1,GETDATE(),'dewi_sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('BirthMethodPhr','5','Lainnya','',1,1,GETDATE(),'dewi_sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL)


