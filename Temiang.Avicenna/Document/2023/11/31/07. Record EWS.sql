DECLARE @templateID INT;
SELECT TOP 1 @templateID = t.TemplateID
FROM   NursingDiagnosaTemplate t
WHERE  [TemplateName] = 'EWS' AND IsActive = 1

IF (@templateID>0)
BEGIN
    DELETE 
    FROM   [NursingDiagnosaTemplateDetail]
    WHERE  TemplateID = @templateID
    
    UPDATE [NursingDiagnosaTemplate] SET [TemplateName]='EWS', [TemplateText]='Early Warning Score'
    WHERE  TemplateID = @templateID
END 
ELSE
BEGIN
	INSERT INTO [NursingDiagnosaTemplate]([TemplateName],[TemplateText],[IsActive],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime])
	VALUES(N'EWS',N'Early Warning Score',1,N'han',getdate(),N'han',getdate());

	SELECT @templateID=SCOPE_IDENTITY()
END

INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'WS.AVPU',N'han',GETDATE(),N'han',GETDATE(),11);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'WS.BPS',N'han',GETDATE(),N'han',GETDATE(),6);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'WS.HR',N'han',GETDATE(),N'han',GETDATE(),5);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'WS.O2S',N'han',GETDATE(),N'han',GETDATE(),3);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'WS.RESP',N'han',GETDATE(),N'han',GETDATE(),1);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'WS.SPO2',N'han',GETDATE(),N'han',GETDATE(),2);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'WS.TEMP',N'han',GETDATE(),N'han',GETDATE(),4);
