using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemConditionRuleServiceUnitCollection
    {
        public DataTable GetFullJoinWRule(string ruleId)
        {
            esParameters par = new esParameters();
            par.Add("p_RuleID", ruleId);

            string commandText =
                @"SELECT a.ServiceUnitID, a.ServiceUnitName,
IsSelect = CONVERT(BIT, CASE WHEN COALESCE(b.ServiceUnitID,'-') = '-' THEN 0 ELSE 1 END)
FROM ServiceUnit a
LEFT  JOIN ItemConditionRuleServiceUnit b
      ON  a.ServiceUnitID = b.ServiceUnitID
      AND b.ItemConditionRuleID = @p_RuleID 
WHERE a.SRRegistrationType <> ''";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
        public DataTable GetInnerJoinWRule(string ruleId)
        {
            esParameters par = new esParameters();
            par.Add("p_RuleID", ruleId);

            string commandText =
                @"SELECT a.ServiceUnitID,a.ServiceUnitName,
IsSelect = CONVERT(BIT,1)
FROM ServiceUnit a
INNER JOIN ItemConditionRuleServiceUnit b
      ON  a.ServiceUnitID = b.ServiceUnitID
      AND b.ItemConditionRuleID = @p_RuleID 
WHERE a.SRRegistrationType <> ''";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
