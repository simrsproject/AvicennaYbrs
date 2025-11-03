using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppraisalScoresheetItemCollection
    {
        public DataTable GetJoin(string scoresheetId, string participantItemId, string evaluatorId, string versionNo)
        {
            esParameters par = new esParameters();
            par.Add("p_ScoresheetID", scoresheetId);
            par.Add("p_ParticipantItemID", participantItemId);
            par.Add("p_EvaluatorID", evaluatorId);

            string commandText;
            if (versionNo == "3")
            {
                commandText =
                    @"SELECT DISTINCT apq.ParticipantItemID, ape.EvaluatorID, aq.QuestionerID, aq.QuestionerNo, aq.QuestionerName AS QuestionerName, 
	                    aqi.QuestionerItemID, aqi.QuestionCode, aqi.QuestionGroupName, aqi.QuestionName, aqi.Notes AS QuestionNotes, ISNULL(ss.ScoresheetItemID, 0) AS ScoresheetItemID, ISNULL(ss.Notes, '') AS Notes, 
                        ISNULL(ss.Score, 0) AS Score, CASE WHEN (aqi.Rating = 0 AND aqi.MaxValue = 0) THEN '' ELSE Format(ISNULL(ss.Score, 0),'N','en-US' ) END ScoreX, 
                        ISNULL(ss.TotalScore, 0) AS TotalScore, CASE WHEN (aqi.Rating = 0 AND aqi.MaxValue = 0) THEN '' ELSE Format(ISNULL(ss.TotalScore, 0),'N','en-US' ) END TotalScoreX,
                        ISNULL(aqr.RatingName, '') AS RatingX, ISNULL(ss.RatingID, -1) AS RatingID, 
                        aqi.MinValue, aqi.MaxValue, CASE WHEN (aqi.Rating = 0 AND aqi.MaxValue = 0) THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsScoringEnabled' 
                    FROM AppraisalParticipantQuestioner AS apq
                    INNER JOIN AppraisalQuestionItem AS aqi ON aqi.QuestionerID = apq.QuestionerID 
                    INNER JOIN AppraisalQuestion AS aq ON aq.QuestionerID = aqi.QuestionerID
                    INNER JOIN AppraisalParticipantItem AS api ON api.ParticipantItemID = apq.ParticipantItemID
                    INNER JOIN AppraisalParticipantEvaluator AS ape ON ape.ParticipantItemID = api.ParticipantItemID
                    LEFT JOIN (
	                    SELECT as1.ParticipantItemID, as1.EvaluatorID, asi.QuestionerItemID, asi.ScoresheetItemID, asi.Notes, asi.Score, asi.TotalScore, ISNULL(asi.RatingID, -1) AS RatingID
	                    FROM AppraisalScoresheet AS as1
	                    INNER JOIN AppraisalScoresheetItem AS asi ON asi.ScoresheetID = as1.ScoresheetID
	                    WHERE as1.ScoresheetID = @p_ScoresheetID
                    ) ss ON ss.ParticipantItemID = ape.ParticipantItemID AND ss.EvaluatorID = ape.EvaluatorID AND ss.QuestionerItemID = aqi.QuestionerItemID
                    LEFT JOIN AppraisalQuestionRating AS aqr on aqr.RatingID = ss.RatingID
                    WHERE (apq.EvaluatorID = -1 OR apq.EvaluatorID = @p_EvaluatorID) AND ape.ParticipantItemID = @p_ParticipantItemID AND ape.EvaluatorID = @p_EvaluatorID";
            }
            else
            {
                commandText =
                    @"SELECT DISTINCT apq.ParticipantItemID, ape.EvaluatorID, aq.QuestionerID, aq.QuestionerNo, CASE WHEN aq.Notes = '' THEN aq.QuestionerName ELSE aq.QuestionerName + ' - ' + aq.Notes END AS QuestionerName, 
	                    aqi.QuestionerItemID, aqi.QuestionCode, aqi.QuestionGroupName, aqi.QuestionName, aqi.Notes AS QuestionNotes, ISNULL(ss.ScoresheetItemID, 0) AS ScoresheetItemID, ISNULL(ss.Notes, '') AS Notes, 
                        ISNULL(ss.Score, 0) AS Score, ISNULL(ss.TotalScore, 0) AS TotalScore, aqi.MinValue, aqi.MaxValue, CASE WHEN (aqi.Rating = 0 AND aqi.MaxValue = 0) THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsScoringEnabled' 
                    FROM AppraisalParticipantQuestioner AS apq
                    INNER JOIN AppraisalQuestionItem AS aqi ON aqi.QuestionerID = apq.QuestionerID 
                    INNER JOIN AppraisalQuestion AS aq ON aq.QuestionerID = aqi.QuestionerID
                    INNER JOIN AppraisalParticipantItem AS api ON api.ParticipantItemID = apq.ParticipantItemID
                    INNER JOIN AppraisalParticipantEvaluator AS ape ON ape.ParticipantItemID = api.ParticipantItemID
                    LEFT JOIN (
	                    SELECT as1.ParticipantItemID, as1.EvaluatorID, asi.QuestionerItemID, asi.ScoresheetItemID, asi.Notes, asi.Score, asi.TotalScore
	                    FROM AppraisalScoresheet AS as1
	                    INNER JOIN AppraisalScoresheetItem AS asi ON asi.ScoresheetID = as1.ScoresheetID
	                    WHERE as1.ScoresheetID = @p_ScoresheetID
                    ) ss ON ss.ParticipantItemID = ape.ParticipantItemID AND ss.EvaluatorID = ape.EvaluatorID AND ss.QuestionerItemID = aqi.QuestionerItemID
                    WHERE (apq.EvaluatorID = -1 OR apq.EvaluatorID = @p_EvaluatorID) AND (aqi.Rating > 0 OR aqi.MaxValue > 0) AND ape.ParticipantItemID = @p_ParticipantItemID AND ape.EvaluatorID = @p_EvaluatorID";
            }

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetReferenceJoin(string scoresheetId, string referenceId)
        {
            esParameters par = new esParameters();
            par.Add("p_ScoresheetID", scoresheetId);
            par.Add("p_ReferenceID", referenceId);

            string commandText =
                @"SELECT as1.ParticipantItemID, as1.EvaluatorID, aq.QuestionerID, aq.QuestionerNo, CASE WHEN aq.Notes = '' THEN aq.QuestionerName ELSE aq.QuestionerName + ' - ' + aq.Notes END AS QuestionerName, 
                    asi.QuestionerItemID, aqi.QuestionCode, aqi.QuestionName, aqi.Notes AS 'QuestionNotes', asi.Notes, asi.Score, ISNULL(scr.ScoresheetItemID, 0) AS ScoresheetItemID, ISNULL(scr.Notes, '') AS InterventionNotes, 
                    ISNULL(scr.Score, 0) AS InterventionScore, ISNULL(scr.TotalScore, 0) AS TotalScore, aqi.MinValue, aqi.MaxValue  
                FROM AppraisalScoresheet as1 
                INNER JOIN AppraisalScoresheetItem AS asi ON asi.ScoresheetID = as1.ScoresheetID
                INNER JOIN AppraisalQuestionItem AS aqi ON aqi.QuestionerItemID = asi.QuestionerItemID
                INNER JOIN AppraisalQuestion AS aq ON aq.QuestionerID = aqi.QuestionerID
                LEFT JOIN (SELECT as2.ScoresheetID, as2.ReferenceID, asi2.ScoresheetItemID, asi2.QuestionerItemID, asi2.Notes, asi2.Score, asi2.TotalScore
                           FROM AppraisalScoresheet AS as2
                           INNER JOIN AppraisalScoresheetItem AS asi2 ON asi2.ScoresheetID = as2.ScoresheetID
                           WHERE as2.ScoresheetID = @p_ScoresheetID) AS scr ON scr.ReferenceID = as1.ScoresheetID AND scr.QuestionerItemID = asi.QuestionerItemID
                WHERE as1.ScoresheetID = @p_ReferenceID";
            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetQuestionRating(string scoresheetId, string participantItemId, string evaluatorId)
        {
            esParameters par = new esParameters();
            par.Add("p_ScoresheetID", scoresheetId);
            par.Add("p_ParticipantItemID", participantItemId);
            par.Add("p_EvaluatorID", evaluatorId);

            string commandText = @"SELECT DISTINCT aq.QuestionerID, aq.QuestionerNo, aq.QuestionerName, aqr.RatingID, aqr.RatingCode, aqr.RatingName, aqr.RatingValue, aqr.RatingValueMin, aqr.RatingValueMax
                    FROM AppraisalParticipantQuestioner AS apq
                    INNER JOIN AppraisalQuestionItem AS aqi ON aqi.QuestionerID = apq.QuestionerID 
                    INNER JOIN AppraisalQuestion AS aq ON aq.QuestionerID = aqi.QuestionerID
                    INNER JOIN AppraisalParticipantItem AS api ON api.ParticipantItemID = apq.ParticipantItemID
                    INNER JOIN AppraisalParticipantEvaluator AS ape ON ape.ParticipantItemID = api.ParticipantItemID
                    INNER JOIN AppraisalQuestionRating AS aqr ON aqr.QuestionerID = aq.QuestionerID 
                    WHERE (apq.EvaluatorID = -1 OR apq.EvaluatorID = @p_EvaluatorID) AND ape.ParticipantItemID = @p_ParticipantItemID AND ape.EvaluatorID = @p_EvaluatorID";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
