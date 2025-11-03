using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ItemConditionRuleSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.ItemConditionRule;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ItemConditionRuleQuery("a");
            var qRule = new AppStandardReferenceItemQuery("b");
            query.Select(query.ItemConditionRuleID, query.ItemConditionRuleName, query.StartingDate, query.EndingDate,
                         qRule.ItemName.As("ItemConditionRuleType"), query.AmountValue, query.IsValueInPercent);
            query.InnerJoin(qRule).On(query.SRItemConditionRuleType == qRule.ItemID & qRule.StandardReferenceID == "ItemConditionRuleType");

            if (!string.IsNullOrEmpty(txtItemID.Text))
            {
                if (cboFilterItemID.SelectedIndex == 1)
                    query.Where(query.ItemConditionRuleID == txtItemID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemID.Text);
                    query.Where(query.ItemConditionRuleID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtItemName.Text))
            {
                if (cboFilterItemName.SelectedIndex == 1)
                    query.Where(query.ItemConditionRuleName == txtItemName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtItemName.Text);
                    query.Where(query.ItemConditionRuleName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.ItemConditionRuleID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
