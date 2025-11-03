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
    public partial class RequestOrderOutstandingList : BasePage
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

            ProgramID = AppConstant.Program.RequestOrderOutstanding;
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, BusinessObject.Reference.TransactionCode.PurchaseOrder, true);
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, BusinessObject.Reference.TransactionCode.PurchaseRequest, false);
                ComboBox.PopulateWithItemTypeProduct(cboSRItemType);
                txtFromDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtToDate.SelectedDate = (new DateTime()).NowAtSqlServer();
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
            var isEmptyFilter = txtFromDate.IsEmpty && txtToDate.IsEmpty && string.IsNullOrEmpty(txtPRNo.Text) && string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboSRItemType.SelectedValue);
            if (!ValidateSearch(isEmptyFilter, "Purchase Request Maintenance")) return null;

            var hd = new ItemTransactionQuery("a");
            var iti = new ItemTransactionItemQuery("iti");
            var su = new ServiceUnitQuery("b");
            var sr = new AppStandardReferenceItemQuery("c");
            var usr = new AppUserServiceUnitQuery("d");
            var su2 = new ServiceUnitQuery("e");
            
            hd.Select(
                hd.TransactionNo,
                hd.TransactionDate,
                su2.ServiceUnitName.As("FromServiceUnit"),
                su.ServiceUnitName.As("ToServiceUnit"),
                sr.ItemName.As("ItemType"),
                hd.Notes
                );
            hd.InnerJoin(iti).On(hd.TransactionNo == iti.TransactionNo && iti.IsClosed == false);
            hd.InnerJoin(su).On(hd.ToServiceUnitID == su.ServiceUnitID);
            hd.InnerJoin(su2).On(hd.FromServiceUnitID == su2.ServiceUnitID);
            hd.InnerJoin(sr).On(hd.SRItemType == sr.ItemID && sr.StandardReferenceID == "ItemType");
            hd.InnerJoin(usr).On(hd.ToServiceUnitID == usr.ServiceUnitID && usr.UserID == AppSession.UserLogin.UserID);

            if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
                hd.Where(hd.TransactionDate.Between(txtFromDate.SelectedDate.Value.Date, txtToDate.SelectedDate.Value.Date));
            if (!string.IsNullOrEmpty(txtPRNo.Text))
                hd.Where(hd.TransactionNo == txtPRNo.Text);
            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
                hd.Where(hd.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                hd.Where(hd.ToServiceUnitID == cboToServiceUnitID.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                hd.Where(hd.SRItemType == cboSRItemType.SelectedValue);

            hd.Where(
                hd.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseRequest,
                hd.IsApproved == true,
                //hd.IsClosed == false /*di po kyknya gak pandang flag ini, sudah close header juga masih outstanding*/,
                hd.IsVoid == false
                );

            hd.OrderBy(hd.TransactionDate.Ascending, hd.TransactionNo.Ascending);

            hd.es.Distinct = true;

            var tab = hd.LoadDataTable();

            //foreach (DataRow row in tab.Rows.Cast<DataRow>().Where(row => ItemTransactionItems(row["TransactionNo"].ToString()).Rows.Count == 0))
            //{
            //    row.Delete();
            //}

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
                    query.Description,
                    @"<a.Quantity - ISNULL((SELECT SUM(dt.Quantity) 
                                            FROM ItemTransactionItem dt
                                            INNER JOIN ItemTransaction hd
                                                ON dt.TransactionNo = hd.TransactionNo
                                            WHERE dt.ReferenceNo = a.TransactionNo
                                                AND dt.ItemID = a.ItemID
                                                AND hd.IsApproved = 1), 0) AS Quantity>",
                    query.Quantity,
                    query.SRItemUnit
                );
            query.LeftJoin(item).On(query.ItemID == item.ItemID);
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
                            if (!(dt.IsClosed ?? false))
                            {
                                dt.IsClosed = true;
                                dt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                dt.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            }
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
