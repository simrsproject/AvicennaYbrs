using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PersonalRecruitmentTestInterviewCollection
    {
        public DataTable GetJoin(string personalRecruitmentTestId)
        {
            esParameters par = new esParameters();
            par.Add("p_PersonalRecruitmentTestID", personalRecruitmentTestId);

            string commandText;
            commandText =
                    @"SELECT a.ItemID AS 'QuestionID', a.ItemName AS 'QuestionName', 
	                    ISNULL(b.Score1, 0) AS Score1, ISNULL(b.Score2, 0) AS Score2, ISNULL(b.Score3, 0) AS Score3, 
	                    ISNULL(b.Score4, 0) AS Score4, ISNULL(b.Score5, 0) AS Score5, ISNULL(b.Score6, 0) AS Score6, ISNULL(b.AverageScore, 0) AS AverageScore,
	                    a.IsActive AS IsDetail 
                    FROM AppStandardReferenceItem AS a 
                    LEFT JOIN (
	                    SELECT aa.SRRecruitmentTestQuestion, aa.Score1, aa.Score2, aa.Score3, aa.Score4, aa.Score5, aa.Score6, aa.AverageScore 
	                    FROM PersonalRecruitmentTestInterview AS aa 
	                    WHERE aa.PersonalRecruitmentTestID = @p_PersonalRecruitmentTestID 
                    ) b ON b.SRRecruitmentTestQuestion = a.ItemID
                    WHERE a.StandardReferenceID = 'RecruitmentTestQuestion'
                    ORDER BY a.ItemID";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetInnerJoin(string personalRecruitmentTestId)
        {
            esParameters par = new esParameters();
            par.Add("p_PersonalRecruitmentTestID", personalRecruitmentTestId);

            string commandText;
            commandText =
                    @"SELECT a.ItemID AS 'QuestionID', a.ItemName AS 'QuestionName', 
	                    ISNULL(b.Score1, 0) AS Score1, ISNULL(b.Score2, 0) AS Score2, ISNULL(b.Score3, 0) AS Score3, 
	                    ISNULL(b.Score4, 0) AS Score4, ISNULL(b.Score5, 0) AS Score5, ISNULL(b.Score6, 0) AS Score6, ISNULL(b.AverageScore, 0) AS AverageScore,
	                    a.IsActive AS IsDetail 
                    FROM AppStandardReferenceItem AS a 
                    INNER JOIN (
	                    SELECT aa.SRRecruitmentTestQuestion, aa.Score1, aa.Score2, aa.Score3, aa.Score4, aa.Score5, aa.Score6, aa.AverageScore 
	                    FROM PersonalRecruitmentTestInterview AS aa 
	                    WHERE aa.PersonalRecruitmentTestID = @p_PersonalRecruitmentTestID 
                    ) b ON b.SRRecruitmentTestQuestion = a.ItemID
                    WHERE a.StandardReferenceID = 'RecruitmentTestQuestion'
                    ORDER BY a.ItemID";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        //public DataTable GetTotalScore(string personalRecruitmentTestId)
        //{
        //    esParameters par = new esParameters();
        //    par.Add("p_PersonalRecruitmentTestID", personalRecruitmentTestId);

        //    string commandText =
        //        @"SELECT a.ScoresheetNo, SUM(ISNULL(a.Score, 0) * b.LoadScore) AS TotalScore 
        //        FROM ClinicalPerformanceAppraisalQuestionnaireScoresheetItem AS a 
        //        INNER JOIN ClinicalPerformanceAppraisalQuestionnaireItem b ON b.QuestionnaireItemID = a.QuestionnaireItemID 
        //        WHERE a.ScoresheetNo = @p_ScoresheetNo 
        //        GROUP BY a.ScoresheetNo";
        //    return FillDataTable(esQueryType.Text, commandText, par);
        //}
    }
}
