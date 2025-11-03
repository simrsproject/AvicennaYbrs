using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppraisalParticipantItemCollection
    {
        public DataTable GetAccumulationScores(string participantItemId)
        {
            esParameters par = new esParameters();
            par.Add("p_ParticipantItemID", participantItemId);

            string commandText =
                @"SELECT DISTINCT aqi.QuestionerID, aq.QuestionerNo, aq.QuestionerName, aqi.QuestionerItemID, aqi.QuestionCode, aqi.QuestionGroupName, aqi.QuestionName, 
                    CASE WHEN (aqi.Rating = 0 AND aqi.MaxValue = 0) THEN '0' ELSE '1' END IsScoringEnabled, 
	                CASE WHEN (aqi.Rating = 0 AND aqi.MaxValue = 0) THEN '' ELSE Format(ISNULL(asa.SupervisorScore, 0),'N','en-US' ) END SupervisorScoreX, 
	                CASE WHEN (aqi.Rating = 0 AND aqi.MaxValue = 0) THEN '' ELSE Format(ISNULL(asa.PartnerScore, 0),'N','en-US' ) END PartnerScoreX, 
	                CASE WHEN (aqi.Rating = 0 AND aqi.MaxValue = 0) THEN '' ELSE Format(ISNULL(asa.SubordinateScore, 0),'N','en-US' ) END SubordinateScoreX, 
	                CASE WHEN (aqi.Rating = 0 AND aqi.MaxValue = 0) THEN '' ELSE Format(ISNULL(asa.SelfScore, 0),'N','en-US' ) END SelfScoreX, 
	                CASE WHEN (aqi.Rating = 0 AND aqi.MaxValue = 0) THEN '' ELSE Format(ISNULL(asa.SupervisorScore, 0)+ISNULL(asa.PartnerScore, 0)+ISNULL(asa.SubordinateScore, 0)+ISNULL(asa.SelfScore, 0),'N','en-US' ) END TotalScoreX, 
	                CASE WHEN (aqi.Rating = 0 AND aqi.MaxValue = 0) THEN '' ELSE Format(ISNULL(asa.AverageScore, 0),'N','en-US' ) END AverageScoreX,
	                ISNULL(asa.SupervisorScore, 0) AS SupervisorScore, 
	                ISNULL(asa.PartnerScore, 0) AS PartnerScore, 
	                ISNULL(asa.SubordinateScore, 0) AS SubordinateScore, 
	                ISNULL(asa.SelfScore, 0) AS SelfScore, 
	                ISNULL(asa.SupervisorScore, 0)+ISNULL(asa.PartnerScore, 0)+ISNULL(asa.SubordinateScore, 0)+ISNULL(asa.SelfScore, 0) AS TotalScore, 
	                ISNULL(asa.AverageScore, 0) AS AverageScore
                FROM AppraisalParticipantQuestioner AS apq
                INNER JOIN AppraisalQuestionItem AS aqi ON aqi.QuestionerID = apq.QuestionerID 
                INNER JOIN AppraisalQuestion AS aq ON aq.QuestionerID = aqi.QuestionerID
                LEFT JOIN (SELECT x.ParticipantItemID, x.QuestionerItemID, 
	                CASE WHEN x.SupervisorScoreIntervention > 0 THEN x.SupervisorScoreIntervention ELSE x.SupervisorScore END AS SupervisorScore, 
	                CASE WHEN x.PartnerScoreIntervention > 0 THEN x.PartnerScoreIntervention ELSE x.PartnerScore END AS PartnerScore, 
	                CASE WHEN x.SubordinateScoreIntervention > 0 THEN x.SubordinateScoreIntervention ELSE x.SubordinateScore END AS SubordinateScore, 
	                CASE WHEN x.SelfScoreIntervention > 0 THEN x.SelfScoreIntervention ELSE x.SelfScore END AS SelfScore, 
	                x.AverageScore
                    FROM AppraisalScoringAccumulation x) AS asa ON asa.ParticipantItemID = apq.ParticipantItemID AND asa.QuestionerItemID = aqi.QuestionerItemID
                WHERE apq.ParticipantItemID = @p_ParticipantItemID 
                ORDER BY aq.QuestionerNo, aqi.QuestionCode";
            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetQuestionerEvalCount(string participantItemId, string questionerId)
        {
            esParameters par = new esParameters();
            par.Add("p_ParticipantItemID", participantItemId);
            par.Add("p_QuestionerID", questionerId);

            string commandText =
                @"SELECT x.ParticipantItemID, x.QuestionerID, SUM(x.EvalCount) AS EvalCount 
                FROM 
                (
	                SELECT DISTINCT apq.ParticipantItemID, apq.QuestionerID, 0 AS EvalCount 
	                FROM AppraisalParticipantQuestioner AS apq WITH(NOLOCK)
	                WHERE apq.ParticipantItemID = @p_ParticipantItemID

	                UNION ALL

	                SELECT ape.ParticipantItemID, apq.QuestionerID, 1 AS EvalCount 
	                FROM AppraisalParticipantEvaluator AS ape WITH(NOLOCK)
	                INNER JOIN AppraisalParticipantQuestioner AS apq WITH(NOLOCK) ON apq.ParticipantItemID = ape.ParticipantItemID AND apq.EvaluatorID = ape.EvaluatorID
	                WHERE ape.ParticipantItemID = @p_ParticipantItemID

	                UNION ALL

	                SELECT ape.ParticipantItemID, apq.QuestionerID, 1 AS EvalCount 
	                FROM AppraisalParticipantEvaluator AS ape WITH(NOLOCK)
	                INNER JOIN AppraisalParticipantQuestioner AS apq WITH(NOLOCK) ON apq.ParticipantItemID = ape.ParticipantItemID AND apq.EvaluatorID = -1
	                WHERE ape.ParticipantItemID = @p_ParticipantItemID
                ) x
                GROUP BY x.ParticipantItemID, x.QuestionerID
                HAVING x.QuestionerID = @p_QuestionerID";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetAccumulationRecap(string participantItemId)
        {
            esParameters par = new esParameters();
            par.Add("p_ParticipantItemID", participantItemId);

            string commandText =
                @"SELECT DISTINCT aqi.QuestionerID, aq.QuestionerNo, aq.QuestionerName, 
	                ISNULL(asa.AverageScore, 0) AS AverageScore, ISNULL(asa.NumberOfDividers, 1) AS NumberOfDividers, ISNULL(asa.AverageScore, 0)/ISNULL(asa.NumberOfDividers, 1) AS Score, 
	                aq.LoadScore, ISNULL(asa.AverageScore, 0)/ISNULL(asa.NumberOfDividers, 1) * aq.LoadScore / 100 AS TotalScore 
                FROM AppraisalParticipantQuestioner AS apq
                INNER JOIN AppraisalQuestionItem AS aqi ON aqi.QuestionerID = apq.QuestionerID 
                INNER JOIN AppraisalQuestion AS aq ON aq.QuestionerID = aqi.QuestionerID
                LEFT JOIN (
	                SELECT x.ParticipantItemID, y.QuestionerID, SUM(x.AverageScore) AS AverageScore , COUNT(x.QuestionerItemID) AS NumberOfDividers
	                FROM AppraisalScoringAccumulation x
	                INNER JOIN AppraisalQuestionItem AS y ON y.QuestionerItemID = x.QuestionerItemID
	                GROUP BY x.ParticipantItemID, y.QuestionerID
                ) AS asa ON asa.ParticipantItemID = apq.ParticipantItemID AND asa.QuestionerID = aqi.QuestionerID
                WHERE apq.ParticipantItemID = @p_ParticipantItemID AND (aqi.Rating > 0 OR aqi.MaxValue > 0)
                ORDER BY aq.QuestionerNo";
            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetScoringRecapitulations(string participantItemId)
        {
            esParameters par = new esParameters();
            par.Add("p_ParticipantItemID", participantItemId);

            string commandText =
                @"SELECT DISTINCT apq.QuestionerID, aq.QuestionerNo, aq.QuestionerName, ISNULL(asr.Capacity, '') AS Capacity, ISNULL(asr.NeedsToBeDeveloped, '') AS NeedsToBeDeveloped
                FROM AppraisalParticipantQuestioner AS apq
                INNER JOIN AppraisalQuestionItem AS aqi ON aqi.QuestionerID = apq.QuestionerID 
                INNER JOIN AppraisalQuestion AS aq ON aq.QuestionerID = aqi.QuestionerID
                LEFT JOIN AppraisalScoringRecapitulation AS asr ON asr.QuestionerID = apq.QuestionerID AND asr.ParticipantItemID = apq.ParticipantItemID
                WHERE apq.ParticipantItemID = @p_ParticipantItemID AND (aqi.Rating > 0 OR aqi.MaxValue > 0)
                ORDER BY aq.QuestionerNo";
            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetScoringRecapitulationsList(int employeeId, string participantName, string periodYear, string quarter, int status, string role, int personId)
        {
            esParameters par = new esParameters();
            par.Add("p_EmployeeID", employeeId);
            par.Add("p_ParticipantName", participantName);
            par.Add("p_PeriodYear", periodYear);
            par.Add("p_SRQuarterPeriod", quarter);
            par.Add("p_PersonID", personId);

            string commandText =
                @"SELECT api.[ParticipantID], api.[ParticipantItemID], ap.[ParticipantName], ap.[PeriodYear], ap.[Notes], api.[EmployeeID], emp.[EmployeeNumber] AS 'SubjectNumber', emp.[EmployeeName] AS 'SubjectName',
	                COALESCE(org.[OrganizationUnitName],'') AS 'OrganizationUnitName', COALESCE(pos.[PositionName],'') AS 'PositionName', 
	                CASE WHEN ap.[IsScoringRecapitulation] = 1 THEN api.[IsClosed] ELSE CAST(1 AS BIT) END AS 'IsClosed', ap.[IsScoringRecapitulation], 
	                CASE WHEN ISNULL((SELECT COUNT(asa.[ScoringAccumulationID]) FROM [AppraisalScoringAccumulation] AS asa WHERE asa.[ParticipantItemID] = api.[ParticipantItemID]), 0) > 0 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsNewRecord'  
                FROM [AppraisalParticipantItem] api 
                INNER JOIN [AppraisalParticipant] ap ON ap.[ParticipantID] = api.[ParticipantID] 
                INNER JOIN [Vw_EmployeeTable] emp ON emp.[PersonID] = api.[EmployeeID] 
                LEFT JOIN [OrganizationUnit] org ON org.[OrganizationUnitID] = emp.[ServiceUnitID] 
                LEFT JOIN [Position] pos ON pos.[PositionID] = emp.[PositionID] 
                LEFT JOIN (SELECT ape.[ParticipantItemID], COUNT(ape.[ParticipantEvaluatorID]) AS EvalCount
                            FROM [AppraisalParticipantEvaluator] AS ape 
                            GROUP BY ape.[ParticipantItemID]) ape ON ape.[ParticipantItemID] = api.[ParticipantItemID] 
                LEFT JOIN (SELECT as1.[ParticipantItemID], COUNT(as1.ScoresheetID) AS ScoresheetCount
                            FROM [AppraisalScoresheet] AS as1 
                            WHERE as1.[ReferenceID] IS NULL AND as1.[IsApproved] = 1
                            GROUP BY as1.ParticipantItemID) as1 ON as1.[ParticipantItemID] = ape.[ParticipantItemID]
                WHERE ISNULL(ape.[EvalCount], 0) <= ISNULL(as1.[ScoresheetCount], 0) ";

            //hanya supervisor atau manager
            if (role.ToLower() == "svr")
                commandText += "AND (emp.[SupervisorId] = @p_PersonID OR emp.[ManagerID] = @p_PersonID) ";
            
            if (employeeId != -1)
                commandText += "AND api.[EmployeeID] = @p_EmployeeID ";

            if (participantName != string.Empty)
                commandText += "AND ap.[ParticipantName] LIKE '%' + @p_ParticipantName + '%' ";

            if (periodYear != string.Empty)
                commandText += "AND ap.[PeriodYear] = @p_PeriodYear ";
            if (quarter != string.Empty)
                commandText += "AND ap.[SRQuarterPeriod] = @p_SRQuarterPeriod ";
            if (status == 0)
                commandText += "AND ap.[IsScoringRecapitulation] = 1 ";
            else if (status == 1)
                commandText += "AND ap.[IsScoringRecapitulation] = 1 " +
                    "AND (CASE WHEN ISNULL((SELECT COUNT(asa.[ScoringAccumulationID]) FROM [AppraisalScoringAccumulation] AS asa WHERE asa.[ParticipantItemID] = api.[ParticipantItemID]), 0) > 0 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END) = 1 ";
            else if (status == 2)
                commandText += "AND ap.[IsScoringRecapitulation] = 1 " +
                    "AND (CASE WHEN ISNULL((SELECT COUNT(asa.[ScoringAccumulationID]) FROM [AppraisalScoringAccumulation] AS asa WHERE asa.[ParticipantItemID] = api.[ParticipantItemID]), 0) > 0 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END) = 0 ";

            commandText += "ORDER BY ap.[PeriodYear] ASC,api.[EmployeeID] ASC";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
