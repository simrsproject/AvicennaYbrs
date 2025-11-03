using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class SalaryTemplateItemCollection
    {
        public DataTable GetFullJoinWithSalaryComponent(string templateId)
        {
            esParameters par = new esParameters();
            par.Add("p_TemplateID", templateId);

            string commandText =
                @"SELECT a.SalaryComponentID, a.SalaryComponentCode, a.SalaryComponentName,
		IsSelect = CONVERT(BIT, CASE WHEN ISNULL(b.SalaryComponentID, -1)= -1 THEN 0 ELSE 1 END)
FROM SalaryComponent a
LEFT JOIN SalaryTemplateItem b ON b.SalaryComponentID = a.SalaryComponentID AND b.SalaryTemplateID = @p_TemplateID 
ORDER BY a.SalaryComponentCode";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
        public DataTable GetInnerJoinWithSalaryComponent(string templateId)
        {
            esParameters par = new esParameters();
            par.Add("p_TemplateID", templateId);

            string commandText =
                @"SELECT a.SalaryComponentID, a.SalaryComponentCode, a.SalaryComponentName,
		IsSelect = CONVERT(BIT, 1)
FROM SalaryComponent a
INNER JOIN SalaryTemplateItem b ON b.SalaryComponentID = a.SalaryComponentID AND b.SalaryTemplateID = @p_TemplateID 
ORDER BY a.SalaryComponentCode";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
