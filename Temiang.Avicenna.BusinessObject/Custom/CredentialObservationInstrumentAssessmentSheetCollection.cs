using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CredentialObservationInstrumentAssessmentSheetCollection
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
                cqi.QuestionGroupName,
                cqi.QuestionLevel,
	            cqi.IsDetail,
                ISNULL(cps.IsEval1, 0) AS IsEval1,
                ISNULL(cps.IsEval2, 0) AS IsEval2,
                ISNULL(cps.IsEval3, 0) AS IsEval3,
                ISNULL(cps.IsEval4, 0) AS IsEval4,
                ISNULL(cps.IsEval5, 0) AS IsEval5,
                ISNULL(cps.Notes, '') AS Notes,
                cps.Score,
                CASE WHEN cqi.QuestionnaireItemParentID = -1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsHdDetail 
            FROM
	            CredentialObservationInstrumentQuestionnaireItem AS cqi
            INNER JOIN CredentialObservationInstrumentQuestionnaire AS cq ON cq.QuestionnaireID = cqi.QuestionnaireID
            LEFT JOIN (SELECT cps.QuestionnaireItemID, cps.IsEval1, cps.IsEval2, cps.IsEval3, cps.IsEval4, cps.IsEval5, cps.Notes, cps.Score  
                         FROM CredentialObservationInstrumentAssessmentSheet AS cps WHERE cps.TransactionNo = @p_TransactionNo) cps ON cps.QuestionnaireItemID = cqi.QuestionnaireItemID
            WHERE cq.QuestionnaireID = @p_QuestionnaireID 
                AND (cqi.QuestionnaireItemID IN (SELECT  DISTINCT x.QuestionnaireItemParentID FROM CredentialObservationInstrumentQuestionnaireItem x)
                    OR cqi.QuestionnaireItemParentID <> -1)
            ORDER BY cqi.QuestionCode";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
