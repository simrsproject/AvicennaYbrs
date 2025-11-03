using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class InventoryIssueRequestMaintenanceList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.InventoryIssueRequestMaintenance;
            if (!IsPostBack)
            {
                txtFromDate.SelectedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                txtToDate.SelectedDate = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, TransactionCode.InventoryIssueRequestOut, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToUnit, TransactionCode.InventoryIssueOut, true);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
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

        private DataTable ItemTransactions()
        {
            var isEmptyFilter = txtFromDate.IsEmpty && txtToDate.IsEmpty && string.IsNullOrEmpty(txtRequestNo.Text) && string.IsNullOrEmpty(cboServiceUnitID.Text) && string.IsNullOrEmpty(cboToUnit.Text) && string.IsNullOrEmpty(cboSRItemType.Text);
            if (!ValidateSearch(isEmptyFilter, "Inventory Issue Request Maintenance")) return null;

            var hd = new ItemTransactionQuery("a");
            var su = new ServiceUnitQuery("b");
            var su2 = new ServiceUnitQuery("c");
            var std = new AppStandardReferenceItemQuery("d");
            var qusr = new AppUserServiceUnitQuery("u");
            var dt = new ItemTransactionItemQuery("e");

            hd.es.Top = AppSession.Parameter.MaxResultRecord;
            hd.es.Distinct = true;
            hd.Select(
                hd.TransactionNo,
                hd.TransactionDate,
                su.ServiceUnitName,
                su2.ServiceUnitName.As("ToUnit"),
                std.ItemName.As("ItemType"),
                hd.Notes
                );
            hd.InnerJoin(su).On(hd.FromServiceUnitID == su.ServiceUnitID);
            hd.InnerJoin(qusr).On(hd.ToServiceUnitID == qusr.ServiceUnitID &&
                                         qusr.UserID == AppSession.UserLogin.UserID);
            hd.InnerJoin(su2).On(hd.ToServiceUnitID == su2.ServiceUnitID);
            hd.InnerJoin(std).On(hd.SRItemType == std.ItemID &
                                 std.StandardReferenceID == AppEnum.StandardReference.ItemType);
            hd.InnerJoin(dt).On(hd.TransactionNo == dt.TransactionNo);

            if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                hd.Where(hd.TransactionDate.Between(txtFromDate.SelectedDate.Value.Date, txtToDate.SelectedDate.Value.Date));
            if (!string.IsNullOrEmpty(txtRequestNo.Text))
                hd.Where(hd.TransactionNo == txtRequestNo.Text);
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                hd.Where(hd.FromServiceUnitID == cboServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboToUnit.SelectedValue))
                hd.Where(hd.ToServiceUnitID == cboToUnit.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                hd.Where(hd.SRItemType == cboSRItemType.SelectedValue);

            hd.Where(
                hd.TransactionCode == TransactionCode.InventoryIssueRequestOut,
                hd.IsApproved == true,
                dt.IsClosed == false
                );

            hd.OrderBy(hd.TransactionDate.Ascending, hd.TransactionNo.Ascending);

            var tab = hd.LoadDataTable();

            return tab;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ItemTransactions();
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                grd.DataSource = dataSource;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            e.DetailTableView.DataSource = ItemTransactionItems(e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString());
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grdList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }

        private DataTable ItemTransactionItems(string transactionNo)
        {
            var query = new ItemTransactionItemQuery("a");
            var item = new ItemQuery("b");

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    item.ItemName,
                    @"<(a.QuantityFinishInBaseUnit / a.ConversionFactor) AS QuantityFinishInBaseUnit>",
                    query.Quantity,
                    query.SRItemUnit
                );
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.Where(
                query.TransactionNo == transactionNo,
                query.IsClosed == false
                );

            var tab = query.LoadDataTable();

            return tab;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "close")
            {
                foreach (GridDataItem dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked))
                {
                    using (var trans = new esTransactionScope())
                    {
                        var hd = new ItemTransaction();
                        hd.LoadByPrimaryKey(dataItem["TransactionNo"].Text);

                        var hds = new ItemTransactionCollection();
                        //hds.Query.Where(hds.Query.TransactionNo.In(hd.TransactionNo, hd.ReferenceNo));
                        hds.Query.Where(hds.Query.TransactionNo == hd.TransactionNo);
                        hds.LoadAll();

                        foreach (var entity in hds)
                        {
                            entity.IsClosed = true;
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                        }

                        hds.Save();

                        var dts = new ItemTransactionItemCollection();
                        //dts.Query.Where(dts.Query.TransactionNo.In(hd.TransactionNo, hd.ReferenceNo));
                        dts.Query.Where(dts.Query.TransactionNo == hd.TransactionNo);
                        dts.LoadAll();

                        foreach (var dt in dts)
                        {
                            dt.IsClosed = true;
                            dt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            dt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();// DateTime.Now;
                        }

                        dts.Save();

                        trans.Complete();
                    }
                }

                grdList.Rebind();
            }
        }
    }
}
