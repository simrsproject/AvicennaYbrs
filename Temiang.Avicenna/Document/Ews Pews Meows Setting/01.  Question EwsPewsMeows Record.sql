BEGIN TRANSACTION
DELETE FROM Question WHERE QuestionID LIKE 'WS.%'
IF (@@ROWCOUNT <20)
	COMMIT 
ELSE 
	ROLLBACK
	
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'WS.AVPU',NULL,NULL,1,N'AVPU',N'AVPU',N'RBT',0,NULL,NULL,1,NULL,NULL,N'WS.AVPU','',NULL,'',NULL,1,NULL,N'sci',0,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'WS.BPD',NULL,NULL,1,N'Tekanan darah Diastolik','',N'NUM',0,NULL,N'mmHg',1,NULL,NULL,NULL,'',NULL,'',NULL,1,NULL,N'sci',0,NULL,N'BP2',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'WS.BPS',NULL,NULL,1,N'Tekanan darah Sistolik','',N'NUM',0,NULL,N'mmHg',1,NULL,NULL,NULL,'',NULL,'',NULL,1,NULL,N'sci',0,NULL,N'BP1',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'WS.CARD',NULL,NULL,1,N'Kardiovaskular','',N'RBT',0,NULL,NULL,1,NULL,NULL,N'WS.CARD','',NULL,'',NULL,1,NULL,N'sci',0,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'WS.GCON',NULL,NULL,1,N'Keadaan Umum','',N'RBT',0,NULL,NULL,1,NULL,NULL,N'WS.GCON','',NULL,'',NULL,1,NULL,N'sci',0,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'WS.HR',NULL,NULL,1,N'Denyut Jantung','',N'NUM',0,NULL,N'x/mnt',1,NULL,NULL,NULL,'',NULL,'',NULL,1,NULL,N'sci',0,NULL,N'HR',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'WS.O2S',NULL,NULL,1,N'Penggunaan alat bantu 02','',N'RBT',0,NULL,NULL,1,NULL,NULL,N'WS.O2S','',NULL,'',NULL,1,NULL,N'sci',0,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'WS.OUT',NULL,NULL,1,N'Pengeluaran / Lochia','',N'RBT',0,NULL,NULL,1,NULL,NULL,N'WS.OUT','',NULL,'',NULL,1,NULL,N'sci',0,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'WS.PAIN',NULL,NULL,1,N'Nyeri','',N'RBT',0,NULL,NULL,1,NULL,NULL,N'WS.PAIN','',NULL,'',NULL,1,NULL,N'sci',0,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'WS.PRES',NULL,NULL,1,N'Respirasi','',N'RBT',0,NULL,NULL,1,NULL,NULL,N'WS.PRES','',NULL,'',NULL,1,NULL,N'sci',0,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'WS.PROT',NULL,NULL,1,N'Protelnuria','',N'RBT',0,NULL,NULL,1,NULL,NULL,N'WS.PROT','',NULL,'',NULL,1,NULL,N'sci',0,NULL,'',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'WS.RESP',NULL,NULL,1,N'Pernafasan','',N'NUM',0,NULL,N'x/mnt',1,NULL,NULL,NULL,'',NULL,'',NULL,1,NULL,N'sci',0,NULL,N'RESP',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'WS.SPO2',NULL,NULL,1,N'Warna Kulit (SpO2)','',N'NUM',0,NULL,N'%',1,NULL,NULL,NULL,'',NULL,'',NULL,1,NULL,N'han',0,NULL,N'SPO2',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'WS.TEMP',NULL,NULL,1,N'Suhu','',N'NUM',1,NULL,N'C',1,NULL,NULL,NULL,'',NULL,'',NULL,1,NULL,N'sci',0,NULL,N'TEMP',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);


