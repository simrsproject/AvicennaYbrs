using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class SterileItemsReceivedList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SterileItemsReceivedSearch.aspx";
            UrlPageDetail = "SterileItemsReceivedDetail.aspx";

            ProgramID = AppConstant.Program.CssdSterileItemsReceived;

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
            string id = dataItem.GetDataKeyValue(CssdSterileItemsReceivedMetadata.ColumnNames.ReceivedNo).ToString();
            Page.Response.Redirect("SterileItemsReceivedDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = CssdSterileItemsReceiveds;
        }

        private DataTable CssdSterileItemsReceiveds
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                CssdSterileItemsReceivedQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (CssdSterileItemsReceivedQuery)Session[SessionNameForQuery];
                else
                {
                    query = new CssdSterileItemsReceivedQuery("a");
                    var fromunit = new ServiceUnitQuery("b");
                    var fromroom = new ServiceRoomQuery("c");
                    var usr = new AppUserQuery("d");
                    

                    query.Select
                        (
                            query.ReceivedNo,
                            query.ReceivedDate,
                            query.ReceivedTime,
                            fromunit.ServiceUnitName.As("FromServiceUnitName"),
                            fromroom.RoomName.As("FromRoomName"),
                            query.SenderBy,
                            usr.UserName.As("ReceivedByUserName"),
                            query.IsApproved,
                            query.IsVoid,
                            "<'SterileItemsReceivedDetail.aspx?md=view&id='+a.ReceivedNo AS RUrl>"
                        );

                    query.InnerJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
                    query.LeftJoin(fromroom).On(fromroom.RoomID == query.FromRoomID);
                    query.LeftJoin(usr).On(usr.UserID == query.ReceivedByUserID);

                    query.OrderBy(query.ReceivedDate.Descending, query.ReceivedNo.Descending);
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
            string receivedNo = dataItem.GetDataKeyValue("ReceivedNo").ToString();
            if (e.DetailTableView.Name.Equals("grdDetail"))
            {
                var query = new CssdSterileItemsReceivedItemQuery("a");
                var iq = new ItemQuery("b");
                var unitq = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query.ReceivedNo,
                        query.ReceivedSeqNo,
                        query.CssdItemNo,
                        @"<CAST(a.CssdItemNo AS VARCHAR) AS 'ItemNo'>",

                        query.ItemID,
                        iq.ItemName.As("ItemName"),

                        query.Qty,
                        @"<0 AS 'QtyProcessed'>",
                        @"<0 AS 'QtyReturn'>",

                        query.SRCssdItemUnit,
                        unitq.ItemName.As("CssdItemUnit"),
                        query.Notes,
                        query.ExpiredDate,
                        query.ReuseTo,
                        query.IsNeedUltrasound,
                        query.IsDtt
                    );
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.InnerJoin(unitq).On(query.SRCssdItemUnit == unitq.ItemID &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.ReceivedNo == receivedNo);
                query.OrderBy(query.CssdItemNo.Ascending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var qr = new CssdSterileItemsReceivedItemQuery("qr");
                    var pi = new CssdSterilizationProcessItemQuery("pi");
                    var p = new CssdSterilizationProcessQuery("p");
                    qr.Select(qr.ReceivedNo, qr.ReceivedSeqNo, pi.Qty.Sum().As("Qty"));
                    qr.InnerJoin(pi).On(pi.ReceivedNo == qr.ReceivedNo && pi.ReceivedSeqNo == qr.ReceivedSeqNo);
                    qr.InnerJoin(p).On(p.ProcessNo == pi.ProcessNo && p.IsApproved == true);
                    qr.Where(qr.ReceivedNo == row["ReceivedNo"].ToString(), qr.ReceivedSeqNo == row["ReceivedSeqNo"].ToString());
                    qr.GroupBy(qr.ReceivedNo, qr.ReceivedSeqNo);

                    DataTable piDtb = qr.LoadDataTable();
                    if (piDtb.Rows.Count > 0)
                        row["QtyProcessed"] = Convert.ToDouble(piDtb.Rows[0]["Qty"]);

                    qr = new CssdSterileItemsReceivedItemQuery("qr");
                    pi = new CssdSterilizationProcessItemQuery("pi");
                    p = new CssdSterilizationProcessQuery("p");
                    var ri = new CssdSterileItemsReturnedItemQuery("ri");
                    var r = new CssdSterileItemsReturnedQuery("r");
                    qr.Select(qr.ReceivedNo, qr.ReceivedSeqNo, pi.Qty.Sum().As("Qty"));
                    qr.InnerJoin(pi).On(pi.ReceivedNo == qr.ReceivedNo && pi.ReceivedSeqNo == qr.ReceivedSeqNo);
                    qr.InnerJoin(p).On(p.ProcessNo == pi.ProcessNo && p.IsApproved == true);
                    qr.InnerJoin(ri).On(ri.ProcessNo == pi.ProcessNo && ri.ProcessSeqNo == pi.ProcessSeqNo);
                    qr.InnerJoin(r).On(r.ReturnNo == ri.ReturnNo && r.IsApproved == true);
                    qr.Where(qr.ReceivedNo == row["ReceivedNo"].ToString(), qr.ReceivedSeqNo == row["ReceivedSeqNo"].ToString());
                    qr.GroupBy(qr.ReceivedNo, qr.ReceivedSeqNo);

                    DataTable riDtb = qr.LoadDataTable();
                    if (riDtb.Rows.Count > 0)
                        row["QtyReturn"] = Convert.ToDouble(riDtb.Rows[0]["Qty"]);

                }
                dtb.AcceptChanges();

                //Apply
                e.DetailTableView.DataSource = dtb;
            }
        }
    }
}
