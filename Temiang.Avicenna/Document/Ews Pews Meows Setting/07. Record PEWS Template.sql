DECLARE @templateID INT;
SELECT TOP 1 @templateID = t.TemplateID
FROM   NursingDiagnosaTemplate t
WHERE  [TemplateName] = 'PEWS' AND IsActive = 1

IF (@templateID>0)
BEGIN
    DELETE 
    FROM   [NursingDiagnosaTemplateDetail]
    WHERE  TemplateID = @templateID
    
    UPDATE [NursingDiagnosaTemplate] SET [TemplateName]='PEWS', [TemplateText]='Pediatric Early Warning Score'
    WHERE  TemplateID = @templateID
END 
ELSE
BEGIN
    INSERT INTO [NursingDiagnosaTemplate]([TemplateName],[TemplateText],[IsActive],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime])
    VALUES(N'PEWS',N'Pediatric Early Warning Score',1,N'han',getdate(),N'han',getdate());

	SELECT @templateID=SCOPE_IDENTITY()
END

--INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
--VALUES(@templateID,N'WS.CARD',N'han',getdate(),N'han',getdate(),2);
--INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
--VALUES(@templateID,N'WS.GCON',N'han',getdate(),N'han',getdate(),1);
--INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
--VALUES(@templateID,N'WS.PRES',N'han',getdate(),N'han',getdate(),3);

-- Pakai Fromat PEWS lama (Bawaan RSBK)
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PEWS.001',N'nuno','2019-10-31 07:49:38.700',N'nuno','2019-10-31 07:49:38.700',1);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PEWS.002',N'nuno','2019-10-31 07:49:38.700',N'nuno','2019-10-31 07:49:38.700',2);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PEWS.003',N'nuno','2019-10-31 07:49:38.700',N'nuno','2019-10-31 07:49:38.700',3);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PEWS.004',N'nuno','2019-10-31 07:49:38.700',N'nuno','2019-10-31 07:49:38.700',4);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PEWS.005',N'nuno','2023-12-19 08:01:20.967',N'han','2023-12-19 08:01:20.967',5);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PEWS.006',N'nuno','2019-10-31 07:49:38.700',N'nuno','2019-10-31 07:49:38.700',6);


INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'PEWS.001','',NULL,1,N'KEADAAN UMUM','',N'CBO',NULL,'','',1,NULL,NULL,N'PEWSQ1','','','','',1,'2020-01-05 08:07:36.640',N'sci',NULL,'','','','','','',NULL,NULL,'','',NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'PEWS.002','',NULL,1,N'KARDIOVASKULAR','',N'CBO',NULL,'','',1,NULL,NULL,N'PEWSQ2','','','','',1,'2020-01-05 08:07:36.640',N'sci',NULL,'','','','','','',NULL,NULL,'','',NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'PEWS.003','',NULL,1,N'RESPIRASI','',N'CBO',NULL,'','',1,NULL,NULL,N'PEWSQ3','','','','',1,'2020-01-05 08:07:36.640',N'sci',NULL,'','','','','','',NULL,NULL,'','',NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'PEWS.004','',NULL,1,N'Total Score','',N'NUM',0,'','',1,NULL,NULL,'','','','',N'(parseInt([PEWS.001]) + parseInt([PEWS.002]) + parseInt([PEWS.003]))',NULL,NULL,'',NULL,'','','','','','',NULL,NULL,'','',NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'PEWS.005','',NULL,1,N'Frekuensi Monitoring','',N'TXT',NULL,'','',1,NULL,NULL,'','','','',N'((parseInt([PEWS.004]) >= 5) ? ''Setiap 30 menit'' : (parseInt([PEWS.004]) >=  4) ? ''Setiap 1 jam'' : (parseInt([PEWS.004]) >= 3) ? ''Setiap 2 jam'' : ''Setiap 4 jam'')',1,'2020-01-05 08:07:36.640',N'sci',0,'','','','','','',NULL,NULL,'','',NULL,NULL);
INSERT INTO [Question]([QuestionID],[ParentQuestionID],[IndexNo],[QuestionLevel],[QuestionText],[QuestionShortText],[SRAnswerType],[AnswerDecimalDigit],[AnswerPrefix],[AnswerSuffix],[IsActive],[AnswerWidth],[AnswerWidth2],[QuestionAnswerSelectionID],[QuestionAnswerDefaultSelectionID],[QuestionAnswerSelectionID2],[QuestionAnswerDefaultSelectionID2],[Formula],[IsAlwaysPrint],[LastUpdateDateTime],[LastUpdateByUserID],[IsMandatory],[ReferenceQuestionID],[VitalSignID],[BodyID],[RelatedEntityName],[RelatedColumnName],[LookUpID],[IsUpdateRelatedEntity],[IsReadOnly],[NursingDisplayAs],[EquivalentQuestionID],[IsEmptyDefault],[IsNotOverWriteRelatedEntity])
VALUES(N'PEWS.006','',NULL,1,N'Respon Klinis','',N'MEM',NULL,'','',1,NULL,NULL,'','','','',N'((parseInt([PEWS.004]) >= 5) ? ''Laporan perubahan klinis ke dokter jaga dan DPJP, Kolaborasi langkah selanjutnya dengan seluruh tim perawatan, Hubungi Tim Cod Blue dan DPJP, Kolaborasikan langkah selanjutnya dengan seluruh tim perawatan'' : (parseInt([PEWS.004]) >= 4) ? ''Laporkan dokter jaga dan lapor DPJP lakukan terapi sesuai advice, Kolaborasikan langkah selanjutnya dengan seluruh tim perawatan, Jika masih diperlukan lapor ulang ke DPJP atau hubungi Tim Code Blue bila pasien henti jantung atau henti napas, Dokumentasikan dan re-asses setelah intervensi'' : (parseInt([PEWS.004]) >= 3) ? ''Laporkan dokter Jaga adanya perubahan klinis, Kolaborasikan langkah selanjutnya dengan seluruh tim perawatan, Dokumentasikan dan tentukan waktu untuk penilaian ulang berikutnya'' : ''Lanjutkan pemantauan PEWS rutin'')',1,'2020-01-05 08:07:36.640',N'sci',0,'','','','','','',NULL,NULL,'','',NULL,NULL);


