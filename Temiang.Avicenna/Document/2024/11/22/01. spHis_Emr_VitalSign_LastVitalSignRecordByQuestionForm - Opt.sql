/***************************************************************************************************  
Procedure:          spHis_Emr_VitalSign_LastVitalSignRecordByQuestionForm  
Create Date:        2014-10-17  
Author:             Handono  
Description:        Record untuk status EWS pada layar EmrList  
Used By:            Temiang.Avicenna.BusinessObject.VitalSign.VitalSign_LastVitalSignRecordByQuestionForm   
Parameter(s):       @p_RegistrationNo - RegistrationNo  
     @p_FromRegistrationNo  
     @p_QuestionFormID  
  
Usage:              EXEC spHis_Emr_VitalSign_LastVitalSignRecordByQuestionForm   
                      @p_RegistrationNo = 'REG/OP/241015-0779',  
       @p_FromRegistrationNo = 'REG/IP/241015-0042',  
       @p_QuestionFormID = '1077'  
****************************************************************************************************  
SUMMARY OF CHANGES  
Date(yyyy-mm-dd)    Author              Comments  
------------------- ------------------- ------------------------------------------------------------  
  2024-11-22        Handono             Optimize join add -> INNER JOIN [NursingTransHD] nth ON 
										(nth.[RegistrationNo] = @p_RegistrationNo OR nth.[RegistrationNo] = @p_FromRegistrationNo)
***************************************************************************************************/  
  
ALTER PROCEDURE spHis_Emr_VitalSign_LastVitalSignRecordByQuestionForm (  
  @p_RegistrationNo varchar(20),   
  @p_FromRegistrationNo varchar(20),   
  @p_QuestionFormID varchar(20)  
) AS  
BEGIN  
SET   
  NOCOUNT ON;  
  
SELECT   
  q.[SRAnswerType],   
  v.[VitalSignID],   
  v.[VitalSignName],   
  phrl.[QuestionAnswerPrefix],   
  phrl.[QuestionAnswerSuffix],   
  phrl.[QuestionAnswerText],   
  phrl.[QuestionAnswerNum],   
  phrl.[QuestionAnswerText2],   
  phr.[RecordDate],   
  phr.[RecordTime],   
  v.[VitalSignInitial],   
  phrl.[QuestionID],   
  phrl.[QuestionAnswerSelectionLineID]   
FROM   
  [PatientHealthRecordLine] phrl   
  LEFT JOIN [Question] q ON  phrl.[QuestionID] = q.[QuestionID]   
  LEFT JOIN [VitalSign] v ON q.[VitalSignID] = v.[VitalSignID]   
  INNER JOIN [PatientHealthRecord] phr ON phrl.[TransactionNo] = phr.[TransactionNo]   
  
  -- Untuk ambil status delete yg ada di [NursingDiagnosaTransDT] dan ReferenceToPhrNo duplikat shg harus join ke [NursingTransHD]  
  INNER JOIN [NursingTransHD] nth ON (nth.[RegistrationNo] = @p_RegistrationNo OR nth.[RegistrationNo] = @p_FromRegistrationNo) 
		AND phr.[RegistrationNo] = nth.[RegistrationNo]   
  INNER JOIN [NursingDiagnosaTransDT] ndtd ON (  
    nth.[TransactionNo] = ndtd.[TransactionNo]   
    AND phr.[TransactionNo] = ndtd.[ReferenceToPhrNo]  
  )   
WHERE   
  (  
      phr.[RegistrationNo] = @p_RegistrationNo  
      OR phr.[RegistrationNo] = @p_FromRegistrationNo  
  )   
  AND phr.[QuestionFormID] = @p_QuestionFormID  
  AND (  
      ndtd.[IsDeleted] IS NULL   
      OR ndtd.[IsDeleted] = 0  
  )   
ORDER BY   
  v.[SRVitalSignGroup] ASC,   
  v.[RowIndexInGroup] ASC,   
  v.[VitalSignName] ASC,   
  phr.[RecordDate] DESC,   
  phr.[TransactionNo] DESC  
END