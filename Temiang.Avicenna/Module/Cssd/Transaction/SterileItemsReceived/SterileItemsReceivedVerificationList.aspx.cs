using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Collections.Generic;
using System.Web;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class SterileItemsReceivedVerificationList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); // !!Jika tidak dipanggil, tampilan jadi tidak rapih
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.CssdSterileItemsReceived;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnit(cboRequestFromServiceUnitID, false);
                ComboBox.PopulateWithServiceUnit(cboFromServiceUnitID, false);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack) return;

            RestoreValueFromCookie();
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdRequest_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit) return;

            var grd = (RadGrid)source;
            var dataSource = SterileItemsRequests;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }

            //grdRequest.DataSource = SterileItemsRequests;
        }

        private DataTable SterileItemsRequests
        {
            get
            {
                var isEmptyFilter = txtRequestFromDate.IsEmpty && txtRequestToDate.IsEmpty && string.IsNullOrEmpty(txtRequestNo.Text) && string.IsNullOrEmpty(cboRequestFromServiceUnitID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Request List")) return null;

                var query = new CssdSterileItemsRequestQuery("a");
                var fromunit = new ServiceUnitQuery("b");
                var fromroom = new ServiceRoomQuery("c");
                var received = new CssdSterileItemsReceivedQuery("d");
                
                query.Select
                    (
                        query.RequestNo,
                        query.RequestDate,
                        fromunit.ServiceUnitName.As("FromServiceUnitName"),
                        fromroom.RoomName.As("FromRoomName"),
                        query.SenderBy,
                        query.IsApproved,
                        query.IsVoid,
                        "<'SterileItemsReceivedDetail.aspx?md=new&id=&rn=' + a.RequestNo + '&type=ver' AS RUrl>"
                    );

                query.InnerJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
                query.LeftJoin(fromroom).On(fromroom.RoomID == query.FromRoomID);
                query.LeftJoin(received).On(received.IsFromProductionOfGoods == false && received.ProductionNo == query.RequestNo && received.IsVoid == false);
                query.Where(query.IsApproved == true, received.ReceivedNo.IsNull());

                query.OrderBy(query.RequestDate.Descending, query.RequestNo.Descending);

                if (!txtRequestFromDate.IsEmpty && !txtRequestToDate.IsEmpty)
                    query.Where(query.RequestDate.Between(txtRequestFromDate.SelectedDate, txtRequestToDate.SelectedDate));
                if (!string.IsNullOrEmpty(txtRequestNo.Text))
                    query.Where(query.RequestNo == txtRequestNo.Text);
                if (cboRequestFromServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.FromServiceUnitID == cboRequestFromServiceUnitID.SelectedValue);

                return query.LoadDataTable();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit) return;

            var grd = (RadGrid)source;
            var dataSource = SterileItemsReceiveds;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }

            //grdList.DataSource = SterileItemsReceiveds;
        }

        private DataTable SterileItemsReceiveds
        {
            get
            {
                var isEmptyFilter = txtReceivedFromDate.IsEmpty && txtReceivedToDate.IsEmpty && string.IsNullOrEmpty(txtReceivedNo.Text) && string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtReferenceNo.Text);
                if (!ValidateSearch(isEmptyFilter, "Receive List")) return null;

                var query = new CssdSterileItemsReceivedQuery("a");
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
                        query.IsFromProductionOfGoods,
                        query.ProductionNo,
                        query.IsApproved,
                        query.IsVoid,
                        "<'SterileItemsReceivedDetail.aspx?md=view&id=' + a.ReceivedNo + '&rn=&type=ver' AS RUrl>"
                    );
                
                query.InnerJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
                query.LeftJoin(fromroom).On(fromroom.RoomID == query.FromRoomID);
                query.LeftJoin(usr).On(usr.UserID == query.ReceivedByUserID);
                query.OrderBy(query.ReceivedDate.Descending, query.ReceivedNo.Descending);

                if (!txtReceivedFromDate.IsEmpty && !txtReceivedToDate.IsEmpty)
                    query.Where(query.ReceivedDate.Between(txtReceivedFromDate.SelectedDate, txtReceivedToDate.SelectedDate));
                if (!string.IsNullOrEmpty(txtReceivedNo.Text))
                    query.Where(query.ReceivedNo == txtReceivedNo.Text);
                if (cboFromServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(txtReferenceNo.Text))
                    query.Where(query.ProductionNo == txtReferenceNo.Text);

                return query.LoadDataTable();
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
                        @"<CAST((CAST(a.CssdItemNo AS INT)) AS VARCHAR) AS 'ItemNo'>",

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

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();
            grdList.Rebind();
        }

        protected void btnFilterRequest_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();
            grdRequest.Rebind();
        }
    }
}