using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class MedicalRecordFileCompletenessItemCollection
    {
        public DataTable GetJoin(string departmentID, string filesAnalysis)
        {
            esParameters par = new esParameters();
            par.Add("p_DepartmentID", departmentID);
            par.Add("p_FilesAnalysis", filesAnalysis);

            string commandText =
            @"SELECT
	            cqi.QuestionnaireItemID,
	            cqi.QuestionnaireID,
	            cqi.QuestionCode,
	            cqi.QuestionNo,
	            cqi.QuestionName,
	            cqi.SRCredentialQuestionLevel,
	            cqi.SRCredentialActionType,
	            act.ItemName AS CredentialActionTypeName,
	            cqi.IsDetail,
	            ISNULL(cps.SRCurrentAbility, '') AS SRCurrentAbility, 
                ISNULL(cps.SelfAssessmentNotes, '') AS SelfAssessmentNotes, 
                ISNULL(cps.SRCurrentAbilitySupervisor, '') AS SRCurrentAbilitySupervisor, 
                ISNULL(cps.SRCurrentAbilitySupervisor2, '') AS SRCurrentAbilitySupervisor2, 
                ISNULL(cps.SRReview, '') AS SRReview, 
                ISNULL(cps.SRRecomendation, '') AS SRRecomendation, 
                ISNULL(cps.SRConclusion, '') AS SRConclusion, 

                ISNULL(cur.Note, '') AS CurrentAbility, 
                ISNULL(cur.ItemName, '') AS CurrentAbilityName, 
                ISNULL(cur2.Note, '') AS CurrentAbilitySupervisor, 
                ISNULL(cur3.Note, '') AS CurrentAbilitySupervisor2, 
                ISNULL(rev.Note, '') AS Review, 
                ISNULL(rev.ItemName, '') AS ReviewName, 
                ISNULL(rec.Note, '') AS Recomendation, 
                ISNULL(rec.ItemName, '') AS RecomendationName, 
                ISNULL(con.ItemName, '') AS Conclusion, 

                ISNULL(cps.Notes, '') AS Notes,
                CASE WHEN cqi.SRCredentialActionType = '01' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsIndependent,
                CASE WHEN cqi.SRCredentialActionType = '02' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsDelegation,
                CASE WHEN cqi.SRCredentialActionType = '03' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsMandate 
            FROM
	            CredentialQuestionnaireItem AS cqi
            INNER JOIN CredentialQuestionnaire AS cq ON cq.QuestionnaireID = cqi.QuestionnaireID
            LEFT JOIN (SELECT cps.QuestionnaireItemID, cps.SRCurrentAbility, cps.SelfAssessmentNotes, cps.SRCurrentAbilitySupervisor, cps.SRCurrentAbilitySupervisor2, cps.SRReview, cps.SRRecomendation, cps.SRConclusion, cps.Notes  
                         FROM CredentialProcessSheet AS cps WHERE cps.TransactionNo = @p_TransactionNo) cps ON cps.QuestionnaireItemID = cqi.QuestionnaireItemID
            LEFT JOIN (SELECT * FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'CredentialActionType') act ON act.ItemID = cqi.SRCredentialActionType
            LEFT JOIN (SELECT * FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'CredentialResultCurrentAbility') cur ON cur.ItemID = cps.SRCurrentAbility
            LEFT JOIN (SELECT * FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'CredentialResultCurrentAbility') cur2 ON cur2.ItemID = cps.SRCurrentAbilitySupervisor
            LEFT JOIN (SELECT * FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'CredentialResultCurrentAbility') cur3 ON cur3.ItemID = cps.SRCurrentAbilitySupervisor2
            LEFT JOIN (SELECT * FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'CredentialResultReview') rev ON rev.ItemID = cps.SRReview
            LEFT JOIN (SELECT * FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'CredentialResultRecomendation') rec ON rec.ItemID = cps.SRRecomendation
            LEFT JOIN (SELECT * FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'CredentialResultConclusion') con ON con.ItemID = cps.SRConclusion
            WHERE cq.QuestionnaireID = @p_QuestionnaireID 
            ORDER BY cqi.QuestionCode";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetFullJoinWDocument(string departmentID, string filesAnalysis)
        {
            esParameters par = new esParameters();
            par.Add("p_DepartmentID", departmentID);
            par.Add("p_FilesAnalysis", filesAnalysis);

            string commandText =
                @"SELECT B.DocumentFilesID, C.DocumentName, C.DocumentNumber, CONVERT(BIT,0) AS IsComplete, CONVERT(BIT,0) AS IsNotApplicable, '' AS Notes, C.QuestionFormID, C.ProgramID, C.SRDocumentFileType, C.SRAssessmentType, C.SRHaisMonitoring
                FROM DocumentDefinition A
                INNER JOIN DocumentDefinitionItem B ON A.DocumentDefinitionID = B.DocumentDefinitionID
                INNER JOIN DocumentFiles C ON B.DocumentFilesID = C.DocumentFilesID
                WHERE A.DepartmentID = @p_DepartmentID AND A.SRFilesAnalysis = @p_FilesAnalysis 
                ORDER BY C.DocumentNumber";
            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetInnerJoinWDocument(string registrationNo)
        {
            esParameters par = new esParameters();
            par.Add("p_RegistrationNo", registrationNo);

            string commandText =
                @"SELECT A.RegistrationNo, B.DocumentFilesID, C.DocumentName, C.DocumentNumber,
                 ISNULL(B.IsComplete,0) AS IsComplete, ISNULL(B.IsNotApplicable,0) AS IsNotApplicable, B.Notes, C.QuestionFormID, C.ProgramID, C.SRDocumentFileType, C.SRAssessmentType, C.SRHaisMonitoring
                 FROM MedicalRecordFileCompleteness A
                 INNER JOIN MedicalRecordFileCompletenessItem B ON A.RegistrationNo = B.RegistrationNo AND A.RegistrationNo = @p_RegistrationNo
                 INNER JOIN DocumentFiles C ON B.DocumentFilesID = C.DocumentFilesID
                 ORDER BY C.DocumentNumber";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
