--1. Insert Into AppStandardReference--
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
	'CollectMethod',
	'Specimen Collection Method',
	'5',
	1,
	1,
	'',
	'',
	GETDATE(),
	'sci',
	0,
	NULL
)
--2. Insert Into AppStandardReferenceItem--

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
('CollectMethod','01','Aspiration - action','',1,1,GETDATE(),'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('CollectMethod','02','Biopsy - action','',1,1,GETDATE(),'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('CollectMethod','03','Puncture - action','',1,1,GETDATE(),'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('CollectMethod','04','Excision - action','',1,1,GETDATE(),'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('CollectMethod','05','Scraping - action','',1,1,GETDATE(),'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('CollectMethod','06','Collection of blood specimen for laboratory','',1,1,GETDATE(),'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('CollectMethod','07','Timed urine collection','',1,1,GETDATE(),'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('CollectMethod','08','Collection of coughed sputum','',1,1,GETDATE(),'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('CollectMethod','09','Collection of arterial blood specimen','',1,1,GETDATE(),'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('CollectMethod','10','Capillary specimen collection','',1,1,GETDATE(),'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('CollectMethod','11','Urine specimen collection, catheterized','',1,1,GETDATE(),'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('CollectMethod','12','Urine specimen collection, clean catch','',1,1,GETDATE(),'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('CollectMethod','13','Blood sampling from extracorporeal blood circuit','',1,1,GETDATE(),'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('CollectMethod','14','Finger-prick sampling','',1,1,GETDATE(),'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL),
('CollectMethod','15','Taking of swab','',1,1,GETDATE(),'sci',NULL,NULL,NULL,NULL,NULL,NULL,NULL)