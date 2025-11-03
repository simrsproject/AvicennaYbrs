BEGIN TRANSACTION	
UPDATE a
SET    a.IsActive = 1
FROM   [Question] a
       INNER JOIN [NursingDiagnosaTemplateDetail] c
            ON  a.[QuestionID] = c.[QuestionID]
WHERE  c.[TemplateID] = (
           SELECT TOP 1 t.[TemplateID]
           FROM   NursingDiagnosaTemplate t
           WHERE  t.TemplateName = 'Partograph'
                  AND t.IsActive = 1
       )

IF (@@ROWCOUNT=17)
BEGIN
    COMMIT 
    PRINT 'COMMIT'
END
ELSE
BEGIN
    ROLLBACK 
    PRINT 'ROLLBACK'
END 
	 
go
UPDATE question SET SRAnswerType = 'RBT' WHERE QuestionID IN('PTG.KJ.KTB','PTG.KJ.PNY','PTG.KP.KUT')