INSERT INTO [QuestionAnswerSelection]([QuestionAnswerSelectionID],[QuestionAnswerSelectionText],[LastUpdateDateTime],[LastUpdateByUserID])
VALUES(N'WS.AVPU',N'AVPU',NULL,N'han');
INSERT INTO [QuestionAnswerSelection]([QuestionAnswerSelectionID],[QuestionAnswerSelectionText],[LastUpdateDateTime],[LastUpdateByUserID])
VALUES(N'WS.CARD',N'Kardiovaskular (PEWS)',NULL,N'han');
INSERT INTO [QuestionAnswerSelection]([QuestionAnswerSelectionID],[QuestionAnswerSelectionText],[LastUpdateDateTime],[LastUpdateByUserID])
VALUES(N'WS.GCON',N'Keadaan Umum (PEWS)',NULL,N'han');
INSERT INTO [QuestionAnswerSelection]([QuestionAnswerSelectionID],[QuestionAnswerSelectionText],[LastUpdateDateTime],[LastUpdateByUserID])
VALUES(N'WS.O2S',N'O2 Status',NULL,N'han');
INSERT INTO [QuestionAnswerSelection]([QuestionAnswerSelectionID],[QuestionAnswerSelectionText],[LastUpdateDateTime],[LastUpdateByUserID])
VALUES(N'WS.OUT',N'Pengeluaran / Lochia',NULL,N'han');
INSERT INTO [QuestionAnswerSelection]([QuestionAnswerSelectionID],[QuestionAnswerSelectionText],[LastUpdateDateTime],[LastUpdateByUserID])
VALUES(N'WS.PAIN',N'Nyeri',NULL,N'han');
INSERT INTO [QuestionAnswerSelection]([QuestionAnswerSelectionID],[QuestionAnswerSelectionText],[LastUpdateDateTime],[LastUpdateByUserID])
VALUES(N'WS.PRES',N'Respirasi (PEWS)',NULL,N'han');
INSERT INTO [QuestionAnswerSelection]([QuestionAnswerSelectionID],[QuestionAnswerSelectionText],[LastUpdateDateTime],[LastUpdateByUserID])
VALUES(N'WS.PROT',N'Protelnuria',NULL,N'han');


INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.AVPU',N'WS.AVPU.01',N'Alert/Awake (Sadar)',NULL,N'han',N'1.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.AVPU',N'WS.AVPU.02',N'Verbal Stimulation',NULL,N'han',N'3.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.AVPU',N'WS.AVPU.03',N'Pain Stimulation',NULL,N'han',N'3.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.AVPU',N'WS.AVPU.04',N'Unresponsive',NULL,N'han',N'3.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.CARD',N'WS.CARD.01',N'Tidak Sianosi, ATAU CRT <2 detik',NULL,'',N'0.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.CARD',N'WS.CARD.02',N'Tampak pucat, ATAU CRT 2 detik',NULL,'',N'1.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.CARD',N'WS.CARD.03',N'Tampak sianotik, ATAU CRT >2detik, ATAU takikardia >20x diatas parameter nadi sesuai usia/menit',NULL,'',N'2.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.CARD',N'WS.CARD.04',N'Sianotik atau motfet. ATAU CRT >5 detik, ATAU takikardia >30x diatas parameter nadi sesuai usia/menit, atau Bradikardia (sesuai usia)',NULL,'',N'3.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.GCON',N'WS.GCON.01',N'Interaksi Biasa',NULL,'',N'0.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.GCON',N'WS.GCON.02',N'Somnolen',NULL,'',N'1.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.GCON',N'WS.GCON.03',N'Iritabel',NULL,'',N'2.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.GCON',N'WS.GCON.04',N'Letargi, gelisah, penurunan respon terhadap nyeri',NULL,'',N'3.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.O2S',N'WS.O2S.01',N'Tidak Terpasang O2',NULL,'',N'0.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.O2S',N'WS.O2S.02',N'Terpasang O2',NULL,'',N'2.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.OUT',N'WS.OUT.01',N'Abnormal',NULL,'',N'3.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.OUT',N'WS.OUT.02',N'Normal',NULL,'',N'0.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.PAIN',N'WS.PAIN.01',N'Abnormal',NULL,'',N'3.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.PAIN',N'WS.PAIN.02',N'Normal',NULL,'',N'0.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.PRES',N'WS.PRES.01',N'Normal, tidak ada retraksi',NULL,'',N'0.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.PRES',N'WS.PRES.02',N'Takipneu >10x diatas parameter pernapasan sesuai usia /menit ATAU',NULL,'',N'1.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.PRES',N'WS.PRES.03',N'Menggunakan otot bantu napas, ATAU menggunakan FiO2 lebih dari 30% (setara dengan 3 liters/menit nasal kanuf)',NULL,'',N'1.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.PRES',N'WS.PRES.04',N'Takipneu >20x diatas parameter RR sesuai usia/menit, ATAU',NULL,'',N'2.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.PRES',N'WS.PRES.05',N'Ada retraksi, ATAU menggunakan FiO2 lebih dari 40% (setara dengan 6 liters/menit simple mask)',NULL,'',N'2.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.PRES',N'WS.PRES.06',N'Takipneu >=30x diatas parameter napas sesuai usia/menit dengan retraksi, ATAU merintih, ATAU menggunakan FiO2 lebih dari 50% (setara dengan 8 liters/menit simple mask)',NULL,'',N'3.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.PROT',N'WS.PROT.01',N'>= ++',NULL,'',N'3.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'WS.PROT',N'WS.PROT.02',N'+',NULL,'',N'2.00');
