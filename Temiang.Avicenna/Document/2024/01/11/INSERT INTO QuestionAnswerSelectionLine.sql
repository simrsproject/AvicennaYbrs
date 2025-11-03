
delete FROM QuestionInGroup  WHERE QuestionGroupID='IGD.PS.JNP' AND QuestionID IN('JNP.001','JNP.003')
delete FROM QuestionInGroup  WHERE QuestionGroupID='IGD.PS.PNP' AND QuestionID IN('PNP.005','PNP.006')

delete FROM QuestionAnswerSelectionLine  WHERE QuestionAnswerSelectionID='RESP' AND QuestionAnswerSelectionLineID IN('0','1')
delete FROM QuestionAnswerSelectionLine  WHERE QuestionAnswerSelectionID='CRT' AND QuestionAnswerSelectionLineID IN('1','2')


UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'PUPIL.01'
WHERE QuestionID='DSB.001'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'LITRS.03'
WHERE QuestionID='DSB.005'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'KONJ.02'
WHERE QuestionID='KLH.K01'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'SKLR.02'
WHERE QuestionID='KLH.K02'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'BBR.02'
WHERE QuestionID='KLH.K03'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'MUKS.02'
WHERE QuestionID='KLH.K04'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'NTJELASKAN1'
WHERE QuestionID='KLH.K05'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'NOR'
WHERE QuestionID='KLH.K99'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'DEVS.03'
WHERE QuestionID='KLH.L01'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'JVP.02'
WHERE QuestionID='KLH.L02'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'LNN.02'
WHERE QuestionID='KLH.L03'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'TIRD.02'
WHERE QuestionID='KLH.L04'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'NOR'
WHERE QuestionID='KLH.L99'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'NOR'
WHERE QuestionID='TRX.003'


UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'NOR'
WHERE QuestionID='TRX.001'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'NOR'
WHERE QuestionID='TRX.002'


UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'KJN.01'
WHERE QuestionID='SIR.006'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'CRT.01'
WHERE QuestionID='SIR.004'

UPDATE QuestionAnswerSelectionLine
SET QuestionAnswerSelectionLineText = '<=2 detik'
WHERE QuestionAnswerSelectionID='CRT' AND QuestionAnswerSelectionLineID='CRT.01'

UPDATE QuestionAnswerSelectionLine
SET QuestionAnswerSelectionLineText = '<2 detik'
WHERE QuestionAnswerSelectionID='CRT' AND QuestionAnswerSelectionLineID='CRT.02'

UPDATE Question
SET SRAnswerType = 'CB2',QuestionAnswerSelectionID2 = 'NADI',QuestionAnswerDefaultSelectionID = 'NADI.03',QuestionAnswerDefaultSelectionID2 = 'NADI.01'
WHERE QuestionID='SIR.001'

UPDATE Question
SET SRAnswerType = 'CB2',QuestionAnswerSelectionID2 = 'AKRAL',QuestionAnswerDefaultSelectionID = 'AKRAL.01',QuestionAnswerDefaultSelectionID2 = 'AKRAL3'
WHERE QuestionID='SIR.003'

UPDATE Question
SET SRAnswerType = 'CBO', QuestionAnswerSelectionID = 'BA', QuestionAnswerDefaultSelectionID = 'BA.01'
WHERE QuestionID='JNP.006'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'KULIT.01'
WHERE QuestionID='SIR.002'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'RESP.03'
WHERE QuestionID='PNP.001'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'SIMT.YS'
WHERE QuestionID='PNP.002'

UPDATE Question
SET SRAnswerType = 'CBO', QuestionAnswerSelectionID = 'WOB',QuestionAnswerDefaultSelectionID = 'WOB.01'
WHERE QuestionID='PNP.004'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'KJN.01'
WHERE QuestionID='PNP.006'



UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'OBP.01'
WHERE QuestionID='JNP.002'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'TJN.01'
WHERE QuestionID='JNP.004'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'RSA.01'
WHERE QuestionID='JNP.005'

UPDATE Question
SET QuestionAnswerDefaultSelectionID = 'KJN.01'
WHERE QuestionID='JNP.007'

UPDATE QuestionAnswerSelectionLine
SET QuestionAnswerSelectionLineText = 'Pernafasan spontan'
WHERE QuestionAnswerSelectionID='RESP' AND QuestionAnswerSelectionLineID='RESP.01'

UPDATE QuestionAnswerSelectionLine
SET QuestionAnswerSelectionLineText = 'Tidak Bernafasan'
WHERE QuestionAnswerSelectionID='RESP' AND QuestionAnswerSelectionLineID='RESP.02'

UPDATE QuestionAnswerSelectionLine
SET QuestionAnswerSelectionLineText = 'Reguler Spontan'
WHERE QuestionAnswerSelectionID='RESP' AND QuestionAnswerSelectionLineID='RESP.03'

UPDATE QuestionAnswerSelectionLine
SET QuestionAnswerSelectionLineText = 'Ireguler'
WHERE QuestionAnswerSelectionID='RESP' AND QuestionAnswerSelectionLineID='RESP.04'

