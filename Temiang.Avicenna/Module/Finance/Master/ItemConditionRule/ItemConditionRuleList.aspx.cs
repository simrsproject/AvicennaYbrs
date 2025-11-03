using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ItemConditionRuleList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ItemConditionRuleSearch.aspx";
            UrlPageDetail = "ItemConditionRuleDetail.aspx";

            WindowSearch.Height = 170;

            ProgramID = AppConstant.Program.ItemConditionRule;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }

        public override void OnMenuEditClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(ItemConditionRuleMetadata.ColumnNames.ItemConditionRuleID).ToString();
            Page.Response.Redirect("ItemConditionRuleDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemConditionRules;
        }

        private DataTable ItemConditionRules
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ItemConditionRuleQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ItemConditionRuleQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ItemConditionRuleQuery("a");
                    var qRule = new AppStandardReferenceItemQuery("b");
                    query.Select(query.ItemConditionRuleID, query.ItemConditionRuleName, query.StartingDate, query.EndingDate,
                                 qRule.ItemName.As("ItemConditionRuleType"), query.AmountValue, query.IsValueInPercent);
                    query.InnerJoin(qRule).On(query.SRItemConditionRuleType == qRule.ItemID & qRule.StandardReferenceID == "ItemConditionRuleType");
                    query.OrderBy(query.ItemConditionRuleID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "ItemConditionRuleName", "ItemConditionRuleID");
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
