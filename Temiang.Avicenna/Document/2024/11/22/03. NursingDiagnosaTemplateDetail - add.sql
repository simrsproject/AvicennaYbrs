ALTER TABLE NursingDiagnosaTemplateDetail ADD TemplateIDStr AS (CONVERT([varchar](10),[TemplateID],0))
CREATE INDEX IX_NursingDiagnosaTemplateDetail_TemplateIDStr_QuestionID_RowIndex ON NursingDiagnosaTemplateDetail(TemplateIDStr, QuestionID,RowIndex)

