using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class SterileItemsReceivedInfoList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.CssdSterileItemsReceivedInfo;

            if (!IsPostBack)
            {
                txtFromReceivedDate.SelectedDate = DateTime.Now;
                txtToReceivedDate.SelectedDate = DateTime.Now;
            }
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
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

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = CssdSterileItemsReceiveds;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
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
                var phaseq = new AppStandardReferenceItemQuery("d");

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
                        query.IsDtt,
                        phaseq.ItemName.As("CssdPhaseName")
                    );
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.InnerJoin(unitq).On(query.SRCssdItemUnit == unitq.ItemID &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.LeftJoin(phaseq).On(query.SRCssdPhase == phaseq.ItemID &&
                                          phaseq.StandardReferenceID == AppEnum.StandardReference.CssdPhase);
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

                    //qr = new CssdSterileItemsReceivedItemQuery("qr");
                    //pi = new CssdSterilizationProcessItemQuery("pi");
                    //p = new CssdSterilizationProcessQuery("p");
                    //var ri = new CssdSterileItemsReturnedItemQuery("ri");
                    //var r = new CssdSterileItemsReturnedQuery("r");
                    //qr.Select(qr.ReceivedNo, qr.ReceivedSeqNo, pi.Qty.Sum().As("Qty"));
                    //qr.InnerJoin(pi).On(pi.ReceivedNo == qr.ReceivedNo && pi.ReceivedSeqNo == qr.ReceivedSeqNo);
                    //qr.InnerJoin(p).On(p.ProcessNo == pi.ProcessNo && p.IsApproved == true);
                    //qr.InnerJoin(ri).On(ri.ProcessNo == pi.ProcessNo && ri.ProcessSeqNo == pi.ProcessSeqNo);
                    //qr.InnerJoin(r).On(r.ReturnNo == ri.ReturnNo && r.IsApproved == true);
                    //qr.Where(qr.ReceivedNo == row["ReceivedNo"].ToString(), qr.ReceivedSeqNo == row["ReceivedSeqNo"].ToString());
                    //qr.GroupBy(qr.ReceivedNo, qr.ReceivedSeqNo);

                    //DataTable riDtb = qr.LoadDataTable();
                    //if (riDtb.Rows.Count > 0)
                    //    row["QtyReturn"] = Convert.ToDouble(riDtb.Rows[0]["Qty"]);

                }
                dtb.AcceptChanges();

                //Apply
                e.DetailTableView.DataSource = dtb;
            }
        }

        private DataTable CssdSterileItemsReceiveds
        {
            get
            {
                var isEmptyFilter = txtFromReceivedDate.IsEmpty && txtToReceivedDate.IsEmpty && string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Receive Information")) return null;

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
                        query.IsApproved,
                        query.IsVoid,
                        "<'SterileItemsReceivedDetail.aspx?md=view&id='+a.ReceivedNo AS RUrl>"
                    );

                query.InnerJoin(fromunit).On(fromunit.ServiceUnitID == query.FromServiceUnitID);
                query.LeftJoin(fromroom).On(fromroom.RoomID == query.FromRoomID);
                query.LeftJoin(usr).On(usr.UserID == query.ReceivedByUserID);
                query.Where(query.IsApproved == true);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!txtFromReceivedDate.IsEmpty)
                    query.Where(query.ReceivedDate.Date() >= txtFromReceivedDate.SelectedDate.Value.Date);
                if (!txtToReceivedDate.IsEmpty)
                    query.Where(query.ReceivedDate.Date() <= txtToReceivedDate.SelectedDate.Value.Date);
                if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);

                return query.LoadDataTable();
            }
        }

        protected void cboFromServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ServiceUnitItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboFromServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ServiceUnitItemDataBound(e);
        }
    }
}