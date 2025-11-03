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
	'AnswerMethod',
	'Question and Answer Method',
	5,
	0,
	1,
	NULL	,
	NULL,
	GETDATE(),
	'han',
	NULL,
	NULL
)

INSERT INTO [AppStandardReferenceItem]([StandardReferenceID],[ItemID],[ItemName],[Note],[IsUsedBySystem],[IsActive],[LastUpdateDateTime],[LastUpdateByUserID],[ReferenceID],[coaID],[subledgerID],[CustomField],[LineNumber],[NumericValue],[CustomField2])
VALUES(N'AnswerMethod',N'LTR',N'Letter','',1,1,NULL,N'han',NULL,NULL,NULL,NULL,2,NULL,NULL);
INSERT INTO [AppStandardReferenceItem]([StandardReferenceID],[ItemID],[ItemName],[Note],[IsUsedBySystem],[IsActive],[LastUpdateDateTime],[LastUpdateByUserID],[ReferenceID],[coaID],[subledgerID],[CustomField],[LineNumber],[NumericValue],[CustomField2])
VALUES(N'AnswerMethod',N'PHN',N'Phone','',1,1,NULL,N'han',NULL,NULL,NULL,NULL,3,NULL,NULL);
INSERT INTO [AppStandardReferenceItem]([StandardReferenceID],[ItemID],[ItemName],[Note],[IsUsedBySystem],[IsActive],[LastUpdateDateTime],[LastUpdateByUserID],[ReferenceID],[coaID],[subledgerID],[CustomField],[LineNumber],[NumericValue],[CustomField2])
VALUES(N'AnswerMethod',N'SMD',N'Social Media','',1,1,NULL,N'han',NULL,NULL,NULL,NULL,4,NULL,NULL);
INSERT INTO [AppStandardReferenceItem]([StandardReferenceID],[ItemID],[ItemName],[Note],[IsUsedBySystem],[IsActive],[LastUpdateDateTime],[LastUpdateByUserID],[ReferenceID],[coaID],[subledgerID],[CustomField],[LineNumber],[NumericValue],[CustomField2])
VALUES(N'AnswerMethod',N'VRB',N'Verbal','',1,1,NULL,N'han',NULL,NULL,NULL,NULL,1,NULL,NULL);


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
	'DurationType',
	'Answer Duration',
	5,
	0,
	1,
	NULL	,
	NULL,
	GETDATE(),
	'han',
	NULL,
	NULL
)

INSERT INTO [AppStandardReferenceItem]([StandardReferenceID],[ItemID],[ItemName],[Note],[IsUsedBySystem],[IsActive],[LastUpdateDateTime],[LastUpdateByUserID],[ReferenceID],[coaID],[subledgerID],[CustomField],[LineNumber],[NumericValue],[CustomField2])
VALUES(N'DurationType',N'05M',N'5 Minute','',1,1,NULL,N'han',NULL,NULL,NULL,NULL,1,NULL,NULL);
INSERT INTO [AppStandardReferenceItem]([StandardReferenceID],[ItemID],[ItemName],[Note],[IsUsedBySystem],[IsActive],[LastUpdateDateTime],[LastUpdateByUserID],[ReferenceID],[coaID],[subledgerID],[CustomField],[LineNumber],[NumericValue],[CustomField2])
VALUES(N'DurationType',N'10M',N'10 Minute','',1,1,NULL,N'han',NULL,NULL,NULL,NULL,2,NULL,NULL);
INSERT INTO [AppStandardReferenceItem]([StandardReferenceID],[ItemID],[ItemName],[Note],[IsUsedBySystem],[IsActive],[LastUpdateDateTime],[LastUpdateByUserID],[ReferenceID],[coaID],[subledgerID],[CustomField],[LineNumber],[NumericValue],[CustomField2])
VALUES(N'DurationType',N'15M',N'15 Minute','',1,1,NULL,N'han',NULL,NULL,NULL,NULL,3,NULL,NULL);
INSERT INTO [AppStandardReferenceItem]([StandardReferenceID],[ItemID],[ItemName],[Note],[IsUsedBySystem],[IsActive],[LastUpdateDateTime],[LastUpdateByUserID],[ReferenceID],[coaID],[subledgerID],[CustomField],[LineNumber],[NumericValue],[CustomField2])
VALUES(N'DurationType',N'30M',N'30 Minute','',1,1,NULL,N'han',NULL,NULL,NULL,NULL,4,NULL,NULL);
