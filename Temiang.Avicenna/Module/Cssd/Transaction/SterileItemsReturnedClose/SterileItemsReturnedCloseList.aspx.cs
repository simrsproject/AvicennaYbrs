using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class SterileItemsReturnedCloseList : BasePage
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

            ProgramID = AppConstant.Program.CssdSterileItemsReturnedClose;

            if (!IsPostBack)
            {
            }
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

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit) return;

            var grd = (RadGrid)source;
            var dataSource = CssdSterileItemsReturneds;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }

            //if (!e.IsFromDetailTable)
            //    grdList.DataSource = CssdSterileItemsReturneds;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            DataTable dtb;
            //if (!AppSession.Parameter.IsCssdUsingPackagingProcess)
            {
                var query = new CssdSterileItemsReturnedItemQuery("a");
                var proceed = new CssdSterilizationProcessItemQuery("b");
                var received = new CssdSterileItemsReceivedItemQuery("c");
                var iq = new ItemQuery("d");
                var unitq = new AppStandardReferenceItemQuery("e");

                query.Select
                    (
                        query.ReturnNo,
                        query.ReturnSeqNo,
                        query.ProcessNo,
                        query.ProcessSeqNo,
                        proceed.ReceivedNo,
                        proceed.ReceivedSeqNo,
                        proceed.Qty,
                        proceed.Weight,

                        received.CssdItemNo,
                        @"<CAST(c.CssdItemNo AS VARCHAR) AS 'ItemNo'>",

                        received.ItemID,
                        iq.ItemName.As("ItemName"),

                        received.SRCssdItemUnit,
                        unitq.ItemName.As("CssdItemUnit"),
                        received.Notes
                    );
                query.InnerJoin(proceed).On(proceed.ProcessNo == query.ProcessNo &&
                                            proceed.ProcessSeqNo == query.ProcessSeqNo);
                query.InnerJoin(received).On(received.ReceivedNo == proceed.ReceivedNo &&
                                             received.ReceivedSeqNo == proceed.ReceivedSeqNo);
                query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
                query.InnerJoin(unitq).On(unitq.ItemID == received.SRCssdItemUnit &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.ReturnNo == e.DetailTableView.ParentItem.GetDataKeyValue("ReturnNo").ToString());
                query.OrderBy(received.CssdItemNo.Ascending);

                dtb = query.LoadDataTable();
            }
            //else
            //{
            //    var query = new CssdSterileItemsReturnedItemQuery("a");
            //    var proceed = new CssdSterilizationProcessItemQuery("b");
            //    var packaged = new CssdPackagingItemQuery("c");
            //    var received = new CssdSterileItemsReceivedItemQuery("d");
            //    var iq = new ItemQuery("d");
            //    var unitq = new AppStandardReferenceItemQuery("e");

            //    query.Select
            //        (
            //            query.ReturnNo,
            //            query.ReturnSeqNo,
            //            query.ProcessNo,
            //            query.ProcessSeqNo,
            //            proceed.ReceivedNo,
            //            proceed.ReceivedSeqNo,
            //            proceed.Qty,
            //            proceed.Weight,

            //            packaged.CssdItemNo,
            //            @"<CAST((CAST(c.CssdItemNo AS INT)) AS VARCHAR) AS 'ItemNo'>",

            //            received.ItemID,
            //            iq.ItemName.As("ItemName"),

            //            received.SRItemUnit.As("SRCssdItemUnit"),
            //            unitq.ItemName.As("CssdItemUnit"),
            //            received.Notes
            //        );
            //    query.InnerJoin(proceed).On(proceed.ProcessNo == query.ProcessNo &&
            //                                proceed.ProcessSeqNo == query.ProcessSeqNo);
            //    query.InnerJoin(received).On(received.TransactionNo == proceed.ReceivedNo &&
            //                                 received.SeqNo == proceed.ReceivedSeqNo);
            //    query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
            //    query.InnerJoin(unitq).On(unitq.ItemID == received.SRItemUnit &&
            //                              unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
            //    query.Where(query.ReturnNo == e.DetailTableView.ParentItem.GetDataKeyValue("ReturnNo").ToString());
            //    query.OrderBy(received.CssdItemNo.Ascending);

            //    dtb = query.LoadDataTable();
            //}

            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        private DataTable CssdSterileItemsReturneds
        {
            get
            {
                var isEmptyFilter = txtFromReturnDate.IsEmpty && txtToReturnDate.IsEmpty && string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Return List")) return null;

                var query = new CssdSterileItemsReturnedQuery("a");
                var tounit = new ServiceUnitQuery("b");
                var usr = new AppUserQuery("c");
                var usrunit = new AppUserServiceUnitQuery("d");

                query.Select
                    (
                        query.ReturnNo,
                        query.ReturnDate,
                        query.ReturnTime,
                        tounit.ServiceUnitName.As("ToServiceUnitName"),
                        query.HandedByUserID,
                        usr.UserName.As("HandedBy"),
                        query.ReceivedBy,
                        query.IsApproved,
                        query.IsVoid
                    );

                query.InnerJoin(tounit).On(tounit.ServiceUnitID == query.ToServiceUnitID);
                query.InnerJoin(usr).On(usr.UserID == query.HandedByUserID);
                query.InnerJoin(usrunit).On(usrunit.ServiceUnitID == query.ToServiceUnitID &&
                                            usrunit.UserID == AppSession.UserLogin.UserID);
                query.Where(query.IsApproved == true, query.Or(query.IsClosed == false, query.IsClosed.IsNull()));
                if (!txtFromReturnDate.IsEmpty)
                    query.Where(query.ReturnDate.Date() >= txtFromReturnDate.SelectedDate.Value.Date);
                if (!txtToReturnDate.IsEmpty)
                    query.Where(query.ReturnDate.Date() <= txtToReturnDate.SelectedDate.Value.Date);
                if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboToServiceUnitID.SelectedValue);

                return query.LoadDataTable();
            }
        }

        protected void cboToServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ServiceUnitUserItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboToServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ServiceUnitItemDataBound(e);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;
            if (eventArgument == "closed")
            {
                pnlInfo.Visible = false;

                Validate();
                if (!IsValid)
                    return;

                var msg = Closed();

                if (!string.IsNullOrEmpty(msg))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = msg;
                }

                grdList.Rebind();
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grdList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }

        private string Closed()
        {
            var msg = string.Empty;
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                if ((dataItem.FindControl("detailChkbox") as CheckBox).Checked)
                {
                    string returnNo = dataItem.GetDataKeyValue("ReturnNo").ToString();
                    string receivedBy = ((RadTextBox)dataItem.FindControl("txtReceivedBy")).Text ?? string.Empty;

                    if (string.IsNullOrEmpty(receivedBy))
                    {
                        if (msg == string.Empty)
                        {
                            msg = "Received By is required for " + returnNo;
                        }
                        else
                        {
                            msg += ", " + "Received By is required for " + returnNo;
                        }
                    }
                    else if (receivedBy.Length < 3)
                    {
                        if (msg == string.Empty)
                        {
                            msg = "Received By must be more than or equal 3 characters long for " + returnNo;
                        }
                        else
                        {
                            msg += ", " + "Received By must be more than or equal 3 characters long for " + returnNo;
                        }
                    }
                    else
                    {
                        var ret = new CssdSterileItemsReturned();
                        ret.LoadByPrimaryKey(returnNo);
                        ret.IsClosed = true;
                        ret.ClosedByUserID = AppSession.UserLogin.UserID;
                        ret.ClosedDateTime = (new DateTime()).NowAtSqlServer();
                        ret.ReceivedBy = receivedBy;
                        ret.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        ret.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        ret.Save();
                    }
                }
            }
            return msg;
        }
    }
}