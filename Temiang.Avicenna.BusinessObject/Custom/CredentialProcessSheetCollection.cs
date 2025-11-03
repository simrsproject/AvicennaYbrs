using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CredentialProcessSheetCollection
    {
        public DataTable GetJoin(string questionnaireId, string transactionNo)
        {
            esParameters par = new esParameters();
            par.Add("p_QuestionnaireID", questionnaireId);
            par.Add("p_TransactionNo", transactionNo);

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

        public DataTable GetJoinMedicWithDefaultValue(string questionnaireId, string transactionNo)
        {
            esParameters par = new esParameters();
            par.Add("p_QuestionnaireID", questionnaireId);
            par.Add("p_TransactionNo", transactionNo);

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
                ISNULL(cps.SRReview, ISNULL(cps.SRCurrentAbility, '')) AS SRReview, 
                ISNULL(cps.SRRecomendation, ISNULL(cps.SRReview, '')) AS SRRecomendation, 
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

        public DataTable GetJoinV2(string questionnaireId, string transactionNo)
        {
            esParameters par = new esParameters();
            par.Add("p_QuestionnaireID", questionnaireId);
            par.Add("p_TransactionNo", transactionNo);

            string commandText =
            @"SELECT
	            cqi.QuestionnaireItemID,
	            cqi.QuestionnaireID,
	            cqi.QuestionCode,
	            cqi.QuestionNo,
	            cqi.QuestionName,
	            cqi.SRCredentialQuestionLevel,
	            cqi.SRCredentialActionType,
	            ISNULL(act.ItemName, '') AS CredentialActionTypeName,
	            cqi.IsDetail,
	            ISNULL(cps.SRCurrentAbility, CASE WHEN cqi.IsDetail = 0 THEN '' ELSE (CASE WHEN cq.SRProfessionGroup = '01' THEN '11' WHEN cq.SRProfessionGroup = '02' THEN '21' ELSE '31' END) END)  AS SRCurrentAbility, 
                ISNULL(cps.SRRecomendation, CASE WHEN cqi.IsDetail = 0 THEN '' ELSE (CASE WHEN cq.SRProfessionGroup = '01' THEN '11' WHEN cq.SRProfessionGroup = '02' THEN '21' ELSE '31' END) END) AS SRRecomendation, 

                ISNULL(cur.Note, '') AS CurrentAbility, 
                ISNULL(rec.Note, '') AS Recomendation, 

                ISNULL(cps.IsRequested, 1) AS IsRequested,
                ISNULL(cps.IsReduced, 0) AS IsReduced,
                ISNULL(cps.Notes, '') AS Notes,
                CASE WHEN cqi.SRCredentialActionType = '01' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsIndependent,
                CASE WHEN cqi.SRCredentialActionType = '02' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsDelegation,
                CASE WHEN cqi.SRCredentialActionType = '03' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsMandate 
            FROM
	            CredentialQuestionnaireItem AS cqi
            INNER JOIN CredentialQuestionnaire AS cq ON cq.QuestionnaireID = cqi.QuestionnaireID
            LEFT JOIN (SELECT cps.QuestionnaireItemID, cps.SRCurrentAbility, cps.SRRecomendation, cps.IsRequested, cps.IsReduced, cps.Notes  
                         FROM CredentialProcessSheet AS cps WHERE cps.TransactionNo = @p_TransactionNo) cps ON cps.QuestionnaireItemID = cqi.QuestionnaireItemID
            LEFT JOIN (SELECT * FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'CredentialActionType') act ON act.ItemID = cqi.SRCredentialActionType
            LEFT JOIN (SELECT * FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'CredentialResultCurrentAbility') cur ON cur.ItemID = cps.SRCurrentAbility
            LEFT JOIN (SELECT * FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'CredentialResultRecomendation') rec ON rec.ItemID = cps.SRRecomendation
            WHERE cq.QuestionnaireID = @p_QuestionnaireID 
            ORDER BY cqi.QuestionCode";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
