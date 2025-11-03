using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class SterileItemsRequestList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SterileItemsRequestSearch.aspx";
            UrlPageDetail = "SterileItemsRequestDetail.aspx";

            ProgramID = AppConstant.Program.CssdSterileItemsRequest;

            this.WindowSearch.Height = 400;
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
            string id = dataItem.GetDataKeyValue(CssdSterileItemsRequestMetadata.ColumnNames.RequestNo).ToString();
            Page.Response.Redirect("SterileItemsRequestDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = CssdSterileItemsRequests;
        }

        private DataTable CssdSterileItemsRequests
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                CssdSterileItemsRequestQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (CssdSterileItemsRequestQuery)Session[SessionNameForQuery];
                else
                {
                    query = new CssdSterileItemsRequestQuery("a");
                    var fromunit = new ServiceUnitQuery("b");
                    var fromroom = new ServiceRoomQuery("c");

                    query.Select
                        (
                            query.RequestNo,
                            query.RequestDate,
                            fromunit.ServiceUnitName.As("FromServiceUnitName"),
                            fromroom.RoomName.As("FromRoomName"),
                            query.SenderBy,
                            query.IsApproved,
                            query.IsVoid,
                            "<'SterileItemsRequestDetail.aspx?md=view&id='+a.RequestNo AS RUrl>"
                        );

                    query.InnerJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
                    query.LeftJoin(fromroom).On(fromroom.RoomID == query.FromRoomID);

                    query.OrderBy(query.RequestDate.Descending, query.RequestNo.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;

                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string requestNo = dataItem.GetDataKeyValue("RequestNo").ToString();
            var query = new CssdSterileItemsRequestItemQuery("a");
            var iq = new ItemQuery("b");
            var unitq = new AppStandardReferenceItemQuery("c");

            query.Select
                (
                    query.RequestNo,
                    query.RequestSeqNo,
                    query.ItemID,
                    iq.ItemName.As("ItemName"),
                    query.Qty,
                    query.SRCssdItemUnit,
                    unitq.ItemName.As("CssdItemUnit"),
                    query.Notes
                );
            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
            query.InnerJoin(unitq).On(query.SRCssdItemUnit == unitq.ItemID &&
                                      unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
            query.Where(query.RequestNo == requestNo);
            query.OrderBy(query.RequestSeqNo.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}