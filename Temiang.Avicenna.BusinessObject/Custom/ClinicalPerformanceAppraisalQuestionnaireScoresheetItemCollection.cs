using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ClinicalPerformanceAppraisalQuestionnaireScoresheetItemCollection
    {
        public DataTable GetJoin(string scoresheetNo, string questionnaireId)
        {
            esParameters par = new esParameters();
            par.Add("p_ScoresheetNo", scoresheetNo);
            par.Add("p_QuestionnaireID", questionnaireId);

            string commandText;
            commandText =
                    @"SELECT a.QuestionnaireID, a.QuestionnaireItemID, a.QuestionCode, a.QuestionNo, a.QuestionName, a.QuestionGroupName, 
	                    ISNULL(c.Score, 0) AS Score, a.LoadScore, (ISNULL(c.Score, 0) * a.LoadScore) AS TotalScore, 
	                    CASE WHEN a.IsDetail = 0 THEN 0 ELSE b.MinValue END AS MinValue, CASE WHEN a.IsDetail = 0 THEN 0 ELSE b.MaxValue END AS MaxValue, a.IsDetail 
                    FROM ClinicalPerformanceAppraisalQuestionnaireItem AS a 
                    INNER JOIN ClinicalPerformanceAppraisalQuestionnaire AS b ON b.QuestionnaireID = a.QuestionnaireID 
                    LEFT JOIN (
	                    SELECT ab.QuestionnaireID, aa.ScoresheetNo, aa.QuestionnaireItemID, aa.Score 
	                    FROM ClinicalPerformanceAppraisalQuestionnaireScoresheetItem AS aa 
	                    INNER JOIN ClinicalPerformanceAppraisalQuestionnaireScoresheet AS ab ON ab.ScoresheetNo = aa.ScoresheetNo 
	                    WHERE aa.ScoresheetNo = @p_ScoresheetNo 
                    ) c ON c.QuestionnaireID = a.QuestionnaireID AND c.QuestionnaireItemID = a.QuestionnaireItemID 
                    WHERE a.QuestionnaireID = @p_QuestionnaireID";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetTotalScore(string scoresheetNo)
        {
            esParameters par = new esParameters();
            par.Add("p_ScoresheetNo", scoresheetNo);

            string commandText =
                @"SELECT a.ScoresheetNo, SUM(ISNULL(a.Score, 0) * b.LoadScore) AS TotalScore 
                FROM ClinicalPerformanceAppraisalQuestionnaireScoresheetItem AS a 
                INNER JOIN ClinicalPerformanceAppraisalQuestionnaireItem b ON b.QuestionnaireItemID = a.QuestionnaireItemID 
                WHERE a.ScoresheetNo = @p_ScoresheetNo 
                GROUP BY a.ScoresheetNo";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
