using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundryReceivedList : BasePageList
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "LaundryReceivedSearch.aspx?type=" + getPageID;
            UrlPageDetail = "LaundryReceivedDetail.aspx?type=" + getPageID;

            ProgramID = getPageID == "i" ? AppConstant.Program.LaundryReceivedInfectious : AppConstant.Program.LaundryReceived;

            this.WindowSearch.Height = 400;
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", getPageID);
            return script;
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
            string id = dataItem.GetDataKeyValue(LaundryReceivedMetadata.ColumnNames.ReceivedNo).ToString();
            Page.Response.Redirect("LaundryReceivedDetail.aspx?md=" + mode + "&id=" + id + "&type=" + getPageID, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = LaundryReceiveds;
        }

        private DataTable LaundryReceiveds
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                LaundryReceivedQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (LaundryReceivedQuery)Session[SessionNameForQuery];
                else
                {
                    query = new LaundryReceivedQuery("a");
                    var fromunit = new ServiceUnitQuery("b");
                    var usr = new AppUserQuery("c");
                    var fromroom = new ServiceRoomQuery("d");

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
                            query.IsVoid
                        );

                    query.InnerJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
                    query.InnerJoin(usr).On(usr.UserID == query.ReceivedByUserID);
                    query.LeftJoin(fromroom).On(fromroom.RoomID == query.FromRoomID);

                    if (getPageID == "")
                    {
                        query.Select(@"<'LaundryReceivedDetail.aspx?md=view&id='+a.ReceivedNo+'&type=' AS RUrl>");
                        query.Where(query.IsInfectious == false);
                    }
                    else if (getPageID == "n")
                    {
                        query.Select(@"<'LaundryReceivedDetail.aspx?md=view&id='+a.ReceivedNo+'&type=n' AS RUrl>");
                        query.Where(query.IsInfectious == false);
                    }
                    else
                    {
                        query.Select(@"<'LaundryReceivedDetail.aspx?md=view&id='+a.ReceivedNo+'&type=i' AS RUrl>");
                        query.Where(query.IsInfectious == true);
                    }
                       
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
            if (getPageID == "")
            {
                GridDataItem dataItem = e.DetailTableView.ParentItem;
                string receivedNo = dataItem.GetDataKeyValue("ReceivedNo").ToString();

                var query = new LaundryReceivedItemQuery("a");
                var iq = new ItemQuery("b");
                var unitq = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query.ReceivedNo,
                        query.ReceivedSeqNo,
                        query.ItemID,
                        iq.ItemName.As("ItemName"),

                        query.Qty,
                        @"<0 AS 'QtyProcessed'>",
                        @"<0 AS 'QtyReturn'>",

                        query.SRItemUnit,
                        unitq.ItemName.As("ItemUnit"),
                        query.Notes
                    );
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.InnerJoin(unitq).On(query.SRItemUnit == unitq.ItemID &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.ReceivedNo == receivedNo);
                query.OrderBy(query.ReceivedSeqNo.Ascending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var qr = new LaundryReceivedItemQuery("qr");
                    var pi = new LaunderedProcessItemQuery("pi");
                    var p = new LaunderedProcessQuery("p");
                    qr.Select(qr.ReceivedNo, qr.ReceivedSeqNo, pi.Qty.Sum().As("Qty"));
                    qr.InnerJoin(pi).On(pi.ReceivedNo == qr.ReceivedNo && pi.ReceivedSeqNo == qr.ReceivedSeqNo);
                    qr.InnerJoin(p).On(p.ProcessNo == pi.ProcessNo && p.IsApproved == true);
                    qr.Where(qr.ReceivedNo == row["ReceivedNo"].ToString(), qr.ReceivedSeqNo == row["ReceivedSeqNo"].ToString());
                    qr.GroupBy(qr.ReceivedNo, qr.ReceivedSeqNo);

                    DataTable piDtb = qr.LoadDataTable();
                    if (piDtb.Rows.Count > 0)
                        row["QtyProcessed"] = Convert.ToDouble(piDtb.Rows[0]["Qty"]);

                    qr = new LaundryReceivedItemQuery("qr");
                    pi = new LaunderedProcessItemQuery("pi");
                    p = new LaunderedProcessQuery("p");
                    var ri = new LaundryReturnedItemQuery("ri");
                    var r = new LaundryReturnedQuery("r");
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
            else
            {
                GridDataItem dataItem = e.DetailTableView.ParentItem;
                string receivedNo = dataItem.GetDataKeyValue("ReceivedNo").ToString();

                var query = new LaundryReceivedItemInfectiousQuery("a");
                var iq = new ItemLinenQuery("b");
                var unitq = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query.ReceivedNo,
                        query.ReceivedSeqNo,
                        query.ItemID,
                        iq.ItemName.As("ItemName"),

                        query.Qty,
                        @"<0 AS 'QtyProcessed'>",
                        @"<0 AS 'QtyReturn'>",

                        query.SRItemUnit,
                        unitq.ItemName.As("ItemUnit"),
                        query.Notes
                    );
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.InnerJoin(unitq).On(query.SRItemUnit == unitq.ItemID &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.ReceivedNo == receivedNo);
                query.OrderBy(query.ReceivedSeqNo.Ascending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var qr = new LaundryReceivedItemInfectiousQuery("qr");
                    var pi = new LaunderedProcessItemInfectiousQuery("pi");
                    var p = new LaunderedProcessQuery("p");
                    qr.Select(qr.ReceivedNo, qr.ReceivedSeqNo, pi.Qty.Sum().As("Qty"));
                    qr.InnerJoin(pi).On(pi.ReceivedNo == qr.ReceivedNo && pi.ReceivedSeqNo == qr.ReceivedSeqNo);
                    qr.InnerJoin(p).On(p.ProcessNo == pi.ProcessNo && p.IsApproved == true);
                    qr.Where(qr.ReceivedNo == row["ReceivedNo"].ToString(), qr.ReceivedSeqNo == row["ReceivedSeqNo"].ToString());
                    qr.GroupBy(qr.ReceivedNo, qr.ReceivedSeqNo);

                    DataTable piDtb = qr.LoadDataTable();
                    if (piDtb.Rows.Count > 0)
                        row["QtyProcessed"] = Convert.ToDouble(piDtb.Rows[0]["Qty"]);
                }
                dtb.AcceptChanges();

                //Apply
                e.DetailTableView.DataSource = dtb;
            }
        }
    }
}
