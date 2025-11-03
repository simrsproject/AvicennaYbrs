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

INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'WS.CARD',N'han',getdate(),N'han',getdate(),2);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'WS.GCON',N'han',getdate(),N'han',getdate(),1);
INSERT INTO [NursingDiagnosaTemplateDetail]([TemplateID],[QuestionID],[CreateByUserID],[CreateDateTime],[LastUpdateByUserID],[LastUpdateDateTime],[RowIndex])
VALUES(@templateID,N'WS.PRES',N'han',getdate(),N'han',getdate(),3);