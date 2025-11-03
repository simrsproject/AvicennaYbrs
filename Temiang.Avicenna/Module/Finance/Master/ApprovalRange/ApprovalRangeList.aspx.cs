using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ApprovalRangeList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ApprovalRangeSearch.aspx";
            UrlPageDetail = "ApprovalRangeDetail.aspx";

            WindowSearch.Height = 200;

            ProgramID = AppConstant.Program.ApprovalRange;
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
            string id = dataItem.GetDataKeyValue(ApprovalRangeMetadata.ColumnNames.ApprovalRangeID).ToString();
            Page.Response.Redirect("ApprovalRangeDetail.aspx?md=" + mode + "&id=" + id, true);
        }	

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            grdList.DataSource = ApprovalRanges;
        }

        private DataTable ApprovalRanges
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ApprovalRangeQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ApprovalRangeQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ApprovalRangeQuery("a");
                    var txCode = new AppStandardReferenceItemQuery("tc");
                    var asriq = new AppStandardReferenceItemQuery("b");
                    var igq = new ItemGroupQuery("c");
                    query.InnerJoin(txCode).On(txCode.StandardReferenceID == "TransactionCode" && query.TransactionCode == txCode.ItemID);
                    query.InnerJoin(asriq).On(asriq.StandardReferenceID=="ItemType" && query.SRItemType == asriq.ItemID);
                    query.LeftJoin(igq).On(query.ItemGroupID == igq.ItemGroupID);

                    query.Select
                        (
                            query.ApprovalRangeID,
                            query.AmountFrom,
                            query.ApprovalLevelFinal,
                            asriq.ItemName.As("ItemTypeName"),
                            txCode.ItemName.As("TransactionName"),
                            igq.ItemGroupName
                        );
                    query.OrderBy(txCode.ItemName.Ascending, asriq.ItemName.Ascending, igq.ItemGroupName.Ascending, query.AmountFrom.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var dataItem = e.DetailTableView.ParentItem;

            var query = new ApprovalRangeUserQuery("a");
            var uq = new AppUserQuery("b");
            query.InnerJoin(uq).On(query.UserID == uq.UserID);

            query.Where(query.ApprovalRangeID == dataItem.GetDataKeyValue("ApprovalRangeID").ToString());

            query.Select
                (
                    query.ApprovalRangeID,
                    query.ApprovalLevel,
                    uq.UserName
                );

            query.OrderBy(query.ApprovalLevel.Ascending);

            var dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;

        }
    }
}