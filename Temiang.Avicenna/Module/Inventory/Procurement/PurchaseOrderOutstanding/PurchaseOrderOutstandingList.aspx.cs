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

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class PurchaseOrderOutstandingList : BasePage
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

            ProgramID = AppConstant.Program.PurchaseOrderOutstanding;
            if (!IsPostBack)
            {
                txtFromDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtToDate.SelectedDate = (new DateTime()).NowAtSqlServer();

                RadToolBar2.Visible = this.IsUserAddAble || this.IsUserEditAble;
                grdList.Columns[0].Visible = RadToolBar2.Visible;

                ComboBox.PopulateWithServiceUnitForTransaction(cboPurchasingUnitID, BusinessObject.Reference.TransactionCode.PurchaseOrder, RadToolBar2.Visible);
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitRequestID, BusinessObject.Reference.TransactionCode.PurchaseRequest, !RadToolBar2.Visible);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void cboSupplier_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.SupplierItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.SupplierItemDataBound(e);
        }

        private DataTable ItemTransactions()
        {
            var isEmptyFilter = txtFromDate.IsEmpty && txtToDate.IsEmpty && string.IsNullOrEmpty(txtPONo.Text) && string.IsNullOrEmpty(cboSupplierID.SelectedValue) && string.IsNullOrEmpty(cboPurchasingUnitID.SelectedValue) && string.IsNullOrEmpty(cboServiceUnitRequestID.SelectedValue) && string.IsNullOrEmpty(cboSRItemType.SelectedValue);
            if (!ValidateSearch(isEmptyFilter, "Purchase Order Maintenance")) return null;

            var hd = new ItemTransactionQuery("a");
            var supp = new SupplierQuery("b");
            var usr = new AppUserServiceUnitQuery("c");
            var su = new ServiceUnitQuery("d");
            var std = new AppStandardReferenceItemQuery("e");
            var hdref = new ItemTransactionQuery("aa");
            var suref = new ServiceUnitQuery("dd");

            hd.es.Top = AppSession.Parameter.MaxResultRecord;

            hd.Select(
                hd.TransactionNo,
                hd.TransactionDate,
                supp.SupplierName,
                su.ServiceUnitName,
                std.ItemName.As("ItemType"),
                hd.ReferenceNo,
                suref.ServiceUnitName.As("ServiceUnitRequest")
                );
            hd.InnerJoin(supp).On(hd.BusinessPartnerID == supp.SupplierID);
            hd.InnerJoin(usr).On(hd.FromServiceUnitID == usr.ServiceUnitID && usr.UserID == AppSession.UserLogin.UserID);
            hd.InnerJoin(su).On(hd.FromServiceUnitID == su.ServiceUnitID);
            hd.InnerJoin(std).On(hd.SRItemType == std.ItemID && std.StandardReferenceID == "ItemType");
            hd.LeftJoin(hdref).On(hd.ReferenceNo == hdref.TransactionNo);
            hd.LeftJoin(suref).On(hdref.FromServiceUnitID == suref.ServiceUnitID);

            if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                hd.Where(hd.TransactionDate.Between(txtFromDate.SelectedDate.Value.Date, txtToDate.SelectedDate.Value.Date));
            if (!string.IsNullOrEmpty(txtPONo.Text))
                hd.Where(hd.TransactionNo == txtPONo.Text);
            if (!string.IsNullOrEmpty(cboSupplierID.SelectedValue))
                hd.Where(hd.BusinessPartnerID == cboSupplierID.SelectedValue);
            if (!string.IsNullOrEmpty(cboPurchasingUnitID.SelectedValue))
                hd.Where(hd.FromServiceUnitID == cboPurchasingUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                hd.Where(hd.SRItemType == cboSRItemType.SelectedValue);
            if (!string.IsNullOrEmpty(cboServiceUnitRequestID.SelectedValue))
                hd.Where(hdref.FromServiceUnitID == cboServiceUnitRequestID.SelectedValue);

            hd.Where(
                hd.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrder,
                hd.IsApproved == true,
                hd.IsClosed == false
                );

            hd.OrderBy(hd.TransactionDate.Ascending, hd.TransactionNo.Ascending);

            var tab = hd.LoadDataTable();

            foreach (DataRow row in tab.Rows.Cast<DataRow>().Where(row => ItemTransactionItems(row["TransactionNo"].ToString()).Rows.Count == 0))
            {
                row.Delete();
            }

            return tab;
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
            var qrref = new ItemTransactionItemQuery("aa");

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    @"<ISNULL(b.ItemName, a.[Description]) AS ItemName>",
                    @"<(ISNULL(aa.Quantity, 0) * ISNULL(aa.ConversionFactor, 0)) / a.ConversionFactor AS QtyReq>",
                    query.Quantity.As("QtyOrder"),
                    @"<(ISNULL((SELECT SUM(dt.Quantity * dt.ConversionFactor) 
                                            FROM ItemTransactionItem dt
                                            INNER JOIN ItemTransaction hd
                                                ON dt.TransactionNo = hd.TransactionNo
                                            WHERE dt.ReferenceNo = a.TransactionNo
                                                AND dt.ItemID = a.ItemID
                                                AND hd.IsApproved = 1), 0))/a.ConversionFactor AS QtyReceived>",
                    @"<((a.Quantity * a.ConversionFactor) - ISNULL((SELECT SUM(dt.Quantity * dt.ConversionFactor) 
                                            FROM ItemTransactionItem dt
                                            INNER JOIN ItemTransaction hd
                                                ON dt.TransactionNo = hd.TransactionNo
                                            WHERE dt.ReferenceNo = a.TransactionNo
                                                AND dt.ItemID = a.ItemID
                                                AND hd.IsApproved = 1), 0))/a.ConversionFactor AS Quantity>",
                    query.SRItemUnit
                );
            query.LeftJoin(item).On(query.ItemID == item.ItemID);
            query.LeftJoin(qrref).On(query.ReferenceNo == qrref.TransactionNo &&
                                     query.ReferenceSequenceNo == qrref.SequenceNo);
            query.Where(
                query.TransactionNo == transactionNo,
                query.IsClosed == false
                );

            var tab = query.LoadDataTable();

            foreach (DataRow row in tab.Rows.Cast<DataRow>().Where(row => Convert.ToDecimal(row["Quantity"]) == 0))
            {
                row.Delete();
            }

            tab.AcceptChanges();

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
                            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
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
                            dt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
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
