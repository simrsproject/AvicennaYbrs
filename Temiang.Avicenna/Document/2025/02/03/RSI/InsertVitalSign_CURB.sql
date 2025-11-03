/*RSI*/
INSERT INTO [VitalSign]([VitalSignID],[VitalSignName],[VitalSignInitial],[SRVitalSignGroup],
[RowIndexInGroup],[ValueType],[StandardReferenceID],[EntryMask],[VitalSignUnit],[NumType],
[NumDecimalDigits],[NumMinValue],[NumMaxValue],[NumMaxLength],[IsMonitoring],[IsChart],
[ChartColor],[ChartMinValue],[ChartMaxValue],[LastUpdateDateTime],[LastUpdateByUserID],[QuestionID],[ParentVitalSignID])
VALUES(N'CURB.11',N'Condusion of mental Score','CURB',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'CURB.11',NULL);

INSERT INTO [VitalSign]([VitalSignID],[VitalSignName],[VitalSignInitial],[SRVitalSignGroup],
[RowIndexInGroup],[ValueType],[StandardReferenceID],[EntryMask],[VitalSignUnit],[NumType],
[NumDecimalDigits],[NumMinValue],[NumMaxValue],[NumMaxLength],[IsMonitoring],[IsChart],
[ChartColor],[ChartMinValue],[ChartMaxValue],[LastUpdateDateTime],[LastUpdateByUserID],[QuestionID],[ParentVitalSignID])
VALUES(N'CURB.13',N'Ureum','CURB',NULL,NULL,NULL,
NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'CURB.13',NULL);

INSERT INTO [VitalSign]([VitalSignID],[VitalSignName],[VitalSignInitial],[SRVitalSignGroup],
[RowIndexInGroup],[ValueType],[StandardReferenceID],[EntryMask],[VitalSignUnit],[NumType],
[NumDecimalDigits],[NumMinValue],[NumMaxValue],[NumMaxLength],[IsMonitoring],[IsChart],
[ChartColor],[ChartMinValue],[ChartMaxValue],[LastUpdateDateTime],[LastUpdateByUserID],[QuestionID],[ParentVitalSignID])
VALUES(N'CURB.14',N'Respiratory Rate','CURB',NULL,NULL,NULL,
NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'CURB.14',NULL);

INSERT INTO [VitalSign]([VitalSignID],[VitalSignName],[VitalSignInitial],[SRVitalSignGroup],
[RowIndexInGroup],[ValueType],[StandardReferenceID],[EntryMask],[VitalSignUnit],[NumType],
[NumDecimalDigits],[NumMinValue],[NumMaxValue],[NumMaxLength],[IsMonitoring],[IsChart],
[ChartColor],[ChartMinValue],[ChartMaxValue],[LastUpdateDateTime],[LastUpdateByUserID],[QuestionID],[ParentVitalSignID])
VALUES(N'CURB.15',N'Blood Pressure','CURB',NULL,NULL,NULL,
NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'CURB.15',NULL);

INSERT INTO [VitalSign]([VitalSignID],[VitalSignName],[VitalSignInitial],[SRVitalSignGroup],
[RowIndexInGroup],[ValueType],[StandardReferenceID],[EntryMask],[VitalSignUnit],[NumType],
[NumDecimalDigits],[NumMinValue],[NumMaxValue],[NumMaxLength],[IsMonitoring],[IsChart],
[ChartColor],[ChartMinValue],[ChartMaxValue],[LastUpdateDateTime],[LastUpdateByUserID],[QuestionID],[ParentVitalSignID])
VALUES(N'CURB.16',N'Age','CURB',NULL,NULL,NULL,
NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'CURB.16',NULL);

INSERT INTO [VitalSign]([VitalSignID],[VitalSignName],[VitalSignInitial],[SRVitalSignGroup],
[RowIndexInGroup],[ValueType],[StandardReferenceID],[EntryMask],[VitalSignUnit],[NumType],
[NumDecimalDigits],[NumMinValue],[NumMaxValue],[NumMaxLength],[IsMonitoring],[IsChart],
[ChartColor],[ChartMinValue],[ChartMaxValue],[LastUpdateDateTime],[LastUpdateByUserID],[QuestionID],[ParentVitalSignID])
VALUES(N'CURB.17',N'Total Score','CURB',NULL,NULL,NULL,
NULL,NULL,NULL,N'NUM',4,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'CURB.17',NULL);

INSERT INTO [VitalSign]([VitalSignID],[VitalSignName],[VitalSignInitial],[SRVitalSignGroup],
[RowIndexInGroup],[ValueType],[StandardReferenceID],[EntryMask],[VitalSignUnit],[NumType],
[NumDecimalDigits],[NumMinValue],[NumMaxValue],[NumMaxLength],[IsMonitoring],[IsChart],
[ChartColor],[ChartMinValue],[ChartMaxValue],[LastUpdateDateTime],[LastUpdateByUserID],[QuestionID],[ParentVitalSignID])
VALUES(N'CURB.18',N'Respon Klinis','CURB',NULL,NULL,NULL,
NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'CURB.18',NULL);

UPDATE Question
SET VitalSignID = 
    CASE 
        WHEN QuestionID = 'CURB.11' THEN 'CURB.11'
        WHEN QuestionID = 'CURB.13' THEN 'CURB.13'
        WHEN QuestionID = 'CURB.14' THEN 'CURB.14'
        WHEN QuestionID = 'CURB.15' THEN 'CURB.15'
        WHEN QuestionID = 'CURB.16' THEN 'CURB.16'
        WHEN QuestionID = 'CURB.17' THEN 'CURB.17'
        WHEN QuestionID = 'CURB.18' THEN 'CURB.18'
    END
WHERE QuestionID LIKE 'CURB.1%';