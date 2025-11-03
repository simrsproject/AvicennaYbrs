using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class SterileItemsReturnedInfoList : BasePage
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

            ProgramID = AppConstant.Program.CssdSterileItemsReturnedInfo;

            if (!IsPostBack)
            {
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
            var dataSource = CssdSterileItemsReturneds;
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
                    @"<CAST((CAST(c.CssdItemNo AS INT)) AS VARCHAR) AS 'ItemNo'>",

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

            DataTable dtb = query.LoadDataTable();

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
                var usr2 = new AppUserQuery("e");

                query.Select
                    (
                        query.ReturnNo,
                        query.ReturnDate,
                        query.ReturnTime,
                        tounit.ServiceUnitName.As("ToServiceUnitName"),
                        query.HandedByUserID,
                        usr.UserName.As("HandedBy"),
                        query.ReceivedBy,
                        query.IsClosed,
                        usr2.UserName.As("ClosedBy"),
                        query.LastUpdateDateTime,
                        query.IsApproved,
                        query.IsVoid
                    );

                query.InnerJoin(tounit).On(tounit.ServiceUnitID == query.ToServiceUnitID);
                query.InnerJoin(usr).On(usr.UserID == query.HandedByUserID);
                query.InnerJoin(usrunit).On(usrunit.ServiceUnitID == query.ToServiceUnitID &&
                                            usrunit.UserID == AppSession.UserLogin.UserID);
                query.LeftJoin(usr2).On(usr2.UserID == query.ClosedByUserID);
                query.Where(query.IsApproved == true);
                query.OrderBy(query.ReturnDate.Descending, query.ReturnNo.Descending);
                query.es.Top = AppSession.Parameter.MaxResultRecord;

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
    }
}
