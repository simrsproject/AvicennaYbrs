using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class QuestionFormInServiceUnitCollection
    {
        public DataTable GetFullJoinWQuestionForm(string unitId)
        {
            esParameters par = new esParameters();
            par.Add("p_ServiceUnitID", unitId);

            string commandText =
                @"SELECT a.QuestionFormID, a.QuestionFormName, a.RmNO, 
	                IsSelect = CONVERT(BIT,CASE WHEN COALESCE(b.QuestionFormID,'-')='-' THEN 0 ELSE 1 END) 
                FROM QuestionForm a 
                LEFT JOIN QuestionFormInServiceUnit b ON a.QuestionFormID = b.QuestionFormID AND b.ServiceUnitID = @p_ServiceUnitID 
                WHERE a.IsActive = 1";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
        public DataTable GetInnerJoinWQuestionForm(string unitId)
        {
            esParameters par = new esParameters();
            par.Add("p_ServiceUnitID", unitId);

            string commandText =
                @"SELECT a.QuestionFormID, a.QuestionFormName, a.RmNO, 
                    IsSelect = CONVERT(BIT,1)
                FROM QuestionForm a
                INNER JOIN QuestionFormInServiceUnit b ON a.QuestionFormID = b.QuestionFormID AND b.ServiceUnitID = @p_ServiceUnitID 
                WHERE a.IsActive = 1";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
