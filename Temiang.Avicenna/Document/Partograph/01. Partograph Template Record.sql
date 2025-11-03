DECLARE @templateID INT;
SELECT TOP 1 @templateID = t.TemplateID
FROM   NursingDiagnosaTemplate t
WHERE  [TemplateName] = 'Partograph' AND IsActive = 1

IF (@templateID>0)
BEGIN
    DELETE 
    FROM   [NursingDiagnosaTemplateDetail]
    WHERE  TemplateID = @templateID
    
    UPDATE [NursingDiagnosaTemplate] SET [TemplateName]='Partograph', [TemplateText]='Partograph'
    WHERE  TemplateID = @templateID
END 
ELSE
BEGIN
	INSERT INTO [NursingDiagnosaTemplate]([TemplateName],[TemplateText],[IsActive],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime])
	VALUES(N'Partograph',N'Partograph',1,N'han',getdate(),N'han',getdate());

	SELECT @templateID=SCOPE_IDENTITY()
END


INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'A.KDV.ND',N'han',getdate(),N'han',NULL,10);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'A.KDV.1TDS',N'han',getdate(),N'han',NULL,11);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'A.KDV.2TDD',N'han',getdate(),N'han',NULL,12);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'A.KDV.TEM',N'han',getdate(),N'han',NULL,13);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PTG.KJ.DJJ',N'han',getdate(),N'han',NULL,1);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PTG.KJ.KTB',N'han',getdate(),N'han',NULL,2);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PTG.KJ.PNY',N'han',getdate(),N'han',NULL,3);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PTG.KP.JMK',N'han',getdate(),N'han',NULL,7);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PTG.KP.KUT',N'han',getdate(),N'han',NULL,6);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PTG.KP.PKP',N'han',getdate(),N'han',NULL,5);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PTG.KP.SVK',N'han',getdate(),N'han',NULL,4);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PTG.OB.OBC',N'han',getdate(),N'han',NULL,10);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PTG.OK.OKS',N'han',getdate(),N'han',NULL,8);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PTG.OK.TTS',N'han',getdate(),N'han',NULL,9);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PTG.U.AST',N'han',getdate(),N'han',NULL,15);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PTG.U.PTN',N'han',getdate(),N'han',NULL,14);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'PTG.U.VOL',N'han',getdate(),N'han',NULL,16);