INSERT INTO [QuestionAnswerSelection]([QuestionAnswerSelectionID],[QuestionAnswerSelectionText],[LastUpdateDateTime],[LastUpdateByUserID])
VALUES(N'PEWSQ1',N'KEADAANUMUM' + CHAR(13) + CHAR(10),'2021-06-24 12:27:00.103',N'sci');
INSERT INTO [QuestionAnswerSelection]([QuestionAnswerSelectionID],[QuestionAnswerSelectionText],[LastUpdateDateTime],[LastUpdateByUserID])
VALUES(N'PEWSQ2',N'KARDIOVASKULAR' + CHAR(13) + CHAR(10),'2021-06-24 12:27:00.103',N'sci');
INSERT INTO [QuestionAnswerSelection]([QuestionAnswerSelectionID],[QuestionAnswerSelectionText],[LastUpdateDateTime],[LastUpdateByUserID])
VALUES(N'PEWSQ3',N'RESPIRASI','2021-06-24 12:27:00.103',N'sci');


INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'PEWSQ1',N'0',N'Interaksi biasa','2021-06-24 12:27:00.103',N'sci',N'0.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'PEWSQ1',N'1',N'Somnolen','2021-06-24 12:27:00.103',N'sci',N'1.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'PEWSQ1',N'2',N'Iritabel','2021-06-24 12:27:00.103',N'sci',N'2.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'PEWSQ1',N'3',N'Letargi, Gelisah, Penurunan respon terhadap nyeri','2021-06-24 12:27:00.103',N'sci',N'3.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'PEWSQ2',N'0',N'Tidak sianosi, ATAU CRT < 2 detik','2021-06-24 12:27:00.103',N'sci',N'0.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'PEWSQ2',N'1',N'Tampak pucat, ATAU CRT 2 detik','2021-06-24 12:27:00.103',N'sci',N'1.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'PEWSQ2',N'2',N'Tampak sianotik, ATAU CRT > 2 detik, ATAU takikardia > 20x diatas parameter nadi sesuai usia/menit','2021-06-24 12:27:00.103',N'sci',N'2.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'PEWSQ2',N'3',N'Sianotik atau motlet, ATAU CRT > 5 detik, ATAU takikardia > 30x diatas parameter nadi sesuai usia/menit, atau Bradikardia (sesuai usia)','2021-06-24 12:27:00.103',N'sci',N'3.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'PEWSQ3',N'0',N'Normal, tidak ada retraksi','2021-06-24 12:27:00.103',N'sci',N'0.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'PEWSQ3',N'01',N'Menggunakan otot bantu napas, ATAU menggunakan FiO2 lebih dari 30% (Setara dengan 3 liters/menit nasal kanul)','2021-06-24 12:27:00.103',N'sci',N'1.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'PEWSQ3',N'02',N'Ada retraksi, ATAU menggunakan FiO2 lebih dari 40% (setara dengan 6 liters/menit simple mask)','2021-06-24 12:27:00.103',N'sci',N'2.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'PEWSQ3',N'1',N'Takipneu > 10x diatas parameter pernapasan sesuai usia/menit','2021-06-24 12:27:00.103',N'sci',N'1.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'PEWSQ3',N'2',N'Takipneu > 20x diatas parameter RR sesuai usia/menit','2021-06-24 12:27:00.103',N'sci',N'2.00');
INSERT INTO [QuestionAnswerSelectionLine]([QuestionAnswerSelectionID],[QuestionAnswerSelectionLineID],[QuestionAnswerSelectionLineText],[LastUpdateDateTime],[LastUpdateByUserID],[Score])
VALUES(N'PEWSQ3',N'3',N'Takipneu >= 30x diatas parameter napas sesuai usia/menit dengan retraksi, ATAU merintih, ATAU menggunakan FiO2 lebih dari 50% (setara dengan 8 liters/menit simple mask)','2021-06-24 12:27:00.103',N'sci',N'3.00');
