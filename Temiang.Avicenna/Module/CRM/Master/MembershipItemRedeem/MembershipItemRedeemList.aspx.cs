using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.CRM.Master
{
    public partial class MembershipItemRedeemList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "MembershipItemRedeemSearch.aspx";
            UrlPageDetail = "MembershipItemRedeemDetail.aspx";

            ProgramID = AppConstant.Program.MembershipItemRedeem;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(MembershipItemRedeemMetadata.ColumnNames.ItemReedemID).ToString();
            Page.Response.Redirect("MembershipItemRedeemDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = MembershipItemRedeems;
        }

        private DataTable MembershipItemRedeems
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                MembershipItemRedeemQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (MembershipItemRedeemQuery)Session[SessionNameForQuery];
                else
                {
                    query = new MembershipItemRedeemQuery("a");
                    var item = new AppStandardReferenceItemQuery("b");
                    query.InnerJoin(item).On(item.StandardReferenceID == "ItemReedemGroup" && item.ItemID == query.SRItemReedemGroup);
                    query.Select
                        (
                            query.ItemReedemID,
                            query.ItemReedemName,
                            item.ItemName.As("ItemReedemGroup"),
                            query.PointsUsed,
                            query.IsActive
                        );
                    query.OrderBy(query.ItemReedemID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "ItemReedemName", "ItemReedemID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}