using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CredentialCompetencyAssessmentDtCollection
    {
        public DataTable GetJoin(string transactionNo)
        {
            esParameters par = new esParameters();
            par.Add("p_TransactionNo", transactionNo);

            string commandText =
            @"SELECT b.ItemID AS SRMedicalCompetencyAssessment, b.ItemName AS MedicalCompetencyAssessmentName, a.ItemID AS SRMedicalCompetencyAssessmentDt, 
	            a.ItemName AS MedicalCompetencyAssessmentDtName, a.Note AS MedicalCompetencyAssessmentDtDesc, 
	            ISNULL(dt.SRMedicalCompetencyAsstResult, '') AS SRMedicalCompetencyAsstResult, ISNULL(dta.ItemName, '') AS MedicalCompetencyAsstResultName, 
                ISNULL(dt.Notes, '') AS Notes 
            FROM AppStandardReferenceItem AS a
            INNER JOIN AppStandardReferenceItem AS b ON b.StandardReferenceID = 'MedicalCompetencyAssessment' AND b.ItemID = a.ReferenceID
            LEFT JOIN (SELECT x.TransactionNo, x.SRMedicalCompetencyAssessmentDt, x.SRMedicalCompetencyAsstResult, x.Notes 
                       FROM CredentialCompetencyAssessmentDt x WHERE x.TransactionNo = @p_TransactionNo) dt ON dt.SRMedicalCompetencyAssessmentDt = a.ItemID 
            LEFT JOIN AppStandardReferenceItem AS dta ON dta.StandardReferenceID = 'MedicalCompetencyAsstResult' AND dta.ItemID = dt.SRMedicalCompetencyAsstResult
            WHERE a.StandardReferenceID = 'MedicalCompetencyAssessmentDt'
            ORDER BY a.ItemID";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