DELETE FROM QuestionAnswerSelectionLine WHERE QuestionAnswerSelectionID='OBP'
DELETE FROM QuestionAnswerSelectionLine WHERE QuestionAnswerSelectionID='TJN'
DELETE FROM QuestionAnswerSelectionLine WHERE QuestionAnswerSelectionID='RSA'

INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'OBP',
	'OBP.01',
	'Paten',
	GETDATE(),
	'dk',
	NULL
)

INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'OBP',
	'OBP.02',
	'Stridor',
	GETDATE(),
	'dk',
	NULL
)

INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'OBP',
	'OBP.03',
	'Snoring',
	GETDATE(),
	'dk',
	NULL
)
INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'OBP',
	'OBP.04',
	'Gurgling',
	GETDATE(),
	'dk',
	NULL
)


INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'OBP',
	'OBP.05',
	'Wheezing',
	GETDATE(),
	'dk',
	NULL
)

INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'OBP',
	'OBP.06',
	'Obstruksi total',
	GETDATE(),
	'dk',
	NULL
)

INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'TJN',
	'TJN.01',
	'Tidak Ada',
	GETDATE(),
	'dk',
	NULL
)

INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'TJN',
	'TJN.02',
	'Fasial',
	GETDATE(),
	'dk',
	NULL
)


INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'TJN',
	'TJN.03',
	'Leher',
	GETDATE(),
	'dk',
	NULL
)


INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'TJN',
	'TJN.04',
	'Inhalasi',
	GETDATE(),
	'dk',
	NULL
)

INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'RSA',
	'RSA.01',
	'Tidak Ada',
	GETDATE(),
	'dk',
	NULL
)


INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'RSA',
	'RSA.02',
	'Pendarahan',
	GETDATE(),
	'dk',
	NULL
)


INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'RSA',
	'RSA.03',
	'Muntahan',
	GETDATE(),
	'dk',
	NULL
)


INSERT INTO QuestionAnswerSelection
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionText,
	LastUpdateDateTime,
	LastUpdateByUserID
)
VALUES
(
	'BA',
	'Benda asing',
	GETDATE(),
	'dk'
)

INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'BA',
	'BA.01',
	'Tidak Ada',
	GETDATE(),
	'dk',
	NULL
)


INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'BA',
	'BA.02',
	'Ada',
	GETDATE(),
	'dk',
	NULL
)

INSERT INTO QuestionAnswerSelection
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionText,
	LastUpdateDateTime,
	LastUpdateByUserID
)
VALUES
(
	'WOB',
	'WOB',
	GETDATE(),
	'dk'
)



INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'WOB',
	'WOB.01',
	'Tidak ada',
	GETDATE(),
	'dk',
	NULL
)


INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'WOB',
	'WOB.02',
	'Retraksi ringan',
	GETDATE(),
	'dk',
	NULL
)


INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'WOB',
	'WOB.03',
	'Retraksi sedang',
	GETDATE(),
	'dk',
	NULL
)


INSERT INTO QuestionAnswerSelectionLine
(
	QuestionAnswerSelectionID,
	QuestionAnswerSelectionLineID,
	QuestionAnswerSelectionLineText,
	LastUpdateDateTime,
	LastUpdateByUserID,
	Score
)
VALUES
(
	'WOB',
	'WOB.04',
	'Retraksi berat',
	GETDATE(),
	'dk',
	NULL
)

INSERT INTO Question
(
	QuestionID,
	ParentQuestionID,
	IndexNo,
	QuestionLevel,
	QuestionText,
	QuestionShortText,
	SRAnswerType,
	AnswerDecimalDigit,
	AnswerPrefix,
	AnswerSuffix,
	IsActive,
	AnswerWidth,
	AnswerWidth2,
	QuestionAnswerSelectionID,
	QuestionAnswerDefaultSelectionID,
	QuestionAnswerSelectionID2,
	QuestionAnswerDefaultSelectionID2,
	Formula,
	IsAlwaysPrint,
	LastUpdateDateTime,
	LastUpdateByUserID,
	IsMandatory,
	ReferenceQuestionID,
	VitalSignID,
	BodyID,
	RelatedEntityName,
	RelatedColumnName,
	LookUpID,
	IsUpdateRelatedEntity,
	IsReadOnly,
	NursingDisplayAs,
	EquivalentQuestionID,
	IsEmptyDefault,
	IsNotOverWriteRelatedEntity
)
VALUES
(
	'PNP.007',
	'',
	'',
	'',
	'WOB',
	'WOB',
	'CBO',
	'0',
	'',
	'',
	'1',
	'',
	'',
	'WOB',
	'WOB.01',
	'',
	'',
	'',
	'1',
	GETDATE(),
	'dk',
	'0',
	'',
	'',
	'',
	'',
	'',
	'',
	'',
	'',
	'',
	'',
	'',
	''
)

INSERT INTO QuestionInGroup
(
	QuestionGroupID,
	QuestionID,
	RowIndex,
	LastUpdateDateTime,
	LastUpdateByUserID,
	PageNo,
	ParentQuestionID,
	QuestionLevel
)
VALUES
(
	'IGD.PS.PNP',
	'PNP.007',
	'5',
	GETDATE(),
	'dk',
	NULL,
	NULL,
	'1'
)